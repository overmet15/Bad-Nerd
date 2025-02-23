using System;
using System.Collections.Generic;
using System.Text;
using Thrift.Protocol;

[Serializable]
public class ThriftPrivateMessageRequest : TBase
{
	[Serializable]
	public struct Isset
	{
		public bool message;

		public bool userNames;

		public bool esObject;
	}

	private string _message;

	private List<string> _userNames;

	private ThriftFlattenedEsObject _esObject;

	public Isset __isset;

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

	public List<string> UserNames
	{
		get
		{
			return _userNames;
		}
		set
		{
			__isset.userNames = true;
			_userNames = value;
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
					Message = iprot.ReadString();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 2:
				if (tField.Type == TType.List)
				{
					UserNames = new List<string>();
					TList tList = iprot.ReadListBegin();
					for (int i = 0; i < tList.Count; i++)
					{
						string text = null;
						text = iprot.ReadString();
						UserNames.Add(text);
					}
					iprot.ReadListEnd();
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
		TStruct struc = new TStruct("ThriftPrivateMessageRequest");
		oprot.WriteStructBegin(struc);
		TField field = default(TField);
		if (Message != null && __isset.message)
		{
			field.Name = "message";
			field.Type = TType.String;
			field.ID = 1;
			oprot.WriteFieldBegin(field);
			oprot.WriteString(Message);
			oprot.WriteFieldEnd();
		}
		if (UserNames != null && __isset.userNames)
		{
			field.Name = "userNames";
			field.Type = TType.List;
			field.ID = 2;
			oprot.WriteFieldBegin(field);
			oprot.WriteListBegin(new TList(TType.String, UserNames.Count));
			foreach (string userName in UserNames)
			{
				oprot.WriteString(userName);
				oprot.WriteListEnd();
			}
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
		StringBuilder stringBuilder = new StringBuilder("ThriftPrivateMessageRequest(");
		stringBuilder.Append("Message: ");
		stringBuilder.Append(Message);
		stringBuilder.Append(",UserNames: ");
		stringBuilder.Append(UserNames);
		stringBuilder.Append(",EsObject: ");
		stringBuilder.Append((EsObject != null) ? EsObject.ToString() : "<null>");
		stringBuilder.Append(")");
		return stringBuilder.ToString();
	}
}
