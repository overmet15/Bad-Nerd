using System;
using System.Collections.Generic;
using System.Text;
using Thrift.Protocol;

[Serializable]
public class ThriftUserUpdateEvent : TBase
{
	[Serializable]
	public struct Isset
	{
		public bool zoneId;

		public bool roomId;

		public bool action;

		public bool userName;

		public bool userVariables;

		public bool sendingVideo;

		public bool videoStreamName;
	}

	private int _zoneId;

	private int _roomId;

	private ThriftUserUpdateAction _action;

	private string _userName;

	private List<ThriftUserVariable> _userVariables;

	private bool _sendingVideo;

	private string _videoStreamName;

	public Isset __isset;

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

	public int RoomId
	{
		get
		{
			return _roomId;
		}
		set
		{
			__isset.roomId = true;
			_roomId = value;
		}
	}

	public ThriftUserUpdateAction Action
	{
		get
		{
			return _action;
		}
		set
		{
			__isset.action = true;
			_action = value;
		}
	}

	public string UserName
	{
		get
		{
			return _userName;
		}
		set
		{
			__isset.userName = true;
			_userName = value;
		}
	}

	public List<ThriftUserVariable> UserVariables
	{
		get
		{
			return _userVariables;
		}
		set
		{
			__isset.userVariables = true;
			_userVariables = value;
		}
	}

	public bool SendingVideo
	{
		get
		{
			return _sendingVideo;
		}
		set
		{
			__isset.sendingVideo = true;
			_sendingVideo = value;
		}
	}

	public string VideoStreamName
	{
		get
		{
			return _videoStreamName;
		}
		set
		{
			__isset.videoStreamName = true;
			_videoStreamName = value;
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
				if (tField.Type == TType.I32)
				{
					ZoneId = iprot.ReadI32();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 2:
				if (tField.Type == TType.I32)
				{
					RoomId = iprot.ReadI32();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 3:
				if (tField.Type == TType.I32)
				{
					Action = (ThriftUserUpdateAction)iprot.ReadI32();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 4:
				if (tField.Type == TType.String)
				{
					UserName = iprot.ReadString();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 5:
				if (tField.Type == TType.List)
				{
					UserVariables = new List<ThriftUserVariable>();
					TList tList = iprot.ReadListBegin();
					for (int i = 0; i < tList.Count; i++)
					{
						ThriftUserVariable thriftUserVariable = new ThriftUserVariable();
						thriftUserVariable = new ThriftUserVariable();
						thriftUserVariable.Read(iprot);
						UserVariables.Add(thriftUserVariable);
					}
					iprot.ReadListEnd();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 6:
				if (tField.Type == TType.Bool)
				{
					SendingVideo = iprot.ReadBool();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 7:
				if (tField.Type == TType.String)
				{
					VideoStreamName = iprot.ReadString();
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
		TStruct struc = new TStruct("ThriftUserUpdateEvent");
		oprot.WriteStructBegin(struc);
		TField field = default(TField);
		if (__isset.zoneId)
		{
			field.Name = "zoneId";
			field.Type = TType.I32;
			field.ID = 1;
			oprot.WriteFieldBegin(field);
			oprot.WriteI32(ZoneId);
			oprot.WriteFieldEnd();
		}
		if (__isset.roomId)
		{
			field.Name = "roomId";
			field.Type = TType.I32;
			field.ID = 2;
			oprot.WriteFieldBegin(field);
			oprot.WriteI32(RoomId);
			oprot.WriteFieldEnd();
		}
		if (__isset.action)
		{
			field.Name = "action";
			field.Type = TType.I32;
			field.ID = 3;
			oprot.WriteFieldBegin(field);
			oprot.WriteI32((int)Action);
			oprot.WriteFieldEnd();
		}
		if (UserName != null && __isset.userName)
		{
			field.Name = "userName";
			field.Type = TType.String;
			field.ID = 4;
			oprot.WriteFieldBegin(field);
			oprot.WriteString(UserName);
			oprot.WriteFieldEnd();
		}
		if (UserVariables != null && __isset.userVariables)
		{
			field.Name = "userVariables";
			field.Type = TType.List;
			field.ID = 5;
			oprot.WriteFieldBegin(field);
			oprot.WriteListBegin(new TList(TType.Struct, UserVariables.Count));
			foreach (ThriftUserVariable userVariable in UserVariables)
			{
				userVariable.Write(oprot);
				oprot.WriteListEnd();
			}
			oprot.WriteFieldEnd();
		}
		if (__isset.sendingVideo)
		{
			field.Name = "sendingVideo";
			field.Type = TType.Bool;
			field.ID = 6;
			oprot.WriteFieldBegin(field);
			oprot.WriteBool(SendingVideo);
			oprot.WriteFieldEnd();
		}
		if (VideoStreamName != null && __isset.videoStreamName)
		{
			field.Name = "videoStreamName";
			field.Type = TType.String;
			field.ID = 7;
			oprot.WriteFieldBegin(field);
			oprot.WriteString(VideoStreamName);
			oprot.WriteFieldEnd();
		}
		oprot.WriteFieldStop();
		oprot.WriteStructEnd();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder("ThriftUserUpdateEvent(");
		stringBuilder.Append("ZoneId: ");
		stringBuilder.Append(ZoneId);
		stringBuilder.Append(",RoomId: ");
		stringBuilder.Append(RoomId);
		stringBuilder.Append(",Action: ");
		stringBuilder.Append(Action);
		stringBuilder.Append(",UserName: ");
		stringBuilder.Append(UserName);
		stringBuilder.Append(",UserVariables: ");
		stringBuilder.Append(UserVariables);
		stringBuilder.Append(",SendingVideo: ");
		stringBuilder.Append(SendingVideo);
		stringBuilder.Append(",VideoStreamName: ");
		stringBuilder.Append(VideoStreamName);
		stringBuilder.Append(")");
		return stringBuilder.ToString();
	}
}
