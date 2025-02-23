using System;
using System.Text;
using Thrift.Protocol;

[Serializable]
public class ThriftDHPublicNumbersResponse : TBase
{
	[Serializable]
	public struct Isset
	{
		public bool baseNumber;

		public bool primeNumber;
	}

	private string _baseNumber;

	private string _primeNumber;

	public Isset __isset;

	public string BaseNumber
	{
		get
		{
			return _baseNumber;
		}
		set
		{
			__isset.baseNumber = true;
			_baseNumber = value;
		}
	}

	public string PrimeNumber
	{
		get
		{
			return _primeNumber;
		}
		set
		{
			__isset.primeNumber = true;
			_primeNumber = value;
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
					BaseNumber = iprot.ReadString();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 2:
				if (tField.Type == TType.String)
				{
					PrimeNumber = iprot.ReadString();
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
		TStruct struc = new TStruct("ThriftDHPublicNumbersResponse");
		oprot.WriteStructBegin(struc);
		TField field = default(TField);
		if (BaseNumber != null && __isset.baseNumber)
		{
			field.Name = "baseNumber";
			field.Type = TType.String;
			field.ID = 1;
			oprot.WriteFieldBegin(field);
			oprot.WriteString(BaseNumber);
			oprot.WriteFieldEnd();
		}
		if (PrimeNumber != null && __isset.primeNumber)
		{
			field.Name = "primeNumber";
			field.Type = TType.String;
			field.ID = 2;
			oprot.WriteFieldBegin(field);
			oprot.WriteString(PrimeNumber);
			oprot.WriteFieldEnd();
		}
		oprot.WriteFieldStop();
		oprot.WriteStructEnd();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder("ThriftDHPublicNumbersResponse(");
		stringBuilder.Append("BaseNumber: ");
		stringBuilder.Append(BaseNumber);
		stringBuilder.Append(",PrimeNumber: ");
		stringBuilder.Append(PrimeNumber);
		stringBuilder.Append(")");
		return stringBuilder.ToString();
	}
}
