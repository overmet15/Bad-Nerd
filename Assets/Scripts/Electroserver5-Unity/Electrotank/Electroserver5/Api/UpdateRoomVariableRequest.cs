using Electrotank.Electroserver5.Api.Helper;
using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class UpdateRoomVariableRequest : EsRequest
	{
		private int ZoneId_;

		private int RoomId_;

		private string Name_;

		private bool ValueChanged_;

		private EsObject Value_;

		private bool LockStatusChanged_;

		private bool Locked_;

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

		public UpdateRoomVariableRequest()
		{
			base.MessageType = MessageType.UpdateRoomVariableRequest;
		}

		public UpdateRoomVariableRequest(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftUpdateRoomVariableRequest thriftUpdateRoomVariableRequest = new ThriftUpdateRoomVariableRequest();
			if (ZoneId_Set_)
			{
				int zoneId = ZoneId;
				thriftUpdateRoomVariableRequest.ZoneId = zoneId;
			}
			if (RoomId_Set_)
			{
				int roomId = RoomId;
				thriftUpdateRoomVariableRequest.RoomId = roomId;
			}
			if (Name_Set_ && Name != null)
			{
				string name = Name;
				thriftUpdateRoomVariableRequest.Name = name;
			}
			if (ValueChanged_Set_)
			{
				bool valueChanged = ValueChanged;
				thriftUpdateRoomVariableRequest.ValueChanged = valueChanged;
			}
			if (Value_Set_ && Value != null)
			{
				ThriftFlattenedEsObject value = EsObjectCodec.FlattenEsObject(Value).ToThrift() as ThriftFlattenedEsObject;
				thriftUpdateRoomVariableRequest.Value = value;
			}
			if (LockStatusChanged_Set_)
			{
				bool lockStatusChanged = LockStatusChanged;
				thriftUpdateRoomVariableRequest.LockStatusChanged = lockStatusChanged;
			}
			if (Locked_Set_)
			{
				bool locked = Locked;
				thriftUpdateRoomVariableRequest.Locked = locked;
			}
			return thriftUpdateRoomVariableRequest;
		}

		public override TBase NewThrift()
		{
			return new ThriftUpdateRoomVariableRequest();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftUpdateRoomVariableRequest thriftUpdateRoomVariableRequest = (ThriftUpdateRoomVariableRequest)t_;
			if (thriftUpdateRoomVariableRequest.__isset.zoneId)
			{
				ZoneId = thriftUpdateRoomVariableRequest.ZoneId;
			}
			if (thriftUpdateRoomVariableRequest.__isset.roomId)
			{
				RoomId = thriftUpdateRoomVariableRequest.RoomId;
			}
			if (thriftUpdateRoomVariableRequest.__isset.name && thriftUpdateRoomVariableRequest.Name != null)
			{
				Name = thriftUpdateRoomVariableRequest.Name;
			}
			if (thriftUpdateRoomVariableRequest.__isset.valueChanged)
			{
				ValueChanged = thriftUpdateRoomVariableRequest.ValueChanged;
			}
			if (thriftUpdateRoomVariableRequest.__isset.value && thriftUpdateRoomVariableRequest.Value != null)
			{
				Value = EsObjectCodec.UnflattenEsObject(new FlattenedEsObject(thriftUpdateRoomVariableRequest.Value));
			}
			if (thriftUpdateRoomVariableRequest.__isset.lockStatusChanged)
			{
				LockStatusChanged = thriftUpdateRoomVariableRequest.LockStatusChanged;
			}
			if (thriftUpdateRoomVariableRequest.__isset.locked)
			{
				Locked = thriftUpdateRoomVariableRequest.Locked;
			}
		}
	}
}
