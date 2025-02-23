using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class AddRoomOperatorRequest : EsRequest
	{
		private int ZoneId_;

		private int RoomId_;

		private string UserName_;

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

		public AddRoomOperatorRequest()
		{
			base.MessageType = MessageType.AddRoomOperatorRequest;
		}

		public AddRoomOperatorRequest(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftAddRoomOperatorRequest thriftAddRoomOperatorRequest = new ThriftAddRoomOperatorRequest();
			if (ZoneId_Set_)
			{
				int zoneId = ZoneId;
				thriftAddRoomOperatorRequest.ZoneId = zoneId;
			}
			if (RoomId_Set_)
			{
				int roomId = RoomId;
				thriftAddRoomOperatorRequest.RoomId = roomId;
			}
			if (UserName_Set_ && UserName != null)
			{
				string userName = UserName;
				thriftAddRoomOperatorRequest.UserName = userName;
			}
			return thriftAddRoomOperatorRequest;
		}

		public override TBase NewThrift()
		{
			return new ThriftAddRoomOperatorRequest();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftAddRoomOperatorRequest thriftAddRoomOperatorRequest = (ThriftAddRoomOperatorRequest)t_;
			if (thriftAddRoomOperatorRequest.__isset.zoneId)
			{
				ZoneId = thriftAddRoomOperatorRequest.ZoneId;
			}
			if (thriftAddRoomOperatorRequest.__isset.roomId)
			{
				RoomId = thriftAddRoomOperatorRequest.RoomId;
			}
			if (thriftAddRoomOperatorRequest.__isset.userName && thriftAddRoomOperatorRequest.UserName != null)
			{
				UserName = thriftAddRoomOperatorRequest.UserName;
			}
		}
	}
}
