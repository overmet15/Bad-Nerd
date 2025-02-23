using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class GetRoomsInZoneRequest : EsRequest
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

		public GetRoomsInZoneRequest()
		{
			base.MessageType = MessageType.GetRoomsInZoneRequest;
			ZoneId = -1;
			ZoneId_Set_ = true;
		}

		public GetRoomsInZoneRequest(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftGetRoomsInZoneRequest thriftGetRoomsInZoneRequest = new ThriftGetRoomsInZoneRequest();
			if (ZoneId_Set_)
			{
				int zoneId = ZoneId;
				thriftGetRoomsInZoneRequest.ZoneId = zoneId;
			}
			if (ZoneName_Set_ && ZoneName != null)
			{
				string zoneName = ZoneName;
				thriftGetRoomsInZoneRequest.ZoneName = zoneName;
			}
			return thriftGetRoomsInZoneRequest;
		}

		public override TBase NewThrift()
		{
			return new ThriftGetRoomsInZoneRequest();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftGetRoomsInZoneRequest thriftGetRoomsInZoneRequest = (ThriftGetRoomsInZoneRequest)t_;
			if (thriftGetRoomsInZoneRequest.__isset.zoneId)
			{
				ZoneId = thriftGetRoomsInZoneRequest.ZoneId;
			}
			if (thriftGetRoomsInZoneRequest.__isset.zoneName && thriftGetRoomsInZoneRequest.ZoneName != null)
			{
				ZoneName = thriftGetRoomsInZoneRequest.ZoneName;
			}
		}
	}
}
