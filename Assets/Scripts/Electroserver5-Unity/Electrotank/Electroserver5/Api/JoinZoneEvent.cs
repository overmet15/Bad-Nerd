using System.Collections.Generic;
using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class JoinZoneEvent : EsEvent
	{
		private int ZoneId_;

		private string ZoneName_;

		private List<RoomListEntry> Rooms_;

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

		public List<RoomListEntry> Rooms
		{
			get
			{
				return Rooms_;
			}
			set
			{
				Rooms_ = value;
				Rooms_Set_ = true;
			}
		}

		private bool Rooms_Set_ { get; set; }

		public JoinZoneEvent()
		{
			base.MessageType = MessageType.JoinZoneEvent;
		}

		public JoinZoneEvent(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftJoinZoneEvent thriftJoinZoneEvent = new ThriftJoinZoneEvent();
			if (ZoneId_Set_)
			{
				int zoneId = ZoneId;
				thriftJoinZoneEvent.ZoneId = zoneId;
			}
			if (ZoneName_Set_ && ZoneName != null)
			{
				string zoneName = ZoneName;
				thriftJoinZoneEvent.ZoneName = zoneName;
			}
			if (Rooms_Set_ && Rooms != null)
			{
				List<ThriftRoomListEntry> list = new List<ThriftRoomListEntry>();
				foreach (RoomListEntry room in Rooms)
				{
					ThriftRoomListEntry item = room.ToThrift() as ThriftRoomListEntry;
					list.Add(item);
				}
				thriftJoinZoneEvent.Rooms = list;
			}
			return thriftJoinZoneEvent;
		}

		public override TBase NewThrift()
		{
			return new ThriftJoinZoneEvent();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftJoinZoneEvent thriftJoinZoneEvent = (ThriftJoinZoneEvent)t_;
			if (thriftJoinZoneEvent.__isset.zoneId)
			{
				ZoneId = thriftJoinZoneEvent.ZoneId;
			}
			if (thriftJoinZoneEvent.__isset.zoneName && thriftJoinZoneEvent.ZoneName != null)
			{
				ZoneName = thriftJoinZoneEvent.ZoneName;
			}
			if (!thriftJoinZoneEvent.__isset.rooms || thriftJoinZoneEvent.Rooms == null)
			{
				return;
			}
			Rooms = new List<RoomListEntry>();
			foreach (ThriftRoomListEntry room in thriftJoinZoneEvent.Rooms)
			{
				RoomListEntry item = new RoomListEntry(room);
				Rooms.Add(item);
			}
		}
	}
}
