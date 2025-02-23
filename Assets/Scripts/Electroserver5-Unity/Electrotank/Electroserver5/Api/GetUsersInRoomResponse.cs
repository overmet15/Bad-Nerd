using System.Collections.Generic;
using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class GetUsersInRoomResponse : EsResponse
	{
		private int ZoneId_;

		private int RoomId_;

		private List<UserListEntry> Users_;

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

		public List<UserListEntry> Users
		{
			get
			{
				return Users_;
			}
			set
			{
				Users_ = value;
				Users_Set_ = true;
			}
		}

		private bool Users_Set_ { get; set; }

		public GetUsersInRoomResponse()
		{
			base.MessageType = MessageType.GetUsersInRoomResponse;
		}

		public GetUsersInRoomResponse(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftGetUsersInRoomResponse thriftGetUsersInRoomResponse = new ThriftGetUsersInRoomResponse();
			if (ZoneId_Set_)
			{
				int zoneId = ZoneId;
				thriftGetUsersInRoomResponse.ZoneId = zoneId;
			}
			if (RoomId_Set_)
			{
				int roomId = RoomId;
				thriftGetUsersInRoomResponse.RoomId = roomId;
			}
			if (Users_Set_ && Users != null)
			{
				List<ThriftUserListEntry> list = new List<ThriftUserListEntry>();
				foreach (UserListEntry user in Users)
				{
					ThriftUserListEntry item = user.ToThrift() as ThriftUserListEntry;
					list.Add(item);
				}
				thriftGetUsersInRoomResponse.Users = list;
			}
			return thriftGetUsersInRoomResponse;
		}

		public override TBase NewThrift()
		{
			return new ThriftGetUsersInRoomResponse();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftGetUsersInRoomResponse thriftGetUsersInRoomResponse = (ThriftGetUsersInRoomResponse)t_;
			if (thriftGetUsersInRoomResponse.__isset.zoneId)
			{
				ZoneId = thriftGetUsersInRoomResponse.ZoneId;
			}
			if (thriftGetUsersInRoomResponse.__isset.roomId)
			{
				RoomId = thriftGetUsersInRoomResponse.RoomId;
			}
			if (!thriftGetUsersInRoomResponse.__isset.users || thriftGetUsersInRoomResponse.Users == null)
			{
				return;
			}
			Users = new List<UserListEntry>();
			foreach (ThriftUserListEntry user in thriftGetUsersInRoomResponse.Users)
			{
				UserListEntry item = new UserListEntry(user);
				Users.Add(item);
			}
		}
	}
}
