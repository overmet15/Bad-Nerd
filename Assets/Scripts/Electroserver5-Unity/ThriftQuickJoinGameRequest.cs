using System;
using System.Text;
using Thrift.Protocol;

[Serializable]
public class ThriftQuickJoinGameRequest : TBase
{
	[Serializable]
	public struct Isset
	{
		public bool gameType;

		public bool zoneName;

		public bool password;

		public bool locked;

		public bool hidden;

		public bool createOnly;

		public bool gameDetails;

		public bool criteria;
	}

	private string _gameType;

	private string _zoneName;

	private string _password;

	private bool _locked;

	private bool _hidden;

	private bool _createOnly;

	private ThriftFlattenedEsObject _gameDetails;

	private ThriftSearchCriteria _criteria;

	public Isset __isset;

	public string GameType
	{
		get
		{
			return _gameType;
		}
		set
		{
			__isset.gameType = true;
			_gameType = value;
		}
	}

	public string ZoneName
	{
		get
		{
			return _zoneName;
		}
		set
		{
			__isset.zoneName = true;
			_zoneName = value;
		}
	}

	public string Password
	{
		get
		{
			return _password;
		}
		set
		{
			__isset.password = true;
			_password = value;
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

	public bool Hidden
	{
		get
		{
			return _hidden;
		}
		set
		{
			__isset.hidden = true;
			_hidden = value;
		}
	}

	public bool CreateOnly
	{
		get
		{
			return _createOnly;
		}
		set
		{
			__isset.createOnly = true;
			_createOnly = value;
		}
	}

	public ThriftFlattenedEsObject GameDetails
	{
		get
		{
			return _gameDetails;
		}
		set
		{
			__isset.gameDetails = true;
			_gameDetails = value;
		}
	}

	public ThriftSearchCriteria Criteria
	{
		get
		{
			return _criteria;
		}
		set
		{
			__isset.criteria = true;
			_criteria = value;
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
					GameType = iprot.ReadString();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 2:
				if (tField.Type == TType.String)
				{
					ZoneName = iprot.ReadString();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 3:
				if (tField.Type == TType.String)
				{
					Password = iprot.ReadString();
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
			case 5:
				if (tField.Type == TType.Bool)
				{
					Hidden = iprot.ReadBool();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 6:
				if (tField.Type == TType.Bool)
				{
					CreateOnly = iprot.ReadBool();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 7:
				if (tField.Type == TType.Struct)
				{
					GameDetails = new ThriftFlattenedEsObject();
					GameDetails.Read(iprot);
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 8:
				if (tField.Type == TType.Struct)
				{
					Criteria = new ThriftSearchCriteria();
					Criteria.Read(iprot);
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
		TStruct struc = new TStruct("ThriftQuickJoinGameRequest");
		oprot.WriteStructBegin(struc);
		TField field = default(TField);
		if (GameType != null && __isset.gameType)
		{
			field.Name = "gameType";
			field.Type = TType.String;
			field.ID = 1;
			oprot.WriteFieldBegin(field);
			oprot.WriteString(GameType);
			oprot.WriteFieldEnd();
		}
		if (ZoneName != null && __isset.zoneName)
		{
			field.Name = "zoneName";
			field.Type = TType.String;
			field.ID = 2;
			oprot.WriteFieldBegin(field);
			oprot.WriteString(ZoneName);
			oprot.WriteFieldEnd();
		}
		if (Password != null && __isset.password)
		{
			field.Name = "password";
			field.Type = TType.String;
			field.ID = 3;
			oprot.WriteFieldBegin(field);
			oprot.WriteString(Password);
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
		if (__isset.hidden)
		{
			field.Name = "hidden";
			field.Type = TType.Bool;
			field.ID = 5;
			oprot.WriteFieldBegin(field);
			oprot.WriteBool(Hidden);
			oprot.WriteFieldEnd();
		}
		if (__isset.createOnly)
		{
			field.Name = "createOnly";
			field.Type = TType.Bool;
			field.ID = 6;
			oprot.WriteFieldBegin(field);
			oprot.WriteBool(CreateOnly);
			oprot.WriteFieldEnd();
		}
		if (GameDetails != null && __isset.gameDetails)
		{
			field.Name = "gameDetails";
			field.Type = TType.Struct;
			field.ID = 7;
			oprot.WriteFieldBegin(field);
			GameDetails.Write(oprot);
			oprot.WriteFieldEnd();
		}
		if (Criteria != null && __isset.criteria)
		{
			field.Name = "criteria";
			field.Type = TType.Struct;
			field.ID = 8;
			oprot.WriteFieldBegin(field);
			Criteria.Write(oprot);
			oprot.WriteFieldEnd();
		}
		oprot.WriteFieldStop();
		oprot.WriteStructEnd();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder("ThriftQuickJoinGameRequest(");
		stringBuilder.Append("GameType: ");
		stringBuilder.Append(GameType);
		stringBuilder.Append(",ZoneName: ");
		stringBuilder.Append(ZoneName);
		stringBuilder.Append(",Password: ");
		stringBuilder.Append(Password);
		stringBuilder.Append(",Locked: ");
		stringBuilder.Append(Locked);
		stringBuilder.Append(",Hidden: ");
		stringBuilder.Append(Hidden);
		stringBuilder.Append(",CreateOnly: ");
		stringBuilder.Append(CreateOnly);
		stringBuilder.Append(",GameDetails: ");
		stringBuilder.Append((GameDetails != null) ? GameDetails.ToString() : "<null>");
		stringBuilder.Append(",Criteria: ");
		stringBuilder.Append((Criteria != null) ? Criteria.ToString() : "<null>");
		stringBuilder.Append(")");
		return stringBuilder.ToString();
	}
}
