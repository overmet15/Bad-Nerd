using System;
using System.Text;
using Thrift.Protocol;

[Serializable]
public class ThriftPrivateMessageEvent : TBase
{
	[Serializable]
	public struct Isset
	{
		public bool userName;

		public bool message;

		public bool esObject;
	}

	private string _userName;

	private string _message;

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

	public string Message
	{
		get
		{
			return _message;
		}
		set
		{
			__isset.message = true;
			_message = value;
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
				if (tField.Type == TType.String)
				{
					Message = iprot.ReadString();
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
		TStruct struc = new TStruct("ThriftPrivateMessageEvent");
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
		if (Message != null && __isset.message)
		{
			field.Name = "message";
			field.Type = TType.String;
			field.ID = 2;
			oprot.WriteFieldBegin(field);
			oprot.WriteString(Message);
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
		StringBuilder stringBuilder = new StringBuilder("ThriftPrivateMessageEvent(");
		stringBuilder.Append("UserName: ");
		stringBuilder.Append(UserName);
		stringBuilder.Append(",Message: ");
		stringBuilder.Append(Message);
		stringBuilder.Append(",EsObject: ");
		stringBuilder.Append((EsObject != null) ? EsObject.ToString() : "<null>");
		stringBuilder.Append(")");
		return stringBuilder.ToString();
	}
}
