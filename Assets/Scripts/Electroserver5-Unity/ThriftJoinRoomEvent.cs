using System;
using System.Collections.Generic;
using System.Text;
using Thrift.Protocol;

[Serializable]
public class ThriftJoinRoomEvent : TBase
{
	[Serializable]
	public struct Isset
	{
		public bool zoneId;

		public bool roomId;

		public bool roomName;

		public bool roomDescription;

		public bool hasPassword;

		public bool hidden;

		public bool capacity;

		public bool users;

		public bool roomVariables;
	}

	private int _zoneId;

	private int _roomId;

	private string _roomName;

	private string _roomDescription;

	private bool _hasPassword;

	private bool _hidden;

	private int _capacity;

	private List<ThriftUserListEntry> _users;

	private List<ThriftRoomVariable> _roomVariables;

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

	public List<ThriftUserListEntry> Users
	{
		get
		{
			return _users;
		}
		set
		{
			__isset.users = true;
			_users = value;
		}
	}

	public List<ThriftRoomVariable> RoomVariables
	{
		get
		{
			return _roomVariables;
		}
		set
		{
			__isset.roomVariables = true;
			_roomVariables = value;
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
					RoomName = iprot.ReadString();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 4:
				if (tField.Type == TType.String)
				{
					RoomDescription = iprot.ReadString();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 5:
				if (tField.Type == TType.Bool)
				{
					HasPassword = iprot.ReadBool();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 6:
				if (tField.Type == TType.Bool)
				{
					Hidden = iprot.ReadBool();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 7:
				if (tField.Type == TType.I32)
				{
					Capacity = iprot.ReadI32();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 8:
				if (tField.Type == TType.List)
				{
					Users = new List<ThriftUserListEntry>();
					TList tList2 = iprot.ReadListBegin();
					for (int j = 0; j < tList2.Count; j++)
					{
						ThriftUserListEntry thriftUserListEntry = new ThriftUserListEntry();
						thriftUserListEntry = new ThriftUserListEntry();
						thriftUserListEntry.Read(iprot);
						Users.Add(thriftUserListEntry);
					}
					iprot.ReadListEnd();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 9:
				if (tField.Type == TType.List)
				{
					RoomVariables = new List<ThriftRoomVariable>();
					TList tList = iprot.ReadListBegin();
					for (int i = 0; i < tList.Count; i++)
					{
						ThriftRoomVariable thriftRoomVariable = new ThriftRoomVariable();
						thriftRoomVariable = new ThriftRoomVariable();
						thriftRoomVariable.Read(iprot);
						RoomVariables.Add(thriftRoomVariable);
					}
					iprot.ReadListEnd();
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
		TStruct struc = new TStruct("ThriftJoinRoomEvent");
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
		if (RoomName != null && __isset.roomName)
		{
			field.Name = "roomName";
			field.Type = TType.String;
			field.ID = 3;
			oprot.WriteFieldBegin(field);
			oprot.WriteString(RoomName);
			oprot.WriteFieldEnd();
		}
		if (RoomDescription != null && __isset.roomDescription)
		{
			field.Name = "roomDescription";
			field.Type = TType.String;
			field.ID = 4;
			oprot.WriteFieldBegin(field);
			oprot.WriteString(RoomDescription);
			oprot.WriteFieldEnd();
		}
		if (__isset.hasPassword)
		{
			field.Name = "hasPassword";
			field.Type = TType.Bool;
			field.ID = 5;
			oprot.WriteFieldBegin(field);
			oprot.WriteBool(HasPassword);
			oprot.WriteFieldEnd();
		}
		if (__isset.hidden)
		{
			field.Name = "hidden";
			field.Type = TType.Bool;
			field.ID = 6;
			oprot.WriteFieldBegin(field);
			oprot.WriteBool(Hidden);
			oprot.WriteFieldEnd();
		}
		if (__isset.capacity)
		{
			field.Name = "capacity";
			field.Type = TType.I32;
			field.ID = 7;
			oprot.WriteFieldBegin(field);
			oprot.WriteI32(Capacity);
			oprot.WriteFieldEnd();
		}
		if (Users != null && __isset.users)
		{
			field.Name = "users";
			field.Type = TType.List;
			field.ID = 8;
			oprot.WriteFieldBegin(field);
			oprot.WriteListBegin(new TList(TType.Struct, Users.Count));
			foreach (ThriftUserListEntry user in Users)
			{
				user.Write(oprot);
				oprot.WriteListEnd();
			}
			oprot.WriteFieldEnd();
		}
		if (RoomVariables != null && __isset.roomVariables)
		{
			field.Name = "roomVariables";
			field.Type = TType.List;
			field.ID = 9;
			oprot.WriteFieldBegin(field);
			oprot.WriteListBegin(new TList(TType.Struct, RoomVariables.Count));
			foreach (ThriftRoomVariable roomVariable in RoomVariables)
			{
				roomVariable.Write(oprot);
				oprot.WriteListEnd();
			}
			oprot.WriteFieldEnd();
		}
		oprot.WriteFieldStop();
		oprot.WriteStructEnd();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder("ThriftJoinRoomEvent(");
		stringBuilder.Append("ZoneId: ");
		stringBuilder.Append(ZoneId);
		stringBuilder.Append(",RoomId: ");
		stringBuilder.Append(RoomId);
		stringBuilder.Append(",RoomName: ");
		stringBuilder.Append(RoomName);
		stringBuilder.Append(",RoomDescription: ");
		stringBuilder.Append(RoomDescription);
		stringBuilder.Append(",HasPassword: ");
		stringBuilder.Append(HasPassword);
		stringBuilder.Append(",Hidden: ");
		stringBuilder.Append(Hidden);
		stringBuilder.Append(",Capacity: ");
		stringBuilder.Append(Capacity);
		stringBuilder.Append(",Users: ");
		stringBuilder.Append(Users);
		stringBuilder.Append(",RoomVariables: ");
		stringBuilder.Append(RoomVariables);
		stringBuilder.Append(")");
		return stringBuilder.ToString();
	}
}
