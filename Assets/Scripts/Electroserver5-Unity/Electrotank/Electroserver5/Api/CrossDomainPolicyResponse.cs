using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class CrossDomainPolicyResponse : EsResponse
	{
		private bool CustomFileEnabled_;

		private string CustomFileContents_;

		private int Port_;

		public bool CustomFileEnabled
		{
			get
			{
				return CustomFileEnabled_;
			}
			set
			{
				CustomFileEnabled_ = value;
				CustomFileEnabled_Set_ = true;
			}
		}

		private bool CustomFileEnabled_Set_ { get; set; }

		public string CustomFileContents
		{
			get
			{
				return CustomFileContents_;
			}
			set
			{
				CustomFileContents_ = value;
				CustomFileContents_Set_ = true;
			}
		}

		private bool CustomFileContents_Set_ { get; set; }

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

		public CrossDomainPolicyResponse()
		{
			base.MessageType = MessageType.CrossDomainResponse;
		}

		public CrossDomainPolicyResponse(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftCrossDomainPolicyResponse thriftCrossDomainPolicyResponse = new ThriftCrossDomainPolicyResponse();
			if (CustomFileEnabled_Set_)
			{
				bool customFileEnabled = CustomFileEnabled;
				thriftCrossDomainPolicyResponse.CustomFileEnabled = customFileEnabled;
			}
			if (CustomFileContents_Set_ && CustomFileContents != null)
			{
				string customFileContents = CustomFileContents;
				thriftCrossDomainPolicyResponse.CustomFileContents = customFileContents;
			}
			if (Port_Set_)
			{
				int port = Port;
				thriftCrossDomainPolicyResponse.Port = port;
			}
			return thriftCrossDomainPolicyResponse;
		}

		public override TBase NewThrift()
		{
			return new ThriftCrossDomainPolicyResponse();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftCrossDomainPolicyResponse thriftCrossDomainPolicyResponse = (ThriftCrossDomainPolicyResponse)t_;
			if (thriftCrossDomainPolicyResponse.__isset.customFileEnabled)
			{
				CustomFileEnabled = thriftCrossDomainPolicyResponse.CustomFileEnabled;
			}
			if (thriftCrossDomainPolicyResponse.__isset.customFileContents && thriftCrossDomainPolicyResponse.CustomFileContents != null)
			{
				CustomFileContents = thriftCrossDomainPolicyResponse.CustomFileContents;
			}
			if (thriftCrossDomainPolicyResponse.__isset.port)
			{
				Port = thriftCrossDomainPolicyResponse.Port;
			}
		}
	}
}
