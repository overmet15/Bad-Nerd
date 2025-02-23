using System;
using System.Text;
using Thrift.Protocol;

[Serializable]
public class ThriftRegisterUDPConnectionRequest : TBase
{
	[Serializable]
	public struct Isset
	{
		public bool port;
	}

	private int _port;

	public Isset __isset;

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
			short iD = tField.ID;
			if (iD == 1)
			{
				if (tField.Type == TType.I32)
				{
					Port = iprot.ReadI32();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
			}
			else
			{
				TProtocolUtil.Skip(iprot, tField.Type);
			}
			iprot.ReadFieldEnd();
		}
		iprot.ReadStructEnd();
	}

	public void Write(TProtocol oprot)
	{
		TStruct struc = new TStruct("ThriftRegisterUDPConnectionRequest");
		oprot.WriteStructBegin(struc);
		TField field = default(TField);
		if (__isset.port)
		{
			field.Name = "port";
			field.Type = TType.I32;
			field.ID = 1;
			oprot.WriteFieldBegin(field);
			oprot.WriteI32(Port);
			oprot.WriteFieldEnd();
		}
		oprot.WriteFieldStop();
		oprot.WriteStructEnd();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder("ThriftRegisterUDPConnectionRequest(");
		stringBuilder.Append("Port: ");
		stringBuilder.Append(Port);
		stringBuilder.Append(")");
		return stringBuilder.ToString();
	}
}
