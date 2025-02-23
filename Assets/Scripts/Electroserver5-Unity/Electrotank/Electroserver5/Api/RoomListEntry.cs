using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class RoomListEntry : EsEntity
	{
		private int RoomId_;

		private int ZoneId_;

		private string RoomName_;

		private int UserCount_;

		private string RoomDescription_;

		private int Capacity_;

		private bool HasPassword_;

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

		public int UserCount
		{
			get
			{
				return UserCount_;
			}
			set
			{
				UserCount_ = value;
				UserCount_Set_ = true;
			}
		}

		private bool UserCount_Set_ { get; set; }

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

		public RoomListEntry()
		{
		}

		public RoomListEntry(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftRoomListEntry thriftRoomListEntry = new ThriftRoomListEntry();
			if (RoomId_Set_)
			{
				int roomId = RoomId;
				thriftRoomListEntry.RoomId = roomId;
			}
			if (ZoneId_Set_)
			{
				int zoneId = ZoneId;
				thriftRoomListEntry.ZoneId = zoneId;
			}
			if (RoomName_Set_ && RoomName != null)
			{
				string roomName = RoomName;
				thriftRoomListEntry.RoomName = roomName;
			}
			if (UserCount_Set_)
			{
				int userCount = UserCount;
				thriftRoomListEntry.UserCount = userCount;
			}
			if (RoomDescription_Set_ && RoomDescription != null)
			{
				string roomDescription = RoomDescription;
				thriftRoomListEntry.RoomDescription = roomDescription;
			}
			if (Capacity_Set_)
			{
				int capacity = Capacity;
				thriftRoomListEntry.Capacity = capacity;
			}
			if (HasPassword_Set_)
			{
				bool hasPassword = HasPassword;
				thriftRoomListEntry.HasPassword = hasPassword;
			}
			return thriftRoomListEntry;
		}

		public override TBase NewThrift()
		{
			return new ThriftRoomListEntry();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftRoomListEntry thriftRoomListEntry = (ThriftRoomListEntry)t_;
			if (thriftRoomListEntry.__isset.roomId)
			{
				RoomId = thriftRoomListEntry.RoomId;
			}
			if (thriftRoomListEntry.__isset.zoneId)
			{
				ZoneId = thriftRoomListEntry.ZoneId;
			}
			if (thriftRoomListEntry.__isset.roomName && thriftRoomListEntry.RoomName != null)
			{
				RoomName = thriftRoomListEntry.RoomName;
			}
			if (thriftRoomListEntry.__isset.userCount)
			{
				UserCount = thriftRoomListEntry.UserCount;
			}
			if (thriftRoomListEntry.__isset.roomDescription && thriftRoomListEntry.RoomDescription != null)
			{
				RoomDescription = thriftRoomListEntry.RoomDescription;
			}
			if (thriftRoomListEntry.__isset.capacity)
			{
				Capacity = thriftRoomListEntry.Capacity;
			}
			if (thriftRoomListEntry.__isset.hasPassword)
			{
				HasPassword = thriftRoomListEntry.HasPassword;
			}
		}
	}
}
