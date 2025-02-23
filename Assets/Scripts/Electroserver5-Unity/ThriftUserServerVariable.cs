using System;
using System.Text;
using Thrift.Protocol;

[Serializable]
public class ThriftUserServerVariable : TBase
{
	[Serializable]
	public struct Isset
	{
		public bool name;

		public bool value;
	}

	private string _name;

	private ThriftFlattenedEsObject _value;

	public Isset __isset;

	public string Name
	{
		get
		{
			return _name;
		}
		set
		{
			__isset.name = true;
			_name = value;
		}
	}

	public ThriftFlattenedEsObject Value
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
			switch (tField.ID)
			{
			case 1:
				if (tField.Type == TType.String)
				{
					Name = iprot.ReadString();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 2:
				if (tField.Type == TType.Struct)
				{
					Value = new ThriftFlattenedEsObject();
					Value.Read(iprot);
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
		TStruct struc = new TStruct("ThriftUserServerVariable");
		oprot.WriteStructBegin(struc);
		TField field = default(TField);
		if (Name != null && __isset.name)
		{
			field.Name = "name";
			field.Type = TType.String;
			field.ID = 1;
			oprot.WriteFieldBegin(field);
			oprot.WriteString(Name);
			oprot.WriteFieldEnd();
		}
		if (Value != null && __isset.value)
		{
			field.Name = "value";
			field.Type = TType.Struct;
			field.ID = 2;
			oprot.WriteFieldBegin(field);
			Value.Write(oprot);
			oprot.WriteFieldEnd();
		}
		oprot.WriteFieldStop();
		oprot.WriteStructEnd();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder("ThriftUserServerVariable(");
		stringBuilder.Append("Name: ");
		stringBuilder.Append(Name);
		stringBuilder.Append(",Value: ");
		stringBuilder.Append((Value != null) ? Value.ToString() : "<null>");
		stringBuilder.Append(")");
		return stringBuilder.ToString();
	}
}
