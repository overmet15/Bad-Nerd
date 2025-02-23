using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class FindZoneAndRoomByNameRequest : EsRequest
	{
		private string ZoneName_;

		private string RoomName_;

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

		public string RoomName
		{
			get
			{
				return RoomName_;
			}
			set
			{
				RoomName_ = value;
				RoomName_Set_ = true;
			}
		}

		private bool RoomName_Set_ { get; set; }

		public FindZoneAndRoomByNameRequest()
		{
			base.MessageType = MessageType.FindZoneAndRoomByNameRequest;
		}

		public FindZoneAndRoomByNameRequest(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftFindZoneAndRoomByNameRequest thriftFindZoneAndRoomByNameRequest = new ThriftFindZoneAndRoomByNameRequest();
			if (ZoneName_Set_ && ZoneName != null)
			{
				string zoneName = ZoneName;
				thriftFindZoneAndRoomByNameRequest.ZoneName = zoneName;
			}
			if (RoomName_Set_ && RoomName != null)
			{
				string roomName = RoomName;
				thriftFindZoneAndRoomByNameRequest.RoomName = roomName;
			}
			return thriftFindZoneAndRoomByNameRequest;
		}

		public override TBase NewThrift()
		{
			return new ThriftFindZoneAndRoomByNameRequest();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftFindZoneAndRoomByNameRequest thriftFindZoneAndRoomByNameRequest = (ThriftFindZoneAndRoomByNameRequest)t_;
			if (thriftFindZoneAndRoomByNameRequest.__isset.zoneName && thriftFindZoneAndRoomByNameRequest.ZoneName != null)
			{
				ZoneName = thriftFindZoneAndRoomByNameRequest.ZoneName;
			}
			if (thriftFindZoneAndRoomByNameRequest.__isset.roomName && thriftFindZoneAndRoomByNameRequest.RoomName != null)
			{
				RoomName = thriftFindZoneAndRoomByNameRequest.RoomName;
			}
		}
	}
}
