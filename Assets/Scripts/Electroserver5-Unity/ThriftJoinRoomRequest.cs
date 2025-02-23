using System;
using System.Text;
using Thrift.Protocol;

[Serializable]
public class ThriftJoinRoomRequest : TBase
{
	[Serializable]
	public struct Isset
	{
		public bool zoneName;

		public bool roomName;

		public bool zoneId;

		public bool roomId;

		public bool password;

		public bool receivingRoomListUpdates;

		public bool receivingRoomAttributeUpdates;

		public bool receivingUserListUpdates;

		public bool receivingUserVariableUpdates;

		public bool receivingRoomVariableUpdates;

		public bool receivingVideoEvents;
	}

	private string _zoneName;

	private string _roomName;

	private int _zoneId;

	private int _roomId;

	private string _password;

	private bool _receivingRoomListUpdates;

	private bool _receivingRoomAttributeUpdates;

	private bool _receivingUserListUpdates;

	private bool _receivingUserVariableUpdates;

	private bool _receivingRoomVariableUpdates;

	private bool _receivingVideoEvents;

	public Isset __isset;

	public string ZoneName
	{
		get
		{
			return _zoneName;
		}
		set
		{
			__isset.zoneName = true;
			_zoneName = value;
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

	public bool ReceivingRoomListUpdates
	{
		get
		{
			return _receivingRoomListUpdates;
		}
		set
		{
			__isset.receivingRoomListUpdates = true;
			_receivingRoomListUpdates = value;
		}
	}

	public bool ReceivingRoomAttributeUpdates
	{
		get
		{
			return _receivingRoomAttributeUpdates;
		}
		set
		{
			__isset.receivingRoomAttributeUpdates = true;
			_receivingRoomAttributeUpdates = value;
		}
	}

	public bool ReceivingUserListUpdates
	{
		get
		{
			return _receivingUserListUpdates;
		}
		set
		{
			__isset.receivingUserListUpdates = true;
			_receivingUserListUpdates = value;
		}
	}

	public bool ReceivingUserVariableUpdates
	{
		get
		{
			return _receivingUserVariableUpdates;
		}
		set
		{
			__isset.receivingUserVariableUpdates = true;
			_receivingUserVariableUpdates = value;
		}
	}

	public bool ReceivingRoomVariableUpdates
	{
		get
		{
			return _receivingRoomVariableUpdates;
		}
		set
		{
			__isset.receivingRoomVariableUpdates = true;
			_receivingRoomVariableUpdates = value;
		}
	}

	public bool ReceivingVideoEvents
	{
		get
		{
			return _receivingVideoEvents;
		}
		set
		{
			__isset.receivingVideoEvents = true;
			_receivingVideoEvents = value;
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
				if (tField.Type == TType.String)
				{
					ZoneName = iprot.ReadString();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 2:
				if (tField.Type == TType.String)
				{
					RoomName = iprot.ReadString();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 3:
				if (tField.Type == TType.I32)
				{
					ZoneId = iprot.ReadI32();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 4:
				if (tField.Type == TType.I32)
				{
					RoomId = iprot.ReadI32();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 5:
				if (tField.Type == TType.String)
				{
					Password = iprot.ReadString();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 6:
				if (tField.Type == TType.Bool)
				{
					ReceivingRoomListUpdates = iprot.ReadBool();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 7:
				if (tField.Type == TType.Bool)
				{
					ReceivingRoomAttributeUpdates = iprot.ReadBool();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 8:
				if (tField.Type == TType.Bool)
				{
					ReceivingUserListUpdates = iprot.ReadBool();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 9:
				if (tField.Type == TType.Bool)
				{
					ReceivingUserVariableUpdates = iprot.ReadBool();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 10:
				if (tField.Type == TType.Bool)
				{
					ReceivingRoomVariableUpdates = iprot.ReadBool();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 11:
				if (tField.Type == TType.Bool)
				{
					ReceivingVideoEvents = iprot.ReadBool();
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
		TStruct struc = new TStruct("ThriftJoinRoomRequest");
		oprot.WriteStructBegin(struc);
		TField field = default(TField);
		if (ZoneName != null && __isset.zoneName)
		{
			field.Name = "zoneName";
			field.Type = TType.String;
			field.ID = 1;
			oprot.WriteFieldBegin(field);
			oprot.WriteString(ZoneName);
			oprot.WriteFieldEnd();
		}
		if (RoomName != null && __isset.roomName)
		{
			field.Name = "roomName";
			field.Type = TType.String;
			field.ID = 2;
			oprot.WriteFieldBegin(field);
			oprot.WriteString(RoomName);
			oprot.WriteFieldEnd();
		}
		if (__isset.zoneId)
		{
			field.Name = "zoneId";
			field.Type = TType.I32;
			field.ID = 3;
			oprot.WriteFieldBegin(field);
			oprot.WriteI32(ZoneId);
			oprot.WriteFieldEnd();
		}
		if (__isset.roomId)
		{
			field.Name = "roomId";
			field.Type = TType.I32;
			field.ID = 4;
			oprot.WriteFieldBegin(field);
			oprot.WriteI32(RoomId);
			oprot.WriteFieldEnd();
		}
		if (Password != null && __isset.password)
		{
			field.Name = "password";
			field.Type = TType.String;
			field.ID = 5;
			oprot.WriteFieldBegin(field);
			oprot.WriteString(Password);
			oprot.WriteFieldEnd();
		}
		if (__isset.receivingRoomListUpdates)
		{
			field.Name = "receivingRoomListUpdates";
			field.Type = TType.Bool;
			field.ID = 6;
			oprot.WriteFieldBegin(field);
			oprot.WriteBool(ReceivingRoomListUpdates);
			oprot.WriteFieldEnd();
		}
		if (__isset.receivingRoomAttributeUpdates)
		{
			field.Name = "receivingRoomAttributeUpdates";
			field.Type = TType.Bool;
			field.ID = 7;
			oprot.WriteFieldBegin(field);
			oprot.WriteBool(ReceivingRoomAttributeUpdates);
			oprot.WriteFieldEnd();
		}
		if (__isset.receivingUserListUpdates)
		{
			field.Name = "receivingUserListUpdates";
			field.Type = TType.Bool;
			field.ID = 8;
			oprot.WriteFieldBegin(field);
			oprot.WriteBool(ReceivingUserListUpdates);
			oprot.WriteFieldEnd();
		}
		if (__isset.receivingUserVariableUpdates)
		{
			field.Name = "receivingUserVariableUpdates";
			field.Type = TType.Bool;
			field.ID = 9;
			oprot.WriteFieldBegin(field);
			oprot.WriteBool(ReceivingUserVariableUpdates);
			oprot.WriteFieldEnd();
		}
		if (__isset.receivingRoomVariableUpdates)
		{
			field.Name = "receivingRoomVariableUpdates";
			field.Type = TType.Bool;
			field.ID = 10;
			oprot.WriteFieldBegin(field);
			oprot.WriteBool(ReceivingRoomVariableUpdates);
			oprot.WriteFieldEnd();
		}
		if (__isset.receivingVideoEvents)
		{
			field.Name = "receivingVideoEvents";
			field.Type = TType.Bool;
			field.ID = 11;
			oprot.WriteFieldBegin(field);
			oprot.WriteBool(ReceivingVideoEvents);
			oprot.WriteFieldEnd();
		}
		oprot.WriteFieldStop();
		oprot.WriteStructEnd();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder("ThriftJoinRoomRequest(");
		stringBuilder.Append("ZoneName: ");
		stringBuilder.Append(ZoneName);
		stringBuilder.Append(",RoomName: ");
		stringBuilder.Append(RoomName);
		stringBuilder.Append(",ZoneId: ");
		stringBuilder.Append(ZoneId);
		stringBuilder.Append(",RoomId: ");
		stringBuilder.Append(RoomId);
		stringBuilder.Append(",Password: ");
		stringBuilder.Append(Password);
		stringBuilder.Append(",ReceivingRoomListUpdates: ");
		stringBuilder.Append(ReceivingRoomListUpdates);
		stringBuilder.Append(",ReceivingRoomAttributeUpdates: ");
		stringBuilder.Append(ReceivingRoomAttributeUpdates);
		stringBuilder.Append(",ReceivingUserListUpdates: ");
		stringBuilder.Append(ReceivingUserListUpdates);
		stringBuilder.Append(",ReceivingUserVariableUpdates: ");
		stringBuilder.Append(ReceivingUserVariableUpdates);
		stringBuilder.Append(",ReceivingRoomVariableUpdates: ");
		stringBuilder.Append(ReceivingRoomVariableUpdates);
		stringBuilder.Append(",ReceivingVideoEvents: ");
		stringBuilder.Append(ReceivingVideoEvents);
		stringBuilder.Append(")");
		return stringBuilder.ToString();
	}
}
