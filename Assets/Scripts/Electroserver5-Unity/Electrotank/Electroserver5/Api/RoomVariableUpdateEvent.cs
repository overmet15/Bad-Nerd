using System;
using Electrotank.Electroserver5.Api.Helper;
using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class RoomVariableUpdateEvent : EsEvent
	{
		private int ZoneId_;

		private int RoomId_;

		private string Name_;

		private bool ValueChanged_;

		private EsObject Value_;

		private bool Persistent_;

		private bool LockStatusChanged_;

		private bool Locked_;

		private RoomVariableUpdateAction? Action_;

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

		public string Name
		{
			get
			{
				return Name_;
			}
			set
			{
				Name_ = value;
				Name_Set_ = true;
			}
		}

		private bool Name_Set_ { get; set; }

		public bool ValueChanged
		{
			get
			{
				return ValueChanged_;
			}
			set
			{
				ValueChanged_ = value;
				ValueChanged_Set_ = true;
			}
		}

		private bool ValueChanged_Set_ { get; set; }

		public EsObject Value
		{
			get
			{
				return Value_;
			}
			set
			{
				Value_ = value;
				Value_Set_ = true;
			}
		}

		private bool Value_Set_ { get; set; }

		public bool Persistent
		{
			get
			{
				return Persistent_;
			}
			set
			{
				Persistent_ = value;
				Persistent_Set_ = true;
			}
		}

		private bool Persistent_Set_ { get; set; }

		public bool LockStatusChanged
		{
			get
			{
				return LockStatusChanged_;
			}
			set
			{
				LockStatusChanged_ = value;
				LockStatusChanged_Set_ = true;
			}
		}

		private bool LockStatusChanged_Set_ { get; set; }

		public bool Locked
		{
			get
			{
				return Locked_;
			}
			set
			{
				Locked_ = value;
				Locked_Set_ = true;
			}
		}

		private bool Locked_Set_ { get; set; }

		public RoomVariableUpdateAction? Action
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

		public RoomVariableUpdateEvent()
		{
			base.MessageType = MessageType.RoomVariableUpdateEvent;
		}

		public RoomVariableUpdateEvent(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftRoomVariableUpdateEvent thriftRoomVariableUpdateEvent = new ThriftRoomVariableUpdateEvent();
			if (ZoneId_Set_)
			{
				int zoneId = ZoneId;
				thriftRoomVariableUpdateEvent.ZoneId = zoneId;
			}
			if (RoomId_Set_)
			{
				int roomId = RoomId;
				thriftRoomVariableUpdateEvent.RoomId = roomId;
			}
			if (Name_Set_ && Name != null)
			{
				string name = Name;
				thriftRoomVariableUpdateEvent.Name = name;
			}
			if (ValueChanged_Set_)
			{
				bool valueChanged = ValueChanged;
				thriftRoomVariableUpdateEvent.ValueChanged = valueChanged;
			}
			if (Value_Set_ && Value != null)
			{
				ThriftFlattenedEsObject value = EsObjectCodec.FlattenEsObject(Value).ToThrift() as ThriftFlattenedEsObject;
				thriftRoomVariableUpdateEvent.Value = value;
			}
			if (Persistent_Set_)
			{
				bool persistent = Persistent;
				thriftRoomVariableUpdateEvent.Persistent = persistent;
			}
			if (LockStatusChanged_Set_)
			{
				bool lockStatusChanged = LockStatusChanged;
				thriftRoomVariableUpdateEvent.LockStatusChanged = lockStatusChanged;
			}
			if (Locked_Set_)
			{
				bool locked = Locked;
				thriftRoomVariableUpdateEvent.Locked = locked;
			}
			if (Action_Set_ && Action.HasValue)
			{
				ThriftRoomVariableUpdateAction action = (ThriftRoomVariableUpdateAction)(object)ThriftUtil.EnumConvert(typeof(ThriftRoomVariableUpdateAction), (Enum)(object)Action);
				thriftRoomVariableUpdateEvent.Action = action;
			}
			return thriftRoomVariableUpdateEvent;
		}

		public override TBase NewThrift()
		{
			return new ThriftRoomVariableUpdateEvent();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftRoomVariableUpdateEvent thriftRoomVariableUpdateEvent = (ThriftRoomVariableUpdateEvent)t_;
			if (thriftRoomVariableUpdateEvent.__isset.zoneId)
			{
				ZoneId = thriftRoomVariableUpdateEvent.ZoneId;
			}
			if (thriftRoomVariableUpdateEvent.__isset.roomId)
			{
				RoomId = thriftRoomVariableUpdateEvent.RoomId;
			}
			if (thriftRoomVariableUpdateEvent.__isset.name && thriftRoomVariableUpdateEvent.Name != null)
			{
				Name = thriftRoomVariableUpdateEvent.Name;
			}
			if (thriftRoomVariableUpdateEvent.__isset.valueChanged)
			{
				ValueChanged = thriftRoomVariableUpdateEvent.ValueChanged;
			}
			if (thriftRoomVariableUpdateEvent.__isset.value && thriftRoomVariableUpdateEvent.Value != null)
			{
				Value = EsObjectCodec.UnflattenEsObject(new FlattenedEsObject(thriftRoomVariableUpdateEvent.Value));
			}
			if (thriftRoomVariableUpdateEvent.__isset.persistent)
			{
				Persistent = thriftRoomVariableUpdateEvent.Persistent;
			}
			if (thriftRoomVariableUpdateEvent.__isset.lockStatusChanged)
			{
				LockStatusChanged = thriftRoomVariableUpdateEvent.LockStatusChanged;
			}
			if (thriftRoomVariableUpdateEvent.__isset.locked)
			{
				Locked = thriftRoomVariableUpdateEvent.Locked;
			}
			if (thriftRoomVariableUpdateEvent.__isset.action)
			{
				Action = (RoomVariableUpdateAction)(object)ThriftUtil.EnumConvert(Nullable.GetUnderlyingType(typeof(RoomVariableUpdateAction?)), thriftRoomVariableUpdateEvent.Action);
			}
		}
	}
}
