using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class GetUsersInRoomRequest : EsRequest
	{
		private int ZoneId_;

		private int RoomId_;

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

		public GetUsersInRoomRequest()
		{
			base.MessageType = MessageType.GetUsersInRoomRequest;
		}

		public GetUsersInRoomRequest(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftGetUsersInRoomRequest thriftGetUsersInRoomRequest = new ThriftGetUsersInRoomRequest();
			if (ZoneId_Set_)
			{
				int zoneId = ZoneId;
				thriftGetUsersInRoomRequest.ZoneId = zoneId;
			}
			if (RoomId_Set_)
			{
				int roomId = RoomId;
				thriftGetUsersInRoomRequest.RoomId = roomId;
			}
			return thriftGetUsersInRoomRequest;
		}

		public override TBase NewThrift()
		{
			return new ThriftGetUsersInRoomRequest();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftGetUsersInRoomRequest thriftGetUsersInRoomRequest = (ThriftGetUsersInRoomRequest)t_;
			if (thriftGetUsersInRoomRequest.__isset.zoneId)
			{
				ZoneId = thriftGetUsersInRoomRequest.ZoneId;
			}
			if (thriftGetUsersInRoomRequest.__isset.roomId)
			{
				RoomId = thriftGetUsersInRoomRequest.RoomId;
			}
		}
	}
}
