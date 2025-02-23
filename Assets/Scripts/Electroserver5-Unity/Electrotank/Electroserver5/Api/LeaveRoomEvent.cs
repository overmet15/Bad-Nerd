using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class LeaveRoomEvent : EsEvent
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

		public LeaveRoomEvent()
		{
			base.MessageType = MessageType.LeaveRoomEvent;
		}

		public LeaveRoomEvent(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftLeaveRoomEvent thriftLeaveRoomEvent = new ThriftLeaveRoomEvent();
			if (ZoneId_Set_)
			{
				int zoneId = ZoneId;
				thriftLeaveRoomEvent.ZoneId = zoneId;
			}
			if (RoomId_Set_)
			{
				int roomId = RoomId;
				thriftLeaveRoomEvent.RoomId = roomId;
			}
			return thriftLeaveRoomEvent;
		}

		public override TBase NewThrift()
		{
			return new ThriftLeaveRoomEvent();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftLeaveRoomEvent thriftLeaveRoomEvent = (ThriftLeaveRoomEvent)t_;
			if (thriftLeaveRoomEvent.__isset.zoneId)
			{
				ZoneId = thriftLeaveRoomEvent.ZoneId;
			}
			if (thriftLeaveRoomEvent.__isset.roomId)
			{
				RoomId = thriftLeaveRoomEvent.RoomId;
			}
		}
	}
}
