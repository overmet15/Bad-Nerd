using System;
using System.Text;
using Thrift.Protocol;

[Serializable]
public class ThriftUserVariableUpdateEvent : TBase
{
	[Serializable]
	public struct Isset
	{
		public bool userName;

		public bool variable;

		public bool action;
	}

	private string _userName;

	private ThriftUserVariable _variable;

	private ThriftUserVariableUpdateAction _action;

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

	public ThriftUserVariable Variable
	{
		get
		{
			return _variable;
		}
		set
		{
			__isset.variable = true;
			_variable = value;
		}
	}

	public ThriftUserVariableUpdateAction Action
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
				if (tField.Type == TType.Struct)
				{
					Variable = new ThriftUserVariable();
					Variable.Read(iprot);
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 3:
				if (tField.Type == TType.I32)
				{
					Action = (ThriftUserVariableUpdateAction)iprot.ReadI32();
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
		TStruct struc = new TStruct("ThriftUserVariableUpdateEvent");
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
		if (Variable != null && __isset.variable)
		{
			field.Name = "variable";
			field.Type = TType.Struct;
			field.ID = 2;
			oprot.WriteFieldBegin(field);
			Variable.Write(oprot);
			oprot.WriteFieldEnd();
		}
		if (__isset.action)
		{
			field.Name = "action";
			field.Type = TType.I32;
			field.ID = 3;
			oprot.WriteFieldBegin(field);
			oprot.WriteI32((int)Action);
			oprot.WriteFieldEnd();
		}
		oprot.WriteFieldStop();
		oprot.WriteStructEnd();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder("ThriftUserVariableUpdateEvent(");
		stringBuilder.Append("UserName: ");
		stringBuilder.Append(UserName);
		stringBuilder.Append(",Variable: ");
		stringBuilder.Append((Variable != null) ? Variable.ToString() : "<null>");
		stringBuilder.Append(",Action: ");
		stringBuilder.Append(Action);
		stringBuilder.Append(")");
		return stringBuilder.ToString();
	}
}
