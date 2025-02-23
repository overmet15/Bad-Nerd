using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class EvictUserFromRoomRequest : EsRequest
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

		public EvictUserFromRoomRequest()
		{
			base.MessageType = MessageType.EvictUserFromRoomRequest;
		}

		public EvictUserFromRoomRequest(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftEvictUserFromRoomRequest thriftEvictUserFromRoomRequest = new ThriftEvictUserFromRoomRequest();
			if (ZoneId_Set_)
			{
				int zoneId = ZoneId;
				thriftEvictUserFromRoomRequest.ZoneId = zoneId;
			}
			if (RoomId_Set_)
			{
				int roomId = RoomId;
				thriftEvictUserFromRoomRequest.RoomId = roomId;
			}
			if (UserName_Set_ && UserName != null)
			{
				string userName = UserName;
				thriftEvictUserFromRoomRequest.UserName = userName;
			}
			if (Reason_Set_ && Reason != null)
			{
				string reason = Reason;
				thriftEvictUserFromRoomRequest.Reason = reason;
			}
			if (Ban_Set_)
			{
				bool ban = Ban;
				thriftEvictUserFromRoomRequest.Ban = ban;
			}
			if (Duration_Set_)
			{
				int duration = Duration;
				thriftEvictUserFromRoomRequest.Duration = duration;
			}
			return thriftEvictUserFromRoomRequest;
		}

		public override TBase NewThrift()
		{
			return new ThriftEvictUserFromRoomRequest();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftEvictUserFromRoomRequest thriftEvictUserFromRoomRequest = (ThriftEvictUserFromRoomRequest)t_;
			if (thriftEvictUserFromRoomRequest.__isset.zoneId)
			{
				ZoneId = thriftEvictUserFromRoomRequest.ZoneId;
			}
			if (thriftEvictUserFromRoomRequest.__isset.roomId)
			{
				RoomId = thriftEvictUserFromRoomRequest.RoomId;
			}
			if (thriftEvictUserFromRoomRequest.__isset.userName && thriftEvictUserFromRoomRequest.UserName != null)
			{
				UserName = thriftEvictUserFromRoomRequest.UserName;
			}
			if (thriftEvictUserFromRoomRequest.__isset.reason && thriftEvictUserFromRoomRequest.Reason != null)
			{
				Reason = thriftEvictUserFromRoomRequest.Reason;
			}
			if (thriftEvictUserFromRoomRequest.__isset.ban)
			{
				Ban = thriftEvictUserFromRoomRequest.Ban;
			}
			if (thriftEvictUserFromRoomRequest.__isset.duration)
			{
				Duration = thriftEvictUserFromRoomRequest.Duration;
			}
		}
	}
}
