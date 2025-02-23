using Electrotank.Electroserver5.Api.Helper;
using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class RequestDetails : EsEntity
	{
		private string PluginName_;

		private int RoomId_;

		private int ZoneId_;

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

		public RequestDetails()
		{
		}

		public RequestDetails(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftRequestDetails thriftRequestDetails = new ThriftRequestDetails();
			if (PluginName_Set_ && PluginName != null)
			{
				string pluginName = PluginName;
				thriftRequestDetails.PluginName = pluginName;
			}
			if (RoomId_Set_)
			{
				int roomId = RoomId;
				thriftRequestDetails.RoomId = roomId;
			}
			if (ZoneId_Set_)
			{
				int zoneId = ZoneId;
				thriftRequestDetails.ZoneId = zoneId;
			}
			if (Parameters_Set_ && Parameters != null)
			{
				ThriftFlattenedEsObject parameters = EsObjectCodec.FlattenEsObject(Parameters).ToThrift() as ThriftFlattenedEsObject;
				thriftRequestDetails.Parameters = parameters;
			}
			return thriftRequestDetails;
		}

		public override TBase NewThrift()
		{
			return new ThriftRequestDetails();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftRequestDetails thriftRequestDetails = (ThriftRequestDetails)t_;
			if (thriftRequestDetails.__isset.pluginName && thriftRequestDetails.PluginName != null)
			{
				PluginName = thriftRequestDetails.PluginName;
			}
			if (thriftRequestDetails.__isset.roomId)
			{
				RoomId = thriftRequestDetails.RoomId;
			}
			if (thriftRequestDetails.__isset.zoneId)
			{
				ZoneId = thriftRequestDetails.ZoneId;
			}
			if (thriftRequestDetails.__isset.parameters && thriftRequestDetails.Parameters != null)
			{
				Parameters = EsObjectCodec.UnflattenEsObject(new FlattenedEsObject(thriftRequestDetails.Parameters));
			}
		}
	}
}
