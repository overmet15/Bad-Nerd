using System;
using System.Text;
using Thrift.Protocol;

[Serializable]
public class ThriftValidateAdditionalLoginRequest : TBase
{
	[Serializable]
	public struct Isset
	{
		public bool secret;
	}

	private string _secret;

	public Isset __isset;

	public string Secret
	{
		get
		{
			return _secret;
		}
		set
		{
			__isset.secret = true;
			_secret = value;
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
					Secret = iprot.ReadString();
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
		TStruct struc = new TStruct("ThriftValidateAdditionalLoginRequest");
		oprot.WriteStructBegin(struc);
		TField field = default(TField);
		if (Secret != null && __isset.secret)
		{
			field.Name = "secret";
			field.Type = TType.String;
			field.ID = 1;
			oprot.WriteFieldBegin(field);
			oprot.WriteString(Secret);
			oprot.WriteFieldEnd();
		}
		oprot.WriteFieldStop();
		oprot.WriteStructEnd();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder("ThriftValidateAdditionalLoginRequest(");
		stringBuilder.Append("Secret: ");
		stringBuilder.Append(Secret);
		stringBuilder.Append(")");
		return stringBuilder.ToString();
	}
}
