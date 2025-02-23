using System;
using System.Collections.Generic;
using Electrotank.Electroserver5.Api.Helper;
using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class LoginRequest : EsRequest
	{
		private string UserName_;

		private string Password_;

		private string SharedSecret_;

		private EsObjectRO EsObject_;

		private Dictionary<string, EsObject> UserVariables_;

		private Protocol? Protocol_;

		private int HashId_;

		private string ClientVersion_;

		private string ClientType_;

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

		public string SharedSecret
		{
			get
			{
				return SharedSecret_;
			}
			set
			{
				SharedSecret_ = value;
				SharedSecret_Set_ = true;
			}
		}

		private bool SharedSecret_Set_ { get; set; }

		public EsObjectRO EsObject
		{
			get
			{
				return EsObject_;
			}
			set
			{
				EsObject_ = value;
				EsObject_Set_ = true;
			}
		}

		private bool EsObject_Set_ { get; set; }

		public Dictionary<string, EsObject> UserVariables
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

		public Protocol? Protocol
		{
			get
			{
				return Protocol_;
			}
			set
			{
				Protocol_ = value;
				Protocol_Set_ = true;
			}
		}

		private bool Protocol_Set_ { get; set; }

		public int HashId
		{
			get
			{
				return HashId_;
			}
			set
			{
				HashId_ = value;
				HashId_Set_ = true;
			}
		}

		private bool HashId_Set_ { get; set; }

		public string ClientVersion
		{
			get
			{
				return ClientVersion_;
			}
			set
			{
				ClientVersion_ = value;
				ClientVersion_Set_ = true;
			}
		}

		private bool ClientVersion_Set_ { get; set; }

		public string ClientType
		{
			get
			{
				return ClientType_;
			}
			set
			{
				ClientType_ = value;
				ClientType_Set_ = true;
			}
		}

		private bool ClientType_Set_ { get; set; }

		public LoginRequest()
		{
			base.MessageType = MessageType.LoginRequest;
		}

		public LoginRequest(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftLoginRequest thriftLoginRequest = new ThriftLoginRequest();
			if (UserName_Set_ && UserName != null)
			{
				string userName = UserName;
				thriftLoginRequest.UserName = userName;
			}
			if (Password_Set_ && Password != null)
			{
				string password = Password;
				thriftLoginRequest.Password = password;
			}
			if (SharedSecret_Set_ && SharedSecret != null)
			{
				string sharedSecret = SharedSecret;
				thriftLoginRequest.SharedSecret = sharedSecret;
			}
			if (EsObject_Set_ && EsObject != null)
			{
				ThriftFlattenedEsObjectRO esObject = EsObjectCodec.FlattenEsObject(EsObject).ToThrift() as ThriftFlattenedEsObjectRO;
				thriftLoginRequest.EsObject = esObject;
			}
			if (UserVariables_Set_ && UserVariables != null)
			{
				Dictionary<string, ThriftFlattenedEsObject> dictionary = new Dictionary<string, ThriftFlattenedEsObject>();
				foreach (string key2 in UserVariables.Keys)
				{
					EsObject esObject2 = UserVariables[key2];
					string key = key2;
					ThriftFlattenedEsObject value = EsObjectCodec.FlattenEsObject(esObject2).ToThrift() as ThriftFlattenedEsObject;
					dictionary.Add(key, value);
				}
				thriftLoginRequest.UserVariables = dictionary;
			}
			if (Protocol_Set_ && Protocol.HasValue)
			{
				ThriftProtocol protocol = (ThriftProtocol)(object)ThriftUtil.EnumConvert(typeof(ThriftProtocol), (Enum)(object)Protocol);
				thriftLoginRequest.Protocol = protocol;
			}
			if (HashId_Set_)
			{
				int hashId = HashId;
				thriftLoginRequest.HashId = hashId;
			}
			if (ClientVersion_Set_ && ClientVersion != null)
			{
				string clientVersion = ClientVersion;
				thriftLoginRequest.ClientVersion = clientVersion;
			}
			if (ClientType_Set_ && ClientType != null)
			{
				string clientType = ClientType;
				thriftLoginRequest.ClientType = clientType;
			}
			return thriftLoginRequest;
		}

		public override TBase NewThrift()
		{
			return new ThriftLoginRequest();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftLoginRequest thriftLoginRequest = (ThriftLoginRequest)t_;
			if (thriftLoginRequest.__isset.userName && thriftLoginRequest.UserName != null)
			{
				UserName = thriftLoginRequest.UserName;
			}
			if (thriftLoginRequest.__isset.password && thriftLoginRequest.Password != null)
			{
				Password = thriftLoginRequest.Password;
			}
			if (thriftLoginRequest.__isset.sharedSecret && thriftLoginRequest.SharedSecret != null)
			{
				SharedSecret = thriftLoginRequest.SharedSecret;
			}
			if (thriftLoginRequest.__isset.esObject && thriftLoginRequest.EsObject != null)
			{
				EsObject = EsObjectCodec.UnflattenEsObjectRO(new FlattenedEsObjectRO(thriftLoginRequest.EsObject));
			}
			if (thriftLoginRequest.__isset.userVariables && thriftLoginRequest.UserVariables != null)
			{
				UserVariables = new Dictionary<string, EsObject>();
				foreach (string key2 in thriftLoginRequest.UserVariables.Keys)
				{
					ThriftFlattenedEsObject t = thriftLoginRequest.UserVariables[key2];
					string key = key2;
					EsObject value = EsObjectCodec.UnflattenEsObject(new FlattenedEsObject(t));
					UserVariables.Add(key, value);
				}
			}
			if (thriftLoginRequest.__isset.protocol)
			{
				Protocol = (Protocol)(object)ThriftUtil.EnumConvert(Nullable.GetUnderlyingType(typeof(Protocol?)), thriftLoginRequest.Protocol);
			}
			if (thriftLoginRequest.__isset.hashId)
			{
				HashId = thriftLoginRequest.HashId;
			}
			if (thriftLoginRequest.__isset.clientVersion && thriftLoginRequest.ClientVersion != null)
			{
				ClientVersion = thriftLoginRequest.ClientVersion;
			}
			if (thriftLoginRequest.__isset.clientType && thriftLoginRequest.ClientType != null)
			{
				ClientType = thriftLoginRequest.ClientType;
			}
		}
	}
}
