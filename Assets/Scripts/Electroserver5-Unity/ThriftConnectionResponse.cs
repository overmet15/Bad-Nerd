using System;
using System.Text;
using Thrift.Protocol;

[Serializable]
public class ThriftConnectionResponse : TBase
{
	[Serializable]
	public struct Isset
	{
		public bool successful;

		public bool hashId;

		public bool error;

		public bool protocolConfiguration;

		public bool serverVersion;
	}

	private bool _successful;

	private int _hashId;

	private ThriftErrorType _error;

	private ThriftProtocolConfiguration _protocolConfiguration;

	private string _serverVersion;

	public Isset __isset;

	public bool Successful
	{
		get
		{
			return _successful;
		}
		set
		{
			__isset.successful = true;
			_successful = value;
		}
	}

	public int HashId
	{
		get
		{
			return _hashId;
		}
		set
		{
			__isset.hashId = true;
			_hashId = value;
		}
	}

	public ThriftErrorType Error
	{
		get
		{
			return _error;
		}
		set
		{
			__isset.error = true;
			_error = value;
		}
	}

	public ThriftProtocolConfiguration ProtocolConfiguration
	{
		get
		{
			return _protocolConfiguration;
		}
		set
		{
			__isset.protocolConfiguration = true;
			_protocolConfiguration = value;
		}
	}

	public string ServerVersion
	{
		get
		{
			return _serverVersion;
		}
		set
		{
			__isset.serverVersion = true;
			_serverVersion = value;
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
				if (tField.Type == TType.Bool)
				{
					Successful = iprot.ReadBool();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 2:
				if (tField.Type == TType.I32)
				{
					HashId = iprot.ReadI32();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 3:
				if (tField.Type == TType.I32)
				{
					Error = (ThriftErrorType)iprot.ReadI32();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 4:
				if (tField.Type == TType.Struct)
				{
					ProtocolConfiguration = new ThriftProtocolConfiguration();
					ProtocolConfiguration.Read(iprot);
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 5:
				if (tField.Type == TType.String)
				{
					ServerVersion = iprot.ReadString();
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
		TStruct struc = new TStruct("ThriftConnectionResponse");
		oprot.WriteStructBegin(struc);
		TField field = default(TField);
		if (__isset.successful)
		{
			field.Name = "successful";
			field.Type = TType.Bool;
			field.ID = 1;
			oprot.WriteFieldBegin(field);
			oprot.WriteBool(Successful);
			oprot.WriteFieldEnd();
		}
		if (__isset.hashId)
		{
			field.Name = "hashId";
			field.Type = TType.I32;
			field.ID = 2;
			oprot.WriteFieldBegin(field);
			oprot.WriteI32(HashId);
			oprot.WriteFieldEnd();
		}
		if (__isset.error)
		{
			field.Name = "error";
			field.Type = TType.I32;
			field.ID = 3;
			oprot.WriteFieldBegin(field);
			oprot.WriteI32((int)Error);
			oprot.WriteFieldEnd();
		}
		if (ProtocolConfiguration != null && __isset.protocolConfiguration)
		{
			field.Name = "protocolConfiguration";
			field.Type = TType.Struct;
			field.ID = 4;
			oprot.WriteFieldBegin(field);
			ProtocolConfiguration.Write(oprot);
			oprot.WriteFieldEnd();
		}
		if (ServerVersion != null && __isset.serverVersion)
		{
			field.Name = "serverVersion";
			field.Type = TType.String;
			field.ID = 5;
			oprot.WriteFieldBegin(field);
			oprot.WriteString(ServerVersion);
			oprot.WriteFieldEnd();
		}
		oprot.WriteFieldStop();
		oprot.WriteStructEnd();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder("ThriftConnectionResponse(");
		stringBuilder.Append("Successful: ");
		stringBuilder.Append(Successful);
		stringBuilder.Append(",HashId: ");
		stringBuilder.Append(HashId);
		stringBuilder.Append(",Error: ");
		stringBuilder.Append(Error);
		stringBuilder.Append(",ProtocolConfiguration: ");
		stringBuilder.Append((ProtocolConfiguration != null) ? ProtocolConfiguration.ToString() : "<null>");
		stringBuilder.Append(",ServerVersion: ");
		stringBuilder.Append(ServerVersion);
		stringBuilder.Append(")");
		return stringBuilder.ToString();
	}
}
