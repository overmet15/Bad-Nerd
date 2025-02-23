using System;
using System.Text;
using Thrift.Protocol;

[Serializable]
public class ThriftEncryptionStateChangeEvent : TBase
{
	[Serializable]
	public struct Isset
	{
		public bool encryptionOn;
	}

	private bool _encryptionOn;

	public Isset __isset;

	public bool EncryptionOn
	{
		get
		{
			return _encryptionOn;
		}
		set
		{
			__isset.encryptionOn = true;
			_encryptionOn = value;
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
				if (tField.Type == TType.Bool)
				{
					EncryptionOn = iprot.ReadBool();
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
		TStruct struc = new TStruct("ThriftEncryptionStateChangeEvent");
		oprot.WriteStructBegin(struc);
		TField field = default(TField);
		if (__isset.encryptionOn)
		{
			field.Name = "encryptionOn";
			field.Type = TType.Bool;
			field.ID = 1;
			oprot.WriteFieldBegin(field);
			oprot.WriteBool(EncryptionOn);
			oprot.WriteFieldEnd();
		}
		oprot.WriteFieldStop();
		oprot.WriteStructEnd();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder("ThriftEncryptionStateChangeEvent(");
		stringBuilder.Append("EncryptionOn: ");
		stringBuilder.Append(EncryptionOn);
		stringBuilder.Append(")");
		return stringBuilder.ToString();
	}
}
