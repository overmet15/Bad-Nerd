using System;
using System.Text;
using Thrift.Protocol;

[Serializable]
public class ThriftEsNumber : TBase
{
	[Serializable]
	public struct Isset
	{
		public bool value;
	}

	private double _value;

	public Isset __isset;

	public double Value
	{
		get
		{
			return _value;
		}
		set
		{
			__isset.value = true;
			_value = value;
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
				if (tField.Type == TType.Double)
				{
					Value = iprot.ReadDouble();
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
		TStruct struc = new TStruct("ThriftEsNumber");
		oprot.WriteStructBegin(struc);
		TField field = default(TField);
		if (__isset.value)
		{
			field.Name = "value";
			field.Type = TType.Double;
			field.ID = 1;
			oprot.WriteFieldBegin(field);
			oprot.WriteDouble(Value);
			oprot.WriteFieldEnd();
		}
		oprot.WriteFieldStop();
		oprot.WriteStructEnd();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder("ThriftEsNumber(");
		stringBuilder.Append("Value: ");
		stringBuilder.Append(Value);
		stringBuilder.Append(")");
		return stringBuilder.ToString();
	}
}
