using System;
using Electrotank.Electroserver5.Api.Helper;
using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class ConnectionAttemptResponse : EsResponse
	{
		private bool Successful_;

		private int ConnectionId_;

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

		public int ConnectionId
		{
			get
			{
				return ConnectionId_;
			}
			set
			{
				ConnectionId_ = value;
				ConnectionId_Set_ = true;
			}
		}

		private bool ConnectionId_Set_ { get; set; }

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

		public ConnectionAttemptResponse()
		{
			base.MessageType = MessageType.ConnectionAttemptResponse;
		}

		public ConnectionAttemptResponse(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftConnectionAttemptResponse thriftConnectionAttemptResponse = new ThriftConnectionAttemptResponse();
			if (Successful_Set_)
			{
				bool successful = Successful;
				thriftConnectionAttemptResponse.Successful = successful;
			}
			if (ConnectionId_Set_)
			{
				int connectionId = ConnectionId;
				thriftConnectionAttemptResponse.ConnectionId = connectionId;
			}
			if (Error_Set_ && Error.HasValue)
			{
				ThriftErrorType error = (ThriftErrorType)(object)ThriftUtil.EnumConvert(typeof(ThriftErrorType), (Enum)(object)Error);
				thriftConnectionAttemptResponse.Error = error;
			}
			return thriftConnectionAttemptResponse;
		}

		public override TBase NewThrift()
		{
			return new ThriftConnectionAttemptResponse();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftConnectionAttemptResponse thriftConnectionAttemptResponse = (ThriftConnectionAttemptResponse)t_;
			if (thriftConnectionAttemptResponse.__isset.successful)
			{
				Successful = thriftConnectionAttemptResponse.Successful;
			}
			if (thriftConnectionAttemptResponse.__isset.connectionId)
			{
				ConnectionId = thriftConnectionAttemptResponse.ConnectionId;
			}
			if (thriftConnectionAttemptResponse.__isset.error)
			{
				Error = (ErrorType)(object)ThriftUtil.EnumConvert(Nullable.GetUnderlyingType(typeof(ErrorType?)), thriftConnectionAttemptResponse.Error);
			}
		}
	}
}
