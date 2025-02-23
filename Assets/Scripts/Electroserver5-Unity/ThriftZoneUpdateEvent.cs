using System;
using System.Text;
using Thrift.Protocol;

[Serializable]
public class ThriftZoneUpdateEvent : TBase
{
	[Serializable]
	public struct Isset
	{
		public bool zoneId;

		public bool action;

		public bool roomId;

		public bool roomCount;

		public bool roomListEntry;
	}

	private int _zoneId;

	private ThriftZoneUpdateAction _action;

	private int _roomId;

	private int _roomCount;

	private ThriftRoomListEntry _roomListEntry;

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

	public ThriftZoneUpdateAction Action
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

	public int RoomCount
	{
		get
		{
			return _roomCount;
		}
		set
		{
			__isset.roomCount = true;
			_roomCount = value;
		}
	}

	public ThriftRoomListEntry RoomListEntry
	{
		get
		{
			return _roomListEntry;
		}
		set
		{
			__isset.roomListEntry = true;
			_roomListEntry = value;
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
					Action = (ThriftZoneUpdateAction)iprot.ReadI32();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 3:
				if (tField.Type == TType.I32)
				{
					RoomId = iprot.ReadI32();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 4:
				if (tField.Type == TType.I32)
				{
					RoomCount = iprot.ReadI32();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 5:
				if (tField.Type == TType.Struct)
				{
					RoomListEntry = new ThriftRoomListEntry();
					RoomListEntry.Read(iprot);
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
		TStruct struc = new TStruct("ThriftZoneUpdateEvent");
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
		if (__isset.action)
		{
			field.Name = "action";
			field.Type = TType.I32;
			field.ID = 2;
			oprot.WriteFieldBegin(field);
			oprot.WriteI32((int)Action);
			oprot.WriteFieldEnd();
		}
		if (__isset.roomId)
		{
			field.Name = "roomId";
			field.Type = TType.I32;
			field.ID = 3;
			oprot.WriteFieldBegin(field);
			oprot.WriteI32(RoomId);
			oprot.WriteFieldEnd();
		}
		if (__isset.roomCount)
		{
			field.Name = "roomCount";
			field.Type = TType.I32;
			field.ID = 4;
			oprot.WriteFieldBegin(field);
			oprot.WriteI32(RoomCount);
			oprot.WriteFieldEnd();
		}
		if (RoomListEntry != null && __isset.roomListEntry)
		{
			field.Name = "roomListEntry";
			field.Type = TType.Struct;
			field.ID = 5;
			oprot.WriteFieldBegin(field);
			RoomListEntry.Write(oprot);
			oprot.WriteFieldEnd();
		}
		oprot.WriteFieldStop();
		oprot.WriteStructEnd();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder("ThriftZoneUpdateEvent(");
		stringBuilder.Append("ZoneId: ");
		stringBuilder.Append(ZoneId);
		stringBuilder.Append(",Action: ");
		stringBuilder.Append(Action);
		stringBuilder.Append(",RoomId: ");
		stringBuilder.Append(RoomId);
		stringBuilder.Append(",RoomCount: ");
		stringBuilder.Append(RoomCount);
		stringBuilder.Append(",RoomListEntry: ");
		stringBuilder.Append((RoomListEntry != null) ? RoomListEntry.ToString() : "<null>");
		stringBuilder.Append(")");
		return stringBuilder.ToString();
	}
}
