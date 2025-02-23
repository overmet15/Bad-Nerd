using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class LogOutRequest : EsRequest
	{
		private bool DropConnection_;

		private bool DropAllConnections_;

		public bool DropConnection
		{
			get
			{
				return DropConnection_;
			}
			set
			{
				DropConnection_ = value;
				DropConnection_Set_ = true;
			}
		}

		private bool DropConnection_Set_ { get; set; }

		public bool DropAllConnections
		{
			get
			{
				return DropAllConnections_;
			}
			set
			{
				DropAllConnections_ = value;
				DropAllConnections_Set_ = true;
			}
		}

		private bool DropAllConnections_Set_ { get; set; }

		public LogOutRequest()
		{
			base.MessageType = MessageType.LogOutRequest;
			DropAllConnections = true;
			DropAllConnections_Set_ = true;
		}

		public LogOutRequest(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftLogOutRequest thriftLogOutRequest = new ThriftLogOutRequest();
			if (DropConnection_Set_)
			{
				bool dropConnection = DropConnection;
				thriftLogOutRequest.DropConnection = dropConnection;
			}
			if (DropAllConnections_Set_)
			{
				bool dropAllConnections = DropAllConnections;
				thriftLogOutRequest.DropAllConnections = dropAllConnections;
			}
			return thriftLogOutRequest;
		}

		public override TBase NewThrift()
		{
			return new ThriftLogOutRequest();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftLogOutRequest thriftLogOutRequest = (ThriftLogOutRequest)t_;
			if (thriftLogOutRequest.__isset.dropConnection)
			{
				DropConnection = thriftLogOutRequest.DropConnection;
			}
			if (thriftLogOutRequest.__isset.dropAllConnections)
			{
				DropAllConnections = thriftLogOutRequest.DropAllConnections;
			}
		}
	}
}
