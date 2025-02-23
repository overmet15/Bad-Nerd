using System;
using System.Text;
using Thrift.Collections;
using Thrift.Protocol;

[Serializable]
public class ThriftGetUserVariablesRequest : TBase
{
	[Serializable]
	public struct Isset
	{
		public bool userName;

		public bool userVariableNames;
	}

	private string _userName;

	private THashSet<string> _userVariableNames;

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

	public THashSet<string> UserVariableNames
	{
		get
		{
			return _userVariableNames;
		}
		set
		{
			__isset.userVariableNames = true;
			_userVariableNames = value;
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
				if (tField.Type == TType.Set)
				{
					UserVariableNames = new THashSet<string>();
					TSet tSet = iprot.ReadSetBegin();
					for (int i = 0; i < tSet.Count; i++)
					{
						string text = null;
						text = iprot.ReadString();
						UserVariableNames.Add(text);
					}
					iprot.ReadSetEnd();
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
		TStruct struc = new TStruct("ThriftGetUserVariablesRequest");
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
		if (UserVariableNames != null && __isset.userVariableNames)
		{
			field.Name = "userVariableNames";
			field.Type = TType.Set;
			field.ID = 2;
			oprot.WriteFieldBegin(field);
			oprot.WriteSetBegin(new TSet(TType.String, UserVariableNames.Count));
			foreach (string userVariableName in UserVariableNames)
			{
				oprot.WriteString(userVariableName);
				oprot.WriteSetEnd();
			}
			oprot.WriteFieldEnd();
		}
		oprot.WriteFieldStop();
		oprot.WriteStructEnd();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder("ThriftGetUserVariablesRequest(");
		stringBuilder.Append("UserName: ");
		stringBuilder.Append(UserName);
		stringBuilder.Append(",UserVariableNames: ");
		stringBuilder.Append(UserVariableNames);
		stringBuilder.Append(")");
		return stringBuilder.ToString();
	}
}
