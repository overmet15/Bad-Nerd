using System.Collections.Generic;
using Electrotank.Electroserver5.Api.Helper;
using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class AggregatePluginMessageEvent : EsEvent
	{
		private string PluginName_;

		private List<EsObjectRO> EsObjects_;

		private int OriginZoneId_;

		private int OriginRoomId_;

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

		public List<EsObjectRO> EsObjects
		{
			get
			{
				return EsObjects_;
			}
			set
			{
				EsObjects_ = value;
				EsObjects_Set_ = true;
			}
		}

		private bool EsObjects_Set_ { get; set; }

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

		public AggregatePluginMessageEvent()
		{
			base.MessageType = MessageType.AggregatePluginMessageEvent;
		}

		public AggregatePluginMessageEvent(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftAggregatePluginMessageEvent thriftAggregatePluginMessageEvent = new ThriftAggregatePluginMessageEvent();
			if (PluginName_Set_ && PluginName != null)
			{
				string pluginName = PluginName;
				thriftAggregatePluginMessageEvent.PluginName = pluginName;
			}
			if (EsObjects_Set_ && EsObjects != null)
			{
				List<ThriftFlattenedEsObjectRO> list = new List<ThriftFlattenedEsObjectRO>();
				foreach (EsObjectRO esObject in EsObjects)
				{
					ThriftFlattenedEsObjectRO item = EsObjectCodec.FlattenEsObject(esObject).ToThrift() as ThriftFlattenedEsObjectRO;
					list.Add(item);
				}
				thriftAggregatePluginMessageEvent.EsObjects = list;
			}
			if (OriginZoneId_Set_)
			{
				int originZoneId = OriginZoneId;
				thriftAggregatePluginMessageEvent.OriginZoneId = originZoneId;
			}
			if (OriginRoomId_Set_)
			{
				int originRoomId = OriginRoomId;
				thriftAggregatePluginMessageEvent.OriginRoomId = originRoomId;
			}
			return thriftAggregatePluginMessageEvent;
		}

		public override TBase NewThrift()
		{
			return new ThriftAggregatePluginMessageEvent();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftAggregatePluginMessageEvent thriftAggregatePluginMessageEvent = (ThriftAggregatePluginMessageEvent)t_;
			if (thriftAggregatePluginMessageEvent.__isset.pluginName && thriftAggregatePluginMessageEvent.PluginName != null)
			{
				PluginName = thriftAggregatePluginMessageEvent.PluginName;
			}
			if (thriftAggregatePluginMessageEvent.__isset.esObjects && thriftAggregatePluginMessageEvent.EsObjects != null)
			{
				EsObjects = new List<EsObjectRO>();
				foreach (ThriftFlattenedEsObjectRO esObject in thriftAggregatePluginMessageEvent.EsObjects)
				{
					EsObjectRO item = EsObjectCodec.UnflattenEsObjectRO(new FlattenedEsObjectRO(esObject));
					EsObjects.Add(item);
				}
			}
			if (thriftAggregatePluginMessageEvent.__isset.originZoneId)
			{
				OriginZoneId = thriftAggregatePluginMessageEvent.OriginZoneId;
			}
			if (thriftAggregatePluginMessageEvent.__isset.originRoomId)
			{
				OriginRoomId = thriftAggregatePluginMessageEvent.OriginRoomId;
			}
		}
	}
}
