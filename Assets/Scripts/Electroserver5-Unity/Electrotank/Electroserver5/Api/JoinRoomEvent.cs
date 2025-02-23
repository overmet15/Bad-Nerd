using System.Collections.Generic;
using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class JoinRoomEvent : EsEvent
	{
		private int ZoneId_;

		private int RoomId_;

		private string RoomName_;

		private string RoomDescription_;

		private bool HasPassword_;

		private bool Hidden_;

		private int Capacity_;

		private List<UserListEntry> Users_;

		private List<RoomVariable> RoomVariables_;

		public int ZoneId
		{
			get
			{
				return ZoneId_;
			}
			set
			{
				ZoneId_ = value;
				ZoneId_Set_ = true;
			}
		}

		private bool ZoneId_Set_ { get; set; }

		public int RoomId
		{
			get
			{
				return RoomId_;
			}
			set
			{
				RoomId_ = value;
				RoomId_Set_ = true;
			}
		}

		private bool RoomId_Set_ { get; set; }

		public string RoomName
		{
			get
			{
				return RoomName_;
			}
			set
			{
				RoomName_ = value;
				RoomName_Set_ = true;
			}
		}

		private bool RoomName_Set_ { get; set; }

		public string RoomDescription
		{
			get
			{
				return RoomDescription_;
			}
			set
			{
				RoomDescription_ = value;
				RoomDescription_Set_ = true;
			}
		}

		private bool RoomDescription_Set_ { get; set; }

		public bool HasPassword
		{
			get
			{
				return HasPassword_;
			}
			set
			{
				HasPassword_ = value;
				HasPassword_Set_ = true;
			}
		}

		private bool HasPassword_Set_ { get; set; }

		public bool Hidden
		{
			get
			{
				return Hidden_;
			}
			set
			{
				Hidden_ = value;
				Hidden_Set_ = true;
			}
		}

		private bool Hidden_Set_ { get; set; }

		public int Capacity
		{
			get
			{
				return Capacity_;
			}
			set
			{
				Capacity_ = value;
				Capacity_Set_ = true;
			}
		}

		private bool Capacity_Set_ { get; set; }

		public List<UserListEntry> Users
		{
			get
			{
				return Users_;
			}
			set
			{
				Users_ = value;
				Users_Set_ = true;
			}
		}

		private bool Users_Set_ { get; set; }

		public List<RoomVariable> RoomVariables
		{
			get
			{
				return RoomVariables_;
			}
			set
			{
				RoomVariables_ = value;
				RoomVariables_Set_ = true;
			}
		}

		private bool RoomVariables_Set_ { get; set; }

		public JoinRoomEvent()
		{
			base.MessageType = MessageType.JoinRoomEvent;
		}

		public JoinRoomEvent(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftJoinRoomEvent thriftJoinRoomEvent = new ThriftJoinRoomEvent();
			if (ZoneId_Set_)
			{
				int zoneId = ZoneId;
				thriftJoinRoomEvent.ZoneId = zoneId;
			}
			if (RoomId_Set_)
			{
				int roomId = RoomId;
				thriftJoinRoomEvent.RoomId = roomId;
			}
			if (RoomName_Set_ && RoomName != null)
			{
				string roomName = RoomName;
				thriftJoinRoomEvent.RoomName = roomName;
			}
			if (RoomDescription_Set_ && RoomDescription != null)
			{
				string roomDescription = RoomDescription;
				thriftJoinRoomEvent.RoomDescription = roomDescription;
			}
			if (HasPassword_Set_)
			{
				bool hasPassword = HasPassword;
				thriftJoinRoomEvent.HasPassword = hasPassword;
			}
			if (Hidden_Set_)
			{
				bool hidden = Hidden;
				thriftJoinRoomEvent.Hidden = hidden;
			}
			if (Capacity_Set_)
			{
				int capacity = Capacity;
				thriftJoinRoomEvent.Capacity = capacity;
			}
			if (Users_Set_ && Users != null)
			{
				List<ThriftUserListEntry> list = new List<ThriftUserListEntry>();
				foreach (UserListEntry user in Users)
				{
					ThriftUserListEntry item = user.ToThrift() as ThriftUserListEntry;
					list.Add(item);
				}
				thriftJoinRoomEvent.Users = list;
			}
			if (RoomVariables_Set_ && RoomVariables != null)
			{
				List<ThriftRoomVariable> list2 = new List<ThriftRoomVariable>();
				foreach (RoomVariable roomVariable in RoomVariables)
				{
					ThriftRoomVariable item2 = roomVariable.ToThrift() as ThriftRoomVariable;
					list2.Add(item2);
				}
				thriftJoinRoomEvent.RoomVariables = list2;
			}
			return thriftJoinRoomEvent;
		}

		public override TBase NewThrift()
		{
			return new ThriftJoinRoomEvent();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftJoinRoomEvent thriftJoinRoomEvent = (ThriftJoinRoomEvent)t_;
			if (thriftJoinRoomEvent.__isset.zoneId)
			{
				ZoneId = thriftJoinRoomEvent.ZoneId;
			}
			if (thriftJoinRoomEvent.__isset.roomId)
			{
				RoomId = thriftJoinRoomEvent.RoomId;
			}
			if (thriftJoinRoomEvent.__isset.roomName && thriftJoinRoomEvent.RoomName != null)
			{
				RoomName = thriftJoinRoomEvent.RoomName;
			}
			if (thriftJoinRoomEvent.__isset.roomDescription && thriftJoinRoomEvent.RoomDescription != null)
			{
				RoomDescription = thriftJoinRoomEvent.RoomDescription;
			}
			if (thriftJoinRoomEvent.__isset.hasPassword)
			{
				HasPassword = thriftJoinRoomEvent.HasPassword;
			}
			if (thriftJoinRoomEvent.__isset.hidden)
			{
				Hidden = thriftJoinRoomEvent.Hidden;
			}
			if (thriftJoinRoomEvent.__isset.capacity)
			{
				Capacity = thriftJoinRoomEvent.Capacity;
			}
			if (thriftJoinRoomEvent.__isset.users && thriftJoinRoomEvent.Users != null)
			{
				Users = new List<UserListEntry>();
				foreach (ThriftUserListEntry user in thriftJoinRoomEvent.Users)
				{
					UserListEntry item = new UserListEntry(user);
					Users.Add(item);
				}
			}
			if (!thriftJoinRoomEvent.__isset.roomVariables || thriftJoinRoomEvent.RoomVariables == null)
			{
				return;
			}
			RoomVariables = new List<RoomVariable>();
			foreach (ThriftRoomVariable roomVariable in thriftJoinRoomEvent.RoomVariables)
			{
				RoomVariable item2 = new RoomVariable(roomVariable);
				RoomVariables.Add(item2);
			}
		}
	}
}
