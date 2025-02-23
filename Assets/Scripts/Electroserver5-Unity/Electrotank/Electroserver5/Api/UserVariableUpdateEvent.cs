using System;
using Electrotank.Electroserver5.Api.Helper;
using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class UserVariableUpdateEvent : EsEvent
	{
		private string UserName_;

		private UserVariable Variable_;

		private UserVariableUpdateAction? Action_;

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

		public UserVariable Variable
		{
			get
			{
				return Variable_;
			}
			set
			{
				Variable_ = value;
				Variable_Set_ = true;
			}
		}

		private bool Variable_Set_ { get; set; }

		public UserVariableUpdateAction? Action
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

		public UserVariableUpdateEvent()
		{
			base.MessageType = MessageType.UserVariableUpdateEvent;
		}

		public UserVariableUpdateEvent(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftUserVariableUpdateEvent thriftUserVariableUpdateEvent = new ThriftUserVariableUpdateEvent();
			if (UserName_Set_ && UserName != null)
			{
				string userName = UserName;
				thriftUserVariableUpdateEvent.UserName = userName;
			}
			if (Variable_Set_ && Variable != null)
			{
				ThriftUserVariable variable = Variable.ToThrift() as ThriftUserVariable;
				thriftUserVariableUpdateEvent.Variable = variable;
			}
			if (Action_Set_ && Action.HasValue)
			{
				ThriftUserVariableUpdateAction action = (ThriftUserVariableUpdateAction)(object)ThriftUtil.EnumConvert(typeof(ThriftUserVariableUpdateAction), (Enum)(object)Action);
				thriftUserVariableUpdateEvent.Action = action;
			}
			return thriftUserVariableUpdateEvent;
		}

		public override TBase NewThrift()
		{
			return new ThriftUserVariableUpdateEvent();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftUserVariableUpdateEvent thriftUserVariableUpdateEvent = (ThriftUserVariableUpdateEvent)t_;
			if (thriftUserVariableUpdateEvent.__isset.userName && thriftUserVariableUpdateEvent.UserName != null)
			{
				UserName = thriftUserVariableUpdateEvent.UserName;
			}
			if (thriftUserVariableUpdateEvent.__isset.variable && thriftUserVariableUpdateEvent.Variable != null)
			{
				Variable = new UserVariable(thriftUserVariableUpdateEvent.Variable);
			}
			if (thriftUserVariableUpdateEvent.__isset.action)
			{
				Action = (UserVariableUpdateAction)(object)ThriftUtil.EnumConvert(Nullable.GetUnderlyingType(typeof(UserVariableUpdateAction?)), thriftUserVariableUpdateEvent.Action);
			}
		}
	}
}
