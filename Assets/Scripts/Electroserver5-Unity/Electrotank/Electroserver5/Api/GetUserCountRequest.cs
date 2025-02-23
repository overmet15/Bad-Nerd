using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class GetUserCountRequest : EsRequest
	{
		public GetUserCountRequest()
		{
			base.MessageType = MessageType.GetUserCountRequest;
		}

		public GetUserCountRequest(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			return new ThriftGetUserCountRequest();
		}

		public override TBase NewThrift()
		{
			return new ThriftGetUserCountRequest();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftGetUserCountRequest thriftGetUserCountRequest = (ThriftGetUserCountRequest)t_;
		}
	}
}
