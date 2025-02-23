using System;
using System.Text;
using Thrift.Protocol;

[Serializable]
public class ThriftBuddyStatusUpdateEvent : TBase
{
	[Serializable]
	public struct Isset
	{
		public bool userName;

		public bool action;

		public bool esObject;
	}

	private string _userName;

	private ThriftBuddyStatusUpdateAction _action;

	private ThriftFlattenedEsObject _esObject;

	public Isset __isset;

	public string UserName
	{
		get
		{
			return _userName;
		}
		set
		{
			__isset.userName = true;
			_userName = value;
		}
	}

	public ThriftBuddyStatusUpdateAction Action
	{
		get
		{
			return _action;
		}
		set
		{
			__isset.action = true;
			_action = value;
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
					UserName = iprot.ReadString();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 2:
				if (tField.Type == TType.I32)
				{
					Action = (ThriftBuddyStatusUpdateAction)iprot.ReadI32();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 3:
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
		TStruct struc = new TStruct("ThriftBuddyStatusUpdateEvent");
		oprot.WriteStructBegin(struc);
		TField field = default(TField);
		if (UserName != null && __isset.userName)
		{
			field.Name = "userName";
			field.Type = TType.String;
			field.ID = 1;
			oprot.WriteFieldBegin(field);
			oprot.WriteString(UserName);
			oprot.WriteFieldEnd();
		}
		if (__isset.action)
		{
			field.Name = "action";
			field.Type = TType.I32;
			field.ID = 2;
			oprot.WriteFieldBegin(field);
			oprot.WriteI32((int)Action);
			oprot.WriteFieldEnd();
		}
		if (EsObject != null && __isset.esObject)
		{
			field.Name = "esObject";
			field.Type = TType.Struct;
			field.ID = 3;
			oprot.WriteFieldBegin(field);
			EsObject.Write(oprot);
			oprot.WriteFieldEnd();
		}
		oprot.WriteFieldStop();
		oprot.WriteStructEnd();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder("ThriftBuddyStatusUpdateEvent(");
		stringBuilder.Append("UserName: ");
		stringBuilder.Append(UserName);
		stringBuilder.Append(",Action: ");
		stringBuilder.Append(Action);
		stringBuilder.Append(",EsObject: ");
		stringBuilder.Append((EsObject != null) ? EsObject.ToString() : "<null>");
		stringBuilder.Append(")");
		return stringBuilder.ToString();
	}
}
