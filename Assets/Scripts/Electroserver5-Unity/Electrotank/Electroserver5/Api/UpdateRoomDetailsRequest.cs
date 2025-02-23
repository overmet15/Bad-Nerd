using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class UpdateRoomDetailsRequest : EsRequest
	{
		private int ZoneId_;

		private int RoomId_;

		private bool CapacityUpdate_;

		private int Capacity_;

		private bool RoomDescriptionUpdate_;

		private string RoomDescription_;

		private bool RoomNameUpdate_;

		private string RoomName_;

		private bool PasswordUpdate_;

		private string Password_;

		private bool HiddenUpdate_;

		private bool Hidden_;

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

		public bool CapacityUpdate
		{
			get
			{
				return CapacityUpdate_;
			}
			set
			{
				CapacityUpdate_ = value;
				CapacityUpdate_Set_ = true;
			}
		}

		private bool CapacityUpdate_Set_ { get; set; }

		public int Capacity
		{
			get
			{
				return Capacity_;
			}
			set
			{
				Capacity_ = value;
				Capacity_Set_ = true;
			}
		}

		private bool Capacity_Set_ { get; set; }

		public bool RoomDescriptionUpdate
		{
			get
			{
				return RoomDescriptionUpdate_;
			}
			set
			{
				RoomDescriptionUpdate_ = value;
				RoomDescriptionUpdate_Set_ = true;
			}
		}

		private bool RoomDescriptionUpdate_Set_ { get; set; }

		public string RoomDescription
		{
			get
			{
				return RoomDescription_;
			}
			set
			{
				RoomDescription_ = value;
				RoomDescription_Set_ = true;
			}
		}

		private bool RoomDescription_Set_ { get; set; }

		public bool RoomNameUpdate
		{
			get
			{
				return RoomNameUpdate_;
			}
			set
			{
				RoomNameUpdate_ = value;
				RoomNameUpdate_Set_ = true;
			}
		}

		private bool RoomNameUpdate_Set_ { get; set; }

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

		public bool PasswordUpdate
		{
			get
			{
				return PasswordUpdate_;
			}
			set
			{
				PasswordUpdate_ = value;
				PasswordUpdate_Set_ = true;
			}
		}

		private bool PasswordUpdate_Set_ { get; set; }

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

		public bool HiddenUpdate
		{
			get
			{
				return HiddenUpdate_;
			}
			set
			{
				HiddenUpdate_ = value;
				HiddenUpdate_Set_ = true;
			}
		}

		private bool HiddenUpdate_Set_ { get; set; }

		public bool Hidden
		{
			get
			{
				return Hidden_;
			}
			set
			{
				Hidden_ = value;
				Hidden_Set_ = true;
			}
		}

		private bool Hidden_Set_ { get; set; }

		public UpdateRoomDetailsRequest()
		{
			base.MessageType = MessageType.UpdateRoomDetailsRequest;
		}

		public UpdateRoomDetailsRequest(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftUpdateRoomDetailsRequest thriftUpdateRoomDetailsRequest = new ThriftUpdateRoomDetailsRequest();
			if (ZoneId_Set_)
			{
				int zoneId = ZoneId;
				thriftUpdateRoomDetailsRequest.ZoneId = zoneId;
			}
			if (RoomId_Set_)
			{
				int roomId = RoomId;
				thriftUpdateRoomDetailsRequest.RoomId = roomId;
			}
			if (CapacityUpdate_Set_)
			{
				bool capacityUpdate = CapacityUpdate;
				thriftUpdateRoomDetailsRequest.CapacityUpdate = capacityUpdate;
			}
			if (Capacity_Set_)
			{
				int capacity = Capacity;
				thriftUpdateRoomDetailsRequest.Capacity = capacity;
			}
			if (RoomDescriptionUpdate_Set_)
			{
				bool roomDescriptionUpdate = RoomDescriptionUpdate;
				thriftUpdateRoomDetailsRequest.RoomDescriptionUpdate = roomDescriptionUpdate;
			}
			if (RoomDescription_Set_ && RoomDescription != null)
			{
				string roomDescription = RoomDescription;
				thriftUpdateRoomDetailsRequest.RoomDescription = roomDescription;
			}
			if (RoomNameUpdate_Set_)
			{
				bool roomNameUpdate = RoomNameUpdate;
				thriftUpdateRoomDetailsRequest.RoomNameUpdate = roomNameUpdate;
			}
			if (RoomName_Set_ && RoomName != null)
			{
				string roomName = RoomName;
				thriftUpdateRoomDetailsRequest.RoomName = roomName;
			}
			if (PasswordUpdate_Set_)
			{
				bool passwordUpdate = PasswordUpdate;
				thriftUpdateRoomDetailsRequest.PasswordUpdate = passwordUpdate;
			}
			if (Password_Set_ && Password != null)
			{
				string password = Password;
				thriftUpdateRoomDetailsRequest.Password = password;
			}
			if (HiddenUpdate_Set_)
			{
				bool hiddenUpdate = HiddenUpdate;
				thriftUpdateRoomDetailsRequest.HiddenUpdate = hiddenUpdate;
			}
			if (Hidden_Set_)
			{
				bool hidden = Hidden;
				thriftUpdateRoomDetailsRequest.Hidden = hidden;
			}
			return thriftUpdateRoomDetailsRequest;
		}

		public override TBase NewThrift()
		{
			return new ThriftUpdateRoomDetailsRequest();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftUpdateRoomDetailsRequest thriftUpdateRoomDetailsRequest = (ThriftUpdateRoomDetailsRequest)t_;
			if (thriftUpdateRoomDetailsRequest.__isset.zoneId)
			{
				ZoneId = thriftUpdateRoomDetailsRequest.ZoneId;
			}
			if (thriftUpdateRoomDetailsRequest.__isset.roomId)
			{
				RoomId = thriftUpdateRoomDetailsRequest.RoomId;
			}
			if (thriftUpdateRoomDetailsRequest.__isset.capacityUpdate)
			{
				CapacityUpdate = thriftUpdateRoomDetailsRequest.CapacityUpdate;
			}
			if (thriftUpdateRoomDetailsRequest.__isset.capacity)
			{
				Capacity = thriftUpdateRoomDetailsRequest.Capacity;
			}
			if (thriftUpdateRoomDetailsRequest.__isset.roomDescriptionUpdate)
			{
				RoomDescriptionUpdate = thriftUpdateRoomDetailsRequest.RoomDescriptionUpdate;
			}
			if (thriftUpdateRoomDetailsRequest.__isset.roomDescription && thriftUpdateRoomDetailsRequest.RoomDescription != null)
			{
				RoomDescription = thriftUpdateRoomDetailsRequest.RoomDescription;
			}
			if (thriftUpdateRoomDetailsRequest.__isset.roomNameUpdate)
			{
				RoomNameUpdate = thriftUpdateRoomDetailsRequest.RoomNameUpdate;
			}
			if (thriftUpdateRoomDetailsRequest.__isset.roomName && thriftUpdateRoomDetailsRequest.RoomName != null)
			{
				RoomName = thriftUpdateRoomDetailsRequest.RoomName;
			}
			if (thriftUpdateRoomDetailsRequest.__isset.passwordUpdate)
			{
				PasswordUpdate = thriftUpdateRoomDetailsRequest.PasswordUpdate;
			}
			if (thriftUpdateRoomDetailsRequest.__isset.password && thriftUpdateRoomDetailsRequest.Password != null)
			{
				Password = thriftUpdateRoomDetailsRequest.Password;
			}
			if (thriftUpdateRoomDetailsRequest.__isset.hiddenUpdate)
			{
				HiddenUpdate = thriftUpdateRoomDetailsRequest.HiddenUpdate;
			}
			if (thriftUpdateRoomDetailsRequest.__isset.hidden)
			{
				Hidden = thriftUpdateRoomDetailsRequest.Hidden;
			}
		}
	}
}
