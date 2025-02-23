using System.Collections.Generic;
using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class CreateRoomRequest : EsRequest
	{
		private string ZoneName_;

		private int ZoneId_;

		private string RoomName_;

		private int Capacity_;

		private string Password_;

		private string RoomDescription_;

		private bool Persistent_;

		private bool Hidden_;

		private bool ReceivingRoomListUpdates_;

		private bool ReceivingRoomAttributeUpdates_;

		private bool ReceivingUserListUpdates_;

		private bool ReceivingUserVariableUpdates_;

		private bool ReceivingRoomVariableUpdates_;

		private bool ReceivingVideoEvents_;

		private bool NonOperatorUpdateAllowed_;

		private bool NonOperatorVariableUpdateAllowed_;

		private bool CreateOrJoinRoom_;

		private List<PluginListEntry> Plugins_;

		private List<RoomVariable> Variables_;

		private bool UsingLanguageFilter_;

		private bool LanguageFilterSpecified_;

		private string LanguageFilterName_;

		private bool LanguageFilterDeliverMessageOnFailure_;

		private int LanguageFilterFailuresBeforeKick_;

		private int LanguageFilterKicksBeforeBan_;

		private int LanguageFilterBanDuration_;

		private bool LanguageFilterResetAfterKick_;

		private bool UsingFloodingFilter_;

		private bool FloodingFilterSpecified_;

		private string FloodingFilterName_;

		private int FloodingFilterFailuresBeforeKick_;

		private int FloodingFilterKicksBeforeBan_;

		private int FloodingFilterBanDuration_;

		private bool FloodingFilterResetAfterKick_;

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

		public bool NonOperatorUpdateAllowed
		{
			get
			{
				return NonOperatorUpdateAllowed_;
			}
			set
			{
				NonOperatorUpdateAllowed_ = value;
				NonOperatorUpdateAllowed_Set_ = true;
			}
		}

		private bool NonOperatorUpdateAllowed_Set_ { get; set; }

		public bool NonOperatorVariableUpdateAllowed
		{
			get
			{
				return NonOperatorVariableUpdateAllowed_;
			}
			set
			{
				NonOperatorVariableUpdateAllowed_ = value;
				NonOperatorVariableUpdateAllowed_Set_ = true;
			}
		}

		private bool NonOperatorVariableUpdateAllowed_Set_ { get; set; }

		public bool CreateOrJoinRoom
		{
			get
			{
				return CreateOrJoinRoom_;
			}
			set
			{
				CreateOrJoinRoom_ = value;
				CreateOrJoinRoom_Set_ = true;
			}
		}

		private bool CreateOrJoinRoom_Set_ { get; set; }

		public List<PluginListEntry> Plugins
		{
			get
			{
				return Plugins_;
			}
			set
			{
				Plugins_ = value;
				Plugins_Set_ = true;
			}
		}

		private bool Plugins_Set_ { get; set; }

		public List<RoomVariable> Variables
		{
			get
			{
				return Variables_;
			}
			set
			{
				Variables_ = value;
				Variables_Set_ = true;
			}
		}

		private bool Variables_Set_ { get; set; }

		public bool UsingLanguageFilter
		{
			get
			{
				return UsingLanguageFilter_;
			}
			set
			{
				UsingLanguageFilter_ = value;
				UsingLanguageFilter_Set_ = true;
			}
		}

		private bool UsingLanguageFilter_Set_ { get; set; }

		public bool LanguageFilterSpecified
		{
			get
			{
				return LanguageFilterSpecified_;
			}
			set
			{
				LanguageFilterSpecified_ = value;
				LanguageFilterSpecified_Set_ = true;
			}
		}

		private bool LanguageFilterSpecified_Set_ { get; set; }

		public string LanguageFilterName
		{
			get
			{
				return LanguageFilterName_;
			}
			set
			{
				LanguageFilterName_ = value;
				LanguageFilterName_Set_ = true;
			}
		}

		private bool LanguageFilterName_Set_ { get; set; }

		public bool LanguageFilterDeliverMessageOnFailure
		{
			get
			{
				return LanguageFilterDeliverMessageOnFailure_;
			}
			set
			{
				LanguageFilterDeliverMessageOnFailure_ = value;
				LanguageFilterDeliverMessageOnFailure_Set_ = true;
			}
		}

		private bool LanguageFilterDeliverMessageOnFailure_Set_ { get; set; }

		public int LanguageFilterFailuresBeforeKick
		{
			get
			{
				return LanguageFilterFailuresBeforeKick_;
			}
			set
			{
				LanguageFilterFailuresBeforeKick_ = value;
				LanguageFilterFailuresBeforeKick_Set_ = true;
			}
		}

		private bool LanguageFilterFailuresBeforeKick_Set_ { get; set; }

		public int LanguageFilterKicksBeforeBan
		{
			get
			{
				return LanguageFilterKicksBeforeBan_;
			}
			set
			{
				LanguageFilterKicksBeforeBan_ = value;
				LanguageFilterKicksBeforeBan_Set_ = true;
			}
		}

		private bool LanguageFilterKicksBeforeBan_Set_ { get; set; }

		public int LanguageFilterBanDuration
		{
			get
			{
				return LanguageFilterBanDuration_;
			}
			set
			{
				LanguageFilterBanDuration_ = value;
				LanguageFilterBanDuration_Set_ = true;
			}
		}

		private bool LanguageFilterBanDuration_Set_ { get; set; }

		public bool LanguageFilterResetAfterKick
		{
			get
			{
				return LanguageFilterResetAfterKick_;
			}
			set
			{
				LanguageFilterResetAfterKick_ = value;
				LanguageFilterResetAfterKick_Set_ = true;
			}
		}

		private bool LanguageFilterResetAfterKick_Set_ { get; set; }

		public bool UsingFloodingFilter
		{
			get
			{
				return UsingFloodingFilter_;
			}
			set
			{
				UsingFloodingFilter_ = value;
				UsingFloodingFilter_Set_ = true;
			}
		}

		private bool UsingFloodingFilter_Set_ { get; set; }

		public bool FloodingFilterSpecified
		{
			get
			{
				return FloodingFilterSpecified_;
			}
			set
			{
				FloodingFilterSpecified_ = value;
				FloodingFilterSpecified_Set_ = true;
			}
		}

		private bool FloodingFilterSpecified_Set_ { get; set; }

		public string FloodingFilterName
		{
			get
			{
				return FloodingFilterName_;
			}
			set
			{
				FloodingFilterName_ = value;
				FloodingFilterName_Set_ = true;
			}
		}

		private bool FloodingFilterName_Set_ { get; set; }

		public int FloodingFilterFailuresBeforeKick
		{
			get
			{
				return FloodingFilterFailuresBeforeKick_;
			}
			set
			{
				FloodingFilterFailuresBeforeKick_ = value;
				FloodingFilterFailuresBeforeKick_Set_ = true;
			}
		}

		private bool FloodingFilterFailuresBeforeKick_Set_ { get; set; }

		public int FloodingFilterKicksBeforeBan
		{
			get
			{
				return FloodingFilterKicksBeforeBan_;
			}
			set
			{
				FloodingFilterKicksBeforeBan_ = value;
				FloodingFilterKicksBeforeBan_Set_ = true;
			}
		}

		private bool FloodingFilterKicksBeforeBan_Set_ { get; set; }

		public int FloodingFilterBanDuration
		{
			get
			{
				return FloodingFilterBanDuration_;
			}
			set
			{
				FloodingFilterBanDuration_ = value;
				FloodingFilterBanDuration_Set_ = true;
			}
		}

		private bool FloodingFilterBanDuration_Set_ { get; set; }

		public bool FloodingFilterResetAfterKick
		{
			get
			{
				return FloodingFilterResetAfterKick_;
			}
			set
			{
				FloodingFilterResetAfterKick_ = value;
				FloodingFilterResetAfterKick_Set_ = true;
			}
		}

		private bool FloodingFilterResetAfterKick_Set_ { get; set; }

		public CreateRoomRequest()
		{
			base.MessageType = MessageType.CreateRoomRequest;
			ZoneId = -1;
			ZoneId_Set_ = true;
			Capacity = -1;
			Capacity_Set_ = true;
			Persistent = false;
			Persistent_Set_ = true;
			Hidden = false;
			Hidden_Set_ = true;
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
			NonOperatorUpdateAllowed = true;
			NonOperatorUpdateAllowed_Set_ = true;
			NonOperatorVariableUpdateAllowed = true;
			NonOperatorVariableUpdateAllowed_Set_ = true;
			CreateOrJoinRoom = true;
			CreateOrJoinRoom_Set_ = true;
			UsingLanguageFilter = false;
			UsingLanguageFilter_Set_ = true;
			LanguageFilterSpecified = false;
			LanguageFilterSpecified_Set_ = true;
			LanguageFilterDeliverMessageOnFailure = false;
			LanguageFilterDeliverMessageOnFailure_Set_ = true;
			LanguageFilterFailuresBeforeKick = 3;
			LanguageFilterFailuresBeforeKick_Set_ = true;
			LanguageFilterKicksBeforeBan = 3;
			LanguageFilterKicksBeforeBan_Set_ = true;
			LanguageFilterBanDuration = -1;
			LanguageFilterBanDuration_Set_ = true;
			LanguageFilterResetAfterKick = false;
			LanguageFilterResetAfterKick_Set_ = true;
			UsingFloodingFilter = false;
			UsingFloodingFilter_Set_ = true;
			FloodingFilterSpecified = false;
			FloodingFilterSpecified_Set_ = true;
			FloodingFilterFailuresBeforeKick = 1;
			FloodingFilterFailuresBeforeKick_Set_ = true;
			FloodingFilterKicksBeforeBan = 3;
			FloodingFilterKicksBeforeBan_Set_ = true;
			FloodingFilterBanDuration = -1;
			FloodingFilterBanDuration_Set_ = true;
			FloodingFilterResetAfterKick = false;
			FloodingFilterResetAfterKick_Set_ = true;
		}

		public CreateRoomRequest(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftCreateRoomRequest thriftCreateRoomRequest = new ThriftCreateRoomRequest();
			if (ZoneName_Set_ && ZoneName != null)
			{
				string zoneName = ZoneName;
				thriftCreateRoomRequest.ZoneName = zoneName;
			}
			if (ZoneId_Set_)
			{
				int zoneId = ZoneId;
				thriftCreateRoomRequest.ZoneId = zoneId;
			}
			if (RoomName_Set_ && RoomName != null)
			{
				string roomName = RoomName;
				thriftCreateRoomRequest.RoomName = roomName;
			}
			if (Capacity_Set_)
			{
				int capacity = Capacity;
				thriftCreateRoomRequest.Capacity = capacity;
			}
			if (Password_Set_ && Password != null)
			{
				string password = Password;
				thriftCreateRoomRequest.Password = password;
			}
			if (RoomDescription_Set_ && RoomDescription != null)
			{
				string roomDescription = RoomDescription;
				thriftCreateRoomRequest.RoomDescription = roomDescription;
			}
			if (Persistent_Set_)
			{
				bool persistent = Persistent;
				thriftCreateRoomRequest.Persistent = persistent;
			}
			if (Hidden_Set_)
			{
				bool hidden = Hidden;
				thriftCreateRoomRequest.Hidden = hidden;
			}
			if (ReceivingRoomListUpdates_Set_)
			{
				bool receivingRoomListUpdates = ReceivingRoomListUpdates;
				thriftCreateRoomRequest.ReceivingRoomListUpdates = receivingRoomListUpdates;
			}
			if (ReceivingRoomAttributeUpdates_Set_)
			{
				bool receivingRoomAttributeUpdates = ReceivingRoomAttributeUpdates;
				thriftCreateRoomRequest.ReceivingRoomAttributeUpdates = receivingRoomAttributeUpdates;
			}
			if (ReceivingUserListUpdates_Set_)
			{
				bool receivingUserListUpdates = ReceivingUserListUpdates;
				thriftCreateRoomRequest.ReceivingUserListUpdates = receivingUserListUpdates;
			}
			if (ReceivingUserVariableUpdates_Set_)
			{
				bool receivingUserVariableUpdates = ReceivingUserVariableUpdates;
				thriftCreateRoomRequest.ReceivingUserVariableUpdates = receivingUserVariableUpdates;
			}
			if (ReceivingRoomVariableUpdates_Set_)
			{
				bool receivingRoomVariableUpdates = ReceivingRoomVariableUpdates;
				thriftCreateRoomRequest.ReceivingRoomVariableUpdates = receivingRoomVariableUpdates;
			}
			if (ReceivingVideoEvents_Set_)
			{
				bool receivingVideoEvents = ReceivingVideoEvents;
				thriftCreateRoomRequest.ReceivingVideoEvents = receivingVideoEvents;
			}
			if (NonOperatorUpdateAllowed_Set_)
			{
				bool nonOperatorUpdateAllowed = NonOperatorUpdateAllowed;
				thriftCreateRoomRequest.NonOperatorUpdateAllowed = nonOperatorUpdateAllowed;
			}
			if (NonOperatorVariableUpdateAllowed_Set_)
			{
				bool nonOperatorVariableUpdateAllowed = NonOperatorVariableUpdateAllowed;
				thriftCreateRoomRequest.NonOperatorVariableUpdateAllowed = nonOperatorVariableUpdateAllowed;
			}
			if (CreateOrJoinRoom_Set_)
			{
				bool createOrJoinRoom = CreateOrJoinRoom;
				thriftCreateRoomRequest.CreateOrJoinRoom = createOrJoinRoom;
			}
			if (Plugins_Set_ && Plugins != null)
			{
				List<ThriftPluginListEntry> list = new List<ThriftPluginListEntry>();
				foreach (PluginListEntry plugin in Plugins)
				{
					ThriftPluginListEntry item = plugin.ToThrift() as ThriftPluginListEntry;
					list.Add(item);
				}
				thriftCreateRoomRequest.Plugins = list;
			}
			if (Variables_Set_ && Variables != null)
			{
				List<ThriftRoomVariable> list2 = new List<ThriftRoomVariable>();
				foreach (RoomVariable variable in Variables)
				{
					ThriftRoomVariable item2 = variable.ToThrift() as ThriftRoomVariable;
					list2.Add(item2);
				}
				thriftCreateRoomRequest.Variables = list2;
			}
			if (UsingLanguageFilter_Set_)
			{
				bool usingLanguageFilter = UsingLanguageFilter;
				thriftCreateRoomRequest.UsingLanguageFilter = usingLanguageFilter;
			}
			if (LanguageFilterSpecified_Set_)
			{
				bool languageFilterSpecified = LanguageFilterSpecified;
				thriftCreateRoomRequest.LanguageFilterSpecified = languageFilterSpecified;
			}
			if (LanguageFilterName_Set_ && LanguageFilterName != null)
			{
				string languageFilterName = LanguageFilterName;
				thriftCreateRoomRequest.LanguageFilterName = languageFilterName;
			}
			if (LanguageFilterDeliverMessageOnFailure_Set_)
			{
				bool languageFilterDeliverMessageOnFailure = LanguageFilterDeliverMessageOnFailure;
				thriftCreateRoomRequest.LanguageFilterDeliverMessageOnFailure = languageFilterDeliverMessageOnFailure;
			}
			if (LanguageFilterFailuresBeforeKick_Set_)
			{
				int languageFilterFailuresBeforeKick = LanguageFilterFailuresBeforeKick;
				thriftCreateRoomRequest.LanguageFilterFailuresBeforeKick = languageFilterFailuresBeforeKick;
			}
			if (LanguageFilterKicksBeforeBan_Set_)
			{
				int languageFilterKicksBeforeBan = LanguageFilterKicksBeforeBan;
				thriftCreateRoomRequest.LanguageFilterKicksBeforeBan = languageFilterKicksBeforeBan;
			}
			if (LanguageFilterBanDuration_Set_)
			{
				int languageFilterBanDuration = LanguageFilterBanDuration;
				thriftCreateRoomRequest.LanguageFilterBanDuration = languageFilterBanDuration;
			}
			if (LanguageFilterResetAfterKick_Set_)
			{
				bool languageFilterResetAfterKick = LanguageFilterResetAfterKick;
				thriftCreateRoomRequest.LanguageFilterResetAfterKick = languageFilterResetAfterKick;
			}
			if (UsingFloodingFilter_Set_)
			{
				bool usingFloodingFilter = UsingFloodingFilter;
				thriftCreateRoomRequest.UsingFloodingFilter = usingFloodingFilter;
			}
			if (FloodingFilterSpecified_Set_)
			{
				bool floodingFilterSpecified = FloodingFilterSpecified;
				thriftCreateRoomRequest.FloodingFilterSpecified = floodingFilterSpecified;
			}
			if (FloodingFilterName_Set_ && FloodingFilterName != null)
			{
				string floodingFilterName = FloodingFilterName;
				thriftCreateRoomRequest.FloodingFilterName = floodingFilterName;
			}
			if (FloodingFilterFailuresBeforeKick_Set_)
			{
				int floodingFilterFailuresBeforeKick = FloodingFilterFailuresBeforeKick;
				thriftCreateRoomRequest.FloodingFilterFailuresBeforeKick = floodingFilterFailuresBeforeKick;
			}
			if (FloodingFilterKicksBeforeBan_Set_)
			{
				int floodingFilterKicksBeforeBan = FloodingFilterKicksBeforeBan;
				thriftCreateRoomRequest.FloodingFilterKicksBeforeBan = floodingFilterKicksBeforeBan;
			}
			if (FloodingFilterBanDuration_Set_)
			{
				int floodingFilterBanDuration = FloodingFilterBanDuration;
				thriftCreateRoomRequest.FloodingFilterBanDuration = floodingFilterBanDuration;
			}
			if (FloodingFilterResetAfterKick_Set_)
			{
				bool floodingFilterResetAfterKick = FloodingFilterResetAfterKick;
				thriftCreateRoomRequest.FloodingFilterResetAfterKick = floodingFilterResetAfterKick;
			}
			return thriftCreateRoomRequest;
		}

		public override TBase NewThrift()
		{
			return new ThriftCreateRoomRequest();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftCreateRoomRequest thriftCreateRoomRequest = (ThriftCreateRoomRequest)t_;
			if (thriftCreateRoomRequest.__isset.zoneName && thriftCreateRoomRequest.ZoneName != null)
			{
				ZoneName = thriftCreateRoomRequest.ZoneName;
			}
			if (thriftCreateRoomRequest.__isset.zoneId)
			{
				ZoneId = thriftCreateRoomRequest.ZoneId;
			}
			if (thriftCreateRoomRequest.__isset.roomName && thriftCreateRoomRequest.RoomName != null)
			{
				RoomName = thriftCreateRoomRequest.RoomName;
			}
			if (thriftCreateRoomRequest.__isset.capacity)
			{
				Capacity = thriftCreateRoomRequest.Capacity;
			}
			if (thriftCreateRoomRequest.__isset.password && thriftCreateRoomRequest.Password != null)
			{
				Password = thriftCreateRoomRequest.Password;
			}
			if (thriftCreateRoomRequest.__isset.roomDescription && thriftCreateRoomRequest.RoomDescription != null)
			{
				RoomDescription = thriftCreateRoomRequest.RoomDescription;
			}
			if (thriftCreateRoomRequest.__isset.persistent)
			{
				Persistent = thriftCreateRoomRequest.Persistent;
			}
			if (thriftCreateRoomRequest.__isset.hidden)
			{
				Hidden = thriftCreateRoomRequest.Hidden;
			}
			if (thriftCreateRoomRequest.__isset.receivingRoomListUpdates)
			{
				ReceivingRoomListUpdates = thriftCreateRoomRequest.ReceivingRoomListUpdates;
			}
			if (thriftCreateRoomRequest.__isset.receivingRoomAttributeUpdates)
			{
				ReceivingRoomAttributeUpdates = thriftCreateRoomRequest.ReceivingRoomAttributeUpdates;
			}
			if (thriftCreateRoomRequest.__isset.receivingUserListUpdates)
			{
				ReceivingUserListUpdates = thriftCreateRoomRequest.ReceivingUserListUpdates;
			}
			if (thriftCreateRoomRequest.__isset.receivingUserVariableUpdates)
			{
				ReceivingUserVariableUpdates = thriftCreateRoomRequest.ReceivingUserVariableUpdates;
			}
			if (thriftCreateRoomRequest.__isset.receivingRoomVariableUpdates)
			{
				ReceivingRoomVariableUpdates = thriftCreateRoomRequest.ReceivingRoomVariableUpdates;
			}
			if (thriftCreateRoomRequest.__isset.receivingVideoEvents)
			{
				ReceivingVideoEvents = thriftCreateRoomRequest.ReceivingVideoEvents;
			}
			if (thriftCreateRoomRequest.__isset.nonOperatorUpdateAllowed)
			{
				NonOperatorUpdateAllowed = thriftCreateRoomRequest.NonOperatorUpdateAllowed;
			}
			if (thriftCreateRoomRequest.__isset.nonOperatorVariableUpdateAllowed)
			{
				NonOperatorVariableUpdateAllowed = thriftCreateRoomRequest.NonOperatorVariableUpdateAllowed;
			}
			if (thriftCreateRoomRequest.__isset.createOrJoinRoom)
			{
				CreateOrJoinRoom = thriftCreateRoomRequest.CreateOrJoinRoom;
			}
			if (thriftCreateRoomRequest.__isset.plugins && thriftCreateRoomRequest.Plugins != null)
			{
				Plugins = new List<PluginListEntry>();
				foreach (ThriftPluginListEntry plugin in thriftCreateRoomRequest.Plugins)
				{
					PluginListEntry item = new PluginListEntry(plugin);
					Plugins.Add(item);
				}
			}
			if (thriftCreateRoomRequest.__isset.variables && thriftCreateRoomRequest.Variables != null)
			{
				Variables = new List<RoomVariable>();
				foreach (ThriftRoomVariable variable in thriftCreateRoomRequest.Variables)
				{
					RoomVariable item2 = new RoomVariable(variable);
					Variables.Add(item2);
				}
			}
			if (thriftCreateRoomRequest.__isset.usingLanguageFilter)
			{
				UsingLanguageFilter = thriftCreateRoomRequest.UsingLanguageFilter;
			}
			if (thriftCreateRoomRequest.__isset.languageFilterSpecified)
			{
				LanguageFilterSpecified = thriftCreateRoomRequest.LanguageFilterSpecified;
			}
			if (thriftCreateRoomRequest.__isset.languageFilterName && thriftCreateRoomRequest.LanguageFilterName != null)
			{
				LanguageFilterName = thriftCreateRoomRequest.LanguageFilterName;
			}
			if (thriftCreateRoomRequest.__isset.languageFilterDeliverMessageOnFailure)
			{
				LanguageFilterDeliverMessageOnFailure = thriftCreateRoomRequest.LanguageFilterDeliverMessageOnFailure;
			}
			if (thriftCreateRoomRequest.__isset.languageFilterFailuresBeforeKick)
			{
				LanguageFilterFailuresBeforeKick = thriftCreateRoomRequest.LanguageFilterFailuresBeforeKick;
			}
			if (thriftCreateRoomRequest.__isset.languageFilterKicksBeforeBan)
			{
				LanguageFilterKicksBeforeBan = thriftCreateRoomRequest.LanguageFilterKicksBeforeBan;
			}
			if (thriftCreateRoomRequest.__isset.languageFilterBanDuration)
			{
				LanguageFilterBanDuration = thriftCreateRoomRequest.LanguageFilterBanDuration;
			}
			if (thriftCreateRoomRequest.__isset.languageFilterResetAfterKick)
			{
				LanguageFilterResetAfterKick = thriftCreateRoomRequest.LanguageFilterResetAfterKick;
			}
			if (thriftCreateRoomRequest.__isset.usingFloodingFilter)
			{
				UsingFloodingFilter = thriftCreateRoomRequest.UsingFloodingFilter;
			}
			if (thriftCreateRoomRequest.__isset.floodingFilterSpecified)
			{
				FloodingFilterSpecified = thriftCreateRoomRequest.FloodingFilterSpecified;
			}
			if (thriftCreateRoomRequest.__isset.floodingFilterName && thriftCreateRoomRequest.FloodingFilterName != null)
			{
				FloodingFilterName = thriftCreateRoomRequest.FloodingFilterName;
			}
			if (thriftCreateRoomRequest.__isset.floodingFilterFailuresBeforeKick)
			{
				FloodingFilterFailuresBeforeKick = thriftCreateRoomRequest.FloodingFilterFailuresBeforeKick;
			}
			if (thriftCreateRoomRequest.__isset.floodingFilterKicksBeforeBan)
			{
				FloodingFilterKicksBeforeBan = thriftCreateRoomRequest.FloodingFilterKicksBeforeBan;
			}
			if (thriftCreateRoomRequest.__isset.floodingFilterBanDuration)
			{
				FloodingFilterBanDuration = thriftCreateRoomRequest.FloodingFilterBanDuration;
			}
			if (thriftCreateRoomRequest.__isset.floodingFilterResetAfterKick)
			{
				FloodingFilterResetAfterKick = thriftCreateRoomRequest.FloodingFilterResetAfterKick;
			}
		}
	}
}
