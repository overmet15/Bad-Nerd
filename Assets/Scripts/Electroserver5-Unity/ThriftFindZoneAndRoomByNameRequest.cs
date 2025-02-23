using System;
using System.Text;
using Thrift.Protocol;

[Serializable]
public class ThriftFindZoneAndRoomByNameRequest : TBase
{
	[Serializable]
	public struct Isset
	{
		public bool zoneName;

		public bool roomName;
	}

	private string _zoneName;

	private string _roomName;

	public Isset __isset;

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

	public string RoomName
	{
		get
		{
			return _roomName;
		}
		set
		{
			__isset.roomName = true;
			_roomName = value;
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
					ZoneName = iprot.ReadString();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 2:
				if (tField.Type == TType.String)
				{
					RoomName = iprot.ReadString();
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
		TStruct struc = new TStruct("ThriftFindZoneAndRoomByNameRequest");
		oprot.WriteStructBegin(struc);
		TField field = default(TField);
		if (ZoneName != null && __isset.zoneName)
		{
			field.Name = "zoneName";
			field.Type = TType.String;
			field.ID = 1;
			oprot.WriteFieldBegin(field);
			oprot.WriteString(ZoneName);
			oprot.WriteFieldEnd();
		}
		if (RoomName != null && __isset.roomName)
		{
			field.Name = "roomName";
			field.Type = TType.String;
			field.ID = 2;
			oprot.WriteFieldBegin(field);
			oprot.WriteString(RoomName);
			oprot.WriteFieldEnd();
		}
		oprot.WriteFieldStop();
		oprot.WriteStructEnd();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder("ThriftFindZoneAndRoomByNameRequest(");
		stringBuilder.Append("ZoneName: ");
		stringBuilder.Append(ZoneName);
		stringBuilder.Append(",RoomName: ");
		stringBuilder.Append(RoomName);
		stringBuilder.Append(")");
		return stringBuilder.ToString();
	}
}
