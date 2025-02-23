using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class RemoveUDPConnectionRequest : EsRequest
	{
		private int Port_;

		public int Port
		{
			get
			{
				return Port_;
			}
			set
			{
				Port_ = value;
				Port_Set_ = true;
			}
		}

		private bool Port_Set_ { get; set; }

		public RemoveUDPConnectionRequest()
		{
			base.MessageType = MessageType.RemoveUDPConnectionRequest;
		}

		public RemoveUDPConnectionRequest(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftRemoveUDPConnectionRequest thriftRemoveUDPConnectionRequest = new ThriftRemoveUDPConnectionRequest();
			if (Port_Set_)
			{
				int port = Port;
				thriftRemoveUDPConnectionRequest.Port = port;
			}
			return thriftRemoveUDPConnectionRequest;
		}

		public override TBase NewThrift()
		{
			return new ThriftRemoveUDPConnectionRequest();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftRemoveUDPConnectionRequest thriftRemoveUDPConnectionRequest = (ThriftRemoveUDPConnectionRequest)t_;
			if (thriftRemoveUDPConnectionRequest.__isset.port)
			{
				Port = thriftRemoveUDPConnectionRequest.Port;
			}
		}
	}
}
