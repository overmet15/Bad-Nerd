using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class DHInitiateKeyExchangeRequest : EsRequest
	{
		public DHInitiateKeyExchangeRequest()
		{
			base.MessageType = MessageType.DHInitiate;
		}

		public DHInitiateKeyExchangeRequest(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			return new ThriftDHInitiateKeyExchangeRequest();
		}

		public override TBase NewThrift()
		{
			return new ThriftDHInitiateKeyExchangeRequest();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftDHInitiateKeyExchangeRequest thriftDHInitiateKeyExchangeRequest = (ThriftDHInitiateKeyExchangeRequest)t_;
		}
	}
}
