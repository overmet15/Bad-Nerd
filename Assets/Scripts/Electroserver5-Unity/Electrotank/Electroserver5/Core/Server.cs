using System;
using System.Collections;
using System.Collections.Generic;
using Electrotank.Electroserver5.Api;
using Electrotank.Electroserver5.Api.Helper;
using log4net;

namespace Electrotank.Electroserver5.Core
{
	public class Server
	{
		private ILog log = LogManager.GetLogger(typeof(Server));

		public List<Connection> ActiveConnections { get; internal set; }

		public List<AvailableConnection> AvailableConnections { get; internal set; }

		public bool Connected { get; internal set; }

		public string ServerId { get; internal set; }

		internal EsEngine Engine { get; set; }

		private int ExpectedMessageNumber { get; set; }

		private Queue ImmediateQueue { get; set; }

		private SortedList MessageQueue { get; set; }

		public Server(string serverId)
		{
			MessageQueue = new SortedList(Comparers.Ints);
			ImmediateQueue = new Queue();
			AvailableConnections = new List<AvailableConnection>();
			ActiveConnections = new List<Connection>();
			ExpectedMessageNumber = 0;
			ServerId = serverId;
		}

		public List<Connection> ActiveConnectionsForType(Protocol p)
		{
			return ActiveConnections.FindAll((Connection c) => c.Protocol == p);
		}

		public void AddAvailableConnection(AvailableConnection con)
		{
			con.ServerId = ServerId;
			AvailableConnections.Add(con);
		}

		public List<AvailableConnection> AvailableConnectionsForType(AvailableConnection.TransportType t)
		{
			return AvailableConnections.FindAll((AvailableConnection c) => c.Transport == t);
		}

		public void RemoveAvailableConnection(AvailableConnection con)
		{
			AvailableConnections.Remove(con);
		}

		internal void AddActiveConnection(Connection c)
		{
			ActiveConnections.Add(c);
		}

		internal void Close()
		{
			Connected = false;
			ActiveConnections.ForEach(delegate(Connection c)
			{
				c.Close();
			});
		}

		internal Connection ConnectionFor(Protocol p)
		{
			return ActiveConnections.Find((Connection c) => c.Protocol == p);
		}

		internal List<QueuedMessage> Dequeue()
		{
			List<QueuedMessage> list = new List<QueuedMessage>();
			lock (this)
			{
				while (ImmediateQueue.Count > 0)
				{
					QueuedMessage queuedMessage = ImmediateQueue.Dequeue() as QueuedMessage;
					EsMessage message = queuedMessage.Message;
					if (message is AggregatePluginMessageEvent)
					{
						foreach (PluginMessageEvent item in unrollAggregate(message as AggregatePluginMessageEvent))
						{
							list.Add(new QueuedMessage(item, queuedMessage.Connection));
						}
					}
					else
					{
						list.Add(queuedMessage);
					}
				}
			}
			lock (this)
			{
				if (MessageQueue.Count > 0)
				{
					QueuedMessage queuedMessage2 = MessageQueue.GetByIndex(0) as QueuedMessage;
					if (queuedMessage2.Message is LoginResponse && !(queuedMessage2.Message as LoginResponse).Successful)
					{
						ExpectedMessageNumber = 0;
					}
					while (queuedMessage2.Message.MessageNumber == ExpectedMessageNumber)
					{
						MessageQueue.RemoveAt(0);
						ExpectedMessageNumber++;
						if (ExpectedMessageNumber == 10000)
						{
							ExpectedMessageNumber = 0;
						}
						if (queuedMessage2.Message is AggregatePluginMessageEvent)
						{
							foreach (PluginMessageEvent item2 in unrollAggregate(queuedMessage2.Message as AggregatePluginMessageEvent))
							{
								list.Add(new QueuedMessage(item2, queuedMessage2.Connection));
							}
						}
						else
						{
							list.Add(queuedMessage2);
						}
						if (MessageQueue.Count > 0)
						{
							queuedMessage2 = MessageQueue.GetByIndex(0) as QueuedMessage;
						}
					}
				}
			}
			return list;
		}

		internal void Enqueue(EsMessage m, bool immediate, Connection conn)
		{
			QueuedMessage queuedMessage = new QueuedMessage(m, conn);
			if (!immediate)
			{
				lock (this)
				{
					MessageQueue.Add(m.MessageNumber, queuedMessage);
				}
			}
			else
			{
				lock (this)
				{
					ImmediateQueue.Enqueue(queuedMessage);
				}
			}
			if (Engine.Queueing == EsEngine.QueueDispatchType.Internal)
			{
				Engine.Dispatch();
			}
		}

		internal List<AvailableConnection> ForTransportTypes(params AvailableConnection.TransportType[] types)
		{
			List<AvailableConnection> list = new List<AvailableConnection>();
			foreach (AvailableConnection.TransportType t in types)
			{
				list.AddRange(AvailableConnectionsForType(t));
			}
			return list;
		}

		internal void UpdateConnectionStatus()
		{
			bool connected = false;
			for (int num = ActiveConnections.Count - 1; num >= 0; num--)
			{
				Connection connection = ActiveConnections[num];
				if (connection.Connected && connection.PrimaryCapable)
				{
					connected = true;
					break;
				}
				if (!connection.Connected)
				{
					ActiveConnections.Remove(connection);
				}
			}
			Connected = connected;
			Engine.UpdateConnectionStatus();
		}

		internal void validateConnections()
		{
			if (AvailableConnectionsForType(AvailableConnection.TransportType.BinaryTCP).Count == 0 && AvailableConnectionsForType(AvailableConnection.TransportType.BinaryHTTP).Count == 0)
			{
				throw new EsConnectException("No connections with primary transport types defined");
			}
		}

		private void LogQueueStatus()
		{
			Console.WriteLine("MessageQueue: {0} ImmediateQueue: {1}", MessageQueue.Count, ImmediateQueue.Count);
		}

		private List<PluginMessageEvent> unrollAggregate(AggregatePluginMessageEvent e)
		{
			List<PluginMessageEvent> list = new List<PluginMessageEvent>();
			foreach (EsObject esObject in e.EsObjects)
			{
				PluginMessageEvent pluginMessageEvent = new PluginMessageEvent();
				pluginMessageEvent.RoomLevelPlugin = e.OriginRoomId >= 0;
				pluginMessageEvent.PluginName = e.PluginName;
				pluginMessageEvent.OriginRoomId = e.OriginRoomId;
				pluginMessageEvent.OriginZoneId = e.OriginZoneId;
				pluginMessageEvent.Parameters = esObject;
				pluginMessageEvent.MessageNumber = e.MessageNumber;
				list.Add(pluginMessageEvent);
			}
			return list;
		}
	}
}
