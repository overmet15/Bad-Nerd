using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Timers;
using Electrotank.Electroserver5.Api;

namespace Electrotank.Electroserver5.Core
{
	internal class EsEngineDefault : EsEngine
	{
		private readonly EventHandlerList eventHandlers = new EventHandlerList();

		private readonly EventHandlerList internalHandlers = new EventHandlerList();

		private bool closingAllConnection;

		private int connectionIdSequence = -1;

		private Connection defaultConnection;

		private Server defaultServer;

		private System.Timers.Timer pingRateTimer;

		private ConnectionAttemptResponse tempConnEvent;

		private SyncUdpSocketConnection tempUdpConn;

		private System.Timers.Timer timeoutTimer;

		private int udpConnectionPhase;

		public EsEngineDefault()
		{
			base.Queueing = QueueDispatchType.Internal;
			base.ActiveConnections = new List<Connection>();
			base.UdpPingDelay = 200;
			base.UdpConnectionTimeout = 5000;
			base.MessageSizeCompressionThreshold = -1;
			base.RegisterUDPConnectionResponse += OnRegisterUDPConnection;
			base.IgnoreUnknownMessages = false;
		}

		public override void Close()
		{
			closingAllConnection = true;
			for (int num = base.ActiveConnections.Count - 1; num >= 0; num--)
			{
				Connection connection = base.ActiveConnections[num];
				connection.Close();
			}
			closingAllConnection = false;
		}

		public override void Connect()
		{
			if (base.Connected)
			{
				return;
			}
			PreConnect();
			if (base.Servers.Count == 0)
			{
				throw new EsConnectException("No servers are defined");
			}
			for (int i = 0; i < base.Servers.Count; i++)
			{
				Server server = base.Servers[i];
				for (int j = 0; j < server.AvailableConnections.Count; j++)
				{
					base.ConnectionsToAttempt.Add(server.AvailableConnections[j]);
				}
			}
			PruneConnectionsToAttempt();
			AttemptConnection();
		}

		public override void Connect(Server server)
		{
			PreConnect();
			for (int i = 0; i < server.AvailableConnections.Count; i++)
			{
				base.ConnectionsToAttempt.Add(server.AvailableConnections[i]);
			}
			PruneConnectionsToAttempt();
			AttemptConnection();
		}

		public override void Connect(Server server, AvailableConnection availableConnection)
		{
			PreConnect();
			base.ConnectionsToAttempt.Add(availableConnection);
			AttemptConnection();
		}

		public override void Dispatch()
		{
			if (base.Connected)
			{
				try
				{
					_Dispatch();
				}
				catch (Exception ex)
				{
					log.Error("EsEngine.Dispatch() Error: " + ex);
					throw ex;
				}
			}
		}

		public override void Send(EsMessage request)
		{
			Send(request, defaultConnection);
		}

		public override void Send(EsMessage request, Connection c)
		{
			if (c != null)
			{
				if (request is LoginRequest)
				{
					LoginRequest loginRequest = (LoginRequest)request;
					loginRequest.ClientType = ClientType.Type;
					loginRequest.ClientVersion = Utility.Version;
				}
				c.Send(request);
				return;
			}
			throw new ArgumentException("Connection for was null or not active");
		}

		internal override void AddEventHandler(MessageType type, Delegate handler)
		{
			eventHandlers.AddHandler(type.ToString(), handler);
		}

		internal override void AddEventListener(MessageType messageType, EsDelegate<EsMessage> callback)
		{
			internalHandlers.AddHandler(messageType.ToString(), callback);
		}

		internal override int NextConnectionId()
		{
			return Interlocked.Increment(ref connectionIdSequence);
		}

		internal override void Raise(EsMessage message)
		{
			Delegate @delegate = internalHandlers[message.MessageType.ToString()];
			if ((object)@delegate != null)
			{
				@delegate.DynamicInvoke(message);
			}
			Delegate delegate2 = eventHandlers[message.MessageType.ToString()];
			if ((object)delegate2 != null)
			{
				delegate2.DynamicInvoke(message);
			}
		}

		internal override void RemoveEventHandler(MessageType type, Delegate handler)
		{
			eventHandlers.RemoveHandler(type.ToString(), handler);
		}

		internal override void RemoveEventListener(MessageType messageType, EsDelegate<EsMessage> callback)
		{
			internalHandlers.RemoveHandler(messageType.ToString(), callback);
		}

		internal override void UpdateConnectionStatus()
		{
			bool flag = false;
			for (int num = base.ActiveConnections.Count - 1; num >= 0; num--)
			{
				Connection connection = base.ActiveConnections[num];
				if (connection.Connected && connection.PrimaryCapable)
				{
					flag = true;
				}
				if (!connection.Connected)
				{
					base.ActiveConnections.Remove(connection);
					if (connection == defaultConnection)
					{
						defaultConnection = null;
					}
				}
			}
			if (flag && defaultConnection == null)
			{
				foreach (Connection activeConnection in base.ActiveConnections)
				{
					if (activeConnection.PrimaryCapable)
					{
						defaultConnection = activeConnection;
						break;
					}
				}
			}
			if (!flag)
			{
				base.ActiveConnections = new List<Connection>();
			}
			base.Connected = flag;
		}

		private void AttemptConnection()
		{
			bool flag = false;
			bool connected = base.Connected;
			using (List<AvailableConnection>.Enumerator enumerator = base.ConnectionsToAttempt.GetEnumerator())
			{
				for (; enumerator.MoveNext(); base.ConnectionAttemptIndex++)
				{
					AvailableConnection current = enumerator.Current;
					AvailableConnection availableConnection = base.ConnectionsToAttempt[base.ConnectionAttemptIndex];
					Server server = ServerById(availableConnection.ServerId);
					if (defaultServer == null)
					{
						defaultServer = server;
					}
					Connection newConnection = GetNewConnection(availableConnection, server);
					ConnectionsById[newConnection.ConnectionId] = newConnection;
					try
					{
						log.Debug("[Attempting " + newConnection.AvailableConnection.Transport.ToString() + " connection. Host: " + newConnection.AvailableConnection.Host + ", Port: " + newConnection.AvailableConnection.Port + "]");
						if (newConnection is SyncUdpSocketConnection)
						{
							udpConnectionPhase = 1;
							tempUdpConn = newConnection as SyncUdpSocketConnection;
						}
						newConnection.Connect();
						server.AddActiveConnection(newConnection);
						server.UpdateConnectionStatus();
						base.ActiveConnections.Add(newConnection);
						ServersByConnection[newConnection] = server;
						if (defaultConnection == null)
						{
							defaultConnection = newConnection;
						}
						flag = true;
						ConnectionAttemptResponse connectionAttemptResponse = new ConnectionAttemptResponse();
						connectionAttemptResponse.Successful = true;
						connectionAttemptResponse.ServerId = server.ServerId;
						connectionAttemptResponse.ConnectionId = newConnection.ConnectionId;
						if (newConnection is SyncUdpSocketConnection)
						{
							tempConnEvent = connectionAttemptResponse;
							registerUDPPortWithServer();
						}
						else
						{
							server.Enqueue(connectionAttemptResponse, true, null);
						}
					}
					catch (Exception)
					{
						ConnectionAttemptResponse connectionAttemptResponse2 = new ConnectionAttemptResponse();
						connectionAttemptResponse2.Successful = false;
						connectionAttemptResponse2.ServerId = server.ServerId;
						connectionAttemptResponse2.ConnectionId = newConnection.ConnectionId;
						connectionAttemptResponse2.Error = ErrorType.ConnectionFailed;
						if (newConnection is SyncUdpSocketConnection)
						{
							cleanUdpPhases();
						}
						handleConnectionAttemptFailed(connectionAttemptResponse2);
						continue;
					}
					break;
				}
			}
			base.Connected = flag;
			if (!flag && !connected)
			{
				ConnectionResponse connectionResponse = new ConnectionResponse();
				connectionResponse.Successful = false;
				connectionResponse.Error = ErrorType.ConnectionFailed;
				defaultServer.Enqueue(connectionResponse, true, null);
			}
		}

		private void cleanUdpPhases()
		{
			udpConnectionPhase = 0;
			tempConnEvent = null;
			tempUdpConn = null;
			if (timeoutTimer != null)
			{
				timeoutTimer.Stop();
				timeoutTimer.Dispose();
				timeoutTimer = null;
			}
			if (pingRateTimer != null)
			{
				pingRateTimer.Stop();
				pingRateTimer.Dispose();
				pingRateTimer = null;
			}
		}

		private Connection GetNewConnection(AvailableConnection available, Server server)
		{
			if (available.Transport == AvailableConnection.TransportType.BinaryTCP)
			{
				Connection connection = new SyncSocketConnection(available, this, server);
				connection.ConnectionId = NextConnectionId();
				return connection;
			}
			if (available.Transport == AvailableConnection.TransportType.BinaryHTTP)
			{
				Connection connection = new HttpConnection(available, this, server);
				connection.ConnectionId = NextConnectionId();
				return connection;
			}
			if (available.Transport == AvailableConnection.TransportType.BinaryUDP)
			{
				Connection connection = new SyncUdpSocketConnection(available, this, server);
				connection.ConnectionId = NextConnectionId();
				return connection;
			}
			throw new NotImplementedException(string.Concat("Transport ", available.Transport, " not currently implemented. "));
		}

		private void handleConnectionAttemptFailed(ConnectionAttemptResponse car)
		{
			defaultServer.Enqueue(car, true, null);
		}

		private void OnPingDelay(object source, ElapsedEventArgs e)
		{
			SendUdpPing();
		}

		private void OnRegisterUDPConnection(RegisterUDPConnectionResponse e)
		{
			if (udpConnectionPhase == 2)
			{
				if (e.Successful)
				{
					udpConnectionPhase = 3;
					tempUdpConn.SessionKey = e.SessionKey;
					timeoutTimer = new System.Timers.Timer(base.UdpConnectionTimeout);
					timeoutTimer.Enabled = true;
					timeoutTimer.Elapsed += OnUdpTimeout;
					pingRateTimer = new System.Timers.Timer(base.UdpPingDelay);
					pingRateTimer.Enabled = true;
					pingRateTimer.Elapsed += OnPingDelay;
					SendUdpPing();
				}
				else
				{
					udpConnectionFailed();
				}
			}
		}

		private void OnUdpTimeout(object source, ElapsedEventArgs e)
		{
			udpConnectionFailed();
		}

		private void PreConnect()
		{
			base.ConnectionsToAttempt = new List<AvailableConnection>();
			base.ConnectionAttemptIndex = 0;
		}

		private void PreProcess(EsMessage message, Connection con)
		{
			if (con is SyncUdpSocketConnection)
			{
				if (message is PingResponse && udpConnectionPhase == 3)
				{
					udpHandshakeComplete();
					Dispatch();
				}
				else if (message is ConnectionClosedEvent && !closingAllConnection)
				{
					UnRegisterUDPPortWithServer(con as SyncUdpSocketConnection);
				}
			}
			if (message is ConnectionResponse)
			{
				ConnectionResponse connectionResponse = (ConnectionResponse)message;
				if (connectionResponse.ProtocolConfiguration != null)
				{
					base.MessageSizeCompressionThreshold = connectionResponse.ProtocolConfiguration.MessageCompressionThreshold;
				}
				con.ServerVersion = connectionResponse.ServerVersion;
			}
			if (message is LoginResponse && !((LoginResponse)message).Successful)
			{
				con.DecrementOutboundId();
			}
		}

		private void PruneConnectionsToAttempt()
		{
			for (int num = base.ConnectionsToAttempt.Count - 1; num >= 0; num--)
			{
				AvailableConnection availableConnection = base.ConnectionsToAttempt[num];
				if (availableConnection.Transport != 0 && availableConnection.Transport != AvailableConnection.TransportType.BinaryHTTP)
				{
					base.ConnectionsToAttempt.Remove(availableConnection);
				}
			}
		}

		private void registerUDPPortWithServer()
		{
			udpConnectionPhase = 2;
			RegisterUDPConnectionRequest registerUDPConnectionRequest = new RegisterUDPConnectionRequest();
			registerUDPConnectionRequest.Port = tempUdpConn.AvailableConnection.LocalPort;
			Send(registerUDPConnectionRequest);
		}

		private void SendUdpPing()
		{
			PingRequest pingRequest = new PingRequest();
			pingRequest.PingRequestId = 1;
			Send(pingRequest, tempUdpConn);
		}

		private void udpConnectionFailed()
		{
			tempConnEvent.Successful = false;
			tempConnEvent.Error = ErrorType.ConnectionFailed;
			handleConnectionAttemptFailed(tempConnEvent);
			cleanUdpPhases();
		}

		private void udpHandshakeComplete()
		{
			defaultServer.Enqueue(tempConnEvent, true, null);
			cleanUdpPhases();
		}

		private void UnRegisterUDPPortWithServer(SyncUdpSocketConnection con)
		{
			RemoveUDPConnectionRequest removeUDPConnectionRequest = new RemoveUDPConnectionRequest();
			removeUDPConnectionRequest.Port = con.AvailableConnection.LocalPort;
			Send(removeUDPConnectionRequest);
		}

		private void _Dispatch()
		{
			List<Server> list = new List<Server>();
			for (int num = base.ActiveConnections.Count - 1; num >= 0; num--)
			{
				Connection con = base.ActiveConnections[num];
				Server item = ServerByConnection(con);
				if (!list.Contains(item))
				{
					list.Add(item);
				}
			}
			if (list.Count == 0 && defaultServer != null)
			{
				list.Add(defaultServer);
			}
			for (int num2 = list.Count - 1; num2 >= 0; num2--)
			{
				Server server = list[num2];
				foreach (QueuedMessage item2 in server.Dequeue())
				{
					PreProcess(item2.Message, item2.Connection);
					if (item2.Message.MessageType == MessageType.Unknown)
					{
						if (!base.IgnoreUnknownMessages)
						{
							log.Error("Unknown message received");
							throw new Exception("Unknown message received: " + item2.Message);
						}
					}
					else
					{
						Raise(item2.Message);
					}
				}
			}
		}
	}
}
