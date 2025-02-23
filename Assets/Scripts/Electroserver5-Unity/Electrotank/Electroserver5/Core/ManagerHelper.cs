using System.Collections.Generic;
using Electrotank.Electroserver5.Api;
using log4net;

namespace Electrotank.Electroserver5.Core
{
	public class ManagerHelper
	{
		protected ILog log = LogManager.GetLogger(typeof(EsEngine));

		private readonly EsEngine engine;

		public UserManager UserManager { get; protected set; }

		public ZoneManager ZoneManager { get; protected set; }

		internal ManagerHelper(EsEngine engine)
		{
			this.engine = engine;
			UserManager = new UserManager();
			ZoneManager = new ZoneManager();
			engine.AddEventListener(MessageType.LoginResponse, OnLogin);
			engine.AddEventListener(MessageType.JoinRoomEvent, OnJoinRoom);
			engine.AddEventListener(MessageType.LeaveRoomEvent, OnLeaveRoom);
			engine.AddEventListener(MessageType.UserUpdateEvent, OnRoomUserUpdate);
			engine.AddEventListener(MessageType.UpdateRoomDetailsEvent, OnUpdateRoomDetails);
			engine.AddEventListener(MessageType.JoinZoneEvent, OnJoinZone);
			engine.AddEventListener(MessageType.LeaveZoneEvent, OnLeaveZone);
			engine.AddEventListener(MessageType.ZoneUpdateEvent, OnZoneUpdate);
			engine.AddEventListener(MessageType.RoomVariableUpdateEvent, OnRoomVariableUpdate);
			engine.AddEventListener(MessageType.UserVariableUpdateEvent, OnUserVariableUpdate);
			engine.AddEventListener(MessageType.BuddyStatusUpdatedEvent, OnBuddyStatusUpdate);
			engine.AddEventListener(MessageType.AddBuddiesResponse, OnAddBuddies);
			engine.AddEventListener(MessageType.RemoveBuddiesResponse, OnRemoveBuddies);
		}

		private void OnAddBuddies(EsMessage e)
		{
			AddBuddiesResponse addBuddiesResponse = e as AddBuddiesResponse;
			foreach (string item in addBuddiesResponse.BuddiesAdded)
			{
				User user = UserManager.UserByName(item);
				if (user == null)
				{
					user = new User();
					user.UserName = item;
				}
				if (!user.IsBuddy)
				{
					UserManager.AddBuddy(user);
				}
			}
		}

		private void OnBuddyStatusUpdate(EsMessage m)
		{
			BuddyStatusUpdateEvent buddyStatusUpdateEvent = m as BuddyStatusUpdateEvent;
			User user = UserManager.UserByName(buddyStatusUpdateEvent.UserName);
			if (user == null)
			{
				user = new User();
				user.UserName = buddyStatusUpdateEvent.UserName;
			}
			if (!user.IsBuddy)
			{
				user = UserManager.AddBuddy(user);
			}
			BuddyStatusUpdateAction? action = buddyStatusUpdateEvent.Action;
			if (!action.HasValue)
			{
				return;
			}
			switch (action.Value)
			{
			case BuddyStatusUpdateAction.LoggedIn:
				user.IsLoggedIn = true;
				user.BuddyVariable = buddyStatusUpdateEvent.EsObject;
				break;
			case BuddyStatusUpdateAction.LoggedOut:
				user.IsLoggedIn = false;
				if (buddyStatusUpdateEvent.EsObject != null)
				{
					user.BuddyVariable = buddyStatusUpdateEvent.EsObject;
				}
				break;
			}
		}

		private void OnJoinRoom(EsMessage m)
		{
			JoinRoomEvent joinRoomEvent = m as JoinRoomEvent;
			Zone zone = ZoneManager.ZoneById(joinRoomEvent.ZoneId);
			Room room = zone.RoomById(joinRoomEvent.RoomId);
			if (room == null)
			{
				room = new Room();
				room.Id = joinRoomEvent.RoomId;
				room.Name = joinRoomEvent.RoomName;
				zone.AddRoom(room);
			}
			room.Description = joinRoomEvent.RoomDescription;
			room.HasPassword = joinRoomEvent.HasPassword;
			room.Capacity = joinRoomEvent.Capacity;
			room.IsHidden = joinRoomEvent.Hidden;
			foreach (RoomVariable roomVariable in joinRoomEvent.RoomVariables)
			{
				room.AddRoomVariable(roomVariable);
			}
			foreach (UserListEntry user2 in joinRoomEvent.Users)
			{
				User user = new User();
				user.UserName = user2.UserName;
				user = UserManager.AddUser(user);
				user = MergeUserListEntryToUser(user2, user);
				room.AddUser(user);
			}
			zone.AddJoinedRoom(room);
		}

		private User MergeUserListEntryToUser(UserListEntry entry, User u)
		{
			ParseUserVariables(u, entry.UserVariables);
			return u;
		}

		private void OnJoinZone(EsMessage m)
		{
			JoinZoneEvent joinZoneEvent = m as JoinZoneEvent;
			Zone zone = ZoneManager.ZoneById(joinZoneEvent.ZoneId);
			if (zone == null)
			{
				zone = new Zone();
				zone.Id = joinZoneEvent.ZoneId;
				zone.Name = joinZoneEvent.ZoneName;
				ZoneManager.AddZone(zone);
			}
			foreach (RoomListEntry room in joinZoneEvent.Rooms)
			{
				ParseAndAddRoom(zone, room);
			}
		}

		private void OnLeaveRoom(EsMessage m)
		{
			LeaveRoomEvent leaveRoomEvent = m as LeaveRoomEvent;
			Zone zone = ZoneManager.ZoneById(leaveRoomEvent.ZoneId);
			if (zone == null)
			{
				return;
			}
			Room room = zone.RoomById(leaveRoomEvent.RoomId);
			if (room == null)
			{
				return;
			}
			if (room.Users == null)
			{
				zone.RemoveJoinedRoom(room);
				return;
			}
			int userCount = room.Users.Count - 1;
			foreach (User user in room.Users)
			{
				UserManager.RemoveUser(user.UserName);
			}
			room.PurgeUsers();
			room.PurgeRoomVariables();
			room.UserCount = userCount;
			zone.RemoveJoinedRoom(room);
		}

		private void OnLeaveZone(EsMessage m)
		{
			LeaveZoneEvent leaveZoneEvent = m as LeaveZoneEvent;
			ZoneManager.RemoveZone(leaveZoneEvent.ZoneId);
		}

		private void OnLogin(EsMessage m)
		{
			LoginResponse loginResponse = m as LoginResponse;
			if (!loginResponse.Successful)
			{
				return;
			}
			User user = new User();
			user.IsMe = true;
			user.UserName = loginResponse.UserName;
			foreach (string key2 in loginResponse.UserVariables.Keys)
			{
				EsObject value = loginResponse.UserVariables[key2] as EsObject;
				UserVariable userVariable = new UserVariable();
				userVariable.Name = key2;
				userVariable.Value = value;
				user.AddUserVariable(userVariable);
			}
			UserManager.AddUser(user);
			UserManager.Me = user;
			if (loginResponse.BuddyListEntries == null)
			{
				return;
			}
			foreach (KeyValuePair<string, EsObjectRO> buddyListEntry in loginResponse.BuddyListEntries)
			{
				string key = buddyListEntry.Key;
				User user2 = UserManager.UserByName(key);
				if (user2 == null)
				{
					user2 = new User();
					user2.UserName = key;
				}
				user2.BuddyVariable = loginResponse.BuddyListEntries[key] as EsObject;
				if (!user2.IsBuddy)
				{
					UserManager.AddBuddy(user2);
				}
			}
		}

		private void OnRemoveBuddies(EsMessage e)
		{
			RemoveBuddiesResponse removeBuddiesResponse = e as RemoveBuddiesResponse;
			foreach (string item in removeBuddiesResponse.BuddiesRemoved)
			{
				User user = UserManager.UserByName(item);
				if (user != null)
				{
					UserManager.RemoveBuddy(user);
				}
			}
		}

		private void OnRoomUserUpdate(EsMessage m)
		{
			UserUpdateEvent userUpdateEvent = m as UserUpdateEvent;
			User user = UserManager.UserByName(userUpdateEvent.UserName);
			Zone zone = ZoneManager.ZoneById(userUpdateEvent.ZoneId);
			Room room = zone.RoomById(userUpdateEvent.RoomId);
			if (user == null)
			{
				user = new User();
				user.UserName = userUpdateEvent.UserName;
			}
			foreach (UserVariable userVariable in userUpdateEvent.UserVariables)
			{
				user.AddUserVariable(userVariable);
			}
			UserUpdateAction? action = userUpdateEvent.Action;
			if (action.HasValue)
			{
				switch (action.Value)
				{
				case UserUpdateAction.AddUser:
					user = UserManager.AddUser(user);
					room.AddUser(user);
					break;
				case UserUpdateAction.DeleteUser:
					UserManager.RemoveUser(user.UserName);
					room.RemoveUser(user.UserName);
					break;
				case UserUpdateAction.SendingVideoStream:
					break;
				case UserUpdateAction.StoppingVideoStream:
					break;
				case UserUpdateAction.OperatorGranted:
					break;
				case UserUpdateAction.OperatorRevoked:
					break;
				}
			}
		}

		private void OnRoomVariableUpdate(EsMessage m)
		{
			RoomVariableUpdateEvent roomVariableUpdateEvent = m as RoomVariableUpdateEvent;
			Zone zone = ZoneManager.ZoneById(roomVariableUpdateEvent.ZoneId);
			Room room = zone.RoomById(roomVariableUpdateEvent.RoomId);
			RoomVariable roomVariable = room.RoomVariableByName(roomVariableUpdateEvent.Name);
			if (roomVariable == null)
			{
				roomVariable = new RoomVariable();
			}
			RoomVariableUpdateAction? action = roomVariableUpdateEvent.Action;
			if (!action.HasValue)
			{
				return;
			}
			switch (action.Value)
			{
			case RoomVariableUpdateAction.VariableCreated:
				roomVariable.Name = roomVariableUpdateEvent.Name;
				roomVariable.Value = roomVariableUpdateEvent.Value;
				roomVariable.Locked = roomVariableUpdateEvent.Locked;
				room.AddRoomVariable(roomVariable);
				break;
			case RoomVariableUpdateAction.VariableDeleted:
				room.RemoveRoomVariable(roomVariableUpdateEvent.Name);
				break;
			case RoomVariableUpdateAction.VariableUpdated:
				if (roomVariableUpdateEvent.LockStatusChanged)
				{
					roomVariable.Locked = roomVariableUpdateEvent.Locked;
				}
				if (roomVariableUpdateEvent.ValueChanged)
				{
					roomVariable.Value = roomVariableUpdateEvent.Value;
				}
				break;
			}
		}

		private void OnUpdateRoomDetails(EsMessage m)
		{
			UpdateRoomDetailsEvent updateRoomDetailsEvent = m as UpdateRoomDetailsEvent;
			Zone zone = ZoneManager.ZoneById(updateRoomDetailsEvent.ZoneId);
			Room room = zone.RoomById(updateRoomDetailsEvent.RoomId);
			if (updateRoomDetailsEvent.CapacityUpdated)
			{
				room.Capacity = updateRoomDetailsEvent.Capacity;
			}
			if (updateRoomDetailsEvent.RoomDescriptionUpdated)
			{
				room.Description = updateRoomDetailsEvent.RoomDescription;
			}
			if (updateRoomDetailsEvent.HiddenUpdated)
			{
				room.IsHidden = updateRoomDetailsEvent.Hidden;
			}
			if (updateRoomDetailsEvent.HasPasswordUpdated)
			{
			}
			if (updateRoomDetailsEvent.RoomNameUpdated)
			{
				room.Name = updateRoomDetailsEvent.RoomName;
				zone.RemoveRoom(room.Id);
				zone.AddRoom(room);
			}
		}

		private void OnUserVariableUpdate(EsMessage m)
		{
			UserVariableUpdateEvent userVariableUpdateEvent = m as UserVariableUpdateEvent;
			User user = UserManager.UserByName(userVariableUpdateEvent.UserName);
			UserVariable userVariable = user.UserVariableByName(userVariableUpdateEvent.Variable.Name);
			if (userVariable == null)
			{
				userVariable = userVariableUpdateEvent.Variable;
				if (userVariableUpdateEvent.Action == UserVariableUpdateAction.VariableUpdated)
				{
					userVariableUpdateEvent.Action = UserVariableUpdateAction.VariableCreated;
				}
			}
			UserVariableUpdateAction? action = userVariableUpdateEvent.Action;
			if (action.HasValue)
			{
				switch (action.Value)
				{
				case UserVariableUpdateAction.VariableCreated:
					user.AddUserVariable(userVariable);
					break;
				case UserVariableUpdateAction.VariableDeleted:
					user.RemoveUserVaraible(userVariable.Name);
					break;
				case UserVariableUpdateAction.VariableUpdated:
					userVariable.Value = userVariableUpdateEvent.Variable.Value;
					break;
				}
			}
		}

		private void OnZoneUpdate(EsMessage m)
		{
			ZoneUpdateEvent zoneUpdateEvent = m as ZoneUpdateEvent;
			Zone zone = ZoneManager.ZoneById(zoneUpdateEvent.ZoneId);
			Room room = zone.RoomById(zoneUpdateEvent.RoomId);
			ZoneUpdateAction? action = zoneUpdateEvent.Action;
			if (!action.HasValue)
			{
				return;
			}
			switch (action.Value)
			{
			case ZoneUpdateAction.AddRoom:
				ParseAndAddRoom(zone, zoneUpdateEvent.RoomListEntry);
				break;
			case ZoneUpdateAction.DeleteRoom:
				zone.RemoveRoom(zoneUpdateEvent.RoomId);
				break;
			case ZoneUpdateAction.UpdateRoom:
				if (zoneUpdateEvent.RoomListEntry != null)
				{
					room.UserCount = zoneUpdateEvent.RoomListEntry.UserCount;
				}
				else if (room != null)
				{
					room.UserCount = zoneUpdateEvent.RoomCount;
				}
				break;
			}
		}

		private void ParseAndAddRoom(Zone z, RoomListEntry entry)
		{
			Room room = new Room();
			room.Id = entry.RoomId;
			room.Name = entry.RoomName;
			room.Description = entry.RoomDescription;
			room.Capacity = entry.Capacity;
			room.UserCount = entry.UserCount;
			room.HasPassword = entry.HasPassword;
			z.AddRoom(room);
		}

		private void ParseUserVariables(User u, List<UserVariable> arr)
		{
			for (int i = 0; i < arr.Count; i++)
			{
				UserVariable v = arr[i];
				u.AddUserVariable(v);
			}
		}
	}
}
