using Electrotank.Electroserver5.Api.Helper;
using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class CreateRoomVariableRequest : EsRequest
	{
		private int ZoneId_;

		private int RoomId_;

		private string Name_;

		private EsObject Value_;

		private bool Locked_;

		private bool Persistent_;

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

		public EsObject Value
		{
			get
			{
				return Value_;
			}
			set
			{
				Value_ = value;
				Value_Set_ = true;
			}
		}

		private bool Value_Set_ { get; set; }

		public bool Locked
		{
			get
			{
				return Locked_;
			}
			set
			{
				Locked_ = value;
				Locked_Set_ = true;
			}
		}

		private bool Locked_Set_ { get; set; }

		public bool Persistent
		{
			get
			{
				return Persistent_;
			}
			set
			{
				Persistent_ = value;
				Persistent_Set_ = true;
			}
		}

		private bool Persistent_Set_ { get; set; }

		public CreateRoomVariableRequest()
		{
			base.MessageType = MessageType.CreateRoomVariableRequest;
		}

		public CreateRoomVariableRequest(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftCreateRoomVariableRequest thriftCreateRoomVariableRequest = new ThriftCreateRoomVariableRequest();
			if (ZoneId_Set_)
			{
				int zoneId = ZoneId;
				thriftCreateRoomVariableRequest.ZoneId = zoneId;
			}
			if (RoomId_Set_)
			{
				int roomId = RoomId;
				thriftCreateRoomVariableRequest.RoomId = roomId;
			}
			if (Name_Set_ && Name != null)
			{
				string name = Name;
				thriftCreateRoomVariableRequest.Name = name;
			}
			if (Value_Set_ && Value != null)
			{
				ThriftFlattenedEsObject value = EsObjectCodec.FlattenEsObject(Value).ToThrift() as ThriftFlattenedEsObject;
				thriftCreateRoomVariableRequest.Value = value;
			}
			if (Locked_Set_)
			{
				bool locked = Locked;
				thriftCreateRoomVariableRequest.Locked = locked;
			}
			if (Persistent_Set_)
			{
				bool persistent = Persistent;
				thriftCreateRoomVariableRequest.Persistent = persistent;
			}
			return thriftCreateRoomVariableRequest;
		}

		public override TBase NewThrift()
		{
			return new ThriftCreateRoomVariableRequest();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftCreateRoomVariableRequest thriftCreateRoomVariableRequest = (ThriftCreateRoomVariableRequest)t_;
			if (thriftCreateRoomVariableRequest.__isset.zoneId)
			{
				ZoneId = thriftCreateRoomVariableRequest.ZoneId;
			}
			if (thriftCreateRoomVariableRequest.__isset.roomId)
			{
				RoomId = thriftCreateRoomVariableRequest.RoomId;
			}
			if (thriftCreateRoomVariableRequest.__isset.name && thriftCreateRoomVariableRequest.Name != null)
			{
				Name = thriftCreateRoomVariableRequest.Name;
			}
			if (thriftCreateRoomVariableRequest.__isset.value && thriftCreateRoomVariableRequest.Value != null)
			{
				Value = EsObjectCodec.UnflattenEsObject(new FlattenedEsObject(thriftCreateRoomVariableRequest.Value));
			}
			if (thriftCreateRoomVariableRequest.__isset.locked)
			{
				Locked = thriftCreateRoomVariableRequest.Locked;
			}
			if (thriftCreateRoomVariableRequest.__isset.persistent)
			{
				Persistent = thriftCreateRoomVariableRequest.Persistent;
			}
		}
	}
}
