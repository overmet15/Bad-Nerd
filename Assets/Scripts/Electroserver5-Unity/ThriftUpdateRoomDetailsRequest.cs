using System;
using System.Text;
using Thrift.Protocol;

[Serializable]
public class ThriftUpdateRoomDetailsRequest : TBase
{
	[Serializable]
	public struct Isset
	{
		public bool zoneId;

		public bool roomId;

		public bool capacityUpdate;

		public bool capacity;

		public bool roomDescriptionUpdate;

		public bool roomDescription;

		public bool roomNameUpdate;

		public bool roomName;

		public bool passwordUpdate;

		public bool password;

		public bool hiddenUpdate;

		public bool hidden;
	}

	private int _zoneId;

	private int _roomId;

	private bool _capacityUpdate;

	private int _capacity;

	private bool _roomDescriptionUpdate;

	private string _roomDescription;

	private bool _roomNameUpdate;

	private string _roomName;

	private bool _passwordUpdate;

	private string _password;

	private bool _hiddenUpdate;

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

	public bool CapacityUpdate
	{
		get
		{
			return _capacityUpdate;
		}
		set
		{
			__isset.capacityUpdate = true;
			_capacityUpdate = value;
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

	public bool RoomDescriptionUpdate
	{
		get
		{
			return _roomDescriptionUpdate;
		}
		set
		{
			__isset.roomDescriptionUpdate = true;
			_roomDescriptionUpdate = value;
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

	public bool RoomNameUpdate
	{
		get
		{
			return _roomNameUpdate;
		}
		set
		{
			__isset.roomNameUpdate = true;
			_roomNameUpdate = value;
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

	public bool PasswordUpdate
	{
		get
		{
			return _passwordUpdate;
		}
		set
		{
			__isset.passwordUpdate = true;
			_passwordUpdate = value;
		}
	}

	public string Password
	{
		get
		{
			return _password;
		}
		set
		{
			__isset.password = true;
			_password = value;
		}
	}

	public bool HiddenUpdate
	{
		get
		{
			return _hiddenUpdate;
		}
		set
		{
			__isset.hiddenUpdate = true;
			_hiddenUpdate = value;
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
					CapacityUpdate = iprot.ReadBool();
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
					RoomDescriptionUpdate = iprot.ReadBool();
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
					RoomNameUpdate = iprot.ReadBool();
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
					PasswordUpdate = iprot.ReadBool();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 10:
				if (tField.Type == TType.String)
				{
					Password = iprot.ReadString();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 11:
				if (tField.Type == TType.Bool)
				{
					HiddenUpdate = iprot.ReadBool();
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
		TStruct struc = new TStruct("ThriftUpdateRoomDetailsRequest");
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
		if (__isset.capacityUpdate)
		{
			field.Name = "capacityUpdate";
			field.Type = TType.Bool;
			field.ID = 3;
			oprot.WriteFieldBegin(field);
			oprot.WriteBool(CapacityUpdate);
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
		if (__isset.roomDescriptionUpdate)
		{
			field.Name = "roomDescriptionUpdate";
			field.Type = TType.Bool;
			field.ID = 5;
			oprot.WriteFieldBegin(field);
			oprot.WriteBool(RoomDescriptionUpdate);
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
		if (__isset.roomNameUpdate)
		{
			field.Name = "roomNameUpdate";
			field.Type = TType.Bool;
			field.ID = 7;
			oprot.WriteFieldBegin(field);
			oprot.WriteBool(RoomNameUpdate);
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
		if (__isset.passwordUpdate)
		{
			field.Name = "passwordUpdate";
			field.Type = TType.Bool;
			field.ID = 9;
			oprot.WriteFieldBegin(field);
			oprot.WriteBool(PasswordUpdate);
			oprot.WriteFieldEnd();
		}
		if (Password != null && __isset.password)
		{
			field.Name = "password";
			field.Type = TType.String;
			field.ID = 10;
			oprot.WriteFieldBegin(field);
			oprot.WriteString(Password);
			oprot.WriteFieldEnd();
		}
		if (__isset.hiddenUpdate)
		{
			field.Name = "hiddenUpdate";
			field.Type = TType.Bool;
			field.ID = 11;
			oprot.WriteFieldBegin(field);
			oprot.WriteBool(HiddenUpdate);
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
		StringBuilder stringBuilder = new StringBuilder("ThriftUpdateRoomDetailsRequest(");
		stringBuilder.Append("ZoneId: ");
		stringBuilder.Append(ZoneId);
		stringBuilder.Append(",RoomId: ");
		stringBuilder.Append(RoomId);
		stringBuilder.Append(",CapacityUpdate: ");
		stringBuilder.Append(CapacityUpdate);
		stringBuilder.Append(",Capacity: ");
		stringBuilder.Append(Capacity);
		stringBuilder.Append(",RoomDescriptionUpdate: ");
		stringBuilder.Append(RoomDescriptionUpdate);
		stringBuilder.Append(",RoomDescription: ");
		stringBuilder.Append(RoomDescription);
		stringBuilder.Append(",RoomNameUpdate: ");
		stringBuilder.Append(RoomNameUpdate);
		stringBuilder.Append(",RoomName: ");
		stringBuilder.Append(RoomName);
		stringBuilder.Append(",PasswordUpdate: ");
		stringBuilder.Append(PasswordUpdate);
		stringBuilder.Append(",Password: ");
		stringBuilder.Append(Password);
		stringBuilder.Append(",HiddenUpdate: ");
		stringBuilder.Append(HiddenUpdate);
		stringBuilder.Append(",Hidden: ");
		stringBuilder.Append(Hidden);
		stringBuilder.Append(")");
		return stringBuilder.ToString();
	}
}
