using System;
using Electrotank.Electroserver5.Api.Helper;
using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class GatewayKickUserRequest : EsRequest
	{
		private long ClientId_;

		private ErrorType? Error_;

		private EsObjectRO EsObject_;

		public long ClientId
		{
			get
			{
				return ClientId_;
			}
			set
			{
				ClientId_ = value;
				ClientId_Set_ = true;
			}
		}

		private bool ClientId_Set_ { get; set; }

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

		public EsObjectRO EsObject
		{
			get
			{
				return EsObject_;
			}
			set
			{
				EsObject_ = value;
				EsObject_Set_ = true;
			}
		}

		private bool EsObject_Set_ { get; set; }

		public GatewayKickUserRequest()
		{
			base.MessageType = MessageType.GatewayKickUserRequest;
		}

		public GatewayKickUserRequest(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftGatewayKickUserRequest thriftGatewayKickUserRequest = new ThriftGatewayKickUserRequest();
			if (ClientId_Set_)
			{
				long clientId = ClientId;
				thriftGatewayKickUserRequest.ClientId = clientId;
			}
			if (Error_Set_ && Error.HasValue)
			{
				ThriftErrorType error = (ThriftErrorType)(object)ThriftUtil.EnumConvert(typeof(ThriftErrorType), (Enum)(object)Error);
				thriftGatewayKickUserRequest.Error = error;
			}
			if (EsObject_Set_ && EsObject != null)
			{
				ThriftFlattenedEsObjectRO esObject = EsObjectCodec.FlattenEsObject(EsObject).ToThrift() as ThriftFlattenedEsObjectRO;
				thriftGatewayKickUserRequest.EsObject = esObject;
			}
			return thriftGatewayKickUserRequest;
		}

		public override TBase NewThrift()
		{
			return new ThriftGatewayKickUserRequest();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftGatewayKickUserRequest thriftGatewayKickUserRequest = (ThriftGatewayKickUserRequest)t_;
			if (thriftGatewayKickUserRequest.__isset.clientId)
			{
				ClientId = thriftGatewayKickUserRequest.ClientId;
			}
			if (thriftGatewayKickUserRequest.__isset.error)
			{
				Error = (ErrorType)(object)ThriftUtil.EnumConvert(Nullable.GetUnderlyingType(typeof(ErrorType?)), thriftGatewayKickUserRequest.Error);
			}
			if (thriftGatewayKickUserRequest.__isset.esObject && thriftGatewayKickUserRequest.EsObject != null)
			{
				EsObject = EsObjectCodec.UnflattenEsObjectRO(new FlattenedEsObjectRO(thriftGatewayKickUserRequest.EsObject));
			}
		}
	}
}
