using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class PingRequest : EsRequest
	{
		private bool GlobalResponseRequested_;

		private int SessionKey_;

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

		public int SessionKey
		{
			get
			{
				return SessionKey_;
			}
			set
			{
				SessionKey_ = value;
				SessionKey_Set_ = true;
			}
		}

		private bool SessionKey_Set_ { get; set; }

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

		public PingRequest()
		{
			base.MessageType = MessageType.PingRequest;
		}

		public PingRequest(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftPingRequest thriftPingRequest = new ThriftPingRequest();
			if (GlobalResponseRequested_Set_)
			{
				bool globalResponseRequested = GlobalResponseRequested;
				thriftPingRequest.GlobalResponseRequested = globalResponseRequested;
			}
			if (SessionKey_Set_)
			{
				int sessionKey = SessionKey;
				thriftPingRequest.SessionKey = sessionKey;
			}
			if (PingRequestId_Set_)
			{
				int pingRequestId = PingRequestId;
				thriftPingRequest.PingRequestId = pingRequestId;
			}
			return thriftPingRequest;
		}

		public override TBase NewThrift()
		{
			return new ThriftPingRequest();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftPingRequest thriftPingRequest = (ThriftPingRequest)t_;
			if (thriftPingRequest.__isset.globalResponseRequested)
			{
				GlobalResponseRequested = thriftPingRequest.GlobalResponseRequested;
			}
			if (thriftPingRequest.__isset.sessionKey)
			{
				SessionKey = thriftPingRequest.SessionKey;
			}
			if (thriftPingRequest.__isset.pingRequestId)
			{
				PingRequestId = thriftPingRequest.PingRequestId;
			}
		}
	}
}
