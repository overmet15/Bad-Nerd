using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class ProtocolConfiguration : EsEntity
	{
		private int MessageCompressionThreshold_;

		public int MessageCompressionThreshold
		{
			get
			{
				return MessageCompressionThreshold_;
			}
			set
			{
				MessageCompressionThreshold_ = value;
				MessageCompressionThreshold_Set_ = true;
			}
		}

		private bool MessageCompressionThreshold_Set_ { get; set; }

		public ProtocolConfiguration()
		{
		}

		public ProtocolConfiguration(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftProtocolConfiguration thriftProtocolConfiguration = new ThriftProtocolConfiguration();
			if (MessageCompressionThreshold_Set_)
			{
				int messageCompressionThreshold = MessageCompressionThreshold;
				thriftProtocolConfiguration.MessageCompressionThreshold = messageCompressionThreshold;
			}
			return thriftProtocolConfiguration;
		}

		public override TBase NewThrift()
		{
			return new ThriftProtocolConfiguration();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftProtocolConfiguration thriftProtocolConfiguration = (ThriftProtocolConfiguration)t_;
			if (thriftProtocolConfiguration.__isset.messageCompressionThreshold)
			{
				MessageCompressionThreshold = thriftProtocolConfiguration.MessageCompressionThreshold;
			}
		}
	}
}
