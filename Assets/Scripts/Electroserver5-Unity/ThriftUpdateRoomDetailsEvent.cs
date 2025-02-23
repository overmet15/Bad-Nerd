using System;
using System.Text;
using Thrift.Protocol;

[Serializable]
public class ThriftUpdateRoomDetailsEvent : TBase
{
	[Serializable]
	public struct Isset
	{
		public bool zoneId;

		public bool roomId;

		public bool capacityUpdated;

		public bool capacity;

		public bool roomDescriptionUpdated;

		public bool roomDescription;

		public bool roomNameUpdated;

		public bool roomName;

		public bool hasPassword;

		public bool hasPasswordUpdated;

		public bool hiddenUpdated;

		public bool hidden;
	}

	private int _zoneId;

	private int _roomId;

	private bool _capacityUpdated;

	private int _capacity;

	private bool _roomDescriptionUpdated;

	private string _roomDescription;

	private bool _roomNameUpdated;

	private string _roomName;

	private bool _hasPassword;

	private bool _hasPasswordUpdated;

	private bool _hiddenUpdated;

	private bool _hidden;

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

	public bool CapacityUpdated
	{
		get
		{
			return _capacityUpdated;
		}
		set
		{
			__isset.capacityUpdated = true;
			_capacityUpdated = value;
		}
	}

	public int Capacity
	{
		get
		{
			return _capacity;
		}
		set
		{
			__isset.capacity = true;
			_capacity = value;
		}
	}

	public bool RoomDescriptionUpdated
	{
		get
		{
			return _roomDescriptionUpdated;
		}
		set
		{
			__isset.roomDescriptionUpdated = true;
			_roomDescriptionUpdated = value;
		}
	}

	public string RoomDescription
	{
		get
		{
			return _roomDescription;
		}
		set
		{
			__isset.roomDescription = true;
			_roomDescription = value;
		}
	}

	public bool RoomNameUpdated
	{
		get
		{
			return _roomNameUpdated;
		}
		set
		{
			__isset.roomNameUpdated = true;
			_roomNameUpdated = value;
		}
	}

	public string RoomName
	{
		get
		{
			return _roomName;
		}
		set
		{
			__isset.roomName = true;
			_roomName = value;
		}
	}

	public bool HasPassword
	{
		get
		{
			return _hasPassword;
		}
		set
		{
			__isset.hasPassword = true;
			_hasPassword = value;
		}
	}

	public bool HasPasswordUpdated
	{
		get
		{
			return _hasPasswordUpdated;
		}
		set
		{
			__isset.hasPasswordUpdated = true;
			_hasPasswordUpdated = value;
		}
	}

	public bool HiddenUpdated
	{
		get
		{
			return _hiddenUpdated;
		}
		set
		{
			__isset.hiddenUpdated = true;
			_hiddenUpdated = value;
		}
	}

	public bool Hidden
	{
		get
		{
			return _hidden;
		}
		set
		{
			__isset.hidden = true;
			_hidden = value;
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
				if (tField.Type == TType.Bool)
				{
					CapacityUpdated = iprot.ReadBool();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 4:
				if (tField.Type == TType.I32)
				{
					Capacity = iprot.ReadI32();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 5:
				if (tField.Type == TType.Bool)
				{
					RoomDescriptionUpdated = iprot.ReadBool();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 6:
				if (tField.Type == TType.String)
				{
					RoomDescription = iprot.ReadString();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 7:
				if (tField.Type == TType.Bool)
				{
					RoomNameUpdated = iprot.ReadBool();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 8:
				if (tField.Type == TType.String)
				{
					RoomName = iprot.ReadString();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 9:
				if (tField.Type == TType.Bool)
				{
					HasPassword = iprot.ReadBool();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 10:
				if (tField.Type == TType.Bool)
				{
					HasPasswordUpdated = iprot.ReadBool();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 11:
				if (tField.Type == TType.Bool)
				{
					HiddenUpdated = iprot.ReadBool();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 12:
				if (tField.Type == TType.Bool)
				{
					Hidden = iprot.ReadBool();
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
		TStruct struc = new TStruct("ThriftUpdateRoomDetailsEvent");
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
		if (__isset.capacityUpdated)
		{
			field.Name = "capacityUpdated";
			field.Type = TType.Bool;
			field.ID = 3;
			oprot.WriteFieldBegin(field);
			oprot.WriteBool(CapacityUpdated);
			oprot.WriteFieldEnd();
		}
		if (__isset.capacity)
		{
			field.Name = "capacity";
			field.Type = TType.I32;
			field.ID = 4;
			oprot.WriteFieldBegin(field);
			oprot.WriteI32(Capacity);
			oprot.WriteFieldEnd();
		}
		if (__isset.roomDescriptionUpdated)
		{
			field.Name = "roomDescriptionUpdated";
			field.Type = TType.Bool;
			field.ID = 5;
			oprot.WriteFieldBegin(field);
			oprot.WriteBool(RoomDescriptionUpdated);
			oprot.WriteFieldEnd();
		}
		if (RoomDescription != null && __isset.roomDescription)
		{
			field.Name = "roomDescription";
			field.Type = TType.String;
			field.ID = 6;
			oprot.WriteFieldBegin(field);
			oprot.WriteString(RoomDescription);
			oprot.WriteFieldEnd();
		}
		if (__isset.roomNameUpdated)
		{
			field.Name = "roomNameUpdated";
			field.Type = TType.Bool;
			field.ID = 7;
			oprot.WriteFieldBegin(field);
			oprot.WriteBool(RoomNameUpdated);
			oprot.WriteFieldEnd();
		}
		if (RoomName != null && __isset.roomName)
		{
			field.Name = "roomName";
			field.Type = TType.String;
			field.ID = 8;
			oprot.WriteFieldBegin(field);
			oprot.WriteString(RoomName);
			oprot.WriteFieldEnd();
		}
		if (__isset.hasPassword)
		{
			field.Name = "hasPassword";
			field.Type = TType.Bool;
			field.ID = 9;
			oprot.WriteFieldBegin(field);
			oprot.WriteBool(HasPassword);
			oprot.WriteFieldEnd();
		}
		if (__isset.hasPasswordUpdated)
		{
			field.Name = "hasPasswordUpdated";
			field.Type = TType.Bool;
			field.ID = 10;
			oprot.WriteFieldBegin(field);
			oprot.WriteBool(HasPasswordUpdated);
			oprot.WriteFieldEnd();
		}
		if (__isset.hiddenUpdated)
		{
			field.Name = "hiddenUpdated";
			field.Type = TType.Bool;
			field.ID = 11;
			oprot.WriteFieldBegin(field);
			oprot.WriteBool(HiddenUpdated);
			oprot.WriteFieldEnd();
		}
		if (__isset.hidden)
		{
			field.Name = "hidden";
			field.Type = TType.Bool;
			field.ID = 12;
			oprot.WriteFieldBegin(field);
			oprot.WriteBool(Hidden);
			oprot.WriteFieldEnd();
		}
		oprot.WriteFieldStop();
		oprot.WriteStructEnd();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder("ThriftUpdateRoomDetailsEvent(");
		stringBuilder.Append("ZoneId: ");
		stringBuilder.Append(ZoneId);
		stringBuilder.Append(",RoomId: ");
		stringBuilder.Append(RoomId);
		stringBuilder.Append(",CapacityUpdated: ");
		stringBuilder.Append(CapacityUpdated);
		stringBuilder.Append(",Capacity: ");
		stringBuilder.Append(Capacity);
		stringBuilder.Append(",RoomDescriptionUpdated: ");
		stringBuilder.Append(RoomDescriptionUpdated);
		stringBuilder.Append(",RoomDescription: ");
		stringBuilder.Append(RoomDescription);
		stringBuilder.Append(",RoomNameUpdated: ");
		stringBuilder.Append(RoomNameUpdated);
		stringBuilder.Append(",RoomName: ");
		stringBuilder.Append(RoomName);
		stringBuilder.Append(",HasPassword: ");
		stringBuilder.Append(HasPassword);
		stringBuilder.Append(",HasPasswordUpdated: ");
		stringBuilder.Append(HasPasswordUpdated);
		stringBuilder.Append(",HiddenUpdated: ");
		stringBuilder.Append(HiddenUpdated);
		stringBuilder.Append(",Hidden: ");
		stringBuilder.Append(Hidden);
		stringBuilder.Append(")");
		return stringBuilder.ToString();
	}
}
