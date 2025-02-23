using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class LeaveZoneEvent : EsEvent
	{
		private int ZoneId_;

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

		public LeaveZoneEvent()
		{
			base.MessageType = MessageType.LeaveZoneEvent;
		}

		public LeaveZoneEvent(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftLeaveZoneEvent thriftLeaveZoneEvent = new ThriftLeaveZoneEvent();
			if (ZoneId_Set_)
			{
				int zoneId = ZoneId;
				thriftLeaveZoneEvent.ZoneId = zoneId;
			}
			return thriftLeaveZoneEvent;
		}

		public override TBase NewThrift()
		{
			return new ThriftLeaveZoneEvent();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftLeaveZoneEvent thriftLeaveZoneEvent = (ThriftLeaveZoneEvent)t_;
			if (thriftLeaveZoneEvent.__isset.zoneId)
			{
				ZoneId = thriftLeaveZoneEvent.ZoneId;
			}
		}
	}
}
