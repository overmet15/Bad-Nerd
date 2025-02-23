using System;
using System.Text;
using Thrift.Protocol;

[Serializable]
public class ThriftRoomVariable : TBase
{
	[Serializable]
	public struct Isset
	{
		public bool persistent;

		public bool name;

		public bool value;

		public bool locked;
	}

	private bool _persistent;

	private string _name;

	private ThriftFlattenedEsObject _value;

	private bool _locked;

	public Isset __isset;

	public bool Persistent
	{
		get
		{
			return _persistent;
		}
		set
		{
			__isset.persistent = true;
			_persistent = value;
		}
	}

	public string Name
	{
		get
		{
			return _name;
		}
		set
		{
			__isset.name = true;
			_name = value;
		}
	}

	public ThriftFlattenedEsObject Value
	{
		get
		{
			return _value;
		}
		set
		{
			__isset.value = true;
			_value = value;
		}
	}

	public bool Locked
	{
		get
		{
			return _locked;
		}
		set
		{
			__isset.locked = true;
			_locked = value;
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
				if (tField.Type == TType.Bool)
				{
					Persistent = iprot.ReadBool();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 2:
				if (tField.Type == TType.String)
				{
					Name = iprot.ReadString();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 3:
				if (tField.Type == TType.Struct)
				{
					Value = new ThriftFlattenedEsObject();
					Value.Read(iprot);
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 4:
				if (tField.Type == TType.Bool)
				{
					Locked = iprot.ReadBool();
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
		TStruct struc = new TStruct("ThriftRoomVariable");
		oprot.WriteStructBegin(struc);
		TField field = default(TField);
		if (__isset.persistent)
		{
			field.Name = "persistent";
			field.Type = TType.Bool;
			field.ID = 1;
			oprot.WriteFieldBegin(field);
			oprot.WriteBool(Persistent);
			oprot.WriteFieldEnd();
		}
		if (Name != null && __isset.name)
		{
			field.Name = "name";
			field.Type = TType.String;
			field.ID = 2;
			oprot.WriteFieldBegin(field);
			oprot.WriteString(Name);
			oprot.WriteFieldEnd();
		}
		if (Value != null && __isset.value)
		{
			field.Name = "value";
			field.Type = TType.Struct;
			field.ID = 3;
			oprot.WriteFieldBegin(field);
			Value.Write(oprot);
			oprot.WriteFieldEnd();
		}
		if (__isset.locked)
		{
			field.Name = "locked";
			field.Type = TType.Bool;
			field.ID = 4;
			oprot.WriteFieldBegin(field);
			oprot.WriteBool(Locked);
			oprot.WriteFieldEnd();
		}
		oprot.WriteFieldStop();
		oprot.WriteStructEnd();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder("ThriftRoomVariable(");
		stringBuilder.Append("Persistent: ");
		stringBuilder.Append(Persistent);
		stringBuilder.Append(",Name: ");
		stringBuilder.Append(Name);
		stringBuilder.Append(",Value: ");
		stringBuilder.Append((Value != null) ? Value.ToString() : "<null>");
		stringBuilder.Append(",Locked: ");
		stringBuilder.Append(Locked);
		stringBuilder.Append(")");
		return stringBuilder.ToString();
	}
}
