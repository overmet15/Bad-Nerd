using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class UserEvictedFromRoomEvent : EsEvent
	{
		private int ZoneId_;

		private int RoomId_;

		private string UserName_;

		private string Reason_;

		private bool Ban_;

		private int Duration_;

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

		public string UserName
		{
			get
			{
				return UserName_;
			}
			set
			{
				UserName_ = value;
				UserName_Set_ = true;
			}
		}

		private bool UserName_Set_ { get; set; }

		public string Reason
		{
			get
			{
				return Reason_;
			}
			set
			{
				Reason_ = value;
				Reason_Set_ = true;
			}
		}

		private bool Reason_Set_ { get; set; }

		public bool Ban
		{
			get
			{
				return Ban_;
			}
			set
			{
				Ban_ = value;
				Ban_Set_ = true;
			}
		}

		private bool Ban_Set_ { get; set; }

		public int Duration
		{
			get
			{
				return Duration_;
			}
			set
			{
				Duration_ = value;
				Duration_Set_ = true;
			}
		}

		private bool Duration_Set_ { get; set; }

		public UserEvictedFromRoomEvent()
		{
			base.MessageType = MessageType.UserEvictedFromRoomEvent;
		}

		public UserEvictedFromRoomEvent(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftUserEvictedFromRoomEvent thriftUserEvictedFromRoomEvent = new ThriftUserEvictedFromRoomEvent();
			if (ZoneId_Set_)
			{
				int zoneId = ZoneId;
				thriftUserEvictedFromRoomEvent.ZoneId = zoneId;
			}
			if (RoomId_Set_)
			{
				int roomId = RoomId;
				thriftUserEvictedFromRoomEvent.RoomId = roomId;
			}
			if (UserName_Set_ && UserName != null)
			{
				string userName = UserName;
				thriftUserEvictedFromRoomEvent.UserName = userName;
			}
			if (Reason_Set_ && Reason != null)
			{
				string reason = Reason;
				thriftUserEvictedFromRoomEvent.Reason = reason;
			}
			if (Ban_Set_)
			{
				bool ban = Ban;
				thriftUserEvictedFromRoomEvent.Ban = ban;
			}
			if (Duration_Set_)
			{
				int duration = Duration;
				thriftUserEvictedFromRoomEvent.Duration = duration;
			}
			return thriftUserEvictedFromRoomEvent;
		}

		public override TBase NewThrift()
		{
			return new ThriftUserEvictedFromRoomEvent();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftUserEvictedFromRoomEvent thriftUserEvictedFromRoomEvent = (ThriftUserEvictedFromRoomEvent)t_;
			if (thriftUserEvictedFromRoomEvent.__isset.zoneId)
			{
				ZoneId = thriftUserEvictedFromRoomEvent.ZoneId;
			}
			if (thriftUserEvictedFromRoomEvent.__isset.roomId)
			{
				RoomId = thriftUserEvictedFromRoomEvent.RoomId;
			}
			if (thriftUserEvictedFromRoomEvent.__isset.userName && thriftUserEvictedFromRoomEvent.UserName != null)
			{
				UserName = thriftUserEvictedFromRoomEvent.UserName;
			}
			if (thriftUserEvictedFromRoomEvent.__isset.reason && thriftUserEvictedFromRoomEvent.Reason != null)
			{
				Reason = thriftUserEvictedFromRoomEvent.Reason;
			}
			if (thriftUserEvictedFromRoomEvent.__isset.ban)
			{
				Ban = thriftUserEvictedFromRoomEvent.Ban;
			}
			if (thriftUserEvictedFromRoomEvent.__isset.duration)
			{
				Duration = thriftUserEvictedFromRoomEvent.Duration;
			}
		}
	}
}
