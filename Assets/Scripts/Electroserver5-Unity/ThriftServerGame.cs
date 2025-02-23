using System;
using System.Text;
using Thrift.Protocol;

[Serializable]
public class ThriftServerGame : TBase
{
	[Serializable]
	public struct Isset
	{
		public bool gameDetails;

		public bool id;

		public bool roomId;

		public bool zoneId;

		public bool locked;

		public bool passwordProtected;
	}

	private ThriftFlattenedEsObject _gameDetails;

	private int _id;

	private int _roomId;

	private int _zoneId;

	private bool _locked;

	private bool _passwordProtected;

	public Isset __isset;

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

	public int Id
	{
		get
		{
			return _id;
		}
		set
		{
			__isset.id = true;
			_id = value;
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

	public bool PasswordProtected
	{
		get
		{
			return _passwordProtected;
		}
		set
		{
			__isset.passwordProtected = true;
			_passwordProtected = value;
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
			case 2:
				if (tField.Type == TType.I32)
				{
					Id = iprot.ReadI32();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 3:
				if (tField.Type == TType.I32)
				{
					RoomId = iprot.ReadI32();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 4:
				if (tField.Type == TType.I32)
				{
					ZoneId = iprot.ReadI32();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 5:
				if (tField.Type == TType.Bool)
				{
					Locked = iprot.ReadBool();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 6:
				if (tField.Type == TType.Bool)
				{
					PasswordProtected = iprot.ReadBool();
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
		TStruct struc = new TStruct("ThriftServerGame");
		oprot.WriteStructBegin(struc);
		TField field = default(TField);
		if (GameDetails != null && __isset.gameDetails)
		{
			field.Name = "gameDetails";
			field.Type = TType.Struct;
			field.ID = 1;
			oprot.WriteFieldBegin(field);
			GameDetails.Write(oprot);
			oprot.WriteFieldEnd();
		}
		if (__isset.id)
		{
			field.Name = "id";
			field.Type = TType.I32;
			field.ID = 2;
			oprot.WriteFieldBegin(field);
			oprot.WriteI32(Id);
			oprot.WriteFieldEnd();
		}
		if (__isset.roomId)
		{
			field.Name = "roomId";
			field.Type = TType.I32;
			field.ID = 3;
			oprot.WriteFieldBegin(field);
			oprot.WriteI32(RoomId);
			oprot.WriteFieldEnd();
		}
		if (__isset.zoneId)
		{
			field.Name = "zoneId";
			field.Type = TType.I32;
			field.ID = 4;
			oprot.WriteFieldBegin(field);
			oprot.WriteI32(ZoneId);
			oprot.WriteFieldEnd();
		}
		if (__isset.locked)
		{
			field.Name = "locked";
			field.Type = TType.Bool;
			field.ID = 5;
			oprot.WriteFieldBegin(field);
			oprot.WriteBool(Locked);
			oprot.WriteFieldEnd();
		}
		if (__isset.passwordProtected)
		{
			field.Name = "passwordProtected";
			field.Type = TType.Bool;
			field.ID = 6;
			oprot.WriteFieldBegin(field);
			oprot.WriteBool(PasswordProtected);
			oprot.WriteFieldEnd();
		}
		oprot.WriteFieldStop();
		oprot.WriteStructEnd();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder("ThriftServerGame(");
		stringBuilder.Append("GameDetails: ");
		stringBuilder.Append((GameDetails != null) ? GameDetails.ToString() : "<null>");
		stringBuilder.Append(",Id: ");
		stringBuilder.Append(Id);
		stringBuilder.Append(",RoomId: ");
		stringBuilder.Append(RoomId);
		stringBuilder.Append(",ZoneId: ");
		stringBuilder.Append(ZoneId);
		stringBuilder.Append(",Locked: ");
		stringBuilder.Append(Locked);
		stringBuilder.Append(",PasswordProtected: ");
		stringBuilder.Append(PasswordProtected);
		stringBuilder.Append(")");
		return stringBuilder.ToString();
	}
}
