using Electrotank.Electroserver5.Api.Helper;
using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class RoomVariable : EsEntity
	{
		private bool Persistent_;

		private string Name_;

		private EsObject Value_;

		private bool Locked_;

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

		public RoomVariable()
		{
		}

		public RoomVariable(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftRoomVariable thriftRoomVariable = new ThriftRoomVariable();
			if (Persistent_Set_)
			{
				bool persistent = Persistent;
				thriftRoomVariable.Persistent = persistent;
			}
			if (Name_Set_ && Name != null)
			{
				string name = Name;
				thriftRoomVariable.Name = name;
			}
			if (Value_Set_ && Value != null)
			{
				ThriftFlattenedEsObject value = EsObjectCodec.FlattenEsObject(Value).ToThrift() as ThriftFlattenedEsObject;
				thriftRoomVariable.Value = value;
			}
			if (Locked_Set_)
			{
				bool locked = Locked;
				thriftRoomVariable.Locked = locked;
			}
			return thriftRoomVariable;
		}

		public override TBase NewThrift()
		{
			return new ThriftRoomVariable();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftRoomVariable thriftRoomVariable = (ThriftRoomVariable)t_;
			if (thriftRoomVariable.__isset.persistent)
			{
				Persistent = thriftRoomVariable.Persistent;
			}
			if (thriftRoomVariable.__isset.name && thriftRoomVariable.Name != null)
			{
				Name = thriftRoomVariable.Name;
			}
			if (thriftRoomVariable.__isset.value && thriftRoomVariable.Value != null)
			{
				Value = EsObjectCodec.UnflattenEsObject(new FlattenedEsObject(thriftRoomVariable.Value));
			}
			if (thriftRoomVariable.__isset.locked)
			{
				Locked = thriftRoomVariable.Locked;
			}
		}
	}
}
