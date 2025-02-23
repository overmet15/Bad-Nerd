using System;
using System.Text;
using Thrift.Protocol;

[Serializable]
public class ThriftSearchCriteria : TBase
{
	[Serializable]
	public struct Isset
	{
		public bool gameId;

		public bool locked;

		public bool lockedSet;

		public bool gameType;

		public bool gameDetails;
	}

	private int _gameId;

	private bool _locked;

	private bool _lockedSet;

	private string _gameType;

	private ThriftFlattenedEsObject _gameDetails;

	public Isset __isset;

	public int GameId
	{
		get
		{
			return _gameId;
		}
		set
		{
			__isset.gameId = true;
			_gameId = value;
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

	public bool LockedSet
	{
		get
		{
			return _lockedSet;
		}
		set
		{
			__isset.lockedSet = true;
			_lockedSet = value;
		}
	}

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
				if (tField.Type == TType.I32)
				{
					GameId = iprot.ReadI32();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 2:
				if (tField.Type == TType.Bool)
				{
					Locked = iprot.ReadBool();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 3:
				if (tField.Type == TType.Bool)
				{
					LockedSet = iprot.ReadBool();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 4:
				if (tField.Type == TType.String)
				{
					GameType = iprot.ReadString();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 5:
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
		TStruct struc = new TStruct("ThriftSearchCriteria");
		oprot.WriteStructBegin(struc);
		TField field = default(TField);
		if (__isset.gameId)
		{
			field.Name = "gameId";
			field.Type = TType.I32;
			field.ID = 1;
			oprot.WriteFieldBegin(field);
			oprot.WriteI32(GameId);
			oprot.WriteFieldEnd();
		}
		if (__isset.locked)
		{
			field.Name = "locked";
			field.Type = TType.Bool;
			field.ID = 2;
			oprot.WriteFieldBegin(field);
			oprot.WriteBool(Locked);
			oprot.WriteFieldEnd();
		}
		if (__isset.lockedSet)
		{
			field.Name = "lockedSet";
			field.Type = TType.Bool;
			field.ID = 3;
			oprot.WriteFieldBegin(field);
			oprot.WriteBool(LockedSet);
			oprot.WriteFieldEnd();
		}
		if (GameType != null && __isset.gameType)
		{
			field.Name = "gameType";
			field.Type = TType.String;
			field.ID = 4;
			oprot.WriteFieldBegin(field);
			oprot.WriteString(GameType);
			oprot.WriteFieldEnd();
		}
		if (GameDetails != null && __isset.gameDetails)
		{
			field.Name = "gameDetails";
			field.Type = TType.Struct;
			field.ID = 5;
			oprot.WriteFieldBegin(field);
			GameDetails.Write(oprot);
			oprot.WriteFieldEnd();
		}
		oprot.WriteFieldStop();
		oprot.WriteStructEnd();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder("ThriftSearchCriteria(");
		stringBuilder.Append("GameId: ");
		stringBuilder.Append(GameId);
		stringBuilder.Append(",Locked: ");
		stringBuilder.Append(Locked);
		stringBuilder.Append(",LockedSet: ");
		stringBuilder.Append(LockedSet);
		stringBuilder.Append(",GameType: ");
		stringBuilder.Append(GameType);
		stringBuilder.Append(",GameDetails: ");
		stringBuilder.Append((GameDetails != null) ? GameDetails.ToString() : "<null>");
		stringBuilder.Append(")");
		return stringBuilder.ToString();
	}
}
