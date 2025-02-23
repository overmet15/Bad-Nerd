using System;
using System.Text;
using Thrift.Protocol;

[Serializable]
public class ThriftCrossDomainPolicyResponse : TBase
{
	[Serializable]
	public struct Isset
	{
		public bool customFileEnabled;

		public bool customFileContents;

		public bool port;
	}

	private bool _customFileEnabled;

	private string _customFileContents;

	private int _port;

	public Isset __isset;

	public bool CustomFileEnabled
	{
		get
		{
			return _customFileEnabled;
		}
		set
		{
			__isset.customFileEnabled = true;
			_customFileEnabled = value;
		}
	}

	public string CustomFileContents
	{
		get
		{
			return _customFileContents;
		}
		set
		{
			__isset.customFileContents = true;
			_customFileContents = value;
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
					CustomFileEnabled = iprot.ReadBool();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 2:
				if (tField.Type == TType.String)
				{
					CustomFileContents = iprot.ReadString();
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
		TStruct struc = new TStruct("ThriftCrossDomainPolicyResponse");
		oprot.WriteStructBegin(struc);
		TField field = default(TField);
		if (__isset.customFileEnabled)
		{
			field.Name = "customFileEnabled";
			field.Type = TType.Bool;
			field.ID = 1;
			oprot.WriteFieldBegin(field);
			oprot.WriteBool(CustomFileEnabled);
			oprot.WriteFieldEnd();
		}
		if (CustomFileContents != null && __isset.customFileContents)
		{
			field.Name = "customFileContents";
			field.Type = TType.String;
			field.ID = 2;
			oprot.WriteFieldBegin(field);
			oprot.WriteString(CustomFileContents);
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
		oprot.WriteFieldStop();
		oprot.WriteStructEnd();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder("ThriftCrossDomainPolicyResponse(");
		stringBuilder.Append("CustomFileEnabled: ");
		stringBuilder.Append(CustomFileEnabled);
		stringBuilder.Append(",CustomFileContents: ");
		stringBuilder.Append(CustomFileContents);
		stringBuilder.Append(",Port: ");
		stringBuilder.Append(Port);
		stringBuilder.Append(")");
		return stringBuilder.ToString();
	}
}
