using Electrotank.Electroserver5.Api.Helper;
using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class PluginMessageEvent : EsEvent
	{
		private string PluginName_;

		private bool SentToRoom_;

		private int DestinationZoneId_;

		private int DestinationRoomId_;

		private bool RoomLevelPlugin_;

		private int OriginZoneId_;

		private int OriginRoomId_;

		private EsObject Parameters_;

		public string PluginName
		{
			get
			{
				return PluginName_;
			}
			set
			{
				PluginName_ = value;
				PluginName_Set_ = true;
			}
		}

		private bool PluginName_Set_ { get; set; }

		public bool SentToRoom
		{
			get
			{
				return SentToRoom_;
			}
			set
			{
				SentToRoom_ = value;
				SentToRoom_Set_ = true;
			}
		}

		private bool SentToRoom_Set_ { get; set; }

		public int DestinationZoneId
		{
			get
			{
				return DestinationZoneId_;
			}
			set
			{
				DestinationZoneId_ = value;
				DestinationZoneId_Set_ = true;
			}
		}

		private bool DestinationZoneId_Set_ { get; set; }

		public int DestinationRoomId
		{
			get
			{
				return DestinationRoomId_;
			}
			set
			{
				DestinationRoomId_ = value;
				DestinationRoomId_Set_ = true;
			}
		}

		private bool DestinationRoomId_Set_ { get; set; }

		public bool RoomLevelPlugin
		{
			get
			{
				return RoomLevelPlugin_;
			}
			set
			{
				RoomLevelPlugin_ = value;
				RoomLevelPlugin_Set_ = true;
			}
		}

		private bool RoomLevelPlugin_Set_ { get; set; }

		public int OriginZoneId
		{
			get
			{
				return OriginZoneId_;
			}
			set
			{
				OriginZoneId_ = value;
				OriginZoneId_Set_ = true;
			}
		}

		private bool OriginZoneId_Set_ { get; set; }

		public int OriginRoomId
		{
			get
			{
				return OriginRoomId_;
			}
			set
			{
				OriginRoomId_ = value;
				OriginRoomId_Set_ = true;
			}
		}

		private bool OriginRoomId_Set_ { get; set; }

		public EsObject Parameters
		{
			get
			{
				return Parameters_;
			}
			set
			{
				Parameters_ = value;
				Parameters_Set_ = true;
			}
		}

		private bool Parameters_Set_ { get; set; }

		public PluginMessageEvent()
		{
			base.MessageType = MessageType.PluginMessageEvent;
		}

		public PluginMessageEvent(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftPluginMessageEvent thriftPluginMessageEvent = new ThriftPluginMessageEvent();
			if (PluginName_Set_ && PluginName != null)
			{
				string pluginName = PluginName;
				thriftPluginMessageEvent.PluginName = pluginName;
			}
			if (SentToRoom_Set_)
			{
				bool sentToRoom = SentToRoom;
				thriftPluginMessageEvent.SentToRoom = sentToRoom;
			}
			if (DestinationZoneId_Set_)
			{
				int destinationZoneId = DestinationZoneId;
				thriftPluginMessageEvent.DestinationZoneId = destinationZoneId;
			}
			if (DestinationRoomId_Set_)
			{
				int destinationRoomId = DestinationRoomId;
				thriftPluginMessageEvent.DestinationRoomId = destinationRoomId;
			}
			if (RoomLevelPlugin_Set_)
			{
				bool roomLevelPlugin = RoomLevelPlugin;
				thriftPluginMessageEvent.RoomLevelPlugin = roomLevelPlugin;
			}
			if (OriginZoneId_Set_)
			{
				int originZoneId = OriginZoneId;
				thriftPluginMessageEvent.OriginZoneId = originZoneId;
			}
			if (OriginRoomId_Set_)
			{
				int originRoomId = OriginRoomId;
				thriftPluginMessageEvent.OriginRoomId = originRoomId;
			}
			if (Parameters_Set_ && Parameters != null)
			{
				ThriftFlattenedEsObject parameters = EsObjectCodec.FlattenEsObject(Parameters).ToThrift() as ThriftFlattenedEsObject;
				thriftPluginMessageEvent.Parameters = parameters;
			}
			return thriftPluginMessageEvent;
		}

		public override TBase NewThrift()
		{
			return new ThriftPluginMessageEvent();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftPluginMessageEvent thriftPluginMessageEvent = (ThriftPluginMessageEvent)t_;
			if (thriftPluginMessageEvent.__isset.pluginName && thriftPluginMessageEvent.PluginName != null)
			{
				PluginName = thriftPluginMessageEvent.PluginName;
			}
			if (thriftPluginMessageEvent.__isset.sentToRoom)
			{
				SentToRoom = thriftPluginMessageEvent.SentToRoom;
			}
			if (thriftPluginMessageEvent.__isset.destinationZoneId)
			{
				DestinationZoneId = thriftPluginMessageEvent.DestinationZoneId;
			}
			if (thriftPluginMessageEvent.__isset.destinationRoomId)
			{
				DestinationRoomId = thriftPluginMessageEvent.DestinationRoomId;
			}
			if (thriftPluginMessageEvent.__isset.roomLevelPlugin)
			{
				RoomLevelPlugin = thriftPluginMessageEvent.RoomLevelPlugin;
			}
			if (thriftPluginMessageEvent.__isset.originZoneId)
			{
				OriginZoneId = thriftPluginMessageEvent.OriginZoneId;
			}
			if (thriftPluginMessageEvent.__isset.originRoomId)
			{
				OriginRoomId = thriftPluginMessageEvent.OriginRoomId;
			}
			if (thriftPluginMessageEvent.__isset.parameters && thriftPluginMessageEvent.Parameters != null)
			{
				Parameters = EsObjectCodec.UnflattenEsObject(new FlattenedEsObject(thriftPluginMessageEvent.Parameters));
			}
		}
	}
}
