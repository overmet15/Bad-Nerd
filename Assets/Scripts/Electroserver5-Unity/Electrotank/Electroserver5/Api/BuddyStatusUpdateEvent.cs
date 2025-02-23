using System;
using Electrotank.Electroserver5.Api.Helper;
using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class BuddyStatusUpdateEvent : EsEvent
	{
		private string UserName_;

		private BuddyStatusUpdateAction? Action_;

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

		public BuddyStatusUpdateAction? Action
		{
			get
			{
				return Action_;
			}
			set
			{
				Action_ = value;
				Action_Set_ = true;
			}
		}

		private bool Action_Set_ { get; set; }

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

		public BuddyStatusUpdateEvent()
		{
			base.MessageType = MessageType.BuddyStatusUpdatedEvent;
		}

		public BuddyStatusUpdateEvent(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftBuddyStatusUpdateEvent thriftBuddyStatusUpdateEvent = new ThriftBuddyStatusUpdateEvent();
			if (UserName_Set_ && UserName != null)
			{
				string userName = UserName;
				thriftBuddyStatusUpdateEvent.UserName = userName;
			}
			if (Action_Set_ && Action.HasValue)
			{
				ThriftBuddyStatusUpdateAction action = (ThriftBuddyStatusUpdateAction)(object)ThriftUtil.EnumConvert(typeof(ThriftBuddyStatusUpdateAction), (Enum)(object)Action);
				thriftBuddyStatusUpdateEvent.Action = action;
			}
			if (EsObject_Set_ && EsObject != null)
			{
				ThriftFlattenedEsObject esObject = EsObjectCodec.FlattenEsObject(EsObject).ToThrift() as ThriftFlattenedEsObject;
				thriftBuddyStatusUpdateEvent.EsObject = esObject;
			}
			return thriftBuddyStatusUpdateEvent;
		}

		public override TBase NewThrift()
		{
			return new ThriftBuddyStatusUpdateEvent();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftBuddyStatusUpdateEvent thriftBuddyStatusUpdateEvent = (ThriftBuddyStatusUpdateEvent)t_;
			if (thriftBuddyStatusUpdateEvent.__isset.userName && thriftBuddyStatusUpdateEvent.UserName != null)
			{
				UserName = thriftBuddyStatusUpdateEvent.UserName;
			}
			if (thriftBuddyStatusUpdateEvent.__isset.action)
			{
				Action = (BuddyStatusUpdateAction)(object)ThriftUtil.EnumConvert(Nullable.GetUnderlyingType(typeof(BuddyStatusUpdateAction?)), thriftBuddyStatusUpdateEvent.Action);
			}
			if (thriftBuddyStatusUpdateEvent.__isset.esObject && thriftBuddyStatusUpdateEvent.EsObject != null)
			{
				EsObject = EsObjectCodec.UnflattenEsObject(new FlattenedEsObject(thriftBuddyStatusUpdateEvent.EsObject));
			}
		}
	}
}
