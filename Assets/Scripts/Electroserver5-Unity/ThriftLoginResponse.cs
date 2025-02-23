using System;
using System.Collections.Generic;
using System.Text;
using Thrift.Protocol;

[Serializable]
public class ThriftLoginResponse : TBase
{
	[Serializable]
	public struct Isset
	{
		public bool successful;

		public bool error;

		public bool esObject;

		public bool userName;

		public bool userVariables;

		public bool buddyListEntries;
	}

	private bool _successful;

	private ThriftErrorType _error;

	private ThriftFlattenedEsObjectRO _esObject;

	private string _userName;

	private Dictionary<string, ThriftFlattenedEsObjectRO> _userVariables;

	private Dictionary<string, ThriftFlattenedEsObjectRO> _buddyListEntries;

	public Isset __isset;

	public bool Successful
	{
		get
		{
			return _successful;
		}
		set
		{
			__isset.successful = true;
			_successful = value;
		}
	}

	public ThriftErrorType Error
	{
		get
		{
			return _error;
		}
		set
		{
			__isset.error = true;
			_error = value;
		}
	}

	public ThriftFlattenedEsObjectRO EsObject
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

	public Dictionary<string, ThriftFlattenedEsObjectRO> UserVariables
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

	public Dictionary<string, ThriftFlattenedEsObjectRO> BuddyListEntries
	{
		get
		{
			return _buddyListEntries;
		}
		set
		{
			__isset.buddyListEntries = true;
			_buddyListEntries = value;
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
					Successful = iprot.ReadBool();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 2:
				if (tField.Type == TType.I32)
				{
					Error = (ThriftErrorType)iprot.ReadI32();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 3:
				if (tField.Type == TType.Struct)
				{
					EsObject = new ThriftFlattenedEsObjectRO();
					EsObject.Read(iprot);
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 4:
				if (tField.Type == TType.String)
				{
					UserName = iprot.ReadString();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 6:
				if (tField.Type == TType.Map)
				{
					UserVariables = new Dictionary<string, ThriftFlattenedEsObjectRO>();
					TMap tMap2 = iprot.ReadMapBegin();
					for (int j = 0; j < tMap2.Count; j++)
					{
						string key2 = iprot.ReadString();
						ThriftFlattenedEsObjectRO thriftFlattenedEsObjectRO2 = new ThriftFlattenedEsObjectRO();
						thriftFlattenedEsObjectRO2.Read(iprot);
						UserVariables[key2] = thriftFlattenedEsObjectRO2;
					}
					iprot.ReadMapEnd();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 7:
				if (tField.Type == TType.Map)
				{
					BuddyListEntries = new Dictionary<string, ThriftFlattenedEsObjectRO>();
					TMap tMap = iprot.ReadMapBegin();
					for (int i = 0; i < tMap.Count; i++)
					{
						string key = iprot.ReadString();
						ThriftFlattenedEsObjectRO thriftFlattenedEsObjectRO = new ThriftFlattenedEsObjectRO();
						thriftFlattenedEsObjectRO.Read(iprot);
						BuddyListEntries[key] = thriftFlattenedEsObjectRO;
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
		TStruct struc = new TStruct("ThriftLoginResponse");
		oprot.WriteStructBegin(struc);
		TField field = default(TField);
		if (__isset.successful)
		{
			field.Name = "successful";
			field.Type = TType.Bool;
			field.ID = 1;
			oprot.WriteFieldBegin(field);
			oprot.WriteBool(Successful);
			oprot.WriteFieldEnd();
		}
		if (__isset.error)
		{
			field.Name = "error";
			field.Type = TType.I32;
			field.ID = 2;
			oprot.WriteFieldBegin(field);
			oprot.WriteI32((int)Error);
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
		if (UserName != null && __isset.userName)
		{
			field.Name = "userName";
			field.Type = TType.String;
			field.ID = 4;
			oprot.WriteFieldBegin(field);
			oprot.WriteString(UserName);
			oprot.WriteFieldEnd();
		}
		if (UserVariables != null && __isset.userVariables)
		{
			field.Name = "userVariables";
			field.Type = TType.Map;
			field.ID = 6;
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
		if (BuddyListEntries != null && __isset.buddyListEntries)
		{
			field.Name = "buddyListEntries";
			field.Type = TType.Map;
			field.ID = 7;
			oprot.WriteFieldBegin(field);
			oprot.WriteMapBegin(new TMap(TType.String, TType.Struct, BuddyListEntries.Count));
			foreach (string key2 in BuddyListEntries.Keys)
			{
				oprot.WriteString(key2);
				BuddyListEntries[key2].Write(oprot);
				oprot.WriteMapEnd();
			}
			oprot.WriteFieldEnd();
		}
		oprot.WriteFieldStop();
		oprot.WriteStructEnd();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder("ThriftLoginResponse(");
		stringBuilder.Append("Successful: ");
		stringBuilder.Append(Successful);
		stringBuilder.Append(",Error: ");
		stringBuilder.Append(Error);
		stringBuilder.Append(",EsObject: ");
		stringBuilder.Append((EsObject != null) ? EsObject.ToString() : "<null>");
		stringBuilder.Append(",UserName: ");
		stringBuilder.Append(UserName);
		stringBuilder.Append(",UserVariables: ");
		stringBuilder.Append(UserVariables);
		stringBuilder.Append(",BuddyListEntries: ");
		stringBuilder.Append(BuddyListEntries);
		stringBuilder.Append(")");
		return stringBuilder.ToString();
	}
}
