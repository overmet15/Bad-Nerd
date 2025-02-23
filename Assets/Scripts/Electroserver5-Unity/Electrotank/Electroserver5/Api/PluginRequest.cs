using Electrotank.Electroserver5.Api.Helper;
using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class PluginRequest : EsRequest
	{
		private string PluginName_;

		private int ZoneId_;

		private int RoomId_;

		private int SessionKey_;

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

		public int SessionKey
		{
			get
			{
				return SessionKey_;
			}
			set
			{
				SessionKey_ = value;
				SessionKey_Set_ = true;
			}
		}

		private bool SessionKey_Set_ { get; set; }

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

		public PluginRequest()
		{
			base.MessageType = MessageType.PluginRequest;
			ZoneId = -1;
			ZoneId_Set_ = true;
			RoomId = -1;
			RoomId_Set_ = true;
		}

		public PluginRequest(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftPluginRequest thriftPluginRequest = new ThriftPluginRequest();
			if (PluginName_Set_ && PluginName != null)
			{
				string pluginName = PluginName;
				thriftPluginRequest.PluginName = pluginName;
			}
			if (ZoneId_Set_)
			{
				int zoneId = ZoneId;
				thriftPluginRequest.ZoneId = zoneId;
			}
			if (RoomId_Set_)
			{
				int roomId = RoomId;
				thriftPluginRequest.RoomId = roomId;
			}
			if (SessionKey_Set_)
			{
				int sessionKey = SessionKey;
				thriftPluginRequest.SessionKey = sessionKey;
			}
			if (Parameters_Set_ && Parameters != null)
			{
				ThriftFlattenedEsObject parameters = EsObjectCodec.FlattenEsObject(Parameters).ToThrift() as ThriftFlattenedEsObject;
				thriftPluginRequest.Parameters = parameters;
			}
			return thriftPluginRequest;
		}

		public override TBase NewThrift()
		{
			return new ThriftPluginRequest();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftPluginRequest thriftPluginRequest = (ThriftPluginRequest)t_;
			if (thriftPluginRequest.__isset.pluginName && thriftPluginRequest.PluginName != null)
			{
				PluginName = thriftPluginRequest.PluginName;
			}
			if (thriftPluginRequest.__isset.zoneId)
			{
				ZoneId = thriftPluginRequest.ZoneId;
			}
			if (thriftPluginRequest.__isset.roomId)
			{
				RoomId = thriftPluginRequest.RoomId;
			}
			if (thriftPluginRequest.__isset.sessionKey)
			{
				SessionKey = thriftPluginRequest.SessionKey;
			}
			if (thriftPluginRequest.__isset.parameters && thriftPluginRequest.Parameters != null)
			{
				Parameters = EsObjectCodec.UnflattenEsObject(new FlattenedEsObject(thriftPluginRequest.Parameters));
			}
		}
	}
}
