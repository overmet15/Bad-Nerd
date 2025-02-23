using System;
using System.Collections.Generic;
using System.Text;
using Thrift.Protocol;

[Serializable]
public class ThriftGetUserVariablesResponse : TBase
{
	[Serializable]
	public struct Isset
	{
		public bool userName;

		public bool userVariables;
	}

	private string _userName;

	private Dictionary<string, ThriftFlattenedEsObject> _userVariables;

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

	public Dictionary<string, ThriftFlattenedEsObject> UserVariables
	{
		get
		{
			return _userVariables;
		}
		set
		{
			__isset.userVariables = true;
			_userVariables = value;
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
				if (tField.Type == TType.Map)
				{
					UserVariables = new Dictionary<string, ThriftFlattenedEsObject>();
					TMap tMap = iprot.ReadMapBegin();
					for (int i = 0; i < tMap.Count; i++)
					{
						string key = iprot.ReadString();
						ThriftFlattenedEsObject thriftFlattenedEsObject = new ThriftFlattenedEsObject();
						thriftFlattenedEsObject.Read(iprot);
						UserVariables[key] = thriftFlattenedEsObject;
					}
					iprot.ReadMapEnd();
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
		TStruct struc = new TStruct("ThriftGetUserVariablesResponse");
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
		if (UserVariables != null && __isset.userVariables)
		{
			field.Name = "userVariables";
			field.Type = TType.Map;
			field.ID = 2;
			oprot.WriteFieldBegin(field);
			oprot.WriteMapBegin(new TMap(TType.String, TType.Struct, UserVariables.Count));
			foreach (string key in UserVariables.Keys)
			{
				oprot.WriteString(key);
				UserVariables[key].Write(oprot);
				oprot.WriteMapEnd();
			}
			oprot.WriteFieldEnd();
		}
		oprot.WriteFieldStop();
		oprot.WriteStructEnd();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder("ThriftGetUserVariablesResponse(");
		stringBuilder.Append("UserName: ");
		stringBuilder.Append(UserName);
		stringBuilder.Append(",UserVariables: ");
		stringBuilder.Append(UserVariables);
		stringBuilder.Append(")");
		return stringBuilder.ToString();
	}
}
