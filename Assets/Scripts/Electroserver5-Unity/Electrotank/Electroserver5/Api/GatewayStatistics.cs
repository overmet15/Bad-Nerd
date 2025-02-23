using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class GatewayStatistics : EsEntity
	{
		private long BytesInTotal_;

		private long BytesOutTotal_;

		private long MessagesInTotal_;

		private long MessagesOutTotal_;

		public long BytesInTotal
		{
			get
			{
				return BytesInTotal_;
			}
			set
			{
				BytesInTotal_ = value;
				BytesInTotal_Set_ = true;
			}
		}

		private bool BytesInTotal_Set_ { get; set; }

		public long BytesOutTotal
		{
			get
			{
				return BytesOutTotal_;
			}
			set
			{
				BytesOutTotal_ = value;
				BytesOutTotal_Set_ = true;
			}
		}

		private bool BytesOutTotal_Set_ { get; set; }

		public long MessagesInTotal
		{
			get
			{
				return MessagesInTotal_;
			}
			set
			{
				MessagesInTotal_ = value;
				MessagesInTotal_Set_ = true;
			}
		}

		private bool MessagesInTotal_Set_ { get; set; }

		public long MessagesOutTotal
		{
			get
			{
				return MessagesOutTotal_;
			}
			set
			{
				MessagesOutTotal_ = value;
				MessagesOutTotal_Set_ = true;
			}
		}

		private bool MessagesOutTotal_Set_ { get; set; }

		public GatewayStatistics()
		{
		}

		public GatewayStatistics(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftGatewayStatistics thriftGatewayStatistics = new ThriftGatewayStatistics();
			if (BytesInTotal_Set_)
			{
				long bytesInTotal = BytesInTotal;
				thriftGatewayStatistics.BytesInTotal = bytesInTotal;
			}
			if (BytesOutTotal_Set_)
			{
				long bytesOutTotal = BytesOutTotal;
				thriftGatewayStatistics.BytesOutTotal = bytesOutTotal;
			}
			if (MessagesInTotal_Set_)
			{
				long messagesInTotal = MessagesInTotal;
				thriftGatewayStatistics.MessagesInTotal = messagesInTotal;
			}
			if (MessagesOutTotal_Set_)
			{
				long messagesOutTotal = MessagesOutTotal;
				thriftGatewayStatistics.MessagesOutTotal = messagesOutTotal;
			}
			return thriftGatewayStatistics;
		}

		public override TBase NewThrift()
		{
			return new ThriftGatewayStatistics();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftGatewayStatistics thriftGatewayStatistics = (ThriftGatewayStatistics)t_;
			if (thriftGatewayStatistics.__isset.bytesInTotal)
			{
				BytesInTotal = thriftGatewayStatistics.BytesInTotal;
			}
			if (thriftGatewayStatistics.__isset.bytesOutTotal)
			{
				BytesOutTotal = thriftGatewayStatistics.BytesOutTotal;
			}
			if (thriftGatewayStatistics.__isset.messagesInTotal)
			{
				MessagesInTotal = thriftGatewayStatistics.MessagesInTotal;
			}
			if (thriftGatewayStatistics.__isset.messagesOutTotal)
			{
				MessagesOutTotal = thriftGatewayStatistics.MessagesOutTotal;
			}
		}
	}
}
