using System;
using System.Collections.Generic;
using System.Text;
using Thrift.Protocol;

[Serializable]
public class ThriftFlattenedEsObjectRO : TBase
{
	[Serializable]
	public struct Isset
	{
		public bool encodedEntries;
	}

	private List<byte> _encodedEntries;

	public Isset __isset;

	public List<byte> EncodedEntries
	{
		get
		{
			return _encodedEntries;
		}
		set
		{
			__isset.encodedEntries = true;
			_encodedEntries = value;
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
				if (tField.Type == TType.List)
				{
					EncodedEntries = new List<byte>();
					TList tList = iprot.ReadListBegin();
					for (int i = 0; i < tList.Count; i++)
					{
						byte b = 0;
						b = iprot.ReadByte();
						EncodedEntries.Add(b);
					}
					iprot.ReadListEnd();
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
		TStruct struc = new TStruct("ThriftFlattenedEsObjectRO");
		oprot.WriteStructBegin(struc);
		TField field = default(TField);
		if (EncodedEntries != null && __isset.encodedEntries)
		{
			field.Name = "encodedEntries";
			field.Type = TType.List;
			field.ID = 1;
			oprot.WriteFieldBegin(field);
			oprot.WriteListBegin(new TList(TType.Byte, EncodedEntries.Count));
			foreach (byte encodedEntry in EncodedEntries)
			{
				oprot.WriteByte(encodedEntry);
				oprot.WriteListEnd();
			}
			oprot.WriteFieldEnd();
		}
		oprot.WriteFieldStop();
		oprot.WriteStructEnd();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder("ThriftFlattenedEsObjectRO(");
		stringBuilder.Append("EncodedEntries: ");
		stringBuilder.Append(EncodedEntries);
		stringBuilder.Append(")");
		return stringBuilder.ToString();
	}
}
