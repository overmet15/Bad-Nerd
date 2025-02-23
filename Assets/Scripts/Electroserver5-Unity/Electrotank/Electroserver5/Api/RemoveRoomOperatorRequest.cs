using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class RemoveRoomOperatorRequest : EsRequest
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

		public RemoveRoomOperatorRequest()
		{
			base.MessageType = MessageType.RemoveRoomOperatorRequest;
		}

		public RemoveRoomOperatorRequest(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftRemoveRoomOperatorRequest thriftRemoveRoomOperatorRequest = new ThriftRemoveRoomOperatorRequest();
			if (ZoneId_Set_)
			{
				int zoneId = ZoneId;
				thriftRemoveRoomOperatorRequest.ZoneId = zoneId;
			}
			if (RoomId_Set_)
			{
				int roomId = RoomId;
				thriftRemoveRoomOperatorRequest.RoomId = roomId;
			}
			if (UserName_Set_ && UserName != null)
			{
				string userName = UserName;
				thriftRemoveRoomOperatorRequest.UserName = userName;
			}
			return thriftRemoveRoomOperatorRequest;
		}

		public override TBase NewThrift()
		{
			return new ThriftRemoveRoomOperatorRequest();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftRemoveRoomOperatorRequest thriftRemoveRoomOperatorRequest = (ThriftRemoveRoomOperatorRequest)t_;
			if (thriftRemoveRoomOperatorRequest.__isset.zoneId)
			{
				ZoneId = thriftRemoveRoomOperatorRequest.ZoneId;
			}
			if (thriftRemoveRoomOperatorRequest.__isset.roomId)
			{
				RoomId = thriftRemoveRoomOperatorRequest.RoomId;
			}
			if (thriftRemoveRoomOperatorRequest.__isset.userName && thriftRemoveRoomOperatorRequest.UserName != null)
			{
				UserName = thriftRemoveRoomOperatorRequest.UserName;
			}
		}
	}
}
