using System;
using System.Text;
using Thrift.Protocol;

[Serializable]
public class ThriftRoomVariableUpdateEvent : TBase
{
	[Serializable]
	public struct Isset
	{
		public bool zoneId;

		public bool roomId;

		public bool name;

		public bool valueChanged;

		public bool value;

		public bool persistent;

		public bool lockStatusChanged;

		public bool locked;

		public bool action;
	}

	private int _zoneId;

	private int _roomId;

	private string _name;

	private bool _valueChanged;

	private ThriftFlattenedEsObject _value;

	private bool _persistent;

	private bool _lockStatusChanged;

	private bool _locked;

	private ThriftRoomVariableUpdateAction _action;

	public Isset __isset;

	public int ZoneId
	{
		get
		{
			return _zoneId;
		}
		set
		{
			__isset.zoneId = true;
			_zoneId = value;
		}
	}

	public int RoomId
	{
		get
		{
			return _roomId;
		}
		set
		{
			__isset.roomId = true;
			_roomId = value;
		}
	}

	public string Name
	{
		get
		{
			return _name;
		}
		set
		{
			__isset.name = true;
			_name = value;
		}
	}

	public bool ValueChanged
	{
		get
		{
			return _valueChanged;
		}
		set
		{
			__isset.valueChanged = true;
			_valueChanged = value;
		}
	}

	public ThriftFlattenedEsObject Value
	{
		get
		{
			return _value;
		}
		set
		{
			__isset.value = true;
			_value = value;
		}
	}

	public bool Persistent
	{
		get
		{
			return _persistent;
		}
		set
		{
			__isset.persistent = true;
			_persistent = value;
		}
	}

	public bool LockStatusChanged
	{
		get
		{
			return _lockStatusChanged;
		}
		set
		{
			__isset.lockStatusChanged = true;
			_lockStatusChanged = value;
		}
	}

	public bool Locked
	{
		get
		{
			return _locked;
		}
		set
		{
			__isset.locked = true;
			_locked = value;
		}
	}

	public ThriftRoomVariableUpdateAction Action
	{
		get
		{
			return _action;
		}
		set
		{
			__isset.action = true;
			_action = value;
		}
	}

	public void Read(TProtocol iprot)
	{
		iprot.ReadStructBegin();
		while (true)
		{
			TField tField = iprot.ReadFieldBegin();
			if (tField.Type == TType.Stop)
			{
				break;
			}
			switch (tField.ID)
			{
			case 1:
				if (tField.Type == TType.I32)
				{
					ZoneId = iprot.ReadI32();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 2:
				if (tField.Type == TType.I32)
				{
					RoomId = iprot.ReadI32();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 3:
				if (tField.Type == TType.String)
				{
					Name = iprot.ReadString();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 4:
				if (tField.Type == TType.Bool)
				{
					ValueChanged = iprot.ReadBool();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 5:
				if (tField.Type == TType.Struct)
				{
					Value = new ThriftFlattenedEsObject();
					Value.Read(iprot);
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 6:
				if (tField.Type == TType.Bool)
				{
					Persistent = iprot.ReadBool();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 7:
				if (tField.Type == TType.Bool)
				{
					LockStatusChanged = iprot.ReadBool();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 8:
				if (tField.Type == TType.Bool)
				{
					Locked = iprot.ReadBool();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 9:
				if (tField.Type == TType.I32)
				{
					Action = (ThriftRoomVariableUpdateAction)iprot.ReadI32();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			default:
				TProtocolUtil.Skip(iprot, tField.Type);
				break;
			}
			iprot.ReadFieldEnd();
		}
		iprot.ReadStructEnd();
	}

	public void Write(TProtocol oprot)
	{
		TStruct struc = new TStruct("ThriftRoomVariableUpdateEvent");
		oprot.WriteStructBegin(struc);
		TField field = default(TField);
		if (__isset.zoneId)
		{
			field.Name = "zoneId";
			field.Type = TType.I32;
			field.ID = 1;
			oprot.WriteFieldBegin(field);
			oprot.WriteI32(ZoneId);
			oprot.WriteFieldEnd();
		}
		if (__isset.roomId)
		{
			field.Name = "roomId";
			field.Type = TType.I32;
			field.ID = 2;
			oprot.WriteFieldBegin(field);
			oprot.WriteI32(RoomId);
			oprot.WriteFieldEnd();
		}
		if (Name != null && __isset.name)
		{
			field.Name = "name";
			field.Type = TType.String;
			field.ID = 3;
			oprot.WriteFieldBegin(field);
			oprot.WriteString(Name);
			oprot.WriteFieldEnd();
		}
		if (__isset.valueChanged)
		{
			field.Name = "valueChanged";
			field.Type = TType.Bool;
			field.ID = 4;
			oprot.WriteFieldBegin(field);
			oprot.WriteBool(ValueChanged);
			oprot.WriteFieldEnd();
		}
		if (Value != null && __isset.value)
		{
			field.Name = "value";
			field.Type = TType.Struct;
			field.ID = 5;
			oprot.WriteFieldBegin(field);
			Value.Write(oprot);
			oprot.WriteFieldEnd();
		}
		if (__isset.persistent)
		{
			field.Name = "persistent";
			field.Type = TType.Bool;
			field.ID = 6;
			oprot.WriteFieldBegin(field);
			oprot.WriteBool(Persistent);
			oprot.WriteFieldEnd();
		}
		if (__isset.lockStatusChanged)
		{
			field.Name = "lockStatusChanged";
			field.Type = TType.Bool;
			field.ID = 7;
			oprot.WriteFieldBegin(field);
			oprot.WriteBool(LockStatusChanged);
			oprot.WriteFieldEnd();
		}
		if (__isset.locked)
		{
			field.Name = "locked";
			field.Type = TType.Bool;
			field.ID = 8;
			oprot.WriteFieldBegin(field);
			oprot.WriteBool(Locked);
			oprot.WriteFieldEnd();
		}
		if (__isset.action)
		{
			field.Name = "action";
			field.Type = TType.I32;
			field.ID = 9;
			oprot.WriteFieldBegin(field);
			oprot.WriteI32((int)Action);
			oprot.WriteFieldEnd();
		}
		oprot.WriteFieldStop();
		oprot.WriteStructEnd();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder("ThriftRoomVariableUpdateEvent(");
		stringBuilder.Append("ZoneId: ");
		stringBuilder.Append(ZoneId);
		stringBuilder.Append(",RoomId: ");
		stringBuilder.Append(RoomId);
		stringBuilder.Append(",Name: ");
		stringBuilder.Append(Name);
		stringBuilder.Append(",ValueChanged: ");
		stringBuilder.Append(ValueChanged);
		stringBuilder.Append(",Value: ");
		stringBuilder.Append((Value != null) ? Value.ToString() : "<null>");
		stringBuilder.Append(",Persistent: ");
		stringBuilder.Append(Persistent);
		stringBuilder.Append(",LockStatusChanged: ");
		stringBuilder.Append(LockStatusChanged);
		stringBuilder.Append(",Locked: ");
		stringBuilder.Append(Locked);
		stringBuilder.Append(",Action: ");
		stringBuilder.Append(Action);
		stringBuilder.Append(")");
		return stringBuilder.ToString();
	}
}
