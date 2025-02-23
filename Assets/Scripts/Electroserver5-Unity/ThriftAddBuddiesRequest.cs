using System;
using System.Collections.Generic;
using System.Text;
using Thrift.Protocol;

[Serializable]
public class ThriftAddBuddiesRequest : TBase
{
	[Serializable]
	public struct Isset
	{
		public bool buddyNames;

		public bool esObject;

		public bool skipInitialLoggedOutEvents;
	}

	private List<string> _buddyNames;

	private ThriftFlattenedEsObject _esObject;

	private bool _skipInitialLoggedOutEvents;

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

	public ThriftFlattenedEsObject EsObject
	{
		get
		{
			return _esObject;
		}
		set
		{
			__isset.esObject = true;
			_esObject = value;
		}
	}

	public bool SkipInitialLoggedOutEvents
	{
		get
		{
			return _skipInitialLoggedOutEvents;
		}
		set
		{
			__isset.skipInitialLoggedOutEvents = true;
			_skipInitialLoggedOutEvents = value;
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
				break;
			case 2:
				if (tField.Type == TType.Struct)
				{
					EsObject = new ThriftFlattenedEsObject();
					EsObject.Read(iprot);
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 3:
				if (tField.Type == TType.Bool)
				{
					SkipInitialLoggedOutEvents = iprot.ReadBool();
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
		TStruct struc = new TStruct("ThriftAddBuddiesRequest");
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
		if (EsObject != null && __isset.esObject)
		{
			field.Name = "esObject";
			field.Type = TType.Struct;
			field.ID = 2;
			oprot.WriteFieldBegin(field);
			EsObject.Write(oprot);
			oprot.WriteFieldEnd();
		}
		if (__isset.skipInitialLoggedOutEvents)
		{
			field.Name = "skipInitialLoggedOutEvents";
			field.Type = TType.Bool;
			field.ID = 3;
			oprot.WriteFieldBegin(field);
			oprot.WriteBool(SkipInitialLoggedOutEvents);
			oprot.WriteFieldEnd();
		}
		oprot.WriteFieldStop();
		oprot.WriteStructEnd();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder("ThriftAddBuddiesRequest(");
		stringBuilder.Append("BuddyNames: ");
		stringBuilder.Append(BuddyNames);
		stringBuilder.Append(",EsObject: ");
		stringBuilder.Append((EsObject != null) ? EsObject.ToString() : "<null>");
		stringBuilder.Append(",SkipInitialLoggedOutEvents: ");
		stringBuilder.Append(SkipInitialLoggedOutEvents);
		stringBuilder.Append(")");
		return stringBuilder.ToString();
	}
}
