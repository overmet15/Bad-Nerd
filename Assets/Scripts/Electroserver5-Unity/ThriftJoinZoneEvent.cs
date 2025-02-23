using System;
using System.Collections.Generic;
using System.Text;
using Thrift.Protocol;

[Serializable]
public class ThriftJoinZoneEvent : TBase
{
	[Serializable]
	public struct Isset
	{
		public bool zoneId;

		public bool zoneName;

		public bool rooms;
	}

	private int _zoneId;

	private string _zoneName;

	private List<ThriftRoomListEntry> _rooms;

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

	public List<ThriftRoomListEntry> Rooms
	{
		get
		{
			return _rooms;
		}
		set
		{
			__isset.rooms = true;
			_rooms = value;
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
				if (tField.Type == TType.String)
				{
					ZoneName = iprot.ReadString();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 3:
				if (tField.Type == TType.List)
				{
					Rooms = new List<ThriftRoomListEntry>();
					TList tList = iprot.ReadListBegin();
					for (int i = 0; i < tList.Count; i++)
					{
						ThriftRoomListEntry thriftRoomListEntry = new ThriftRoomListEntry();
						thriftRoomListEntry = new ThriftRoomListEntry();
						thriftRoomListEntry.Read(iprot);
						Rooms.Add(thriftRoomListEntry);
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
		TStruct struc = new TStruct("ThriftJoinZoneEvent");
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
		if (ZoneName != null && __isset.zoneName)
		{
			field.Name = "zoneName";
			field.Type = TType.String;
			field.ID = 2;
			oprot.WriteFieldBegin(field);
			oprot.WriteString(ZoneName);
			oprot.WriteFieldEnd();
		}
		if (Rooms != null && __isset.rooms)
		{
			field.Name = "rooms";
			field.Type = TType.List;
			field.ID = 3;
			oprot.WriteFieldBegin(field);
			oprot.WriteListBegin(new TList(TType.Struct, Rooms.Count));
			foreach (ThriftRoomListEntry room in Rooms)
			{
				room.Write(oprot);
				oprot.WriteListEnd();
			}
			oprot.WriteFieldEnd();
		}
		oprot.WriteFieldStop();
		oprot.WriteStructEnd();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder("ThriftJoinZoneEvent(");
		stringBuilder.Append("ZoneId: ");
		stringBuilder.Append(ZoneId);
		stringBuilder.Append(",ZoneName: ");
		stringBuilder.Append(ZoneName);
		stringBuilder.Append(",Rooms: ");
		stringBuilder.Append(Rooms);
		stringBuilder.Append(")");
		return stringBuilder.ToString();
	}
}
