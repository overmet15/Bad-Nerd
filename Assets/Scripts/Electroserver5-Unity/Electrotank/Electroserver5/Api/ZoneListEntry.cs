using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class ZoneListEntry : EsEntity
	{
		private int ZoneId_;

		private string ZoneName_;

		public int ZoneId
		{
			get
			{
				return ZoneId_;
			}
			set
			{
				ZoneId_ = value;
				ZoneId_Set_ = true;
			}
		}

		private bool ZoneId_Set_ { get; set; }

		public string ZoneName
		{
			get
			{
				return ZoneName_;
			}
			set
			{
				ZoneName_ = value;
				ZoneName_Set_ = true;
			}
		}

		private bool ZoneName_Set_ { get; set; }

		public ZoneListEntry()
		{
		}

		public ZoneListEntry(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftZoneListEntry thriftZoneListEntry = new ThriftZoneListEntry();
			if (ZoneId_Set_)
			{
				int zoneId = ZoneId;
				thriftZoneListEntry.ZoneId = zoneId;
			}
			if (ZoneName_Set_ && ZoneName != null)
			{
				string zoneName = ZoneName;
				thriftZoneListEntry.ZoneName = zoneName;
			}
			return thriftZoneListEntry;
		}

		public override TBase NewThrift()
		{
			return new ThriftZoneListEntry();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftZoneListEntry thriftZoneListEntry = (ThriftZoneListEntry)t_;
			if (thriftZoneListEntry.__isset.zoneId)
			{
				ZoneId = thriftZoneListEntry.ZoneId;
			}
			if (thriftZoneListEntry.__isset.zoneName && thriftZoneListEntry.ZoneName != null)
			{
				ZoneName = thriftZoneListEntry.ZoneName;
			}
		}
	}
}
