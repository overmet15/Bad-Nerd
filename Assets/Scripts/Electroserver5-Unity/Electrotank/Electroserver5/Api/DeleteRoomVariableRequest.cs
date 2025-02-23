using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class DeleteRoomVariableRequest : EsRequest
	{
		private int ZoneId_;

		private int RoomId_;

		private string Name_;

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

		public string Name
		{
			get
			{
				return Name_;
			}
			set
			{
				Name_ = value;
				Name_Set_ = true;
			}
		}

		private bool Name_Set_ { get; set; }

		public DeleteRoomVariableRequest()
		{
			base.MessageType = MessageType.DeleteRoomVariableRequest;
		}

		public DeleteRoomVariableRequest(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftDeleteRoomVariableRequest thriftDeleteRoomVariableRequest = new ThriftDeleteRoomVariableRequest();
			if (ZoneId_Set_)
			{
				int zoneId = ZoneId;
				thriftDeleteRoomVariableRequest.ZoneId = zoneId;
			}
			if (RoomId_Set_)
			{
				int roomId = RoomId;
				thriftDeleteRoomVariableRequest.RoomId = roomId;
			}
			if (Name_Set_ && Name != null)
			{
				string name = Name;
				thriftDeleteRoomVariableRequest.Name = name;
			}
			return thriftDeleteRoomVariableRequest;
		}

		public override TBase NewThrift()
		{
			return new ThriftDeleteRoomVariableRequest();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftDeleteRoomVariableRequest thriftDeleteRoomVariableRequest = (ThriftDeleteRoomVariableRequest)t_;
			if (thriftDeleteRoomVariableRequest.__isset.zoneId)
			{
				ZoneId = thriftDeleteRoomVariableRequest.ZoneId;
			}
			if (thriftDeleteRoomVariableRequest.__isset.roomId)
			{
				RoomId = thriftDeleteRoomVariableRequest.RoomId;
			}
			if (thriftDeleteRoomVariableRequest.__isset.name && thriftDeleteRoomVariableRequest.Name != null)
			{
				Name = thriftDeleteRoomVariableRequest.Name;
			}
		}
	}
}
