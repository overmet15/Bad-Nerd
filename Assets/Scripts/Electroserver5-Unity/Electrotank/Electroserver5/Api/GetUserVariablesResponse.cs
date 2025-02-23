using System.Collections.Generic;
using Electrotank.Electroserver5.Api.Helper;
using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class GetUserVariablesResponse : EsResponse
	{
		private string UserName_;

		private Dictionary<string, EsObject> UserVariables_;

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

		public GetUserVariablesResponse()
		{
			base.MessageType = MessageType.GetUserVariablesResponse;
		}

		public GetUserVariablesResponse(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftGetUserVariablesResponse thriftGetUserVariablesResponse = new ThriftGetUserVariablesResponse();
			if (UserName_Set_ && UserName != null)
			{
				string userName = UserName;
				thriftGetUserVariablesResponse.UserName = userName;
			}
			if (UserVariables_Set_ && UserVariables != null)
			{
				Dictionary<string, ThriftFlattenedEsObject> dictionary = new Dictionary<string, ThriftFlattenedEsObject>();
				foreach (string key2 in UserVariables.Keys)
				{
					EsObject esObject = UserVariables[key2];
					string key = key2;
					ThriftFlattenedEsObject value = EsObjectCodec.FlattenEsObject(esObject).ToThrift() as ThriftFlattenedEsObject;
					dictionary.Add(key, value);
				}
				thriftGetUserVariablesResponse.UserVariables = dictionary;
			}
			return thriftGetUserVariablesResponse;
		}

		public override TBase NewThrift()
		{
			return new ThriftGetUserVariablesResponse();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftGetUserVariablesResponse thriftGetUserVariablesResponse = (ThriftGetUserVariablesResponse)t_;
			if (thriftGetUserVariablesResponse.__isset.userName && thriftGetUserVariablesResponse.UserName != null)
			{
				UserName = thriftGetUserVariablesResponse.UserName;
			}
			if (!thriftGetUserVariablesResponse.__isset.userVariables || thriftGetUserVariablesResponse.UserVariables == null)
			{
				return;
			}
			UserVariables = new Dictionary<string, EsObject>();
			foreach (string key2 in thriftGetUserVariablesResponse.UserVariables.Keys)
			{
				ThriftFlattenedEsObject t = thriftGetUserVariablesResponse.UserVariables[key2];
				string key = key2;
				EsObject value = EsObjectCodec.UnflattenEsObject(new FlattenedEsObject(t));
				UserVariables.Add(key, value);
			}
		}
	}
}
