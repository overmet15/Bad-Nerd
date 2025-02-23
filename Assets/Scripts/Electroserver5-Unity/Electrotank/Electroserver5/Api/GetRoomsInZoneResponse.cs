using System.Collections.Generic;
using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class GetRoomsInZoneResponse : EsResponse
	{
		private int ZoneId_;

		private string ZoneName_;

		private List<RoomListEntry> Entries_;

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

		public List<RoomListEntry> Entries
		{
			get
			{
				return Entries_;
			}
			set
			{
				Entries_ = value;
				Entries_Set_ = true;
			}
		}

		private bool Entries_Set_ { get; set; }

		public GetRoomsInZoneResponse()
		{
			base.MessageType = MessageType.GetRoomsInZoneResponse;
		}

		public GetRoomsInZoneResponse(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftGetRoomsInZoneResponse thriftGetRoomsInZoneResponse = new ThriftGetRoomsInZoneResponse();
			if (ZoneId_Set_)
			{
				int zoneId = ZoneId;
				thriftGetRoomsInZoneResponse.ZoneId = zoneId;
			}
			if (ZoneName_Set_ && ZoneName != null)
			{
				string zoneName = ZoneName;
				thriftGetRoomsInZoneResponse.ZoneName = zoneName;
			}
			if (Entries_Set_ && Entries != null)
			{
				List<ThriftRoomListEntry> list = new List<ThriftRoomListEntry>();
				foreach (RoomListEntry entry in Entries)
				{
					ThriftRoomListEntry item = entry.ToThrift() as ThriftRoomListEntry;
					list.Add(item);
				}
				thriftGetRoomsInZoneResponse.Entries = list;
			}
			return thriftGetRoomsInZoneResponse;
		}

		public override TBase NewThrift()
		{
			return new ThriftGetRoomsInZoneResponse();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftGetRoomsInZoneResponse thriftGetRoomsInZoneResponse = (ThriftGetRoomsInZoneResponse)t_;
			if (thriftGetRoomsInZoneResponse.__isset.zoneId)
			{
				ZoneId = thriftGetRoomsInZoneResponse.ZoneId;
			}
			if (thriftGetRoomsInZoneResponse.__isset.zoneName && thriftGetRoomsInZoneResponse.ZoneName != null)
			{
				ZoneName = thriftGetRoomsInZoneResponse.ZoneName;
			}
			if (!thriftGetRoomsInZoneResponse.__isset.entries || thriftGetRoomsInZoneResponse.Entries == null)
			{
				return;
			}
			Entries = new List<RoomListEntry>();
			foreach (ThriftRoomListEntry entry in thriftGetRoomsInZoneResponse.Entries)
			{
				RoomListEntry item = new RoomListEntry(entry);
				Entries.Add(item);
			}
		}
	}
}
