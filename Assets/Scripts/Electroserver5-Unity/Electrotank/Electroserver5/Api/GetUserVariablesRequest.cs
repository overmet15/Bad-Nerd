using Thrift.Collections;
using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class GetUserVariablesRequest : EsRequest
	{
		private string UserName_;

		private THashSet<string> UserVariableNames_;

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

		public THashSet<string> UserVariableNames
		{
			get
			{
				return UserVariableNames_;
			}
			set
			{
				UserVariableNames_ = value;
				UserVariableNames_Set_ = true;
			}
		}

		private bool UserVariableNames_Set_ { get; set; }

		public GetUserVariablesRequest()
		{
			base.MessageType = MessageType.GetUserVariablesRequest;
		}

		public GetUserVariablesRequest(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftGetUserVariablesRequest thriftGetUserVariablesRequest = new ThriftGetUserVariablesRequest();
			if (UserName_Set_ && UserName != null)
			{
				string userName = UserName;
				thriftGetUserVariablesRequest.UserName = userName;
			}
			if (UserVariableNames_Set_ && UserVariableNames != null)
			{
				THashSet<string> tHashSet = new THashSet<string>();
				foreach (string userVariableName in UserVariableNames)
				{
					string item = userVariableName;
					tHashSet.Add(item);
				}
				thriftGetUserVariablesRequest.UserVariableNames = tHashSet;
			}
			return thriftGetUserVariablesRequest;
		}

		public override TBase NewThrift()
		{
			return new ThriftGetUserVariablesRequest();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftGetUserVariablesRequest thriftGetUserVariablesRequest = (ThriftGetUserVariablesRequest)t_;
			if (thriftGetUserVariablesRequest.__isset.userName && thriftGetUserVariablesRequest.UserName != null)
			{
				UserName = thriftGetUserVariablesRequest.UserName;
			}
			if (!thriftGetUserVariablesRequest.__isset.userVariableNames || thriftGetUserVariablesRequest.UserVariableNames == null)
			{
				return;
			}
			UserVariableNames = new THashSet<string>();
			foreach (string userVariableName in thriftGetUserVariablesRequest.UserVariableNames)
			{
				string item = userVariableName;
				UserVariableNames.Add(item);
			}
		}
	}
}
