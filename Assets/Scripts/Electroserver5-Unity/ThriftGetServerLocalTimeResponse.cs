using System;
using System.Text;
using Thrift.Protocol;

[Serializable]
public class ThriftGetServerLocalTimeResponse : TBase
{
	[Serializable]
	public struct Isset
	{
		public bool serverLocalTimeInMilliseconds;
	}

	private long _serverLocalTimeInMilliseconds;

	public Isset __isset;

	public long ServerLocalTimeInMilliseconds
	{
		get
		{
			return _serverLocalTimeInMilliseconds;
		}
		set
		{
			__isset.serverLocalTimeInMilliseconds = true;
			_serverLocalTimeInMilliseconds = value;
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
				if (tField.Type == TType.I64)
				{
					ServerLocalTimeInMilliseconds = iprot.ReadI64();
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
		TStruct struc = new TStruct("ThriftGetServerLocalTimeResponse");
		oprot.WriteStructBegin(struc);
		TField field = default(TField);
		if (__isset.serverLocalTimeInMilliseconds)
		{
			field.Name = "serverLocalTimeInMilliseconds";
			field.Type = TType.I64;
			field.ID = 1;
			oprot.WriteFieldBegin(field);
			oprot.WriteI64(ServerLocalTimeInMilliseconds);
			oprot.WriteFieldEnd();
		}
		oprot.WriteFieldStop();
		oprot.WriteStructEnd();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder("ThriftGetServerLocalTimeResponse(");
		stringBuilder.Append("ServerLocalTimeInMilliseconds: ");
		stringBuilder.Append(ServerLocalTimeInMilliseconds);
		stringBuilder.Append(")");
		return stringBuilder.ToString();
	}
}
