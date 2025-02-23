using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class ValidateAdditionalLoginResponse : EsResponse
	{
		private bool Approved_;

		private string Secret_;

		public bool Approved
		{
			get
			{
				return Approved_;
			}
			set
			{
				Approved_ = value;
				Approved_Set_ = true;
			}
		}

		private bool Approved_Set_ { get; set; }

		public string Secret
		{
			get
			{
				return Secret_;
			}
			set
			{
				Secret_ = value;
				Secret_Set_ = true;
			}
		}

		private bool Secret_Set_ { get; set; }

		public ValidateAdditionalLoginResponse()
		{
			base.MessageType = MessageType.ValidateAdditionalLoginResponse;
		}

		public ValidateAdditionalLoginResponse(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftValidateAdditionalLoginResponse thriftValidateAdditionalLoginResponse = new ThriftValidateAdditionalLoginResponse();
			if (Approved_Set_)
			{
				bool approved = Approved;
				thriftValidateAdditionalLoginResponse.Approved = approved;
			}
			if (Secret_Set_ && Secret != null)
			{
				string secret = Secret;
				thriftValidateAdditionalLoginResponse.Secret = secret;
			}
			return thriftValidateAdditionalLoginResponse;
		}

		public override TBase NewThrift()
		{
			return new ThriftValidateAdditionalLoginResponse();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftValidateAdditionalLoginResponse thriftValidateAdditionalLoginResponse = (ThriftValidateAdditionalLoginResponse)t_;
			if (thriftValidateAdditionalLoginResponse.__isset.approved)
			{
				Approved = thriftValidateAdditionalLoginResponse.Approved;
			}
			if (thriftValidateAdditionalLoginResponse.__isset.secret && thriftValidateAdditionalLoginResponse.Secret != null)
			{
				Secret = thriftValidateAdditionalLoginResponse.Secret;
			}
		}
	}
}
