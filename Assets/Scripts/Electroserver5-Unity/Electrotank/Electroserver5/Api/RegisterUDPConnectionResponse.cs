using System;
using Electrotank.Electroserver5.Api.Helper;
using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class RegisterUDPConnectionResponse : EsRequest
	{
		private bool Successful_;

		private int SessionKey_;

		private ErrorType? Error_;

		public bool Successful
		{
			get
			{
				return Successful_;
			}
			set
			{
				Successful_ = value;
				Successful_Set_ = true;
			}
		}

		private bool Successful_Set_ { get; set; }

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

		public ErrorType? Error
		{
			get
			{
				return Error_;
			}
			set
			{
				Error_ = value;
				Error_Set_ = true;
			}
		}

		private bool Error_Set_ { get; set; }

		public RegisterUDPConnectionResponse()
		{
			base.MessageType = MessageType.RegisterUDPConnectionResponse;
		}

		public RegisterUDPConnectionResponse(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftRegisterUDPConnectionResponse thriftRegisterUDPConnectionResponse = new ThriftRegisterUDPConnectionResponse();
			if (Successful_Set_)
			{
				bool successful = Successful;
				thriftRegisterUDPConnectionResponse.Successful = successful;
			}
			if (SessionKey_Set_)
			{
				int sessionKey = SessionKey;
				thriftRegisterUDPConnectionResponse.SessionKey = sessionKey;
			}
			if (Error_Set_ && Error.HasValue)
			{
				ThriftErrorType error = (ThriftErrorType)(object)ThriftUtil.EnumConvert(typeof(ThriftErrorType), (Enum)(object)Error);
				thriftRegisterUDPConnectionResponse.Error = error;
			}
			return thriftRegisterUDPConnectionResponse;
		}

		public override TBase NewThrift()
		{
			return new ThriftRegisterUDPConnectionResponse();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftRegisterUDPConnectionResponse thriftRegisterUDPConnectionResponse = (ThriftRegisterUDPConnectionResponse)t_;
			if (thriftRegisterUDPConnectionResponse.__isset.successful)
			{
				Successful = thriftRegisterUDPConnectionResponse.Successful;
			}
			if (thriftRegisterUDPConnectionResponse.__isset.sessionKey)
			{
				SessionKey = thriftRegisterUDPConnectionResponse.SessionKey;
			}
			if (thriftRegisterUDPConnectionResponse.__isset.error)
			{
				Error = (ErrorType)(object)ThriftUtil.EnumConvert(Nullable.GetUnderlyingType(typeof(ErrorType?)), thriftRegisterUDPConnectionResponse.Error);
			}
		}
	}
}
