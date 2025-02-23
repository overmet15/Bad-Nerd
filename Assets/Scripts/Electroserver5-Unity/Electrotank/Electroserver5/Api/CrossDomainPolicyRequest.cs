using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class CrossDomainPolicyRequest : EsRequest
	{
		public CrossDomainPolicyRequest()
		{
			base.MessageType = MessageType.CrossDomainRequest;
		}

		public CrossDomainPolicyRequest(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			return new ThriftCrossDomainPolicyRequest();
		}

		public override TBase NewThrift()
		{
			return new ThriftCrossDomainPolicyRequest();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftCrossDomainPolicyRequest thriftCrossDomainPolicyRequest = (ThriftCrossDomainPolicyRequest)t_;
		}
	}
}
