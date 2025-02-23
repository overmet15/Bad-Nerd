using System;
using System.Text;
using Thrift.Protocol;

[Serializable]
public class ThriftCreateOrJoinGameResponse : TBase
{
	[Serializable]
	public struct Isset
	{
		public bool successful;

		public bool error;

		public bool zoneId;

		public bool roomId;

		public bool gameId;

		public bool gameDetails;
	}

	private bool _successful;

	private ThriftErrorType _error;

	private int _zoneId;

	private int _roomId;

	private int _gameId;

	private ThriftFlattenedEsObjectRO _gameDetails;

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

	public int ZoneId
	{
		get
		{
			return _zoneId;
		}
		set
		{
			__isset.zoneId = true;
			_zoneId = value;
		}
	}

	public int RoomId
	{
		get
		{
			return _roomId;
		}
		set
		{
			__isset.roomId = true;
			_roomId = value;
		}
	}

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

	public ThriftFlattenedEsObjectRO GameDetails
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
				if (tField.Type == TType.I32)
				{
					ZoneId = iprot.ReadI32();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 4:
				if (tField.Type == TType.I32)
				{
					RoomId = iprot.ReadI32();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 5:
				if (tField.Type == TType.I32)
				{
					GameId = iprot.ReadI32();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 6:
				if (tField.Type == TType.Struct)
				{
					GameDetails = new ThriftFlattenedEsObjectRO();
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
		TStruct struc = new TStruct("ThriftCreateOrJoinGameResponse");
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
		if (__isset.zoneId)
		{
			field.Name = "zoneId";
			field.Type = TType.I32;
			field.ID = 3;
			oprot.WriteFieldBegin(field);
			oprot.WriteI32(ZoneId);
			oprot.WriteFieldEnd();
		}
		if (__isset.roomId)
		{
			field.Name = "roomId";
			field.Type = TType.I32;
			field.ID = 4;
			oprot.WriteFieldBegin(field);
			oprot.WriteI32(RoomId);
			oprot.WriteFieldEnd();
		}
		if (__isset.gameId)
		{
			field.Name = "gameId";
			field.Type = TType.I32;
			field.ID = 5;
			oprot.WriteFieldBegin(field);
			oprot.WriteI32(GameId);
			oprot.WriteFieldEnd();
		}
		if (GameDetails != null && __isset.gameDetails)
		{
			field.Name = "gameDetails";
			field.Type = TType.Struct;
			field.ID = 6;
			oprot.WriteFieldBegin(field);
			GameDetails.Write(oprot);
			oprot.WriteFieldEnd();
		}
		oprot.WriteFieldStop();
		oprot.WriteStructEnd();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder("ThriftCreateOrJoinGameResponse(");
		stringBuilder.Append("Successful: ");
		stringBuilder.Append(Successful);
		stringBuilder.Append(",Error: ");
		stringBuilder.Append(Error);
		stringBuilder.Append(",ZoneId: ");
		stringBuilder.Append(ZoneId);
		stringBuilder.Append(",RoomId: ");
		stringBuilder.Append(RoomId);
		stringBuilder.Append(",GameId: ");
		stringBuilder.Append(GameId);
		stringBuilder.Append(",GameDetails: ");
		stringBuilder.Append((GameDetails != null) ? GameDetails.ToString() : "<null>");
		stringBuilder.Append(")");
		return stringBuilder.ToString();
	}
}
