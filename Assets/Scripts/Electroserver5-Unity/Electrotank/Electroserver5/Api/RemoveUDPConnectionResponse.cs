using System;
using Electrotank.Electroserver5.Api.Helper;
using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class RemoveUDPConnectionResponse : EsRequest
	{
		private bool Successful_;

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

		public RemoveUDPConnectionResponse()
		{
			base.MessageType = MessageType.RemoveUDPConnectionResponse;
		}

		public RemoveUDPConnectionResponse(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftRemoveUDPConnectionResponse thriftRemoveUDPConnectionResponse = new ThriftRemoveUDPConnectionResponse();
			if (Successful_Set_)
			{
				bool successful = Successful;
				thriftRemoveUDPConnectionResponse.Successful = successful;
			}
			if (Error_Set_ && Error.HasValue)
			{
				ThriftErrorType error = (ThriftErrorType)(object)ThriftUtil.EnumConvert(typeof(ThriftErrorType), (Enum)(object)Error);
				thriftRemoveUDPConnectionResponse.Error = error;
			}
			return thriftRemoveUDPConnectionResponse;
		}

		public override TBase NewThrift()
		{
			return new ThriftRemoveUDPConnectionResponse();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftRemoveUDPConnectionResponse thriftRemoveUDPConnectionResponse = (ThriftRemoveUDPConnectionResponse)t_;
			if (thriftRemoveUDPConnectionResponse.__isset.successful)
			{
				Successful = thriftRemoveUDPConnectionResponse.Successful;
			}
			if (thriftRemoveUDPConnectionResponse.__isset.error)
			{
				Error = (ErrorType)(object)ThriftUtil.EnumConvert(Nullable.GetUnderlyingType(typeof(ErrorType?)), thriftRemoveUDPConnectionResponse.Error);
			}
		}
	}
}
