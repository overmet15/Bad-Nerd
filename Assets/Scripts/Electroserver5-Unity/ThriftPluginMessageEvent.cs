using System;
using System.Text;
using Thrift.Protocol;

[Serializable]
public class ThriftPluginMessageEvent : TBase
{
	[Serializable]
	public struct Isset
	{
		public bool pluginName;

		public bool sentToRoom;

		public bool destinationZoneId;

		public bool destinationRoomId;

		public bool roomLevelPlugin;

		public bool originZoneId;

		public bool originRoomId;

		public bool parameters;
	}

	private string _pluginName;

	private bool _sentToRoom;

	private int _destinationZoneId;

	private int _destinationRoomId;

	private bool _roomLevelPlugin;

	private int _originZoneId;

	private int _originRoomId;

	private ThriftFlattenedEsObject _parameters;

	public Isset __isset;

	public string PluginName
	{
		get
		{
			return _pluginName;
		}
		set
		{
			__isset.pluginName = true;
			_pluginName = value;
		}
	}

	public bool SentToRoom
	{
		get
		{
			return _sentToRoom;
		}
		set
		{
			__isset.sentToRoom = true;
			_sentToRoom = value;
		}
	}

	public int DestinationZoneId
	{
		get
		{
			return _destinationZoneId;
		}
		set
		{
			__isset.destinationZoneId = true;
			_destinationZoneId = value;
		}
	}

	public int DestinationRoomId
	{
		get
		{
			return _destinationRoomId;
		}
		set
		{
			__isset.destinationRoomId = true;
			_destinationRoomId = value;
		}
	}

	public bool RoomLevelPlugin
	{
		get
		{
			return _roomLevelPlugin;
		}
		set
		{
			__isset.roomLevelPlugin = true;
			_roomLevelPlugin = value;
		}
	}

	public int OriginZoneId
	{
		get
		{
			return _originZoneId;
		}
		set
		{
			__isset.originZoneId = true;
			_originZoneId = value;
		}
	}

	public int OriginRoomId
	{
		get
		{
			return _originRoomId;
		}
		set
		{
			__isset.originRoomId = true;
			_originRoomId = value;
		}
	}

	public ThriftFlattenedEsObject Parameters
	{
		get
		{
			return _parameters;
		}
		set
		{
			__isset.parameters = true;
			_parameters = value;
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
					PluginName = iprot.ReadString();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 2:
				if (tField.Type == TType.Bool)
				{
					SentToRoom = iprot.ReadBool();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 3:
				if (tField.Type == TType.I32)
				{
					DestinationZoneId = iprot.ReadI32();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 4:
				if (tField.Type == TType.I32)
				{
					DestinationRoomId = iprot.ReadI32();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 5:
				if (tField.Type == TType.Bool)
				{
					RoomLevelPlugin = iprot.ReadBool();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 6:
				if (tField.Type == TType.I32)
				{
					OriginZoneId = iprot.ReadI32();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 7:
				if (tField.Type == TType.I32)
				{
					OriginRoomId = iprot.ReadI32();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 8:
				if (tField.Type == TType.Struct)
				{
					Parameters = new ThriftFlattenedEsObject();
					Parameters.Read(iprot);
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
		TStruct struc = new TStruct("ThriftPluginMessageEvent");
		oprot.WriteStructBegin(struc);
		TField field = default(TField);
		if (PluginName != null && __isset.pluginName)
		{
			field.Name = "pluginName";
			field.Type = TType.String;
			field.ID = 1;
			oprot.WriteFieldBegin(field);
			oprot.WriteString(PluginName);
			oprot.WriteFieldEnd();
		}
		if (__isset.sentToRoom)
		{
			field.Name = "sentToRoom";
			field.Type = TType.Bool;
			field.ID = 2;
			oprot.WriteFieldBegin(field);
			oprot.WriteBool(SentToRoom);
			oprot.WriteFieldEnd();
		}
		if (__isset.destinationZoneId)
		{
			field.Name = "destinationZoneId";
			field.Type = TType.I32;
			field.ID = 3;
			oprot.WriteFieldBegin(field);
			oprot.WriteI32(DestinationZoneId);
			oprot.WriteFieldEnd();
		}
		if (__isset.destinationRoomId)
		{
			field.Name = "destinationRoomId";
			field.Type = TType.I32;
			field.ID = 4;
			oprot.WriteFieldBegin(field);
			oprot.WriteI32(DestinationRoomId);
			oprot.WriteFieldEnd();
		}
		if (__isset.roomLevelPlugin)
		{
			field.Name = "roomLevelPlugin";
			field.Type = TType.Bool;
			field.ID = 5;
			oprot.WriteFieldBegin(field);
			oprot.WriteBool(RoomLevelPlugin);
			oprot.WriteFieldEnd();
		}
		if (__isset.originZoneId)
		{
			field.Name = "originZoneId";
			field.Type = TType.I32;
			field.ID = 6;
			oprot.WriteFieldBegin(field);
			oprot.WriteI32(OriginZoneId);
			oprot.WriteFieldEnd();
		}
		if (__isset.originRoomId)
		{
			field.Name = "originRoomId";
			field.Type = TType.I32;
			field.ID = 7;
			oprot.WriteFieldBegin(field);
			oprot.WriteI32(OriginRoomId);
			oprot.WriteFieldEnd();
		}
		if (Parameters != null && __isset.parameters)
		{
			field.Name = "parameters";
			field.Type = TType.Struct;
			field.ID = 8;
			oprot.WriteFieldBegin(field);
			Parameters.Write(oprot);
			oprot.WriteFieldEnd();
		}
		oprot.WriteFieldStop();
		oprot.WriteStructEnd();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder("ThriftPluginMessageEvent(");
		stringBuilder.Append("PluginName: ");
		stringBuilder.Append(PluginName);
		stringBuilder.Append(",SentToRoom: ");
		stringBuilder.Append(SentToRoom);
		stringBuilder.Append(",DestinationZoneId: ");
		stringBuilder.Append(DestinationZoneId);
		stringBuilder.Append(",DestinationRoomId: ");
		stringBuilder.Append(DestinationRoomId);
		stringBuilder.Append(",RoomLevelPlugin: ");
		stringBuilder.Append(RoomLevelPlugin);
		stringBuilder.Append(",OriginZoneId: ");
		stringBuilder.Append(OriginZoneId);
		stringBuilder.Append(",OriginRoomId: ");
		stringBuilder.Append(OriginRoomId);
		stringBuilder.Append(",Parameters: ");
		stringBuilder.Append((Parameters != null) ? Parameters.ToString() : "<null>");
		stringBuilder.Append(")");
		return stringBuilder.ToString();
	}
}
