using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class GetUserCountResponse : EsResponse
	{
		private int Count_;

		public int Count
		{
			get
			{
				return Count_;
			}
			set
			{
				Count_ = value;
				Count_Set_ = true;
			}
		}

		private bool Count_Set_ { get; set; }

		public GetUserCountResponse()
		{
			base.MessageType = MessageType.GetUserCountResponse;
		}

		public GetUserCountResponse(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftGetUserCountResponse thriftGetUserCountResponse = new ThriftGetUserCountResponse();
			if (Count_Set_)
			{
				int count = Count;
				thriftGetUserCountResponse.Count = count;
			}
			return thriftGetUserCountResponse;
		}

		public override TBase NewThrift()
		{
			return new ThriftGetUserCountResponse();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftGetUserCountResponse thriftGetUserCountResponse = (ThriftGetUserCountResponse)t_;
			if (thriftGetUserCountResponse.__isset.count)
			{
				Count = thriftGetUserCountResponse.Count;
			}
		}
	}
}
