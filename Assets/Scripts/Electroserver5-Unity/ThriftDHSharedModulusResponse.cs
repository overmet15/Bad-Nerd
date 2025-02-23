using System;
using System.Text;
using Thrift.Protocol;

[Serializable]
public class ThriftDHSharedModulusResponse : TBase
{
	[Serializable]
	public struct Isset
	{
		public bool number;
	}

	private string _number;

	public Isset __isset;

	public string Number
	{
		get
		{
			return _number;
		}
		set
		{
			__isset.number = true;
			_number = value;
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
				if (tField.Type == TType.String)
				{
					Number = iprot.ReadString();
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
		TStruct struc = new TStruct("ThriftDHSharedModulusResponse");
		oprot.WriteStructBegin(struc);
		TField field = default(TField);
		if (Number != null && __isset.number)
		{
			field.Name = "number";
			field.Type = TType.String;
			field.ID = 1;
			oprot.WriteFieldBegin(field);
			oprot.WriteString(Number);
			oprot.WriteFieldEnd();
		}
		oprot.WriteFieldStop();
		oprot.WriteStructEnd();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder("ThriftDHSharedModulusResponse(");
		stringBuilder.Append("Number: ");
		stringBuilder.Append(Number);
		stringBuilder.Append(")");
		return stringBuilder.ToString();
	}
}
