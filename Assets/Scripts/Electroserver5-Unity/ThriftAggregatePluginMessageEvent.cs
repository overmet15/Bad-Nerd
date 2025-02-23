using System;
using System.Collections.Generic;
using System.Text;
using Thrift.Protocol;

[Serializable]
public class ThriftAggregatePluginMessageEvent : TBase
{
	[Serializable]
	public struct Isset
	{
		public bool pluginName;

		public bool esObjects;

		public bool originZoneId;

		public bool originRoomId;
	}

	private string _pluginName;

	private List<ThriftFlattenedEsObjectRO> _esObjects;

	private int _originZoneId;

	private int _originRoomId;

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

	public List<ThriftFlattenedEsObjectRO> EsObjects
	{
		get
		{
			return _esObjects;
		}
		set
		{
			__isset.esObjects = true;
			_esObjects = value;
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
				if (tField.Type == TType.List)
				{
					EsObjects = new List<ThriftFlattenedEsObjectRO>();
					TList tList = iprot.ReadListBegin();
					for (int i = 0; i < tList.Count; i++)
					{
						ThriftFlattenedEsObjectRO thriftFlattenedEsObjectRO = new ThriftFlattenedEsObjectRO();
						thriftFlattenedEsObjectRO = new ThriftFlattenedEsObjectRO();
						thriftFlattenedEsObjectRO.Read(iprot);
						EsObjects.Add(thriftFlattenedEsObjectRO);
					}
					iprot.ReadListEnd();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 3:
				if (tField.Type == TType.I32)
				{
					OriginZoneId = iprot.ReadI32();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 4:
				if (tField.Type == TType.I32)
				{
					OriginRoomId = iprot.ReadI32();
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
		TStruct struc = new TStruct("ThriftAggregatePluginMessageEvent");
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
		if (EsObjects != null && __isset.esObjects)
		{
			field.Name = "esObjects";
			field.Type = TType.List;
			field.ID = 2;
			oprot.WriteFieldBegin(field);
			oprot.WriteListBegin(new TList(TType.Struct, EsObjects.Count));
			foreach (ThriftFlattenedEsObjectRO esObject in EsObjects)
			{
				esObject.Write(oprot);
				oprot.WriteListEnd();
			}
			oprot.WriteFieldEnd();
		}
		if (__isset.originZoneId)
		{
			field.Name = "originZoneId";
			field.Type = TType.I32;
			field.ID = 3;
			oprot.WriteFieldBegin(field);
			oprot.WriteI32(OriginZoneId);
			oprot.WriteFieldEnd();
		}
		if (__isset.originRoomId)
		{
			field.Name = "originRoomId";
			field.Type = TType.I32;
			field.ID = 4;
			oprot.WriteFieldBegin(field);
			oprot.WriteI32(OriginRoomId);
			oprot.WriteFieldEnd();
		}
		oprot.WriteFieldStop();
		oprot.WriteStructEnd();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder("ThriftAggregatePluginMessageEvent(");
		stringBuilder.Append("PluginName: ");
		stringBuilder.Append(PluginName);
		stringBuilder.Append(",EsObjects: ");
		stringBuilder.Append(EsObjects);
		stringBuilder.Append(",OriginZoneId: ");
		stringBuilder.Append(OriginZoneId);
		stringBuilder.Append(",OriginRoomId: ");
		stringBuilder.Append(OriginRoomId);
		stringBuilder.Append(")");
		return stringBuilder.ToString();
	}
}
