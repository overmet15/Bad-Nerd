using System;
using System.Collections.Generic;
using System.Text;
using Thrift.Protocol;

[Serializable]
public class ThriftGetZonesResponse : TBase
{
	[Serializable]
	public struct Isset
	{
		public bool zones;
	}

	private List<ThriftZoneListEntry> _zones;

	public Isset __isset;

	public List<ThriftZoneListEntry> Zones
	{
		get
		{
			return _zones;
		}
		set
		{
			__isset.zones = true;
			_zones = value;
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
					Zones = new List<ThriftZoneListEntry>();
					TList tList = iprot.ReadListBegin();
					for (int i = 0; i < tList.Count; i++)
					{
						ThriftZoneListEntry thriftZoneListEntry = new ThriftZoneListEntry();
						thriftZoneListEntry = new ThriftZoneListEntry();
						thriftZoneListEntry.Read(iprot);
						Zones.Add(thriftZoneListEntry);
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
		TStruct struc = new TStruct("ThriftGetZonesResponse");
		oprot.WriteStructBegin(struc);
		TField field = default(TField);
		if (Zones != null && __isset.zones)
		{
			field.Name = "zones";
			field.Type = TType.List;
			field.ID = 1;
			oprot.WriteFieldBegin(field);
			oprot.WriteListBegin(new TList(TType.Struct, Zones.Count));
			foreach (ThriftZoneListEntry zone in Zones)
			{
				zone.Write(oprot);
				oprot.WriteListEnd();
			}
			oprot.WriteFieldEnd();
		}
		oprot.WriteFieldStop();
		oprot.WriteStructEnd();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder("ThriftGetZonesResponse(");
		stringBuilder.Append("Zones: ");
		stringBuilder.Append(Zones);
		stringBuilder.Append(")");
		return stringBuilder.ToString();
	}
}
