using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class GetServerLocalTimeRequest : EsRequest
	{
		public GetServerLocalTimeRequest()
		{
			base.MessageType = MessageType.GetServerLocalTimeRequest;
		}

		public GetServerLocalTimeRequest(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			return new ThriftGetServerLocalTimeRequest();
		}

		public override TBase NewThrift()
		{
			return new ThriftGetServerLocalTimeRequest();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftGetServerLocalTimeRequest thriftGetServerLocalTimeRequest = (ThriftGetServerLocalTimeRequest)t_;
		}
	}
}
