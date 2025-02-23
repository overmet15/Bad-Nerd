using System.Collections.Generic;
using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class UserListEntry : EsEntity
	{
		private string UserName_;

		private List<UserVariable> UserVariables_;

		private bool SendingVideo_;

		private string VideoStreamName_;

		private bool RoomOperator_;

		public string UserName
		{
			get
			{
				return UserName_;
			}
			set
			{
				UserName_ = value;
				UserName_Set_ = true;
			}
		}

		private bool UserName_Set_ { get; set; }

		public List<UserVariable> UserVariables
		{
			get
			{
				return UserVariables_;
			}
			set
			{
				UserVariables_ = value;
				UserVariables_Set_ = true;
			}
		}

		private bool UserVariables_Set_ { get; set; }

		public bool SendingVideo
		{
			get
			{
				return SendingVideo_;
			}
			set
			{
				SendingVideo_ = value;
				SendingVideo_Set_ = true;
			}
		}

		private bool SendingVideo_Set_ { get; set; }

		public string VideoStreamName
		{
			get
			{
				return VideoStreamName_;
			}
			set
			{
				VideoStreamName_ = value;
				VideoStreamName_Set_ = true;
			}
		}

		private bool VideoStreamName_Set_ { get; set; }

		public bool RoomOperator
		{
			get
			{
				return RoomOperator_;
			}
			set
			{
				RoomOperator_ = value;
				RoomOperator_Set_ = true;
			}
		}

		private bool RoomOperator_Set_ { get; set; }

		public UserListEntry()
		{
		}

		public UserListEntry(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftUserListEntry thriftUserListEntry = new ThriftUserListEntry();
			if (UserName_Set_ && UserName != null)
			{
				string userName = UserName;
				thriftUserListEntry.UserName = userName;
			}
			if (UserVariables_Set_ && UserVariables != null)
			{
				List<ThriftUserVariable> list = new List<ThriftUserVariable>();
				foreach (UserVariable userVariable in UserVariables)
				{
					ThriftUserVariable item = userVariable.ToThrift() as ThriftUserVariable;
					list.Add(item);
				}
				thriftUserListEntry.UserVariables = list;
			}
			if (SendingVideo_Set_)
			{
				bool sendingVideo = SendingVideo;
				thriftUserListEntry.SendingVideo = sendingVideo;
			}
			if (VideoStreamName_Set_ && VideoStreamName != null)
			{
				string videoStreamName = VideoStreamName;
				thriftUserListEntry.VideoStreamName = videoStreamName;
			}
			if (RoomOperator_Set_)
			{
				bool roomOperator = RoomOperator;
				thriftUserListEntry.RoomOperator = roomOperator;
			}
			return thriftUserListEntry;
		}

		public override TBase NewThrift()
		{
			return new ThriftUserListEntry();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftUserListEntry thriftUserListEntry = (ThriftUserListEntry)t_;
			if (thriftUserListEntry.__isset.userName && thriftUserListEntry.UserName != null)
			{
				UserName = thriftUserListEntry.UserName;
			}
			if (thriftUserListEntry.__isset.userVariables && thriftUserListEntry.UserVariables != null)
			{
				UserVariables = new List<UserVariable>();
				foreach (ThriftUserVariable userVariable in thriftUserListEntry.UserVariables)
				{
					UserVariable item = new UserVariable(userVariable);
					UserVariables.Add(item);
				}
			}
			if (thriftUserListEntry.__isset.sendingVideo)
			{
				SendingVideo = thriftUserListEntry.SendingVideo;
			}
			if (thriftUserListEntry.__isset.videoStreamName && thriftUserListEntry.VideoStreamName != null)
			{
				VideoStreamName = thriftUserListEntry.VideoStreamName;
			}
			if (thriftUserListEntry.__isset.roomOperator)
			{
				RoomOperator = thriftUserListEntry.RoomOperator;
			}
		}
	}
}
