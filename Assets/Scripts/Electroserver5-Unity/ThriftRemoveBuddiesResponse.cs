using System;
using System.Collections.Generic;
using System.Text;
using Thrift.Protocol;

[Serializable]
public class ThriftRemoveBuddiesResponse : TBase
{
	[Serializable]
	public struct Isset
	{
		public bool buddiesRemoved;

		public bool buddiesNotRemoved;
	}

	private List<string> _buddiesRemoved;

	private List<string> _buddiesNotRemoved;

	public Isset __isset;

	public List<string> BuddiesRemoved
	{
		get
		{
			return _buddiesRemoved;
		}
		set
		{
			__isset.buddiesRemoved = true;
			_buddiesRemoved = value;
		}
	}

	public List<string> BuddiesNotRemoved
	{
		get
		{
			return _buddiesNotRemoved;
		}
		set
		{
			__isset.buddiesNotRemoved = true;
			_buddiesNotRemoved = value;
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
					BuddiesRemoved = new List<string>();
					TList tList2 = iprot.ReadListBegin();
					for (int j = 0; j < tList2.Count; j++)
					{
						string text2 = null;
						text2 = iprot.ReadString();
						BuddiesRemoved.Add(text2);
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
					BuddiesNotRemoved = new List<string>();
					TList tList = iprot.ReadListBegin();
					for (int i = 0; i < tList.Count; i++)
					{
						string text = null;
						text = iprot.ReadString();
						BuddiesNotRemoved.Add(text);
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
		TStruct struc = new TStruct("ThriftRemoveBuddiesResponse");
		oprot.WriteStructBegin(struc);
		TField field = default(TField);
		if (BuddiesRemoved != null && __isset.buddiesRemoved)
		{
			field.Name = "buddiesRemoved";
			field.Type = TType.List;
			field.ID = 1;
			oprot.WriteFieldBegin(field);
			oprot.WriteListBegin(new TList(TType.String, BuddiesRemoved.Count));
			foreach (string item in BuddiesRemoved)
			{
				oprot.WriteString(item);
				oprot.WriteListEnd();
			}
			oprot.WriteFieldEnd();
		}
		if (BuddiesNotRemoved != null && __isset.buddiesNotRemoved)
		{
			field.Name = "buddiesNotRemoved";
			field.Type = TType.List;
			field.ID = 2;
			oprot.WriteFieldBegin(field);
			oprot.WriteListBegin(new TList(TType.String, BuddiesNotRemoved.Count));
			foreach (string item2 in BuddiesNotRemoved)
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
		StringBuilder stringBuilder = new StringBuilder("ThriftRemoveBuddiesResponse(");
		stringBuilder.Append("BuddiesRemoved: ");
		stringBuilder.Append(BuddiesRemoved);
		stringBuilder.Append(",BuddiesNotRemoved: ");
		stringBuilder.Append(BuddiesNotRemoved);
		stringBuilder.Append(")");
		return stringBuilder.ToString();
	}
}
