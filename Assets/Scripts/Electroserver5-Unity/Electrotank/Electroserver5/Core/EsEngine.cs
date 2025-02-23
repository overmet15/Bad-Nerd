using System;
using System.Collections.Generic;
using Electrotank.Electroserver5.Api;
using Electrotank.Electroserver5.Api.Helper;
using Electrotank.Electroserver5.Core.Util;
using log4net;

namespace Electrotank.Electroserver5.Core
{
	public abstract class EsEngine
	{
		public enum QueueDispatchType
		{
			Internal = 0,
			External = 1
		}

		public delegate void EsDelegate<M>(M message);

		internal Dictionary<int, Connection> ConnectionsById = new Dictionary<int, Connection>(Comparers.Ints);

		internal Dictionary<Connection, Server> ServersByConnection = new Dictionary<Connection, Server>();

		protected ILog log = LogManager.GetLogger(typeof(EsEngine));

		private Dictionary<string, Server> servers = new Dictionary<string, Server>();

		public List<Connection> ActiveConnections { get; internal set; }

		public bool Connected { get; internal set; }

		public int ConnectionAttemptIndex { get; internal set; }

		internal int MessageSizeCompressionThreshold { get; set; }

		public List<AvailableConnection> ConnectionsToAttempt { get; internal set; }

		public QueueDispatchType Queueing { get; set; }

		public List<Server> Servers
		{
			get
			{
				return new List<Server>(servers.Values);
			}
		}

		public int UdpConnectionTimeout { get; set; }

		public int UdpPingDelay { get; set; }

		public bool IgnoreUnknownMessages { get; set; }

		public event EsDelegate<AddBuddiesResponse> AddBuddiesResponse
		{
			add
			{
				AddEventHandler(MessageType.AddBuddiesResponse, value);
			}
			remove
			{
				RemoveEventHandler(MessageType.AddBuddiesResponse, value);
			}
		}

		public event EsDelegate<AggregatePluginMessageEvent> AggregatePluginMessageEvent
		{
			add
			{
				AddEventHandler(MessageType.AggregatePluginMessageEvent, value);
			}
			remove
			{
				RemoveEventHandler(MessageType.AggregatePluginMessageEvent, value);
			}
		}

		public event EsDelegate<GatewayKickUserRequest> GatewayKickUserRequest
		{
			add
			{
				AddEventHandler(MessageType.GatewayKickUserRequest, value);
			}
			remove
			{
				RemoveEventHandler(MessageType.GatewayKickUserRequest, value);
			}
		}

		public event EsDelegate<BuddyStatusUpdateEvent> BuddyStatusUpdatedEvent
		{
			add
			{
				AddEventHandler(MessageType.BuddyStatusUpdatedEvent, value);
			}
			remove
			{
				RemoveEventHandler(MessageType.BuddyStatusUpdatedEvent, value);
			}
		}

		public event EsDelegate<ConnectionAttemptResponse> ConnectionAttemptResponse
		{
			add
			{
				AddEventHandler(MessageType.ConnectionAttemptResponse, value);
			}
			remove
			{
				RemoveEventHandler(MessageType.ConnectionAttemptResponse, value);
			}
		}

		public event EsDelegate<ConnectionClosedEvent> ConnectionClosedEvent
		{
			add
			{
				AddEventHandler(MessageType.ConnectionClosedEvent, value);
			}
			remove
			{
				RemoveEventHandler(MessageType.ConnectionClosedEvent, value);
			}
		}

		public event EsDelegate<ConnectionResponse> ConnectionResponse
		{
			add
			{
				AddEventHandler(MessageType.ConnectionResponse, value);
			}
			remove
			{
				RemoveEventHandler(MessageType.ConnectionResponse, value);
			}
		}

		public event EsDelegate<CreateOrJoinGameResponse> CreateOrJoinGameResponse
		{
			add
			{
				AddEventHandler(MessageType.CreateOrJoinGameResponse, value);
			}
			remove
			{
				RemoveEventHandler(MessageType.CreateOrJoinGameResponse, value);
			}
		}

		public event EsDelegate<CrossDomainPolicyResponse> CrossDomainPolicyResponse
		{
			add
			{
				AddEventHandler(MessageType.CrossDomainResponse, value);
			}
			remove
			{
				RemoveEventHandler(MessageType.CrossDomainResponse, value);
			}
		}

		public event EsDelegate<DHPublicNumbersResponse> DHPublicNumbersResponse
		{
			add
			{
				AddEventHandler(MessageType.DHPublicNumbers, value);
			}
			remove
			{
				RemoveEventHandler(MessageType.DHPublicNumbers, value);
			}
		}

		public event EsDelegate<EncryptionStateChangeEvent> EncryptionStateChangedEvent
		{
			add
			{
				AddEventHandler(MessageType.EncryptionStateChange, value);
			}
			remove
			{
				RemoveEventHandler(MessageType.EncryptionStateChange, value);
			}
		}

		public event EsDelegate<FindGamesResponse> FindGamesResponse
		{
			add
			{
				AddEventHandler(MessageType.FindGamesResponse, value);
			}
			remove
			{
				RemoveEventHandler(MessageType.FindGamesResponse, value);
			}
		}

		public event EsDelegate<FindZoneAndRoomByNameResponse> FindZoneAndRoomByNameResponse
		{
			add
			{
				AddEventHandler(MessageType.FindZoneAndRoomByNameResponse, value);
			}
			remove
			{
				RemoveEventHandler(MessageType.FindZoneAndRoomByNameResponse, value);
			}
		}

		public event EsDelegate<GenericErrorResponse> GenericErrorResponse
		{
			add
			{
				AddEventHandler(MessageType.GenericErrorResponse, value);
			}
			remove
			{
				RemoveEventHandler(MessageType.GenericErrorResponse, value);
			}
		}

		public event EsDelegate<GetRoomsInZoneResponse> GetRoomsInZoneResponse
		{
			add
			{
				AddEventHandler(MessageType.GetRoomsInZoneResponse, value);
			}
			remove
			{
				RemoveEventHandler(MessageType.GetRoomsInZoneResponse, value);
			}
		}

		public event EsDelegate<GetServerLocalTimeResponse> GetServerLocalTimeResponse
		{
			add
			{
				AddEventHandler(MessageType.GetServerLocalTimeResponse, value);
			}
			remove
			{
				RemoveEventHandler(MessageType.GetServerLocalTimeResponse, value);
			}
		}

		public event EsDelegate<GetUserCountResponse> GetUserCountResponse
		{
			add
			{
				AddEventHandler(MessageType.GetUserCountResponse, value);
			}
			remove
			{
				RemoveEventHandler(MessageType.GetUserCountResponse, value);
			}
		}

		public event EsDelegate<GetUsersInRoomResponse> GetUsersInRoomResponse
		{
			add
			{
				AddEventHandler(MessageType.GetUsersInRoomResponse, value);
			}
			remove
			{
				RemoveEventHandler(MessageType.GetUsersInRoomResponse, value);
			}
		}

		public event EsDelegate<GetUserVariablesResponse> GetUserVariablesResponse
		{
			add
			{
				AddEventHandler(MessageType.GetUserVariablesResponse, value);
			}
			remove
			{
				RemoveEventHandler(MessageType.GetUserVariablesResponse, value);
			}
		}

		public event EsDelegate<GetZonesResponse> GetZonesResponse
		{
			add
			{
				AddEventHandler(MessageType.GetZonesResponse, value);
			}
			remove
			{
				RemoveEventHandler(MessageType.GetZonesResponse, value);
			}
		}

		public event EsDelegate<JoinRoomEvent> JoinRoomEvent
		{
			add
			{
				AddEventHandler(MessageType.JoinRoomEvent, value);
			}
			remove
			{
				RemoveEventHandler(MessageType.JoinRoomEvent, value);
			}
		}

		public event EsDelegate<JoinZoneEvent> JoinZoneEvent
		{
			add
			{
				AddEventHandler(MessageType.JoinZoneEvent, value);
			}
			remove
			{
				RemoveEventHandler(MessageType.JoinZoneEvent, value);
			}
		}

		public event EsDelegate<LeaveRoomEvent> LeaveRoomEvent
		{
			add
			{
				AddEventHandler(MessageType.LeaveRoomEvent, value);
			}
			remove
			{
				RemoveEventHandler(MessageType.LeaveRoomEvent, value);
			}
		}

		public event EsDelegate<LeaveZoneEvent> LeaveZoneEvent
		{
			add
			{
				AddEventHandler(MessageType.LeaveZoneEvent, value);
			}
			remove
			{
				RemoveEventHandler(MessageType.LeaveZoneEvent, value);
			}
		}

		public event EsDelegate<LoginResponse> LoginResponse
		{
			add
			{
				AddEventHandler(MessageType.LoginResponse, value);
			}
			remove
			{
				RemoveEventHandler(MessageType.LoginResponse, value);
			}
		}

		public event EsDelegate<PingResponse> PingResponse
		{
			add
			{
				AddEventHandler(MessageType.PingResponse, value);
			}
			remove
			{
				RemoveEventHandler(MessageType.PingResponse, value);
			}
		}

		public event EsDelegate<PluginMessageEvent> PluginMessageEvent
		{
			add
			{
				AddEventHandler(MessageType.PluginMessageEvent, value);
			}
			remove
			{
				RemoveEventHandler(MessageType.PluginMessageEvent, value);
			}
		}

		public event EsDelegate<PrivateMessageEvent> PrivateMessageEvent
		{
			add
			{
				AddEventHandler(MessageType.PrivateMessageEvent, value);
			}
			remove
			{
				RemoveEventHandler(MessageType.PrivateMessageEvent, value);
			}
		}

		public event EsDelegate<PublicMessageEvent> PublicMessageEvent
		{
			add
			{
				AddEventHandler(MessageType.PublicMessageEvent, value);
			}
			remove
			{
				RemoveEventHandler(MessageType.PublicMessageEvent, value);
			}
		}

		public event EsDelegate<RegisterUDPConnectionResponse> RegisterUDPConnectionResponse
		{
			add
			{
				AddEventHandler(MessageType.RegisterUDPConnectionResponse, value);
			}
			remove
			{
				RemoveEventHandler(MessageType.RegisterUDPConnectionResponse, value);
			}
		}

		public event EsDelegate<RemoveBuddiesResponse> RemoveBuddiesResponse
		{
			add
			{
				AddEventHandler(MessageType.RemoveBuddiesResponse, value);
			}
			remove
			{
				RemoveEventHandler(MessageType.RemoveBuddiesResponse, value);
			}
		}

		public event EsDelegate<RemoveUDPConnectionResponse> RemoveUDPConnectionResponse
		{
			add
			{
				AddEventHandler(MessageType.RemoveUDPConnectionResponse, value);
			}
			remove
			{
				RemoveEventHandler(MessageType.RemoveUDPConnectionResponse, value);
			}
		}

		public event EsDelegate<RoomVariableUpdateEvent> RoomVariableUpdateEvent
		{
			add
			{
				AddEventHandler(MessageType.RoomVariableUpdateEvent, value);
			}
			remove
			{
				RemoveEventHandler(MessageType.RoomVariableUpdateEvent, value);
			}
		}

		public event EsDelegate<SessionIdleEvent> SessionIdleEvent
		{
			add
			{
				AddEventHandler(MessageType.SessionIdleEvent, value);
			}
			remove
			{
				RemoveEventHandler(MessageType.SessionIdleEvent, value);
			}
		}

		public event EsDelegate<UpdateRoomDetailsEvent> UpdateRoomDetailsEvent
		{
			add
			{
				AddEventHandler(MessageType.UpdateRoomDetailsEvent, value);
			}
			remove
			{
				RemoveEventHandler(MessageType.UpdateRoomDetailsEvent, value);
			}
		}

		public event EsDelegate<UserEvictedFromRoomEvent> UserEvictedFromRoomEvent
		{
			add
			{
				AddEventHandler(MessageType.UserEvictedFromRoomEvent, value);
			}
			remove
			{
				RemoveEventHandler(MessageType.UserEvictedFromRoomEvent, value);
			}
		}

		public event EsDelegate<UserUpdateEvent> UserUpdateEvent
		{
			add
			{
				AddEventHandler(MessageType.UserUpdateEvent, value);
			}
			remove
			{
				RemoveEventHandler(MessageType.UserUpdateEvent, value);
			}
		}

		public event EsDelegate<UserVariableUpdateEvent> UserVariableUpdateEvent
		{
			add
			{
				AddEventHandler(MessageType.UserVariableUpdateEvent, value);
			}
			remove
			{
				RemoveEventHandler(MessageType.UserVariableUpdateEvent, value);
			}
		}

		public event EsDelegate<ValidateAdditionalLoginResponse> ValidateAdditionalLoginResponse
		{
			add
			{
				AddEventHandler(MessageType.ValidateAdditionalLoginResponse, value);
			}
			remove
			{
				RemoveEventHandler(MessageType.ValidateAdditionalLoginResponse, value);
			}
		}

		public event EsDelegate<ZoneUpdateEvent> ZoneUpdateEvent
		{
			add
			{
				AddEventHandler(MessageType.ZoneUpdateEvent, value);
			}
			remove
			{
				RemoveEventHandler(MessageType.ZoneUpdateEvent, value);
			}
		}

		public void AddServer(Server server)
		{
			server.Engine = this;
			servers[server.ServerId] = server;
		}

		public abstract void Close();

		public abstract void Connect();

		public abstract void Connect(Server server);

		public abstract void Connect(Server server, AvailableConnection availableConnection);

		public abstract void Dispatch();

		public bool EncryptionEnabled()
		{
			bool result = true;
			foreach (Server value in servers.Values)
			{
				foreach (Connection activeConnection in value.ActiveConnections)
				{
					if (activeConnection.EncryptionContext == null || !activeConnection.EncryptionContext.EncryptionEnabled())
					{
						result = false;
					}
				}
			}
			return result;
		}

		public bool EncryptionEnabled(Connection con)
		{
			return con.EncryptionContext.EncryptionEnabled();
		}

		public void RemoveServer(Server server)
		{
			if (servers.ContainsKey(server.ServerId))
			{
				servers.Remove(server.ServerId);
			}
		}

		public abstract void Send(EsMessage request);

		public abstract void Send(EsMessage request, Connection c);

		public Server ServerByConnection(Connection con)
		{
			return ServersByConnection[con];
		}

		public Server ServerById(string serverId)
		{
			Server value;
			servers.TryGetValue(serverId, out value);
			return value;
		}

		public void SetEncryptionEnabled(bool encryptionEnabled)
		{
			foreach (Server value in servers.Values)
			{
				foreach (Connection activeConnection in value.ActiveConnections)
				{
					SetEncryptionEnabled(encryptionEnabled, activeConnection);
				}
			}
		}

		public void SetEncryptionEnabled(bool encryptionEnabled, Connection con)
		{
			if (encryptionEnabled)
			{
				if (con.EncryptionContext == null)
				{
					con.EncryptionContext = new DhAesEncryptionContext();
				}
				con.EncryptionContext.SetEnabled(true, this, con);
			}
			else if (con.EncryptionContext != null)
			{
				con.EncryptionContext.SetEnabled(false, this, con);
			}
		}

		internal abstract void AddEventHandler(MessageType type, Delegate handler);

		internal abstract void AddEventListener(MessageType messageType, EsDelegate<EsMessage> callback);

		internal abstract int NextConnectionId();

		internal abstract void Raise(EsMessage m);

		internal abstract void RemoveEventHandler(MessageType type, Delegate handler);

		internal abstract void RemoveEventListener(MessageType messageType, EsDelegate<EsMessage> callback);

		internal abstract void UpdateConnectionStatus();
	}
}
