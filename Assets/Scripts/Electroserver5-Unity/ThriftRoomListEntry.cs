using System;
using System.Text;
using Thrift.Protocol;

[Serializable]
public class ThriftRoomListEntry : TBase
{
	[Serializable]
	public struct Isset
	{
		public bool roomId;

		public bool zoneId;

		public bool roomName;

		public bool userCount;

		public bool roomDescription;

		public bool capacity;

		public bool hasPassword;
	}

	private int _roomId;

	private int _zoneId;

	private string _roomName;

	private int _userCount;

	private string _roomDescription;

	private int _capacity;

	private bool _hasPassword;

	public Isset __isset;

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

	public int UserCount
	{
		get
		{
			return _userCount;
		}
		set
		{
			__isset.userCount = true;
			_userCount = value;
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
					RoomId = iprot.ReadI32();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 2:
				if (tField.Type == TType.I32)
				{
					ZoneId = iprot.ReadI32();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 3:
				if (tField.Type == TType.String)
				{
					RoomName = iprot.ReadString();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 4:
				if (tField.Type == TType.I32)
				{
					UserCount = iprot.ReadI32();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 5:
				if (tField.Type == TType.String)
				{
					RoomDescription = iprot.ReadString();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 6:
				if (tField.Type == TType.I32)
				{
					Capacity = iprot.ReadI32();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 7:
				if (tField.Type == TType.Bool)
				{
					HasPassword = iprot.ReadBool();
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
		TStruct struc = new TStruct("ThriftRoomListEntry");
		oprot.WriteStructBegin(struc);
		TField field = default(TField);
		if (__isset.roomId)
		{
			field.Name = "roomId";
			field.Type = TType.I32;
			field.ID = 1;
			oprot.WriteFieldBegin(field);
			oprot.WriteI32(RoomId);
			oprot.WriteFieldEnd();
		}
		if (__isset.zoneId)
		{
			field.Name = "zoneId";
			field.Type = TType.I32;
			field.ID = 2;
			oprot.WriteFieldBegin(field);
			oprot.WriteI32(ZoneId);
			oprot.WriteFieldEnd();
		}
		if (RoomName != null && __isset.roomName)
		{
			field.Name = "roomName";
			field.Type = TType.String;
			field.ID = 3;
			oprot.WriteFieldBegin(field);
			oprot.WriteString(RoomName);
			oprot.WriteFieldEnd();
		}
		if (__isset.userCount)
		{
			field.Name = "userCount";
			field.Type = TType.I32;
			field.ID = 4;
			oprot.WriteFieldBegin(field);
			oprot.WriteI32(UserCount);
			oprot.WriteFieldEnd();
		}
		if (RoomDescription != null && __isset.roomDescription)
		{
			field.Name = "roomDescription";
			field.Type = TType.String;
			field.ID = 5;
			oprot.WriteFieldBegin(field);
			oprot.WriteString(RoomDescription);
			oprot.WriteFieldEnd();
		}
		if (__isset.capacity)
		{
			field.Name = "capacity";
			field.Type = TType.I32;
			field.ID = 6;
			oprot.WriteFieldBegin(field);
			oprot.WriteI32(Capacity);
			oprot.WriteFieldEnd();
		}
		if (__isset.hasPassword)
		{
			field.Name = "hasPassword";
			field.Type = TType.Bool;
			field.ID = 7;
			oprot.WriteFieldBegin(field);
			oprot.WriteBool(HasPassword);
			oprot.WriteFieldEnd();
		}
		oprot.WriteFieldStop();
		oprot.WriteStructEnd();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder("ThriftRoomListEntry(");
		stringBuilder.Append("RoomId: ");
		stringBuilder.Append(RoomId);
		stringBuilder.Append(",ZoneId: ");
		stringBuilder.Append(ZoneId);
		stringBuilder.Append(",RoomName: ");
		stringBuilder.Append(RoomName);
		stringBuilder.Append(",UserCount: ");
		stringBuilder.Append(UserCount);
		stringBuilder.Append(",RoomDescription: ");
		stringBuilder.Append(RoomDescription);
		stringBuilder.Append(",Capacity: ");
		stringBuilder.Append(Capacity);
		stringBuilder.Append(",HasPassword: ");
		stringBuilder.Append(HasPassword);
		stringBuilder.Append(")");
		return stringBuilder.ToString();
	}
}
