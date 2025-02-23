using System;
using Electrotank.Electroserver5.Api.Helper;
using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class GenericErrorResponse : EsResponse
	{
		private MessageType? RequestMessageType_;

		private ErrorType? ErrorType_;

		private EsObject ExtraData_;

		public MessageType? RequestMessageType
		{
			get
			{
				return RequestMessageType_;
			}
			set
			{
				RequestMessageType_ = value;
				RequestMessageType_Set_ = true;
			}
		}

		private bool RequestMessageType_Set_ { get; set; }

		public ErrorType? ErrorType
		{
			get
			{
				return ErrorType_;
			}
			set
			{
				ErrorType_ = value;
				ErrorType_Set_ = true;
			}
		}

		private bool ErrorType_Set_ { get; set; }

		public EsObject ExtraData
		{
			get
			{
				return ExtraData_;
			}
			set
			{
				ExtraData_ = value;
				ExtraData_Set_ = true;
			}
		}

		private bool ExtraData_Set_ { get; set; }

		public GenericErrorResponse()
		{
			base.MessageType = MessageType.GenericErrorResponse;
		}

		public GenericErrorResponse(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftGenericErrorResponse thriftGenericErrorResponse = new ThriftGenericErrorResponse();
			if (RequestMessageType_Set_ && RequestMessageType.HasValue)
			{
				ThriftMessageType requestMessageType = (ThriftMessageType)(object)ThriftUtil.EnumConvert(typeof(ThriftMessageType), (Enum)(object)RequestMessageType);
				thriftGenericErrorResponse.RequestMessageType = requestMessageType;
			}
			if (ErrorType_Set_ && ErrorType.HasValue)
			{
				ThriftErrorType errorType = (ThriftErrorType)(object)ThriftUtil.EnumConvert(typeof(ThriftErrorType), (Enum)(object)ErrorType);
				thriftGenericErrorResponse.ErrorType = errorType;
			}
			if (ExtraData_Set_ && ExtraData != null)
			{
				ThriftFlattenedEsObject extraData = EsObjectCodec.FlattenEsObject(ExtraData).ToThrift() as ThriftFlattenedEsObject;
				thriftGenericErrorResponse.ExtraData = extraData;
			}
			return thriftGenericErrorResponse;
		}

		public override TBase NewThrift()
		{
			return new ThriftGenericErrorResponse();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftGenericErrorResponse thriftGenericErrorResponse = (ThriftGenericErrorResponse)t_;
			if (thriftGenericErrorResponse.__isset.requestMessageType)
			{
				RequestMessageType = (MessageType)(object)ThriftUtil.EnumConvert(Nullable.GetUnderlyingType(typeof(MessageType?)), thriftGenericErrorResponse.RequestMessageType);
			}
			if (thriftGenericErrorResponse.__isset.errorType)
			{
				ErrorType = (ErrorType)(object)ThriftUtil.EnumConvert(Nullable.GetUnderlyingType(typeof(ErrorType?)), thriftGenericErrorResponse.ErrorType);
			}
			if (thriftGenericErrorResponse.__isset.extraData && thriftGenericErrorResponse.ExtraData != null)
			{
				ExtraData = EsObjectCodec.UnflattenEsObject(new FlattenedEsObject(thriftGenericErrorResponse.ExtraData));
			}
		}
	}
}
