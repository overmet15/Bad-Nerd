using System;
using System.Collections.Generic;
using System.Text;
using Thrift.Protocol;

[Serializable]
public class ThriftAddBuddiesResponse : TBase
{
	[Serializable]
	public struct Isset
	{
		public bool buddiesAdded;

		public bool buddiesNotAdded;
	}

	private List<string> _buddiesAdded;

	private List<string> _buddiesNotAdded;

	public Isset __isset;

	public List<string> BuddiesAdded
	{
		get
		{
			return _buddiesAdded;
		}
		set
		{
			__isset.buddiesAdded = true;
			_buddiesAdded = value;
		}
	}

	public List<string> BuddiesNotAdded
	{
		get
		{
			return _buddiesNotAdded;
		}
		set
		{
			__isset.buddiesNotAdded = true;
			_buddiesNotAdded = value;
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
				if (tField.Type == TType.List)
				{
					BuddiesAdded = new List<string>();
					TList tList2 = iprot.ReadListBegin();
					for (int j = 0; j < tList2.Count; j++)
					{
						string text2 = null;
						text2 = iprot.ReadString();
						BuddiesAdded.Add(text2);
					}
					iprot.ReadListEnd();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 2:
				if (tField.Type == TType.List)
				{
					BuddiesNotAdded = new List<string>();
					TList tList = iprot.ReadListBegin();
					for (int i = 0; i < tList.Count; i++)
					{
						string text = null;
						text = iprot.ReadString();
						BuddiesNotAdded.Add(text);
					}
					iprot.ReadListEnd();
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
		TStruct struc = new TStruct("ThriftAddBuddiesResponse");
		oprot.WriteStructBegin(struc);
		TField field = default(TField);
		if (BuddiesAdded != null && __isset.buddiesAdded)
		{
			field.Name = "buddiesAdded";
			field.Type = TType.List;
			field.ID = 1;
			oprot.WriteFieldBegin(field);
			oprot.WriteListBegin(new TList(TType.String, BuddiesAdded.Count));
			foreach (string item in BuddiesAdded)
			{
				oprot.WriteString(item);
				oprot.WriteListEnd();
			}
			oprot.WriteFieldEnd();
		}
		if (BuddiesNotAdded != null && __isset.buddiesNotAdded)
		{
			field.Name = "buddiesNotAdded";
			field.Type = TType.List;
			field.ID = 2;
			oprot.WriteFieldBegin(field);
			oprot.WriteListBegin(new TList(TType.String, BuddiesNotAdded.Count));
			foreach (string item2 in BuddiesNotAdded)
			{
				oprot.WriteString(item2);
				oprot.WriteListEnd();
			}
			oprot.WriteFieldEnd();
		}
		oprot.WriteFieldStop();
		oprot.WriteStructEnd();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder("ThriftAddBuddiesResponse(");
		stringBuilder.Append("BuddiesAdded: ");
		stringBuilder.Append(BuddiesAdded);
		stringBuilder.Append(",BuddiesNotAdded: ");
		stringBuilder.Append(BuddiesNotAdded);
		stringBuilder.Append(")");
		return stringBuilder.ToString();
	}
}
