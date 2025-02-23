using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class SessionIdleEvent : EsEvent
	{
		public SessionIdleEvent()
		{
			base.MessageType = MessageType.SessionIdleEvent;
		}

		public SessionIdleEvent(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			return new ThriftSessionIdleEvent();
		}

		public override TBase NewThrift()
		{
			return new ThriftSessionIdleEvent();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftSessionIdleEvent thriftSessionIdleEvent = (ThriftSessionIdleEvent)t_;
		}
	}
}
