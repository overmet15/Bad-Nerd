using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class RegisterUDPConnectionRequest : EsRequest
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

		public RegisterUDPConnectionRequest()
		{
			base.MessageType = MessageType.RegisterUDPConnectionRequest;
		}

		public RegisterUDPConnectionRequest(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftRegisterUDPConnectionRequest thriftRegisterUDPConnectionRequest = new ThriftRegisterUDPConnectionRequest();
			if (Port_Set_)
			{
				int port = Port;
				thriftRegisterUDPConnectionRequest.Port = port;
			}
			return thriftRegisterUDPConnectionRequest;
		}

		public override TBase NewThrift()
		{
			return new ThriftRegisterUDPConnectionRequest();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftRegisterUDPConnectionRequest thriftRegisterUDPConnectionRequest = (ThriftRegisterUDPConnectionRequest)t_;
			if (thriftRegisterUDPConnectionRequest.__isset.port)
			{
				Port = thriftRegisterUDPConnectionRequest.Port;
			}
		}
	}
}
