using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class ConnectionClosedEvent : EsEvent
	{
		private int ConnectionId_;

		public int ConnectionId
		{
			get
			{
				return ConnectionId_;
			}
			set
			{
				ConnectionId_ = value;
				ConnectionId_Set_ = true;
			}
		}

		private bool ConnectionId_Set_ { get; set; }

		public ConnectionClosedEvent()
		{
			base.MessageType = MessageType.ConnectionClosedEvent;
		}

		public ConnectionClosedEvent(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftConnectionClosedEvent thriftConnectionClosedEvent = new ThriftConnectionClosedEvent();
			if (ConnectionId_Set_)
			{
				int connectionId = ConnectionId;
				thriftConnectionClosedEvent.ConnectionId = connectionId;
			}
			return thriftConnectionClosedEvent;
		}

		public override TBase NewThrift()
		{
			return new ThriftConnectionClosedEvent();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftConnectionClosedEvent thriftConnectionClosedEvent = (ThriftConnectionClosedEvent)t_;
			if (thriftConnectionClosedEvent.__isset.connectionId)
			{
				ConnectionId = thriftConnectionClosedEvent.ConnectionId;
			}
		}
	}
}
