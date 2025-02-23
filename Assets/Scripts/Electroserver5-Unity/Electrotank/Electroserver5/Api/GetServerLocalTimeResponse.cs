using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class GetServerLocalTimeResponse : EsResponse
	{
		private long ServerLocalTimeInMilliseconds_;

		public long ServerLocalTimeInMilliseconds
		{
			get
			{
				return ServerLocalTimeInMilliseconds_;
			}
			set
			{
				ServerLocalTimeInMilliseconds_ = value;
				ServerLocalTimeInMilliseconds_Set_ = true;
			}
		}

		private bool ServerLocalTimeInMilliseconds_Set_ { get; set; }

		public GetServerLocalTimeResponse()
		{
			base.MessageType = MessageType.GetServerLocalTimeResponse;
		}

		public GetServerLocalTimeResponse(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftGetServerLocalTimeResponse thriftGetServerLocalTimeResponse = new ThriftGetServerLocalTimeResponse();
			if (ServerLocalTimeInMilliseconds_Set_)
			{
				long serverLocalTimeInMilliseconds = ServerLocalTimeInMilliseconds;
				thriftGetServerLocalTimeResponse.ServerLocalTimeInMilliseconds = serverLocalTimeInMilliseconds;
			}
			return thriftGetServerLocalTimeResponse;
		}

		public override TBase NewThrift()
		{
			return new ThriftGetServerLocalTimeResponse();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftGetServerLocalTimeResponse thriftGetServerLocalTimeResponse = (ThriftGetServerLocalTimeResponse)t_;
			if (thriftGetServerLocalTimeResponse.__isset.serverLocalTimeInMilliseconds)
			{
				ServerLocalTimeInMilliseconds = thriftGetServerLocalTimeResponse.ServerLocalTimeInMilliseconds;
			}
		}
	}
}
