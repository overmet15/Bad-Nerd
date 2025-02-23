using System;
using System.Collections.Generic;
using System.Text;
using Thrift.Protocol;

[Serializable]
public class ThriftFindZoneAndRoomByNameResponse : TBase
{
	[Serializable]
	public struct Isset
	{
		public bool roomAndZoneList;
	}

	private List<List<int>> _roomAndZoneList;

	public Isset __isset;

	public List<List<int>> RoomAndZoneList
	{
		get
		{
			return _roomAndZoneList;
		}
		set
		{
			__isset.roomAndZoneList = true;
			_roomAndZoneList = value;
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
			short iD = tField.ID;
			if (iD == 1)
			{
				if (tField.Type == TType.List)
				{
					RoomAndZoneList = new List<List<int>>();
					TList tList = iprot.ReadListBegin();
					for (int i = 0; i < tList.Count; i++)
					{
						List<int> list = new List<int>();
						list = new List<int>();
						TList tList2 = iprot.ReadListBegin();
						for (int j = 0; j < tList2.Count; j++)
						{
							int num = 0;
							num = iprot.ReadI32();
							list.Add(num);
						}
						iprot.ReadListEnd();
						RoomAndZoneList.Add(list);
					}
					iprot.ReadListEnd();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
			}
			else
			{
				TProtocolUtil.Skip(iprot, tField.Type);
			}
			iprot.ReadFieldEnd();
		}
		iprot.ReadStructEnd();
	}

	public void Write(TProtocol oprot)
	{
		TStruct struc = new TStruct("ThriftFindZoneAndRoomByNameResponse");
		oprot.WriteStructBegin(struc);
		TField field = default(TField);
		if (RoomAndZoneList != null && __isset.roomAndZoneList)
		{
			field.Name = "roomAndZoneList";
			field.Type = TType.List;
			field.ID = 1;
			oprot.WriteFieldBegin(field);
			oprot.WriteListBegin(new TList(TType.List, RoomAndZoneList.Count));
			foreach (List<int> roomAndZone in RoomAndZoneList)
			{
				oprot.WriteListBegin(new TList(TType.I32, roomAndZone.Count));
				foreach (int item in roomAndZone)
				{
					oprot.WriteI32(item);
					oprot.WriteListEnd();
				}
				oprot.WriteListEnd();
			}
			oprot.WriteFieldEnd();
		}
		oprot.WriteFieldStop();
		oprot.WriteStructEnd();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder("ThriftFindZoneAndRoomByNameResponse(");
		stringBuilder.Append("RoomAndZoneList: ");
		stringBuilder.Append(RoomAndZoneList);
		stringBuilder.Append(")");
		return stringBuilder.ToString();
	}
}
