using System;
using System.Text;
using Thrift.Protocol;

[Serializable]
public class ThriftDeleteUserVariableRequest : TBase
{
	[Serializable]
	public struct Isset
	{
		public bool name;
	}

	private string _name;

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
					Name = iprot.ReadString();
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
		TStruct struc = new TStruct("ThriftDeleteUserVariableRequest");
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
		oprot.WriteFieldStop();
		oprot.WriteStructEnd();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder("ThriftDeleteUserVariableRequest(");
		stringBuilder.Append("Name: ");
		stringBuilder.Append(Name);
		stringBuilder.Append(")");
		return stringBuilder.ToString();
	}
}
