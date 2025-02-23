using System;
using System.Text;
using Thrift.Protocol;

[Serializable]
public class ThriftRegistryConnectToPreferredGatewayRequest : TBase
{
	[Serializable]
	public struct Isset
	{
		public bool zoneId;

		public bool host;

		public bool port;

		public bool protocolToUse;
	}

	private int _zoneId;

	private string _host;

	private int _port;

	private ThriftProtocol _protocolToUse;

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

	public string Host
	{
		get
		{
			return _host;
		}
		set
		{
			__isset.host = true;
			_host = value;
		}
	}

	public int Port
	{
		get
		{
			return _port;
		}
		set
		{
			__isset.port = true;
			_port = value;
		}
	}

	public ThriftProtocol ProtocolToUse
	{
		get
		{
			return _protocolToUse;
		}
		set
		{
			__isset.protocolToUse = true;
			_protocolToUse = value;
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
					Host = iprot.ReadString();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 3:
				if (tField.Type == TType.I32)
				{
					Port = iprot.ReadI32();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 4:
				if (tField.Type == TType.I32)
				{
					ProtocolToUse = (ThriftProtocol)iprot.ReadI32();
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
		TStruct struc = new TStruct("ThriftRegistryConnectToPreferredGatewayRequest");
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
		if (Host != null && __isset.host)
		{
			field.Name = "host";
			field.Type = TType.String;
			field.ID = 2;
			oprot.WriteFieldBegin(field);
			oprot.WriteString(Host);
			oprot.WriteFieldEnd();
		}
		if (__isset.port)
		{
			field.Name = "port";
			field.Type = TType.I32;
			field.ID = 3;
			oprot.WriteFieldBegin(field);
			oprot.WriteI32(Port);
			oprot.WriteFieldEnd();
		}
		if (__isset.protocolToUse)
		{
			field.Name = "protocolToUse";
			field.Type = TType.I32;
			field.ID = 4;
			oprot.WriteFieldBegin(field);
			oprot.WriteI32((int)ProtocolToUse);
			oprot.WriteFieldEnd();
		}
		oprot.WriteFieldStop();
		oprot.WriteStructEnd();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder("ThriftRegistryConnectToPreferredGatewayRequest(");
		stringBuilder.Append("ZoneId: ");
		stringBuilder.Append(ZoneId);
		stringBuilder.Append(",Host: ");
		stringBuilder.Append(Host);
		stringBuilder.Append(",Port: ");
		stringBuilder.Append(Port);
		stringBuilder.Append(",ProtocolToUse: ");
		stringBuilder.Append(ProtocolToUse);
		stringBuilder.Append(")");
		return stringBuilder.ToString();
	}
}
