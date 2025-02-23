using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class UpdateRoomDetailsEvent : EsEvent
	{
		private int ZoneId_;

		private int RoomId_;

		private bool CapacityUpdated_;

		private int Capacity_;

		private bool RoomDescriptionUpdated_;

		private string RoomDescription_;

		private bool RoomNameUpdated_;

		private string RoomName_;

		private bool HasPassword_;

		private bool HasPasswordUpdated_;

		private bool HiddenUpdated_;

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

		public bool CapacityUpdated
		{
			get
			{
				return CapacityUpdated_;
			}
			set
			{
				CapacityUpdated_ = value;
				CapacityUpdated_Set_ = true;
			}
		}

		private bool CapacityUpdated_Set_ { get; set; }

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

		public bool RoomDescriptionUpdated
		{
			get
			{
				return RoomDescriptionUpdated_;
			}
			set
			{
				RoomDescriptionUpdated_ = value;
				RoomDescriptionUpdated_Set_ = true;
			}
		}

		private bool RoomDescriptionUpdated_Set_ { get; set; }

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

		public bool RoomNameUpdated
		{
			get
			{
				return RoomNameUpdated_;
			}
			set
			{
				RoomNameUpdated_ = value;
				RoomNameUpdated_Set_ = true;
			}
		}

		private bool RoomNameUpdated_Set_ { get; set; }

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

		public bool HasPassword
		{
			get
			{
				return HasPassword_;
			}
			set
			{
				HasPassword_ = value;
				HasPassword_Set_ = true;
			}
		}

		private bool HasPassword_Set_ { get; set; }

		public bool HasPasswordUpdated
		{
			get
			{
				return HasPasswordUpdated_;
			}
			set
			{
				HasPasswordUpdated_ = value;
				HasPasswordUpdated_Set_ = true;
			}
		}

		private bool HasPasswordUpdated_Set_ { get; set; }

		public bool HiddenUpdated
		{
			get
			{
				return HiddenUpdated_;
			}
			set
			{
				HiddenUpdated_ = value;
				HiddenUpdated_Set_ = true;
			}
		}

		private bool HiddenUpdated_Set_ { get; set; }

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

		public UpdateRoomDetailsEvent()
		{
			base.MessageType = MessageType.UpdateRoomDetailsEvent;
		}

		public UpdateRoomDetailsEvent(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftUpdateRoomDetailsEvent thriftUpdateRoomDetailsEvent = new ThriftUpdateRoomDetailsEvent();
			if (ZoneId_Set_)
			{
				int zoneId = ZoneId;
				thriftUpdateRoomDetailsEvent.ZoneId = zoneId;
			}
			if (RoomId_Set_)
			{
				int roomId = RoomId;
				thriftUpdateRoomDetailsEvent.RoomId = roomId;
			}
			if (CapacityUpdated_Set_)
			{
				bool capacityUpdated = CapacityUpdated;
				thriftUpdateRoomDetailsEvent.CapacityUpdated = capacityUpdated;
			}
			if (Capacity_Set_)
			{
				int capacity = Capacity;
				thriftUpdateRoomDetailsEvent.Capacity = capacity;
			}
			if (RoomDescriptionUpdated_Set_)
			{
				bool roomDescriptionUpdated = RoomDescriptionUpdated;
				thriftUpdateRoomDetailsEvent.RoomDescriptionUpdated = roomDescriptionUpdated;
			}
			if (RoomDescription_Set_ && RoomDescription != null)
			{
				string roomDescription = RoomDescription;
				thriftUpdateRoomDetailsEvent.RoomDescription = roomDescription;
			}
			if (RoomNameUpdated_Set_)
			{
				bool roomNameUpdated = RoomNameUpdated;
				thriftUpdateRoomDetailsEvent.RoomNameUpdated = roomNameUpdated;
			}
			if (RoomName_Set_ && RoomName != null)
			{
				string roomName = RoomName;
				thriftUpdateRoomDetailsEvent.RoomName = roomName;
			}
			if (HasPassword_Set_)
			{
				bool hasPassword = HasPassword;
				thriftUpdateRoomDetailsEvent.HasPassword = hasPassword;
			}
			if (HasPasswordUpdated_Set_)
			{
				bool hasPasswordUpdated = HasPasswordUpdated;
				thriftUpdateRoomDetailsEvent.HasPasswordUpdated = hasPasswordUpdated;
			}
			if (HiddenUpdated_Set_)
			{
				bool hiddenUpdated = HiddenUpdated;
				thriftUpdateRoomDetailsEvent.HiddenUpdated = hiddenUpdated;
			}
			if (Hidden_Set_)
			{
				bool hidden = Hidden;
				thriftUpdateRoomDetailsEvent.Hidden = hidden;
			}
			return thriftUpdateRoomDetailsEvent;
		}

		public override TBase NewThrift()
		{
			return new ThriftUpdateRoomDetailsEvent();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftUpdateRoomDetailsEvent thriftUpdateRoomDetailsEvent = (ThriftUpdateRoomDetailsEvent)t_;
			if (thriftUpdateRoomDetailsEvent.__isset.zoneId)
			{
				ZoneId = thriftUpdateRoomDetailsEvent.ZoneId;
			}
			if (thriftUpdateRoomDetailsEvent.__isset.roomId)
			{
				RoomId = thriftUpdateRoomDetailsEvent.RoomId;
			}
			if (thriftUpdateRoomDetailsEvent.__isset.capacityUpdated)
			{
				CapacityUpdated = thriftUpdateRoomDetailsEvent.CapacityUpdated;
			}
			if (thriftUpdateRoomDetailsEvent.__isset.capacity)
			{
				Capacity = thriftUpdateRoomDetailsEvent.Capacity;
			}
			if (thriftUpdateRoomDetailsEvent.__isset.roomDescriptionUpdated)
			{
				RoomDescriptionUpdated = thriftUpdateRoomDetailsEvent.RoomDescriptionUpdated;
			}
			if (thriftUpdateRoomDetailsEvent.__isset.roomDescription && thriftUpdateRoomDetailsEvent.RoomDescription != null)
			{
				RoomDescription = thriftUpdateRoomDetailsEvent.RoomDescription;
			}
			if (thriftUpdateRoomDetailsEvent.__isset.roomNameUpdated)
			{
				RoomNameUpdated = thriftUpdateRoomDetailsEvent.RoomNameUpdated;
			}
			if (thriftUpdateRoomDetailsEvent.__isset.roomName && thriftUpdateRoomDetailsEvent.RoomName != null)
			{
				RoomName = thriftUpdateRoomDetailsEvent.RoomName;
			}
			if (thriftUpdateRoomDetailsEvent.__isset.hasPassword)
			{
				HasPassword = thriftUpdateRoomDetailsEvent.HasPassword;
			}
			if (thriftUpdateRoomDetailsEvent.__isset.hasPasswordUpdated)
			{
				HasPasswordUpdated = thriftUpdateRoomDetailsEvent.HasPasswordUpdated;
			}
			if (thriftUpdateRoomDetailsEvent.__isset.hiddenUpdated)
			{
				HiddenUpdated = thriftUpdateRoomDetailsEvent.HiddenUpdated;
			}
			if (thriftUpdateRoomDetailsEvent.__isset.hidden)
			{
				Hidden = thriftUpdateRoomDetailsEvent.Hidden;
			}
		}
	}
}
