using System;
using System.Collections.Generic;
using System.Text;
using Thrift.Protocol;

[Serializable]
public class ThriftRemoveBuddiesRequest : TBase
{
	[Serializable]
	public struct Isset
	{
		public bool buddyNames;
	}

	private List<string> _buddyNames;

	public Isset __isset;

	public List<string> BuddyNames
	{
		get
		{
			return _buddyNames;
		}
		set
		{
			__isset.buddyNames = true;
			_buddyNames = value;
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
					BuddyNames = new List<string>();
					TList tList = iprot.ReadListBegin();
					for (int i = 0; i < tList.Count; i++)
					{
						string text = null;
						text = iprot.ReadString();
						BuddyNames.Add(text);
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
		TStruct struc = new TStruct("ThriftRemoveBuddiesRequest");
		oprot.WriteStructBegin(struc);
		TField field = default(TField);
		if (BuddyNames != null && __isset.buddyNames)
		{
			field.Name = "buddyNames";
			field.Type = TType.List;
			field.ID = 1;
			oprot.WriteFieldBegin(field);
			oprot.WriteListBegin(new TList(TType.String, BuddyNames.Count));
			foreach (string buddyName in BuddyNames)
			{
				oprot.WriteString(buddyName);
				oprot.WriteListEnd();
			}
			oprot.WriteFieldEnd();
		}
		oprot.WriteFieldStop();
		oprot.WriteStructEnd();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder("ThriftRemoveBuddiesRequest(");
		stringBuilder.Append("BuddyNames: ");
		stringBuilder.Append(BuddyNames);
		stringBuilder.Append(")");
		return stringBuilder.ToString();
	}
}
