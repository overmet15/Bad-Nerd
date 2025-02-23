using System;
using System.Text;
using Thrift.Protocol;

[Serializable]
public class ThriftGetUserCountRequest : TBase
{
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
			TProtocolUtil.Skip(iprot, tField.Type);
			iprot.ReadFieldEnd();
		}
		iprot.ReadStructEnd();
	}

	public void Write(TProtocol oprot)
	{
		TStruct struc = new TStruct("ThriftGetUserCountRequest");
		oprot.WriteStructBegin(struc);
		oprot.WriteFieldStop();
		oprot.WriteStructEnd();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder("ThriftGetUserCountRequest(");
		stringBuilder.Append(")");
		return stringBuilder.ToString();
	}
}
