using Electrotank.Electroserver5.Api.Helper;
using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class PrivateMessageEvent : EsEvent
	{
		private string UserName_;

		private string Message_;

		private EsObject EsObject_;

		public string UserName
		{
			get
			{
				return UserName_;
			}
			set
			{
				UserName_ = value;
				UserName_Set_ = true;
			}
		}

		private bool UserName_Set_ { get; set; }

		public string Message
		{
			get
			{
				return Message_;
			}
			set
			{
				Message_ = value;
				Message_Set_ = true;
			}
		}

		private bool Message_Set_ { get; set; }

		public EsObject EsObject
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

		public PrivateMessageEvent()
		{
			base.MessageType = MessageType.PrivateMessageEvent;
		}

		public PrivateMessageEvent(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftPrivateMessageEvent thriftPrivateMessageEvent = new ThriftPrivateMessageEvent();
			if (UserName_Set_ && UserName != null)
			{
				string userName = UserName;
				thriftPrivateMessageEvent.UserName = userName;
			}
			if (Message_Set_ && Message != null)
			{
				string message = Message;
				thriftPrivateMessageEvent.Message = message;
			}
			if (EsObject_Set_ && EsObject != null)
			{
				ThriftFlattenedEsObject esObject = EsObjectCodec.FlattenEsObject(EsObject).ToThrift() as ThriftFlattenedEsObject;
				thriftPrivateMessageEvent.EsObject = esObject;
			}
			return thriftPrivateMessageEvent;
		}

		public override TBase NewThrift()
		{
			return new ThriftPrivateMessageEvent();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftPrivateMessageEvent thriftPrivateMessageEvent = (ThriftPrivateMessageEvent)t_;
			if (thriftPrivateMessageEvent.__isset.userName && thriftPrivateMessageEvent.UserName != null)
			{
				UserName = thriftPrivateMessageEvent.UserName;
			}
			if (thriftPrivateMessageEvent.__isset.message && thriftPrivateMessageEvent.Message != null)
			{
				Message = thriftPrivateMessageEvent.Message;
			}
			if (thriftPrivateMessageEvent.__isset.esObject && thriftPrivateMessageEvent.EsObject != null)
			{
				EsObject = EsObjectCodec.UnflattenEsObject(new FlattenedEsObject(thriftPrivateMessageEvent.EsObject));
			}
		}
	}
}
