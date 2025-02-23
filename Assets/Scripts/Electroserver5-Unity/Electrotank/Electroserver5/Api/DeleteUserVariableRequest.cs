using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class DeleteUserVariableRequest : EsRequest
	{
		private string Name_;

		public string Name
		{
			get
			{
				return Name_;
			}
			set
			{
				Name_ = value;
				Name_Set_ = true;
			}
		}

		private bool Name_Set_ { get; set; }

		public DeleteUserVariableRequest()
		{
			base.MessageType = MessageType.DeleteUserVariableRequest;
		}

		public DeleteUserVariableRequest(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftDeleteUserVariableRequest thriftDeleteUserVariableRequest = new ThriftDeleteUserVariableRequest();
			if (Name_Set_ && Name != null)
			{
				string name = Name;
				thriftDeleteUserVariableRequest.Name = name;
			}
			return thriftDeleteUserVariableRequest;
		}

		public override TBase NewThrift()
		{
			return new ThriftDeleteUserVariableRequest();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftDeleteUserVariableRequest thriftDeleteUserVariableRequest = (ThriftDeleteUserVariableRequest)t_;
			if (thriftDeleteUserVariableRequest.__isset.name && thriftDeleteUserVariableRequest.Name != null)
			{
				Name = thriftDeleteUserVariableRequest.Name;
			}
		}
	}
}
