using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class EncryptionStateChangeEvent : EsEvent
	{
		private bool EncryptionOn_;

		public bool EncryptionOn
		{
			get
			{
				return EncryptionOn_;
			}
			set
			{
				EncryptionOn_ = value;
				EncryptionOn_Set_ = true;
			}
		}

		private bool EncryptionOn_Set_ { get; set; }

		public EncryptionStateChangeEvent()
		{
			base.MessageType = MessageType.EncryptionStateChange;
		}

		public EncryptionStateChangeEvent(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftEncryptionStateChangeEvent thriftEncryptionStateChangeEvent = new ThriftEncryptionStateChangeEvent();
			if (EncryptionOn_Set_)
			{
				bool encryptionOn = EncryptionOn;
				thriftEncryptionStateChangeEvent.EncryptionOn = encryptionOn;
			}
			return thriftEncryptionStateChangeEvent;
		}

		public override TBase NewThrift()
		{
			return new ThriftEncryptionStateChangeEvent();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftEncryptionStateChangeEvent thriftEncryptionStateChangeEvent = (ThriftEncryptionStateChangeEvent)t_;
			if (thriftEncryptionStateChangeEvent.__isset.encryptionOn)
			{
				EncryptionOn = thriftEncryptionStateChangeEvent.EncryptionOn;
			}
		}
	}
}
