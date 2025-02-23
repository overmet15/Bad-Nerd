using Electrotank.Electroserver5.Api.Helper;
using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class PublicMessageEvent : EsEvent
	{
		private string Message_;

		private string UserName_;

		private int ZoneId_;

		private int RoomId_;

		private EsObject EsObject_;

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

		public int ZoneId
		{
			get
			{
				return ZoneId_;
			}
			set
			{
				ZoneId_ = value;
				ZoneId_Set_ = true;
			}
		}

		private bool ZoneId_Set_ { get; set; }

		public int RoomId
		{
			get
			{
				return RoomId_;
			}
			set
			{
				RoomId_ = value;
				RoomId_Set_ = true;
			}
		}

		private bool RoomId_Set_ { get; set; }

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

		public PublicMessageEvent()
		{
			base.MessageType = MessageType.PublicMessageEvent;
		}

		public PublicMessageEvent(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftPublicMessageEvent thriftPublicMessageEvent = new ThriftPublicMessageEvent();
			if (Message_Set_ && Message != null)
			{
				string message = Message;
				thriftPublicMessageEvent.Message = message;
			}
			if (UserName_Set_ && UserName != null)
			{
				string userName = UserName;
				thriftPublicMessageEvent.UserName = userName;
			}
			if (ZoneId_Set_)
			{
				int zoneId = ZoneId;
				thriftPublicMessageEvent.ZoneId = zoneId;
			}
			if (RoomId_Set_)
			{
				int roomId = RoomId;
				thriftPublicMessageEvent.RoomId = roomId;
			}
			if (EsObject_Set_ && EsObject != null)
			{
				ThriftFlattenedEsObject esObject = EsObjectCodec.FlattenEsObject(EsObject).ToThrift() as ThriftFlattenedEsObject;
				thriftPublicMessageEvent.EsObject = esObject;
			}
			return thriftPublicMessageEvent;
		}

		public override TBase NewThrift()
		{
			return new ThriftPublicMessageEvent();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftPublicMessageEvent thriftPublicMessageEvent = (ThriftPublicMessageEvent)t_;
			if (thriftPublicMessageEvent.__isset.message && thriftPublicMessageEvent.Message != null)
			{
				Message = thriftPublicMessageEvent.Message;
			}
			if (thriftPublicMessageEvent.__isset.userName && thriftPublicMessageEvent.UserName != null)
			{
				UserName = thriftPublicMessageEvent.UserName;
			}
			if (thriftPublicMessageEvent.__isset.zoneId)
			{
				ZoneId = thriftPublicMessageEvent.ZoneId;
			}
			if (thriftPublicMessageEvent.__isset.roomId)
			{
				RoomId = thriftPublicMessageEvent.RoomId;
			}
			if (thriftPublicMessageEvent.__isset.esObject && thriftPublicMessageEvent.EsObject != null)
			{
				EsObject = EsObjectCodec.UnflattenEsObject(new FlattenedEsObject(thriftPublicMessageEvent.EsObject));
			}
		}
	}
}
