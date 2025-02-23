using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class IdleTimeoutWarningEvent : EsEvent
	{
		public IdleTimeoutWarningEvent()
		{
			base.MessageType = MessageType.IdleTimeoutWarningEvent;
		}

		public IdleTimeoutWarningEvent(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			return new ThriftIdleTimeoutWarningEvent();
		}

		public override TBase NewThrift()
		{
			return new ThriftIdleTimeoutWarningEvent();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftIdleTimeoutWarningEvent thriftIdleTimeoutWarningEvent = (ThriftIdleTimeoutWarningEvent)t_;
		}
	}
}
