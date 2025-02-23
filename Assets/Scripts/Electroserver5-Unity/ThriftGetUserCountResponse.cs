using System;
using System.Text;
using Thrift.Protocol;

[Serializable]
public class ThriftGetUserCountResponse : TBase
{
	[Serializable]
	public struct Isset
	{
		public bool count;
	}

	private int _count;

	public Isset __isset;

	public int Count
	{
		get
		{
			return _count;
		}
		set
		{
			__isset.count = true;
			_count = value;
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
					Count = iprot.ReadI32();
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
		TStruct struc = new TStruct("ThriftGetUserCountResponse");
		oprot.WriteStructBegin(struc);
		TField field = default(TField);
		if (__isset.count)
		{
			field.Name = "count";
			field.Type = TType.I32;
			field.ID = 1;
			oprot.WriteFieldBegin(field);
			oprot.WriteI32(Count);
			oprot.WriteFieldEnd();
		}
		oprot.WriteFieldStop();
		oprot.WriteStructEnd();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder("ThriftGetUserCountResponse(");
		stringBuilder.Append("Count: ");
		stringBuilder.Append(Count);
		stringBuilder.Append(")");
		return stringBuilder.ToString();
	}
}
