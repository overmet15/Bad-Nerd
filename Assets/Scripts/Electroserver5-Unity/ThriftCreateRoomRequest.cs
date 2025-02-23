using System;
using System.Collections.Generic;
using System.Text;
using Thrift.Protocol;

[Serializable]
public class ThriftCreateRoomRequest : TBase
{
	[Serializable]
	public struct Isset
	{
		public bool zoneName;

		public bool zoneId;

		public bool roomName;

		public bool capacity;

		public bool password;

		public bool roomDescription;

		public bool persistent;

		public bool hidden;

		public bool receivingRoomListUpdates;

		public bool receivingRoomAttributeUpdates;

		public bool receivingUserListUpdates;

		public bool receivingUserVariableUpdates;

		public bool receivingRoomVariableUpdates;

		public bool receivingVideoEvents;

		public bool nonOperatorUpdateAllowed;

		public bool nonOperatorVariableUpdateAllowed;

		public bool createOrJoinRoom;

		public bool plugins;

		public bool variables;

		public bool usingLanguageFilter;

		public bool languageFilterSpecified;

		public bool languageFilterName;

		public bool languageFilterDeliverMessageOnFailure;

		public bool languageFilterFailuresBeforeKick;

		public bool languageFilterKicksBeforeBan;

		public bool languageFilterBanDuration;

		public bool languageFilterResetAfterKick;

		public bool usingFloodingFilter;

		public bool floodingFilterSpecified;

		public bool floodingFilterName;

		public bool floodingFilterFailuresBeforeKick;

		public bool floodingFilterKicksBeforeBan;

		public bool floodingFilterBanDuration;

		public bool floodingFilterResetAfterKick;
	}

	private string _zoneName;

	private int _zoneId;

	private string _roomName;

	private int _capacity;

	private string _password;

	private string _roomDescription;

	private bool _persistent;

	private bool _hidden;

	private bool _receivingRoomListUpdates;

	private bool _receivingRoomAttributeUpdates;

	private bool _receivingUserListUpdates;

	private bool _receivingUserVariableUpdates;

	private bool _receivingRoomVariableUpdates;

	private bool _receivingVideoEvents;

	private bool _nonOperatorUpdateAllowed;

	private bool _nonOperatorVariableUpdateAllowed;

	private bool _createOrJoinRoom;

	private List<ThriftPluginListEntry> _plugins;

	private List<ThriftRoomVariable> _variables;

	private bool _usingLanguageFilter;

	private bool _languageFilterSpecified;

	private string _languageFilterName;

	private bool _languageFilterDeliverMessageOnFailure;

	private int _languageFilterFailuresBeforeKick;

	private int _languageFilterKicksBeforeBan;

	private int _languageFilterBanDuration;

	private bool _languageFilterResetAfterKick;

	private bool _usingFloodingFilter;

	private bool _floodingFilterSpecified;

	private string _floodingFilterName;

	private int _floodingFilterFailuresBeforeKick;

	private int _floodingFilterKicksBeforeBan;

	private int _floodingFilterBanDuration;

	private bool _floodingFilterResetAfterKick;

	public Isset __isset;

	public string ZoneName
	{
		get
		{
			return _zoneName;
		}
		set
		{
			__isset.zoneName = true;
			_zoneName = value;
		}
	}

	public int ZoneId
	{
		get
		{
			return _zoneId;
		}
		set
		{
			__isset.zoneId = true;
			_zoneId = value;
		}
	}

	public string RoomName
	{
		get
		{
			return _roomName;
		}
		set
		{
			__isset.roomName = true;
			_roomName = value;
		}
	}

	public int Capacity
	{
		get
		{
			return _capacity;
		}
		set
		{
			__isset.capacity = true;
			_capacity = value;
		}
	}

	public string Password
	{
		get
		{
			return _password;
		}
		set
		{
			__isset.password = true;
			_password = value;
		}
	}

	public string RoomDescription
	{
		get
		{
			return _roomDescription;
		}
		set
		{
			__isset.roomDescription = true;
			_roomDescription = value;
		}
	}

	public bool Persistent
	{
		get
		{
			return _persistent;
		}
		set
		{
			__isset.persistent = true;
			_persistent = value;
		}
	}

	public bool Hidden
	{
		get
		{
			return _hidden;
		}
		set
		{
			__isset.hidden = true;
			_hidden = value;
		}
	}

	public bool ReceivingRoomListUpdates
	{
		get
		{
			return _receivingRoomListUpdates;
		}
		set
		{
			__isset.receivingRoomListUpdates = true;
			_receivingRoomListUpdates = value;
		}
	}

	public bool ReceivingRoomAttributeUpdates
	{
		get
		{
			return _receivingRoomAttributeUpdates;
		}
		set
		{
			__isset.receivingRoomAttributeUpdates = true;
			_receivingRoomAttributeUpdates = value;
		}
	}

	public bool ReceivingUserListUpdates
	{
		get
		{
			return _receivingUserListUpdates;
		}
		set
		{
			__isset.receivingUserListUpdates = true;
			_receivingUserListUpdates = value;
		}
	}

	public bool ReceivingUserVariableUpdates
	{
		get
		{
			return _receivingUserVariableUpdates;
		}
		set
		{
			__isset.receivingUserVariableUpdates = true;
			_receivingUserVariableUpdates = value;
		}
	}

	public bool ReceivingRoomVariableUpdates
	{
		get
		{
			return _receivingRoomVariableUpdates;
		}
		set
		{
			__isset.receivingRoomVariableUpdates = true;
			_receivingRoomVariableUpdates = value;
		}
	}

	public bool ReceivingVideoEvents
	{
		get
		{
			return _receivingVideoEvents;
		}
		set
		{
			__isset.receivingVideoEvents = true;
			_receivingVideoEvents = value;
		}
	}

	public bool NonOperatorUpdateAllowed
	{
		get
		{
			return _nonOperatorUpdateAllowed;
		}
		set
		{
			__isset.nonOperatorUpdateAllowed = true;
			_nonOperatorUpdateAllowed = value;
		}
	}

	public bool NonOperatorVariableUpdateAllowed
	{
		get
		{
			return _nonOperatorVariableUpdateAllowed;
		}
		set
		{
			__isset.nonOperatorVariableUpdateAllowed = true;
			_nonOperatorVariableUpdateAllowed = value;
		}
	}

	public bool CreateOrJoinRoom
	{
		get
		{
			return _createOrJoinRoom;
		}
		set
		{
			__isset.createOrJoinRoom = true;
			_createOrJoinRoom = value;
		}
	}

	public List<ThriftPluginListEntry> Plugins
	{
		get
		{
			return _plugins;
		}
		set
		{
			__isset.plugins = true;
			_plugins = value;
		}
	}

	public List<ThriftRoomVariable> Variables
	{
		get
		{
			return _variables;
		}
		set
		{
			__isset.variables = true;
			_variables = value;
		}
	}

	public bool UsingLanguageFilter
	{
		get
		{
			return _usingLanguageFilter;
		}
		set
		{
			__isset.usingLanguageFilter = true;
			_usingLanguageFilter = value;
		}
	}

	public bool LanguageFilterSpecified
	{
		get
		{
			return _languageFilterSpecified;
		}
		set
		{
			__isset.languageFilterSpecified = true;
			_languageFilterSpecified = value;
		}
	}

	public string LanguageFilterName
	{
		get
		{
			return _languageFilterName;
		}
		set
		{
			__isset.languageFilterName = true;
			_languageFilterName = value;
		}
	}

	public bool LanguageFilterDeliverMessageOnFailure
	{
		get
		{
			return _languageFilterDeliverMessageOnFailure;
		}
		set
		{
			__isset.languageFilterDeliverMessageOnFailure = true;
			_languageFilterDeliverMessageOnFailure = value;
		}
	}

	public int LanguageFilterFailuresBeforeKick
	{
		get
		{
			return _languageFilterFailuresBeforeKick;
		}
		set
		{
			__isset.languageFilterFailuresBeforeKick = true;
			_languageFilterFailuresBeforeKick = value;
		}
	}

	public int LanguageFilterKicksBeforeBan
	{
		get
		{
			return _languageFilterKicksBeforeBan;
		}
		set
		{
			__isset.languageFilterKicksBeforeBan = true;
			_languageFilterKicksBeforeBan = value;
		}
	}

	public int LanguageFilterBanDuration
	{
		get
		{
			return _languageFilterBanDuration;
		}
		set
		{
			__isset.languageFilterBanDuration = true;
			_languageFilterBanDuration = value;
		}
	}

	public bool LanguageFilterResetAfterKick
	{
		get
		{
			return _languageFilterResetAfterKick;
		}
		set
		{
			__isset.languageFilterResetAfterKick = true;
			_languageFilterResetAfterKick = value;
		}
	}

	public bool UsingFloodingFilter
	{
		get
		{
			return _usingFloodingFilter;
		}
		set
		{
			__isset.usingFloodingFilter = true;
			_usingFloodingFilter = value;
		}
	}

	public bool FloodingFilterSpecified
	{
		get
		{
			return _floodingFilterSpecified;
		}
		set
		{
			__isset.floodingFilterSpecified = true;
			_floodingFilterSpecified = value;
		}
	}

	public string FloodingFilterName
	{
		get
		{
			return _floodingFilterName;
		}
		set
		{
			__isset.floodingFilterName = true;
			_floodingFilterName = value;
		}
	}

	public int FloodingFilterFailuresBeforeKick
	{
		get
		{
			return _floodingFilterFailuresBeforeKick;
		}
		set
		{
			__isset.floodingFilterFailuresBeforeKick = true;
			_floodingFilterFailuresBeforeKick = value;
		}
	}

	public int FloodingFilterKicksBeforeBan
	{
		get
		{
			return _floodingFilterKicksBeforeBan;
		}
		set
		{
			__isset.floodingFilterKicksBeforeBan = true;
			_floodingFilterKicksBeforeBan = value;
		}
	}

	public int FloodingFilterBanDuration
	{
		get
		{
			return _floodingFilterBanDuration;
		}
		set
		{
			__isset.floodingFilterBanDuration = true;
			_floodingFilterBanDuration = value;
		}
	}

	public bool FloodingFilterResetAfterKick
	{
		get
		{
			return _floodingFilterResetAfterKick;
		}
		set
		{
			__isset.floodingFilterResetAfterKick = true;
			_floodingFilterResetAfterKick = value;
		}
	}

	public void Read(TProtocol iprot)
	{
		iprot.ReadStructBegin();
		while (true)
		{
			TField tField = iprot.ReadFieldBegin();
			if (tField.Type == TType.Stop)
			{
				break;
			}
			switch (tField.ID)
			{
			case 1:
				if (tField.Type == TType.String)
				{
					ZoneName = iprot.ReadString();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 2:
				if (tField.Type == TType.I32)
				{
					ZoneId = iprot.ReadI32();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 3:
				if (tField.Type == TType.String)
				{
					RoomName = iprot.ReadString();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 4:
				if (tField.Type == TType.I32)
				{
					Capacity = iprot.ReadI32();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 5:
				if (tField.Type == TType.String)
				{
					Password = iprot.ReadString();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 6:
				if (tField.Type == TType.String)
				{
					RoomDescription = iprot.ReadString();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 7:
				if (tField.Type == TType.Bool)
				{
					Persistent = iprot.ReadBool();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 8:
				if (tField.Type == TType.Bool)
				{
					Hidden = iprot.ReadBool();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 9:
				if (tField.Type == TType.Bool)
				{
					ReceivingRoomListUpdates = iprot.ReadBool();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 10:
				if (tField.Type == TType.Bool)
				{
					ReceivingRoomAttributeUpdates = iprot.ReadBool();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 11:
				if (tField.Type == TType.Bool)
				{
					ReceivingUserListUpdates = iprot.ReadBool();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 12:
				if (tField.Type == TType.Bool)
				{
					ReceivingUserVariableUpdates = iprot.ReadBool();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 13:
				if (tField.Type == TType.Bool)
				{
					ReceivingRoomVariableUpdates = iprot.ReadBool();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 14:
				if (tField.Type == TType.Bool)
				{
					ReceivingVideoEvents = iprot.ReadBool();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 15:
				if (tField.Type == TType.Bool)
				{
					NonOperatorUpdateAllowed = iprot.ReadBool();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 16:
				if (tField.Type == TType.Bool)
				{
					NonOperatorVariableUpdateAllowed = iprot.ReadBool();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 17:
				if (tField.Type == TType.Bool)
				{
					CreateOrJoinRoom = iprot.ReadBool();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 18:
				if (tField.Type == TType.List)
				{
					Plugins = new List<ThriftPluginListEntry>();
					TList tList2 = iprot.ReadListBegin();
					for (int j = 0; j < tList2.Count; j++)
					{
						ThriftPluginListEntry thriftPluginListEntry = new ThriftPluginListEntry();
						thriftPluginListEntry = new ThriftPluginListEntry();
						thriftPluginListEntry.Read(iprot);
						Plugins.Add(thriftPluginListEntry);
					}
					iprot.ReadListEnd();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 19:
				if (tField.Type == TType.List)
				{
					Variables = new List<ThriftRoomVariable>();
					TList tList = iprot.ReadListBegin();
					for (int i = 0; i < tList.Count; i++)
					{
						ThriftRoomVariable thriftRoomVariable = new ThriftRoomVariable();
						thriftRoomVariable = new ThriftRoomVariable();
						thriftRoomVariable.Read(iprot);
						Variables.Add(thriftRoomVariable);
					}
					iprot.ReadListEnd();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 20:
				if (tField.Type == TType.Bool)
				{
					UsingLanguageFilter = iprot.ReadBool();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 21:
				if (tField.Type == TType.Bool)
				{
					LanguageFilterSpecified = iprot.ReadBool();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 22:
				if (tField.Type == TType.String)
				{
					LanguageFilterName = iprot.ReadString();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 23:
				if (tField.Type == TType.Bool)
				{
					LanguageFilterDeliverMessageOnFailure = iprot.ReadBool();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 24:
				if (tField.Type == TType.I32)
				{
					LanguageFilterFailuresBeforeKick = iprot.ReadI32();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 25:
				if (tField.Type == TType.I32)
				{
					LanguageFilterKicksBeforeBan = iprot.ReadI32();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 26:
				if (tField.Type == TType.I32)
				{
					LanguageFilterBanDuration = iprot.ReadI32();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 27:
				if (tField.Type == TType.Bool)
				{
					LanguageFilterResetAfterKick = iprot.ReadBool();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 28:
				if (tField.Type == TType.Bool)
				{
					UsingFloodingFilter = iprot.ReadBool();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 29:
				if (tField.Type == TType.Bool)
				{
					FloodingFilterSpecified = iprot.ReadBool();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 30:
				if (tField.Type == TType.String)
				{
					FloodingFilterName = iprot.ReadString();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 31:
				if (tField.Type == TType.I32)
				{
					FloodingFilterFailuresBeforeKick = iprot.ReadI32();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 32:
				if (tField.Type == TType.I32)
				{
					FloodingFilterKicksBeforeBan = iprot.ReadI32();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 33:
				if (tField.Type == TType.I32)
				{
					FloodingFilterBanDuration = iprot.ReadI32();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 34:
				if (tField.Type == TType.Bool)
				{
					FloodingFilterResetAfterKick = iprot.ReadBool();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			default:
				TProtocolUtil.Skip(iprot, tField.Type);
				break;
			}
			iprot.ReadFieldEnd();
		}
		iprot.ReadStructEnd();
	}

	public void Write(TProtocol oprot)
	{
		TStruct struc = new TStruct("ThriftCreateRoomRequest");
		oprot.WriteStructBegin(struc);
		TField field = default(TField);
		if (ZoneName != null && __isset.zoneName)
		{
			field.Name = "zoneName";
			field.Type = TType.String;
			field.ID = 1;
			oprot.WriteFieldBegin(field);
			oprot.WriteString(ZoneName);
			oprot.WriteFieldEnd();
		}
		if (__isset.zoneId)
		{
			field.Name = "zoneId";
			field.Type = TType.I32;
			field.ID = 2;
			oprot.WriteFieldBegin(field);
			oprot.WriteI32(ZoneId);
			oprot.WriteFieldEnd();
		}
		if (RoomName != null && __isset.roomName)
		{
			field.Name = "roomName";
			field.Type = TType.String;
			field.ID = 3;
			oprot.WriteFieldBegin(field);
			oprot.WriteString(RoomName);
			oprot.WriteFieldEnd();
		}
		if (__isset.capacity)
		{
			field.Name = "capacity";
			field.Type = TType.I32;
			field.ID = 4;
			oprot.WriteFieldBegin(field);
			oprot.WriteI32(Capacity);
			oprot.WriteFieldEnd();
		}
		if (Password != null && __isset.password)
		{
			field.Name = "password";
			field.Type = TType.String;
			field.ID = 5;
			oprot.WriteFieldBegin(field);
			oprot.WriteString(Password);
			oprot.WriteFieldEnd();
		}
		if (RoomDescription != null && __isset.roomDescription)
		{
			field.Name = "roomDescription";
			field.Type = TType.String;
			field.ID = 6;
			oprot.WriteFieldBegin(field);
			oprot.WriteString(RoomDescription);
			oprot.WriteFieldEnd();
		}
		if (__isset.persistent)
		{
			field.Name = "persistent";
			field.Type = TType.Bool;
			field.ID = 7;
			oprot.WriteFieldBegin(field);
			oprot.WriteBool(Persistent);
			oprot.WriteFieldEnd();
		}
		if (__isset.hidden)
		{
			field.Name = "hidden";
			field.Type = TType.Bool;
			field.ID = 8;
			oprot.WriteFieldBegin(field);
			oprot.WriteBool(Hidden);
			oprot.WriteFieldEnd();
		}
		if (__isset.receivingRoomListUpdates)
		{
			field.Name = "receivingRoomListUpdates";
			field.Type = TType.Bool;
			field.ID = 9;
			oprot.WriteFieldBegin(field);
			oprot.WriteBool(ReceivingRoomListUpdates);
			oprot.WriteFieldEnd();
		}
		if (__isset.receivingRoomAttributeUpdates)
		{
			field.Name = "receivingRoomAttributeUpdates";
			field.Type = TType.Bool;
			field.ID = 10;
			oprot.WriteFieldBegin(field);
			oprot.WriteBool(ReceivingRoomAttributeUpdates);
			oprot.WriteFieldEnd();
		}
		if (__isset.receivingUserListUpdates)
		{
			field.Name = "receivingUserListUpdates";
			field.Type = TType.Bool;
			field.ID = 11;
			oprot.WriteFieldBegin(field);
			oprot.WriteBool(ReceivingUserListUpdates);
			oprot.WriteFieldEnd();
		}
		if (__isset.receivingUserVariableUpdates)
		{
			field.Name = "receivingUserVariableUpdates";
			field.Type = TType.Bool;
			field.ID = 12;
			oprot.WriteFieldBegin(field);
			oprot.WriteBool(ReceivingUserVariableUpdates);
			oprot.WriteFieldEnd();
		}
		if (__isset.receivingRoomVariableUpdates)
		{
			field.Name = "receivingRoomVariableUpdates";
			field.Type = TType.Bool;
			field.ID = 13;
			oprot.WriteFieldBegin(field);
			oprot.WriteBool(ReceivingRoomVariableUpdates);
			oprot.WriteFieldEnd();
		}
		if (__isset.receivingVideoEvents)
		{
			field.Name = "receivingVideoEvents";
			field.Type = TType.Bool;
			field.ID = 14;
			oprot.WriteFieldBegin(field);
			oprot.WriteBool(ReceivingVideoEvents);
			oprot.WriteFieldEnd();
		}
		if (__isset.nonOperatorUpdateAllowed)
		{
			field.Name = "nonOperatorUpdateAllowed";
			field.Type = TType.Bool;
			field.ID = 15;
			oprot.WriteFieldBegin(field);
			oprot.WriteBool(NonOperatorUpdateAllowed);
			oprot.WriteFieldEnd();
		}
		if (__isset.nonOperatorVariableUpdateAllowed)
		{
			field.Name = "nonOperatorVariableUpdateAllowed";
			field.Type = TType.Bool;
			field.ID = 16;
			oprot.WriteFieldBegin(field);
			oprot.WriteBool(NonOperatorVariableUpdateAllowed);
			oprot.WriteFieldEnd();
		}
		if (__isset.createOrJoinRoom)
		{
			field.Name = "createOrJoinRoom";
			field.Type = TType.Bool;
			field.ID = 17;
			oprot.WriteFieldBegin(field);
			oprot.WriteBool(CreateOrJoinRoom);
			oprot.WriteFieldEnd();
		}
		if (Plugins != null && __isset.plugins)
		{
			field.Name = "plugins";
			field.Type = TType.List;
			field.ID = 18;
			oprot.WriteFieldBegin(field);
			oprot.WriteListBegin(new TList(TType.Struct, Plugins.Count));
			foreach (ThriftPluginListEntry plugin in Plugins)
			{
				plugin.Write(oprot);
				oprot.WriteListEnd();
			}
			oprot.WriteFieldEnd();
		}
		if (Variables != null && __isset.variables)
		{
			field.Name = "variables";
			field.Type = TType.List;
			field.ID = 19;
			oprot.WriteFieldBegin(field);
			oprot.WriteListBegin(new TList(TType.Struct, Variables.Count));
			foreach (ThriftRoomVariable variable in Variables)
			{
				variable.Write(oprot);
				oprot.WriteListEnd();
			}
			oprot.WriteFieldEnd();
		}
		if (__isset.usingLanguageFilter)
		{
			field.Name = "usingLanguageFilter";
			field.Type = TType.Bool;
			field.ID = 20;
			oprot.WriteFieldBegin(field);
			oprot.WriteBool(UsingLanguageFilter);
			oprot.WriteFieldEnd();
		}
		if (__isset.languageFilterSpecified)
		{
			field.Name = "languageFilterSpecified";
			field.Type = TType.Bool;
			field.ID = 21;
			oprot.WriteFieldBegin(field);
			oprot.WriteBool(LanguageFilterSpecified);
			oprot.WriteFieldEnd();
		}
		if (LanguageFilterName != null && __isset.languageFilterName)
		{
			field.Name = "languageFilterName";
			field.Type = TType.String;
			field.ID = 22;
			oprot.WriteFieldBegin(field);
			oprot.WriteString(LanguageFilterName);
			oprot.WriteFieldEnd();
		}
		if (__isset.languageFilterDeliverMessageOnFailure)
		{
			field.Name = "languageFilterDeliverMessageOnFailure";
			field.Type = TType.Bool;
			field.ID = 23;
			oprot.WriteFieldBegin(field);
			oprot.WriteBool(LanguageFilterDeliverMessageOnFailure);
			oprot.WriteFieldEnd();
		}
		if (__isset.languageFilterFailuresBeforeKick)
		{
			field.Name = "languageFilterFailuresBeforeKick";
			field.Type = TType.I32;
			field.ID = 24;
			oprot.WriteFieldBegin(field);
			oprot.WriteI32(LanguageFilterFailuresBeforeKick);
			oprot.WriteFieldEnd();
		}
		if (__isset.languageFilterKicksBeforeBan)
		{
			field.Name = "languageFilterKicksBeforeBan";
			field.Type = TType.I32;
			field.ID = 25;
			oprot.WriteFieldBegin(field);
			oprot.WriteI32(LanguageFilterKicksBeforeBan);
			oprot.WriteFieldEnd();
		}
		if (__isset.languageFilterBanDuration)
		{
			field.Name = "languageFilterBanDuration";
			field.Type = TType.I32;
			field.ID = 26;
			oprot.WriteFieldBegin(field);
			oprot.WriteI32(LanguageFilterBanDuration);
			oprot.WriteFieldEnd();
		}
		if (__isset.languageFilterResetAfterKick)
		{
			field.Name = "languageFilterResetAfterKick";
			field.Type = TType.Bool;
			field.ID = 27;
			oprot.WriteFieldBegin(field);
			oprot.WriteBool(LanguageFilterResetAfterKick);
			oprot.WriteFieldEnd();
		}
		if (__isset.usingFloodingFilter)
		{
			field.Name = "usingFloodingFilter";
			field.Type = TType.Bool;
			field.ID = 28;
			oprot.WriteFieldBegin(field);
			oprot.WriteBool(UsingFloodingFilter);
			oprot.WriteFieldEnd();
		}
		if (__isset.floodingFilterSpecified)
		{
			field.Name = "floodingFilterSpecified";
			field.Type = TType.Bool;
			field.ID = 29;
			oprot.WriteFieldBegin(field);
			oprot.WriteBool(FloodingFilterSpecified);
			oprot.WriteFieldEnd();
		}
		if (FloodingFilterName != null && __isset.floodingFilterName)
		{
			field.Name = "floodingFilterName";
			field.Type = TType.String;
			field.ID = 30;
			oprot.WriteFieldBegin(field);
			oprot.WriteString(FloodingFilterName);
			oprot.WriteFieldEnd();
		}
		if (__isset.floodingFilterFailuresBeforeKick)
		{
			field.Name = "floodingFilterFailuresBeforeKick";
			field.Type = TType.I32;
			field.ID = 31;
			oprot.WriteFieldBegin(field);
			oprot.WriteI32(FloodingFilterFailuresBeforeKick);
			oprot.WriteFieldEnd();
		}
		if (__isset.floodingFilterKicksBeforeBan)
		{
			field.Name = "floodingFilterKicksBeforeBan";
			field.Type = TType.I32;
			field.ID = 32;
			oprot.WriteFieldBegin(field);
			oprot.WriteI32(FloodingFilterKicksBeforeBan);
			oprot.WriteFieldEnd();
		}
		if (__isset.floodingFilterBanDuration)
		{
			field.Name = "floodingFilterBanDuration";
			field.Type = TType.I32;
			field.ID = 33;
			oprot.WriteFieldBegin(field);
			oprot.WriteI32(FloodingFilterBanDuration);
			oprot.WriteFieldEnd();
		}
		if (__isset.floodingFilterResetAfterKick)
		{
			field.Name = "floodingFilterResetAfterKick";
			field.Type = TType.Bool;
			field.ID = 34;
			oprot.WriteFieldBegin(field);
			oprot.WriteBool(FloodingFilterResetAfterKick);
			oprot.WriteFieldEnd();
		}
		oprot.WriteFieldStop();
		oprot.WriteStructEnd();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder("ThriftCreateRoomRequest(");
		stringBuilder.Append("ZoneName: ");
		stringBuilder.Append(ZoneName);
		stringBuilder.Append(",ZoneId: ");
		stringBuilder.Append(ZoneId);
		stringBuilder.Append(",RoomName: ");
		stringBuilder.Append(RoomName);
		stringBuilder.Append(",Capacity: ");
		stringBuilder.Append(Capacity);
		stringBuilder.Append(",Password: ");
		stringBuilder.Append(Password);
		stringBuilder.Append(",RoomDescription: ");
		stringBuilder.Append(RoomDescription);
		stringBuilder.Append(",Persistent: ");
		stringBuilder.Append(Persistent);
		stringBuilder.Append(",Hidden: ");
		stringBuilder.Append(Hidden);
		stringBuilder.Append(",ReceivingRoomListUpdates: ");
		stringBuilder.Append(ReceivingRoomListUpdates);
		stringBuilder.Append(",ReceivingRoomAttributeUpdates: ");
		stringBuilder.Append(ReceivingRoomAttributeUpdates);
		stringBuilder.Append(",ReceivingUserListUpdates: ");
		stringBuilder.Append(ReceivingUserListUpdates);
		stringBuilder.Append(",ReceivingUserVariableUpdates: ");
		stringBuilder.Append(ReceivingUserVariableUpdates);
		stringBuilder.Append(",ReceivingRoomVariableUpdates: ");
		stringBuilder.Append(ReceivingRoomVariableUpdates);
		stringBuilder.Append(",ReceivingVideoEvents: ");
		stringBuilder.Append(ReceivingVideoEvents);
		stringBuilder.Append(",NonOperatorUpdateAllowed: ");
		stringBuilder.Append(NonOperatorUpdateAllowed);
		stringBuilder.Append(",NonOperatorVariableUpdateAllowed: ");
		stringBuilder.Append(NonOperatorVariableUpdateAllowed);
		stringBuilder.Append(",CreateOrJoinRoom: ");
		stringBuilder.Append(CreateOrJoinRoom);
		stringBuilder.Append(",Plugins: ");
		stringBuilder.Append(Plugins);
		stringBuilder.Append(",Variables: ");
		stringBuilder.Append(Variables);
		stringBuilder.Append(",UsingLanguageFilter: ");
		stringBuilder.Append(UsingLanguageFilter);
		stringBuilder.Append(",LanguageFilterSpecified: ");
		stringBuilder.Append(LanguageFilterSpecified);
		stringBuilder.Append(",LanguageFilterName: ");
		stringBuilder.Append(LanguageFilterName);
		stringBuilder.Append(",LanguageFilterDeliverMessageOnFailure: ");
		stringBuilder.Append(LanguageFilterDeliverMessageOnFailure);
		stringBuilder.Append(",LanguageFilterFailuresBeforeKick: ");
		stringBuilder.Append(LanguageFilterFailuresBeforeKick);
		stringBuilder.Append(",LanguageFilterKicksBeforeBan: ");
		stringBuilder.Append(LanguageFilterKicksBeforeBan);
		stringBuilder.Append(",LanguageFilterBanDuration: ");
		stringBuilder.Append(LanguageFilterBanDuration);
		stringBuilder.Append(",LanguageFilterResetAfterKick: ");
		stringBuilder.Append(LanguageFilterResetAfterKick);
		stringBuilder.Append(",UsingFloodingFilter: ");
		stringBuilder.Append(UsingFloodingFilter);
		stringBuilder.Append(",FloodingFilterSpecified: ");
		stringBuilder.Append(FloodingFilterSpecified);
		stringBuilder.Append(",FloodingFilterName: ");
		stringBuilder.Append(FloodingFilterName);
		stringBuilder.Append(",FloodingFilterFailuresBeforeKick: ");
		stringBuilder.Append(FloodingFilterFailuresBeforeKick);
		stringBuilder.Append(",FloodingFilterKicksBeforeBan: ");
		stringBuilder.Append(FloodingFilterKicksBeforeBan);
		stringBuilder.Append(",FloodingFilterBanDuration: ");
		stringBuilder.Append(FloodingFilterBanDuration);
		stringBuilder.Append(",FloodingFilterResetAfterKick: ");
		stringBuilder.Append(FloodingFilterResetAfterKick);
		stringBuilder.Append(")");
		return stringBuilder.ToString();
	}
}
