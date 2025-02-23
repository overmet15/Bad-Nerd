using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class ValidateAdditionalLoginRequest : EsRequest
	{
		private string Secret_;

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

		public ValidateAdditionalLoginRequest()
		{
			base.MessageType = MessageType.ValidateAdditionalLoginRequest;
		}

		public ValidateAdditionalLoginRequest(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftValidateAdditionalLoginRequest thriftValidateAdditionalLoginRequest = new ThriftValidateAdditionalLoginRequest();
			if (Secret_Set_ && Secret != null)
			{
				string secret = Secret;
				thriftValidateAdditionalLoginRequest.Secret = secret;
			}
			return thriftValidateAdditionalLoginRequest;
		}

		public override TBase NewThrift()
		{
			return new ThriftValidateAdditionalLoginRequest();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftValidateAdditionalLoginRequest thriftValidateAdditionalLoginRequest = (ThriftValidateAdditionalLoginRequest)t_;
			if (thriftValidateAdditionalLoginRequest.__isset.secret && thriftValidateAdditionalLoginRequest.Secret != null)
			{
				Secret = thriftValidateAdditionalLoginRequest.Secret;
			}
		}
	}
}
