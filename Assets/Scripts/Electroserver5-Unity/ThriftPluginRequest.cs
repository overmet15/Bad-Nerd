using System;
using System.Text;
using Thrift.Protocol;

[Serializable]
public class ThriftPluginRequest : TBase
{
	[Serializable]
	public struct Isset
	{
		public bool pluginName;

		public bool zoneId;

		public bool roomId;

		public bool sessionKey;

		public bool parameters;
	}

	private string _pluginName;

	private int _zoneId;

	private int _roomId;

	private int _sessionKey;

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

	public int SessionKey
	{
		get
		{
			return _sessionKey;
		}
		set
		{
			__isset.sessionKey = true;
			_sessionKey = value;
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
					SessionKey = iprot.ReadI32();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 5:
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
		TStruct struc = new TStruct("ThriftPluginRequest");
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
		if (__isset.zoneId)
		{
			field.Name = "zoneId";
			field.Type = TType.I32;
			field.ID = 2;
			oprot.WriteFieldBegin(field);
			oprot.WriteI32(ZoneId);
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
		if (__isset.sessionKey)
		{
			field.Name = "sessionKey";
			field.Type = TType.I32;
			field.ID = 4;
			oprot.WriteFieldBegin(field);
			oprot.WriteI32(SessionKey);
			oprot.WriteFieldEnd();
		}
		if (Parameters != null && __isset.parameters)
		{
			field.Name = "parameters";
			field.Type = TType.Struct;
			field.ID = 5;
			oprot.WriteFieldBegin(field);
			Parameters.Write(oprot);
			oprot.WriteFieldEnd();
		}
		oprot.WriteFieldStop();
		oprot.WriteStructEnd();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder("ThriftPluginRequest(");
		stringBuilder.Append("PluginName: ");
		stringBuilder.Append(PluginName);
		stringBuilder.Append(",ZoneId: ");
		stringBuilder.Append(ZoneId);
		stringBuilder.Append(",RoomId: ");
		stringBuilder.Append(RoomId);
		stringBuilder.Append(",SessionKey: ");
		stringBuilder.Append(SessionKey);
		stringBuilder.Append(",Parameters: ");
		stringBuilder.Append((Parameters != null) ? Parameters.ToString() : "<null>");
		stringBuilder.Append(")");
		return stringBuilder.ToString();
	}
}
