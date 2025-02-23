using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class JoinRoomRequest : EsRequest
	{
		private string ZoneName_;

		private string RoomName_;

		private int ZoneId_;

		private int RoomId_;

		private string Password_;

		private bool ReceivingRoomListUpdates_;

		private bool ReceivingRoomAttributeUpdates_;

		private bool ReceivingUserListUpdates_;

		private bool ReceivingUserVariableUpdates_;

		private bool ReceivingRoomVariableUpdates_;

		private bool ReceivingVideoEvents_;

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

		public string Password
		{
			get
			{
				return Password_;
			}
			set
			{
				Password_ = value;
				Password_Set_ = true;
			}
		}

		private bool Password_Set_ { get; set; }

		public bool ReceivingRoomListUpdates
		{
			get
			{
				return ReceivingRoomListUpdates_;
			}
			set
			{
				ReceivingRoomListUpdates_ = value;
				ReceivingRoomListUpdates_Set_ = true;
			}
		}

		private bool ReceivingRoomListUpdates_Set_ { get; set; }

		public bool ReceivingRoomAttributeUpdates
		{
			get
			{
				return ReceivingRoomAttributeUpdates_;
			}
			set
			{
				ReceivingRoomAttributeUpdates_ = value;
				ReceivingRoomAttributeUpdates_Set_ = true;
			}
		}

		private bool ReceivingRoomAttributeUpdates_Set_ { get; set; }

		public bool ReceivingUserListUpdates
		{
			get
			{
				return ReceivingUserListUpdates_;
			}
			set
			{
				ReceivingUserListUpdates_ = value;
				ReceivingUserListUpdates_Set_ = true;
			}
		}

		private bool ReceivingUserListUpdates_Set_ { get; set; }

		public bool ReceivingUserVariableUpdates
		{
			get
			{
				return ReceivingUserVariableUpdates_;
			}
			set
			{
				ReceivingUserVariableUpdates_ = value;
				ReceivingUserVariableUpdates_Set_ = true;
			}
		}

		private bool ReceivingUserVariableUpdates_Set_ { get; set; }

		public bool ReceivingRoomVariableUpdates
		{
			get
			{
				return ReceivingRoomVariableUpdates_;
			}
			set
			{
				ReceivingRoomVariableUpdates_ = value;
				ReceivingRoomVariableUpdates_Set_ = true;
			}
		}

		private bool ReceivingRoomVariableUpdates_Set_ { get; set; }

		public bool ReceivingVideoEvents
		{
			get
			{
				return ReceivingVideoEvents_;
			}
			set
			{
				ReceivingVideoEvents_ = value;
				ReceivingVideoEvents_Set_ = true;
			}
		}

		private bool ReceivingVideoEvents_Set_ { get; set; }

		public JoinRoomRequest()
		{
			base.MessageType = MessageType.JoinRoomRequest;
			ZoneId = -1;
			ZoneId_Set_ = true;
			RoomId = -1;
			RoomId_Set_ = true;
			ReceivingRoomListUpdates = true;
			ReceivingRoomListUpdates_Set_ = true;
			ReceivingRoomAttributeUpdates = true;
			ReceivingRoomAttributeUpdates_Set_ = true;
			ReceivingUserListUpdates = true;
			ReceivingUserListUpdates_Set_ = true;
			ReceivingUserVariableUpdates = true;
			ReceivingUserVariableUpdates_Set_ = true;
			ReceivingRoomVariableUpdates = true;
			ReceivingRoomVariableUpdates_Set_ = true;
			ReceivingVideoEvents = true;
			ReceivingVideoEvents_Set_ = true;
		}

		public JoinRoomRequest(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftJoinRoomRequest thriftJoinRoomRequest = new ThriftJoinRoomRequest();
			if (ZoneName_Set_ && ZoneName != null)
			{
				string zoneName = ZoneName;
				thriftJoinRoomRequest.ZoneName = zoneName;
			}
			if (RoomName_Set_ && RoomName != null)
			{
				string roomName = RoomName;
				thriftJoinRoomRequest.RoomName = roomName;
			}
			if (ZoneId_Set_)
			{
				int zoneId = ZoneId;
				thriftJoinRoomRequest.ZoneId = zoneId;
			}
			if (RoomId_Set_)
			{
				int roomId = RoomId;
				thriftJoinRoomRequest.RoomId = roomId;
			}
			if (Password_Set_ && Password != null)
			{
				string password = Password;
				thriftJoinRoomRequest.Password = password;
			}
			if (ReceivingRoomListUpdates_Set_)
			{
				bool receivingRoomListUpdates = ReceivingRoomListUpdates;
				thriftJoinRoomRequest.ReceivingRoomListUpdates = receivingRoomListUpdates;
			}
			if (ReceivingRoomAttributeUpdates_Set_)
			{
				bool receivingRoomAttributeUpdates = ReceivingRoomAttributeUpdates;
				thriftJoinRoomRequest.ReceivingRoomAttributeUpdates = receivingRoomAttributeUpdates;
			}
			if (ReceivingUserListUpdates_Set_)
			{
				bool receivingUserListUpdates = ReceivingUserListUpdates;
				thriftJoinRoomRequest.ReceivingUserListUpdates = receivingUserListUpdates;
			}
			if (ReceivingUserVariableUpdates_Set_)
			{
				bool receivingUserVariableUpdates = ReceivingUserVariableUpdates;
				thriftJoinRoomRequest.ReceivingUserVariableUpdates = receivingUserVariableUpdates;
			}
			if (ReceivingRoomVariableUpdates_Set_)
			{
				bool receivingRoomVariableUpdates = ReceivingRoomVariableUpdates;
				thriftJoinRoomRequest.ReceivingRoomVariableUpdates = receivingRoomVariableUpdates;
			}
			if (ReceivingVideoEvents_Set_)
			{
				bool receivingVideoEvents = ReceivingVideoEvents;
				thriftJoinRoomRequest.ReceivingVideoEvents = receivingVideoEvents;
			}
			return thriftJoinRoomRequest;
		}

		public override TBase NewThrift()
		{
			return new ThriftJoinRoomRequest();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftJoinRoomRequest thriftJoinRoomRequest = (ThriftJoinRoomRequest)t_;
			if (thriftJoinRoomRequest.__isset.zoneName && thriftJoinRoomRequest.ZoneName != null)
			{
				ZoneName = thriftJoinRoomRequest.ZoneName;
			}
			if (thriftJoinRoomRequest.__isset.roomName && thriftJoinRoomRequest.RoomName != null)
			{
				RoomName = thriftJoinRoomRequest.RoomName;
			}
			if (thriftJoinRoomRequest.__isset.zoneId)
			{
				ZoneId = thriftJoinRoomRequest.ZoneId;
			}
			if (thriftJoinRoomRequest.__isset.roomId)
			{
				RoomId = thriftJoinRoomRequest.RoomId;
			}
			if (thriftJoinRoomRequest.__isset.password && thriftJoinRoomRequest.Password != null)
			{
				Password = thriftJoinRoomRequest.Password;
			}
			if (thriftJoinRoomRequest.__isset.receivingRoomListUpdates)
			{
				ReceivingRoomListUpdates = thriftJoinRoomRequest.ReceivingRoomListUpdates;
			}
			if (thriftJoinRoomRequest.__isset.receivingRoomAttributeUpdates)
			{
				ReceivingRoomAttributeUpdates = thriftJoinRoomRequest.ReceivingRoomAttributeUpdates;
			}
			if (thriftJoinRoomRequest.__isset.receivingUserListUpdates)
			{
				ReceivingUserListUpdates = thriftJoinRoomRequest.ReceivingUserListUpdates;
			}
			if (thriftJoinRoomRequest.__isset.receivingUserVariableUpdates)
			{
				ReceivingUserVariableUpdates = thriftJoinRoomRequest.ReceivingUserVariableUpdates;
			}
			if (thriftJoinRoomRequest.__isset.receivingRoomVariableUpdates)
			{
				ReceivingRoomVariableUpdates = thriftJoinRoomRequest.ReceivingRoomVariableUpdates;
			}
			if (thriftJoinRoomRequest.__isset.receivingVideoEvents)
			{
				ReceivingVideoEvents = thriftJoinRoomRequest.ReceivingVideoEvents;
			}
		}
	}
}
