using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class PingResponse : EsRequest
	{
		private bool GlobalResponseRequested_;

		private int PingRequestId_;

		public bool GlobalResponseRequested
		{
			get
			{
				return GlobalResponseRequested_;
			}
			set
			{
				GlobalResponseRequested_ = value;
				GlobalResponseRequested_Set_ = true;
			}
		}

		private bool GlobalResponseRequested_Set_ { get; set; }

		public int PingRequestId
		{
			get
			{
				return PingRequestId_;
			}
			set
			{
				PingRequestId_ = value;
				PingRequestId_Set_ = true;
			}
		}

		private bool PingRequestId_Set_ { get; set; }

		public PingResponse()
		{
			base.MessageType = MessageType.PingResponse;
		}

		public PingResponse(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftPingResponse thriftPingResponse = new ThriftPingResponse();
			if (GlobalResponseRequested_Set_)
			{
				bool globalResponseRequested = GlobalResponseRequested;
				thriftPingResponse.GlobalResponseRequested = globalResponseRequested;
			}
			if (PingRequestId_Set_)
			{
				int pingRequestId = PingRequestId;
				thriftPingResponse.PingRequestId = pingRequestId;
			}
			return thriftPingResponse;
		}

		public override TBase NewThrift()
		{
			return new ThriftPingResponse();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftPingResponse thriftPingResponse = (ThriftPingResponse)t_;
			if (thriftPingResponse.__isset.globalResponseRequested)
			{
				GlobalResponseRequested = thriftPingResponse.GlobalResponseRequested;
			}
			if (thriftPingResponse.__isset.pingRequestId)
			{
				PingRequestId = thriftPingResponse.PingRequestId;
			}
		}
	}
}
