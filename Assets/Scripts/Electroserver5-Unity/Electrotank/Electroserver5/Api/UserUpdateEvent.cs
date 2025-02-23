using System;
using System.Collections.Generic;
using Electrotank.Electroserver5.Api.Helper;
using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class UserUpdateEvent : EsEvent
	{
		private int ZoneId_;

		private int RoomId_;

		private UserUpdateAction? Action_;

		private string UserName_;

		private List<UserVariable> UserVariables_;

		private bool SendingVideo_;

		private string VideoStreamName_;

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

		public UserUpdateAction? Action
		{
			get
			{
				return Action_;
			}
			set
			{
				Action_ = value;
				Action_Set_ = true;
			}
		}

		private bool Action_Set_ { get; set; }

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

		public UserUpdateEvent()
		{
			base.MessageType = MessageType.UserUpdateEvent;
		}

		public UserUpdateEvent(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftUserUpdateEvent thriftUserUpdateEvent = new ThriftUserUpdateEvent();
			if (ZoneId_Set_)
			{
				int zoneId = ZoneId;
				thriftUserUpdateEvent.ZoneId = zoneId;
			}
			if (RoomId_Set_)
			{
				int roomId = RoomId;
				thriftUserUpdateEvent.RoomId = roomId;
			}
			if (Action_Set_ && Action.HasValue)
			{
				ThriftUserUpdateAction action = (ThriftUserUpdateAction)(object)ThriftUtil.EnumConvert(typeof(ThriftUserUpdateAction), (Enum)(object)Action);
				thriftUserUpdateEvent.Action = action;
			}
			if (UserName_Set_ && UserName != null)
			{
				string userName = UserName;
				thriftUserUpdateEvent.UserName = userName;
			}
			if (UserVariables_Set_ && UserVariables != null)
			{
				List<ThriftUserVariable> list = new List<ThriftUserVariable>();
				foreach (UserVariable userVariable in UserVariables)
				{
					ThriftUserVariable item = userVariable.ToThrift() as ThriftUserVariable;
					list.Add(item);
				}
				thriftUserUpdateEvent.UserVariables = list;
			}
			if (SendingVideo_Set_)
			{
				bool sendingVideo = SendingVideo;
				thriftUserUpdateEvent.SendingVideo = sendingVideo;
			}
			if (VideoStreamName_Set_ && VideoStreamName != null)
			{
				string videoStreamName = VideoStreamName;
				thriftUserUpdateEvent.VideoStreamName = videoStreamName;
			}
			return thriftUserUpdateEvent;
		}

		public override TBase NewThrift()
		{
			return new ThriftUserUpdateEvent();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftUserUpdateEvent thriftUserUpdateEvent = (ThriftUserUpdateEvent)t_;
			if (thriftUserUpdateEvent.__isset.zoneId)
			{
				ZoneId = thriftUserUpdateEvent.ZoneId;
			}
			if (thriftUserUpdateEvent.__isset.roomId)
			{
				RoomId = thriftUserUpdateEvent.RoomId;
			}
			if (thriftUserUpdateEvent.__isset.action)
			{
				Action = (UserUpdateAction)(object)ThriftUtil.EnumConvert(Nullable.GetUnderlyingType(typeof(UserUpdateAction?)), thriftUserUpdateEvent.Action);
			}
			if (thriftUserUpdateEvent.__isset.userName && thriftUserUpdateEvent.UserName != null)
			{
				UserName = thriftUserUpdateEvent.UserName;
			}
			if (thriftUserUpdateEvent.__isset.userVariables && thriftUserUpdateEvent.UserVariables != null)
			{
				UserVariables = new List<UserVariable>();
				foreach (ThriftUserVariable userVariable in thriftUserUpdateEvent.UserVariables)
				{
					UserVariable item = new UserVariable(userVariable);
					UserVariables.Add(item);
				}
			}
			if (thriftUserUpdateEvent.__isset.sendingVideo)
			{
				SendingVideo = thriftUserUpdateEvent.SendingVideo;
			}
			if (thriftUserUpdateEvent.__isset.videoStreamName && thriftUserUpdateEvent.VideoStreamName != null)
			{
				VideoStreamName = thriftUserUpdateEvent.VideoStreamName;
			}
		}
	}
}
