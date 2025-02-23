using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class GetZonesRequest : EsRequest
	{
		public GetZonesRequest()
		{
			base.MessageType = MessageType.GetZonesRequest;
		}

		public GetZonesRequest(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			return new ThriftGetZonesRequest();
		}

		public override TBase NewThrift()
		{
			return new ThriftGetZonesRequest();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftGetZonesRequest thriftGetZonesRequest = (ThriftGetZonesRequest)t_;
		}
	}
}
