using System;
using System.Text;
using Thrift.Protocol;

[Serializable]
public class ThriftPluginListEntry : TBase
{
	[Serializable]
	public struct Isset
	{
		public bool extensionName;

		public bool pluginName;

		public bool pluginHandle;

		public bool pluginId;

		public bool parameters;
	}

	private string _extensionName;

	private string _pluginName;

	private string _pluginHandle;

	private int _pluginId;

	private ThriftFlattenedEsObject _parameters;

	public Isset __isset;

	public string ExtensionName
	{
		get
		{
			return _extensionName;
		}
		set
		{
			__isset.extensionName = true;
			_extensionName = value;
		}
	}

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

	public string PluginHandle
	{
		get
		{
			return _pluginHandle;
		}
		set
		{
			__isset.pluginHandle = true;
			_pluginHandle = value;
		}
	}

	public int PluginId
	{
		get
		{
			return _pluginId;
		}
		set
		{
			__isset.pluginId = true;
			_pluginId = value;
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
					ExtensionName = iprot.ReadString();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 2:
				if (tField.Type == TType.String)
				{
					PluginName = iprot.ReadString();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 3:
				if (tField.Type == TType.String)
				{
					PluginHandle = iprot.ReadString();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 4:
				if (tField.Type == TType.I32)
				{
					PluginId = iprot.ReadI32();
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
		TStruct struc = new TStruct("ThriftPluginListEntry");
		oprot.WriteStructBegin(struc);
		TField field = default(TField);
		if (ExtensionName != null && __isset.extensionName)
		{
			field.Name = "extensionName";
			field.Type = TType.String;
			field.ID = 1;
			oprot.WriteFieldBegin(field);
			oprot.WriteString(ExtensionName);
			oprot.WriteFieldEnd();
		}
		if (PluginName != null && __isset.pluginName)
		{
			field.Name = "pluginName";
			field.Type = TType.String;
			field.ID = 2;
			oprot.WriteFieldBegin(field);
			oprot.WriteString(PluginName);
			oprot.WriteFieldEnd();
		}
		if (PluginHandle != null && __isset.pluginHandle)
		{
			field.Name = "pluginHandle";
			field.Type = TType.String;
			field.ID = 3;
			oprot.WriteFieldBegin(field);
			oprot.WriteString(PluginHandle);
			oprot.WriteFieldEnd();
		}
		if (__isset.pluginId)
		{
			field.Name = "pluginId";
			field.Type = TType.I32;
			field.ID = 4;
			oprot.WriteFieldBegin(field);
			oprot.WriteI32(PluginId);
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
		StringBuilder stringBuilder = new StringBuilder("ThriftPluginListEntry(");
		stringBuilder.Append("ExtensionName: ");
		stringBuilder.Append(ExtensionName);
		stringBuilder.Append(",PluginName: ");
		stringBuilder.Append(PluginName);
		stringBuilder.Append(",PluginHandle: ");
		stringBuilder.Append(PluginHandle);
		stringBuilder.Append(",PluginId: ");
		stringBuilder.Append(PluginId);
		stringBuilder.Append(",Parameters: ");
		stringBuilder.Append((Parameters != null) ? Parameters.ToString() : "<null>");
		stringBuilder.Append(")");
		return stringBuilder.ToString();
	}
}
