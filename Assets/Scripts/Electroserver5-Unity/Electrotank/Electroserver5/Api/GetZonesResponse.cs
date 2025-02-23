using System.Collections.Generic;
using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class GetZonesResponse : EsResponse
	{
		private List<ZoneListEntry> Zones_;

		public List<ZoneListEntry> Zones
		{
			get
			{
				return Zones_;
			}
			set
			{
				Zones_ = value;
				Zones_Set_ = true;
			}
		}

		private bool Zones_Set_ { get; set; }

		public GetZonesResponse()
		{
			base.MessageType = MessageType.GetZonesResponse;
		}

		public GetZonesResponse(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftGetZonesResponse thriftGetZonesResponse = new ThriftGetZonesResponse();
			if (Zones_Set_ && Zones != null)
			{
				List<ThriftZoneListEntry> list = new List<ThriftZoneListEntry>();
				foreach (ZoneListEntry zone in Zones)
				{
					ThriftZoneListEntry item = zone.ToThrift() as ThriftZoneListEntry;
					list.Add(item);
				}
				thriftGetZonesResponse.Zones = list;
			}
			return thriftGetZonesResponse;
		}

		public override TBase NewThrift()
		{
			return new ThriftGetZonesResponse();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftGetZonesResponse thriftGetZonesResponse = (ThriftGetZonesResponse)t_;
			if (!thriftGetZonesResponse.__isset.zones || thriftGetZonesResponse.Zones == null)
			{
				return;
			}
			Zones = new List<ZoneListEntry>();
			foreach (ThriftZoneListEntry zone in thriftGetZonesResponse.Zones)
			{
				ZoneListEntry item = new ZoneListEntry(zone);
				Zones.Add(item);
			}
		}
	}
}
