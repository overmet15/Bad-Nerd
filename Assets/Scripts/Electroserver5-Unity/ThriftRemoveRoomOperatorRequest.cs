using System;
using System.Text;
using Thrift.Protocol;

[Serializable]
public class ThriftRemoveRoomOperatorRequest : TBase
{
	[Serializable]
	public struct Isset
	{
		public bool zoneId;

		public bool roomId;

		public bool userName;
	}

	private int _zoneId;

	private int _roomId;

	private string _userName;

	public Isset __isset;

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
					ZoneId = iprot.ReadI32();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 2:
				if (tField.Type == TType.I32)
				{
					RoomId = iprot.ReadI32();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 3:
				if (tField.Type == TType.String)
				{
					UserName = iprot.ReadString();
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
		TStruct struc = new TStruct("ThriftRemoveRoomOperatorRequest");
		oprot.WriteStructBegin(struc);
		TField field = default(TField);
		if (__isset.zoneId)
		{
			field.Name = "zoneId";
			field.Type = TType.I32;
			field.ID = 1;
			oprot.WriteFieldBegin(field);
			oprot.WriteI32(ZoneId);
			oprot.WriteFieldEnd();
		}
		if (__isset.roomId)
		{
			field.Name = "roomId";
			field.Type = TType.I32;
			field.ID = 2;
			oprot.WriteFieldBegin(field);
			oprot.WriteI32(RoomId);
			oprot.WriteFieldEnd();
		}
		if (UserName != null && __isset.userName)
		{
			field.Name = "userName";
			field.Type = TType.String;
			field.ID = 3;
			oprot.WriteFieldBegin(field);
			oprot.WriteString(UserName);
			oprot.WriteFieldEnd();
		}
		oprot.WriteFieldStop();
		oprot.WriteStructEnd();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder("ThriftRemoveRoomOperatorRequest(");
		stringBuilder.Append("ZoneId: ");
		stringBuilder.Append(ZoneId);
		stringBuilder.Append(",RoomId: ");
		stringBuilder.Append(RoomId);
		stringBuilder.Append(",UserName: ");
		stringBuilder.Append(UserName);
		stringBuilder.Append(")");
		return stringBuilder.ToString();
	}
}
