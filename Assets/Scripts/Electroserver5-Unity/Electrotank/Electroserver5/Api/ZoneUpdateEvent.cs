using System;
using Electrotank.Electroserver5.Api.Helper;
using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class ZoneUpdateEvent : EsEvent
	{
		private int ZoneId_;

		private ZoneUpdateAction? Action_;

		private int RoomId_;

		private int RoomCount_;

		private RoomListEntry RoomListEntry_;

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

		public ZoneUpdateAction? Action
		{
			get
			{
				return Action_;
			}
			set
			{
				Action_ = value;
				Action_Set_ = true;
			}
		}

		private bool Action_Set_ { get; set; }

		public int RoomId
		{
			get
			{
				return RoomId_;
			}
			set
			{
				RoomId_ = value;
				RoomId_Set_ = true;
			}
		}

		private bool RoomId_Set_ { get; set; }

		public int RoomCount
		{
			get
			{
				return RoomCount_;
			}
			set
			{
				RoomCount_ = value;
				RoomCount_Set_ = true;
			}
		}

		private bool RoomCount_Set_ { get; set; }

		public RoomListEntry RoomListEntry
		{
			get
			{
				return RoomListEntry_;
			}
			set
			{
				RoomListEntry_ = value;
				RoomListEntry_Set_ = true;
			}
		}

		private bool RoomListEntry_Set_ { get; set; }

		public ZoneUpdateEvent()
		{
			base.MessageType = MessageType.ZoneUpdateEvent;
		}

		public ZoneUpdateEvent(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftZoneUpdateEvent thriftZoneUpdateEvent = new ThriftZoneUpdateEvent();
			if (ZoneId_Set_)
			{
				int zoneId = ZoneId;
				thriftZoneUpdateEvent.ZoneId = zoneId;
			}
			if (Action_Set_ && Action.HasValue)
			{
				ThriftZoneUpdateAction action = (ThriftZoneUpdateAction)(object)ThriftUtil.EnumConvert(typeof(ThriftZoneUpdateAction), (Enum)(object)Action);
				thriftZoneUpdateEvent.Action = action;
			}
			if (RoomId_Set_)
			{
				int roomId = RoomId;
				thriftZoneUpdateEvent.RoomId = roomId;
			}
			if (RoomCount_Set_)
			{
				int roomCount = RoomCount;
				thriftZoneUpdateEvent.RoomCount = roomCount;
			}
			if (RoomListEntry_Set_ && RoomListEntry != null)
			{
				ThriftRoomListEntry roomListEntry = RoomListEntry.ToThrift() as ThriftRoomListEntry;
				thriftZoneUpdateEvent.RoomListEntry = roomListEntry;
			}
			return thriftZoneUpdateEvent;
		}

		public override TBase NewThrift()
		{
			return new ThriftZoneUpdateEvent();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftZoneUpdateEvent thriftZoneUpdateEvent = (ThriftZoneUpdateEvent)t_;
			if (thriftZoneUpdateEvent.__isset.zoneId)
			{
				ZoneId = thriftZoneUpdateEvent.ZoneId;
			}
			if (thriftZoneUpdateEvent.__isset.action)
			{
				Action = (ZoneUpdateAction)(object)ThriftUtil.EnumConvert(Nullable.GetUnderlyingType(typeof(ZoneUpdateAction?)), thriftZoneUpdateEvent.Action);
			}
			if (thriftZoneUpdateEvent.__isset.roomId)
			{
				RoomId = thriftZoneUpdateEvent.RoomId;
			}
			if (thriftZoneUpdateEvent.__isset.roomCount)
			{
				RoomCount = thriftZoneUpdateEvent.RoomCount;
			}
			if (thriftZoneUpdateEvent.__isset.roomListEntry && thriftZoneUpdateEvent.RoomListEntry != null)
			{
				RoomListEntry = new RoomListEntry(thriftZoneUpdateEvent.RoomListEntry);
			}
		}
	}
}
