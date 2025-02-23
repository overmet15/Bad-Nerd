using System;
using System.Collections.Generic;
using System.Text;
using Thrift.Protocol;

[Serializable]
public class ThriftUserListEntry : TBase
{
	[Serializable]
	public struct Isset
	{
		public bool userName;

		public bool userVariables;

		public bool sendingVideo;

		public bool videoStreamName;

		public bool roomOperator;
	}

	private string _userName;

	private List<ThriftUserVariable> _userVariables;

	private bool _sendingVideo;

	private string _videoStreamName;

	private bool _roomOperator;

	public Isset __isset;

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

	public bool RoomOperator
	{
		get
		{
			return _roomOperator;
		}
		set
		{
			__isset.roomOperator = true;
			_roomOperator = value;
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
					UserName = iprot.ReadString();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 2:
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
			case 3:
				if (tField.Type == TType.Bool)
				{
					SendingVideo = iprot.ReadBool();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 4:
				if (tField.Type == TType.String)
				{
					VideoStreamName = iprot.ReadString();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 5:
				if (tField.Type == TType.Bool)
				{
					RoomOperator = iprot.ReadBool();
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
		TStruct struc = new TStruct("ThriftUserListEntry");
		oprot.WriteStructBegin(struc);
		TField field = default(TField);
		if (UserName != null && __isset.userName)
		{
			field.Name = "userName";
			field.Type = TType.String;
			field.ID = 1;
			oprot.WriteFieldBegin(field);
			oprot.WriteString(UserName);
			oprot.WriteFieldEnd();
		}
		if (UserVariables != null && __isset.userVariables)
		{
			field.Name = "userVariables";
			field.Type = TType.List;
			field.ID = 2;
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
			field.ID = 3;
			oprot.WriteFieldBegin(field);
			oprot.WriteBool(SendingVideo);
			oprot.WriteFieldEnd();
		}
		if (VideoStreamName != null && __isset.videoStreamName)
		{
			field.Name = "videoStreamName";
			field.Type = TType.String;
			field.ID = 4;
			oprot.WriteFieldBegin(field);
			oprot.WriteString(VideoStreamName);
			oprot.WriteFieldEnd();
		}
		if (__isset.roomOperator)
		{
			field.Name = "roomOperator";
			field.Type = TType.Bool;
			field.ID = 5;
			oprot.WriteFieldBegin(field);
			oprot.WriteBool(RoomOperator);
			oprot.WriteFieldEnd();
		}
		oprot.WriteFieldStop();
		oprot.WriteStructEnd();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder("ThriftUserListEntry(");
		stringBuilder.Append("UserName: ");
		stringBuilder.Append(UserName);
		stringBuilder.Append(",UserVariables: ");
		stringBuilder.Append(UserVariables);
		stringBuilder.Append(",SendingVideo: ");
		stringBuilder.Append(SendingVideo);
		stringBuilder.Append(",VideoStreamName: ");
		stringBuilder.Append(VideoStreamName);
		stringBuilder.Append(",RoomOperator: ");
		stringBuilder.Append(RoomOperator);
		stringBuilder.Append(")");
		return stringBuilder.ToString();
	}
}
