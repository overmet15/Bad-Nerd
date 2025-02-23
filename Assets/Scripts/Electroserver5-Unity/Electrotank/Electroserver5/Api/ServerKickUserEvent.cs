using System;
using Electrotank.Electroserver5.Api.Helper;
using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class ServerKickUserEvent : EsEvent
	{
		private ErrorType? Error_;

		private EsObjectRO EsObject_;

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

		public ServerKickUserEvent()
		{
			base.MessageType = MessageType.ServerKickUserEvent;
		}

		public ServerKickUserEvent(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftServerKickUserEvent thriftServerKickUserEvent = new ThriftServerKickUserEvent();
			if (Error_Set_ && Error.HasValue)
			{
				ThriftErrorType error = (ThriftErrorType)(object)ThriftUtil.EnumConvert(typeof(ThriftErrorType), (Enum)(object)Error);
				thriftServerKickUserEvent.Error = error;
			}
			if (EsObject_Set_ && EsObject != null)
			{
				ThriftFlattenedEsObjectRO esObject = EsObjectCodec.FlattenEsObject(EsObject).ToThrift() as ThriftFlattenedEsObjectRO;
				thriftServerKickUserEvent.EsObject = esObject;
			}
			return thriftServerKickUserEvent;
		}

		public override TBase NewThrift()
		{
			return new ThriftServerKickUserEvent();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftServerKickUserEvent thriftServerKickUserEvent = (ThriftServerKickUserEvent)t_;
			if (thriftServerKickUserEvent.__isset.error)
			{
				Error = (ErrorType)(object)ThriftUtil.EnumConvert(Nullable.GetUnderlyingType(typeof(ErrorType?)), thriftServerKickUserEvent.Error);
			}
			if (thriftServerKickUserEvent.__isset.esObject && thriftServerKickUserEvent.EsObject != null)
			{
				EsObject = EsObjectCodec.UnflattenEsObjectRO(new FlattenedEsObjectRO(thriftServerKickUserEvent.EsObject));
			}
		}
	}
}
