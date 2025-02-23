using System;
using System.Collections.Generic;
using System.Text;
using Thrift.Protocol;

[Serializable]
public class ThriftGetUsersInRoomResponse : TBase
{
	[Serializable]
	public struct Isset
	{
		public bool zoneId;

		public bool roomId;

		public bool users;
	}

	private int _zoneId;

	private int _roomId;

	private List<ThriftUserListEntry> _users;

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
				if (tField.Type == TType.List)
				{
					Users = new List<ThriftUserListEntry>();
					TList tList = iprot.ReadListBegin();
					for (int i = 0; i < tList.Count; i++)
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
		TStruct struc = new TStruct("ThriftGetUsersInRoomResponse");
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
		if (Users != null && __isset.users)
		{
			field.Name = "users";
			field.Type = TType.List;
			field.ID = 3;
			oprot.WriteFieldBegin(field);
			oprot.WriteListBegin(new TList(TType.Struct, Users.Count));
			foreach (ThriftUserListEntry user in Users)
			{
				user.Write(oprot);
				oprot.WriteListEnd();
			}
			oprot.WriteFieldEnd();
		}
		oprot.WriteFieldStop();
		oprot.WriteStructEnd();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder("ThriftGetUsersInRoomResponse(");
		stringBuilder.Append("ZoneId: ");
		stringBuilder.Append(ZoneId);
		stringBuilder.Append(",RoomId: ");
		stringBuilder.Append(RoomId);
		stringBuilder.Append(",Users: ");
		stringBuilder.Append(Users);
		stringBuilder.Append(")");
		return stringBuilder.ToString();
	}
}
