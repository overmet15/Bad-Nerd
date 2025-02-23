using System;
using System.Collections.Generic;
using Electrotank.Electroserver5.Api.Helper;
using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class LoginResponse : EsResponse
	{
		private bool Successful_;

		private ErrorType? Error_;

		private EsObjectRO EsObject_;

		private string UserName_;

		private Dictionary<string, EsObjectRO> UserVariables_;

		private Dictionary<string, EsObjectRO> BuddyListEntries_;

		public bool Successful
		{
			get
			{
				return Successful_;
			}
			set
			{
				Successful_ = value;
				Successful_Set_ = true;
			}
		}

		private bool Successful_Set_ { get; set; }

		public ErrorType? Error
		{
			get
			{
				return Error_;
			}
			set
			{
				Error_ = value;
				Error_Set_ = true;
			}
		}

		private bool Error_Set_ { get; set; }

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

		public Dictionary<string, EsObjectRO> UserVariables
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

		public Dictionary<string, EsObjectRO> BuddyListEntries
		{
			get
			{
				return BuddyListEntries_;
			}
			set
			{
				BuddyListEntries_ = value;
				BuddyListEntries_Set_ = true;
			}
		}

		private bool BuddyListEntries_Set_ { get; set; }

		public LoginResponse()
		{
			base.MessageType = MessageType.LoginResponse;
		}

		public LoginResponse(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftLoginResponse thriftLoginResponse = new ThriftLoginResponse();
			if (Successful_Set_)
			{
				bool successful = Successful;
				thriftLoginResponse.Successful = successful;
			}
			if (Error_Set_ && Error.HasValue)
			{
				ThriftErrorType error = (ThriftErrorType)(object)ThriftUtil.EnumConvert(typeof(ThriftErrorType), (Enum)(object)Error);
				thriftLoginResponse.Error = error;
			}
			if (EsObject_Set_ && EsObject != null)
			{
				ThriftFlattenedEsObjectRO esObject = EsObjectCodec.FlattenEsObject(EsObject).ToThrift() as ThriftFlattenedEsObjectRO;
				thriftLoginResponse.EsObject = esObject;
			}
			if (UserName_Set_ && UserName != null)
			{
				string userName = UserName;
				thriftLoginResponse.UserName = userName;
			}
			if (UserVariables_Set_ && UserVariables != null)
			{
				Dictionary<string, ThriftFlattenedEsObjectRO> dictionary = new Dictionary<string, ThriftFlattenedEsObjectRO>();
				foreach (string key3 in UserVariables.Keys)
				{
					EsObjectRO esObject2 = UserVariables[key3];
					string key = key3;
					ThriftFlattenedEsObjectRO value = EsObjectCodec.FlattenEsObject(esObject2).ToThrift() as ThriftFlattenedEsObjectRO;
					dictionary.Add(key, value);
				}
				thriftLoginResponse.UserVariables = dictionary;
			}
			if (BuddyListEntries_Set_ && BuddyListEntries != null)
			{
				Dictionary<string, ThriftFlattenedEsObjectRO> dictionary2 = new Dictionary<string, ThriftFlattenedEsObjectRO>();
				foreach (string key4 in BuddyListEntries.Keys)
				{
					EsObjectRO esObject3 = BuddyListEntries[key4];
					string key2 = key4;
					ThriftFlattenedEsObjectRO value2 = EsObjectCodec.FlattenEsObject(esObject3).ToThrift() as ThriftFlattenedEsObjectRO;
					dictionary2.Add(key2, value2);
				}
				thriftLoginResponse.BuddyListEntries = dictionary2;
			}
			return thriftLoginResponse;
		}

		public override TBase NewThrift()
		{
			return new ThriftLoginResponse();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftLoginResponse thriftLoginResponse = (ThriftLoginResponse)t_;
			if (thriftLoginResponse.__isset.successful)
			{
				Successful = thriftLoginResponse.Successful;
			}
			if (thriftLoginResponse.__isset.error)
			{
				Error = (ErrorType)(object)ThriftUtil.EnumConvert(Nullable.GetUnderlyingType(typeof(ErrorType?)), thriftLoginResponse.Error);
			}
			if (thriftLoginResponse.__isset.esObject && thriftLoginResponse.EsObject != null)
			{
				EsObject = EsObjectCodec.UnflattenEsObjectRO(new FlattenedEsObjectRO(thriftLoginResponse.EsObject));
			}
			if (thriftLoginResponse.__isset.userName && thriftLoginResponse.UserName != null)
			{
				UserName = thriftLoginResponse.UserName;
			}
			if (thriftLoginResponse.__isset.userVariables && thriftLoginResponse.UserVariables != null)
			{
				UserVariables = new Dictionary<string, EsObjectRO>();
				foreach (string key3 in thriftLoginResponse.UserVariables.Keys)
				{
					ThriftFlattenedEsObjectRO t = thriftLoginResponse.UserVariables[key3];
					string key = key3;
					EsObjectRO value = EsObjectCodec.UnflattenEsObjectRO(new FlattenedEsObjectRO(t));
					UserVariables.Add(key, value);
				}
			}
			if (!thriftLoginResponse.__isset.buddyListEntries || thriftLoginResponse.BuddyListEntries == null)
			{
				return;
			}
			BuddyListEntries = new Dictionary<string, EsObjectRO>();
			foreach (string key4 in thriftLoginResponse.BuddyListEntries.Keys)
			{
				ThriftFlattenedEsObjectRO t2 = thriftLoginResponse.BuddyListEntries[key4];
				string key2 = key4;
				EsObjectRO value2 = EsObjectCodec.UnflattenEsObjectRO(new FlattenedEsObjectRO(t2));
				BuddyListEntries.Add(key2, value2);
			}
		}
	}
}
