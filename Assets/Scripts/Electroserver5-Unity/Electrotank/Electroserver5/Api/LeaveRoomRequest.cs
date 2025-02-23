using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class LeaveRoomRequest : EsRequest
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

		public LeaveRoomRequest()
		{
			base.MessageType = MessageType.LeaveRoomRequest;
		}

		public LeaveRoomRequest(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftLeaveRoomRequest thriftLeaveRoomRequest = new ThriftLeaveRoomRequest();
			if (ZoneId_Set_)
			{
				int zoneId = ZoneId;
				thriftLeaveRoomRequest.ZoneId = zoneId;
			}
			if (RoomId_Set_)
			{
				int roomId = RoomId;
				thriftLeaveRoomRequest.RoomId = roomId;
			}
			return thriftLeaveRoomRequest;
		}

		public override TBase NewThrift()
		{
			return new ThriftLeaveRoomRequest();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftLeaveRoomRequest thriftLeaveRoomRequest = (ThriftLeaveRoomRequest)t_;
			if (thriftLeaveRoomRequest.__isset.zoneId)
			{
				ZoneId = thriftLeaveRoomRequest.ZoneId;
			}
			if (thriftLeaveRoomRequest.__isset.roomId)
			{
				RoomId = thriftLeaveRoomRequest.RoomId;
			}
		}
	}
}
