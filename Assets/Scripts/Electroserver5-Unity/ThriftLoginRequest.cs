using System;
using System.Collections.Generic;
using System.Text;
using Thrift.Protocol;

[Serializable]
public class ThriftLoginRequest : TBase
{
	[Serializable]
	public struct Isset
	{
		public bool userName;

		public bool password;

		public bool sharedSecret;

		public bool esObject;

		public bool userVariables;

		public bool protocol;

		public bool hashId;

		public bool clientVersion;

		public bool clientType;

		public bool remoteAddress;
	}

	private string _userName;

	private string _password;

	private string _sharedSecret;

	private ThriftFlattenedEsObjectRO _esObject;

	private Dictionary<string, ThriftFlattenedEsObject> _userVariables;

	private ThriftProtocol _protocol;

	private int _hashId;

	private string _clientVersion;

	private string _clientType;

	private List<byte> _remoteAddress;

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

	public string SharedSecret
	{
		get
		{
			return _sharedSecret;
		}
		set
		{
			__isset.sharedSecret = true;
			_sharedSecret = value;
		}
	}

	public ThriftFlattenedEsObjectRO EsObject
	{
		get
		{
			return _esObject;
		}
		set
		{
			__isset.esObject = true;
			_esObject = value;
		}
	}

	public Dictionary<string, ThriftFlattenedEsObject> UserVariables
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

	public ThriftProtocol Protocol
	{
		get
		{
			return _protocol;
		}
		set
		{
			__isset.protocol = true;
			_protocol = value;
		}
	}

	public int HashId
	{
		get
		{
			return _hashId;
		}
		set
		{
			__isset.hashId = true;
			_hashId = value;
		}
	}

	public string ClientVersion
	{
		get
		{
			return _clientVersion;
		}
		set
		{
			__isset.clientVersion = true;
			_clientVersion = value;
		}
	}

	public string ClientType
	{
		get
		{
			return _clientType;
		}
		set
		{
			__isset.clientType = true;
			_clientType = value;
		}
	}

	public List<byte> RemoteAddress
	{
		get
		{
			return _remoteAddress;
		}
		set
		{
			__isset.remoteAddress = true;
			_remoteAddress = value;
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
				if (tField.Type == TType.String)
				{
					Password = iprot.ReadString();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 3:
				if (tField.Type == TType.String)
				{
					SharedSecret = iprot.ReadString();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 4:
				if (tField.Type == TType.Struct)
				{
					EsObject = new ThriftFlattenedEsObjectRO();
					EsObject.Read(iprot);
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 5:
				if (tField.Type == TType.Map)
				{
					UserVariables = new Dictionary<string, ThriftFlattenedEsObject>();
					TMap tMap = iprot.ReadMapBegin();
					for (int j = 0; j < tMap.Count; j++)
					{
						string key = iprot.ReadString();
						ThriftFlattenedEsObject thriftFlattenedEsObject = new ThriftFlattenedEsObject();
						thriftFlattenedEsObject.Read(iprot);
						UserVariables[key] = thriftFlattenedEsObject;
					}
					iprot.ReadMapEnd();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 6:
				if (tField.Type == TType.I32)
				{
					Protocol = (ThriftProtocol)iprot.ReadI32();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 7:
				if (tField.Type == TType.I32)
				{
					HashId = iprot.ReadI32();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 8:
				if (tField.Type == TType.String)
				{
					ClientVersion = iprot.ReadString();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 9:
				if (tField.Type == TType.String)
				{
					ClientType = iprot.ReadString();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 10:
				if (tField.Type == TType.List)
				{
					RemoteAddress = new List<byte>();
					TList tList = iprot.ReadListBegin();
					for (int i = 0; i < tList.Count; i++)
					{
						byte b = 0;
						b = iprot.ReadByte();
						RemoteAddress.Add(b);
					}
					iprot.ReadListEnd();
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
		TStruct struc = new TStruct("ThriftLoginRequest");
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
		if (Password != null && __isset.password)
		{
			field.Name = "password";
			field.Type = TType.String;
			field.ID = 2;
			oprot.WriteFieldBegin(field);
			oprot.WriteString(Password);
			oprot.WriteFieldEnd();
		}
		if (SharedSecret != null && __isset.sharedSecret)
		{
			field.Name = "sharedSecret";
			field.Type = TType.String;
			field.ID = 3;
			oprot.WriteFieldBegin(field);
			oprot.WriteString(SharedSecret);
			oprot.WriteFieldEnd();
		}
		if (EsObject != null && __isset.esObject)
		{
			field.Name = "esObject";
			field.Type = TType.Struct;
			field.ID = 4;
			oprot.WriteFieldBegin(field);
			EsObject.Write(oprot);
			oprot.WriteFieldEnd();
		}
		if (UserVariables != null && __isset.userVariables)
		{
			field.Name = "userVariables";
			field.Type = TType.Map;
			field.ID = 5;
			oprot.WriteFieldBegin(field);
			oprot.WriteMapBegin(new TMap(TType.String, TType.Struct, UserVariables.Count));
			foreach (string key in UserVariables.Keys)
			{
				oprot.WriteString(key);
				UserVariables[key].Write(oprot);
				oprot.WriteMapEnd();
			}
			oprot.WriteFieldEnd();
		}
		if (__isset.protocol)
		{
			field.Name = "protocol";
			field.Type = TType.I32;
			field.ID = 6;
			oprot.WriteFieldBegin(field);
			oprot.WriteI32((int)Protocol);
			oprot.WriteFieldEnd();
		}
		if (__isset.hashId)
		{
			field.Name = "hashId";
			field.Type = TType.I32;
			field.ID = 7;
			oprot.WriteFieldBegin(field);
			oprot.WriteI32(HashId);
			oprot.WriteFieldEnd();
		}
		if (ClientVersion != null && __isset.clientVersion)
		{
			field.Name = "clientVersion";
			field.Type = TType.String;
			field.ID = 8;
			oprot.WriteFieldBegin(field);
			oprot.WriteString(ClientVersion);
			oprot.WriteFieldEnd();
		}
		if (ClientType != null && __isset.clientType)
		{
			field.Name = "clientType";
			field.Type = TType.String;
			field.ID = 9;
			oprot.WriteFieldBegin(field);
			oprot.WriteString(ClientType);
			oprot.WriteFieldEnd();
		}
		if (RemoteAddress != null && __isset.remoteAddress)
		{
			field.Name = "remoteAddress";
			field.Type = TType.List;
			field.ID = 10;
			oprot.WriteFieldBegin(field);
			oprot.WriteListBegin(new TList(TType.Byte, RemoteAddress.Count));
			foreach (byte item in RemoteAddress)
			{
				oprot.WriteByte(item);
				oprot.WriteListEnd();
			}
			oprot.WriteFieldEnd();
		}
		oprot.WriteFieldStop();
		oprot.WriteStructEnd();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder("ThriftLoginRequest(");
		stringBuilder.Append("UserName: ");
		stringBuilder.Append(UserName);
		stringBuilder.Append(",Password: ");
		stringBuilder.Append(Password);
		stringBuilder.Append(",SharedSecret: ");
		stringBuilder.Append(SharedSecret);
		stringBuilder.Append(",EsObject: ");
		stringBuilder.Append((EsObject != null) ? EsObject.ToString() : "<null>");
		stringBuilder.Append(",UserVariables: ");
		stringBuilder.Append(UserVariables);
		stringBuilder.Append(",Protocol: ");
		stringBuilder.Append(Protocol);
		stringBuilder.Append(",HashId: ");
		stringBuilder.Append(HashId);
		stringBuilder.Append(",ClientVersion: ");
		stringBuilder.Append(ClientVersion);
		stringBuilder.Append(",ClientType: ");
		stringBuilder.Append(ClientType);
		stringBuilder.Append(",RemoteAddress: ");
		stringBuilder.Append(RemoteAddress);
		stringBuilder.Append(")");
		return stringBuilder.ToString();
	}
}
