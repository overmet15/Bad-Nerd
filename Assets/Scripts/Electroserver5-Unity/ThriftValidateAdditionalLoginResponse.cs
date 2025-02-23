using System;
using System.Text;
using Thrift.Protocol;

[Serializable]
public class ThriftValidateAdditionalLoginResponse : TBase
{
	[Serializable]
	public struct Isset
	{
		public bool approved;

		public bool secret;
	}

	private bool _approved;

	private string _secret;

	public Isset __isset;

	public bool Approved
	{
		get
		{
			return _approved;
		}
		set
		{
			__isset.approved = true;
			_approved = value;
		}
	}

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
			switch (tField.ID)
			{
			case 1:
				if (tField.Type == TType.Bool)
				{
					Approved = iprot.ReadBool();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 2:
				if (tField.Type == TType.String)
				{
					Secret = iprot.ReadString();
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
		TStruct struc = new TStruct("ThriftValidateAdditionalLoginResponse");
		oprot.WriteStructBegin(struc);
		TField field = default(TField);
		if (__isset.approved)
		{
			field.Name = "approved";
			field.Type = TType.Bool;
			field.ID = 1;
			oprot.WriteFieldBegin(field);
			oprot.WriteBool(Approved);
			oprot.WriteFieldEnd();
		}
		if (Secret != null && __isset.secret)
		{
			field.Name = "secret";
			field.Type = TType.String;
			field.ID = 2;
			oprot.WriteFieldBegin(field);
			oprot.WriteString(Secret);
			oprot.WriteFieldEnd();
		}
		oprot.WriteFieldStop();
		oprot.WriteStructEnd();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder("ThriftValidateAdditionalLoginResponse(");
		stringBuilder.Append("Approved: ");
		stringBuilder.Append(Approved);
		stringBuilder.Append(",Secret: ");
		stringBuilder.Append(Secret);
		stringBuilder.Append(")");
		return stringBuilder.ToString();
	}
}
