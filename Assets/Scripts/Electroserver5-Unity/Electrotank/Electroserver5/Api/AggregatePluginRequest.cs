using System.Collections.Generic;
using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class AggregatePluginRequest : EsRequest
	{
		private RequestDetails[] PluginRequestArray_;

		public RequestDetails[] PluginRequestArray
		{
			get
			{
				return PluginRequestArray_;
			}
			set
			{
				PluginRequestArray_ = value;
				PluginRequestArray_Set_ = true;
			}
		}

		private bool PluginRequestArray_Set_ { get; set; }

		public AggregatePluginRequest()
		{
			base.MessageType = MessageType.AggregatePluginRequest;
		}

		public AggregatePluginRequest(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftAggregatePluginRequest thriftAggregatePluginRequest = new ThriftAggregatePluginRequest();
			if (PluginRequestArray_Set_ && PluginRequestArray != null)
			{
				List<ThriftRequestDetails> list = new List<ThriftRequestDetails>();
				RequestDetails[] pluginRequestArray = PluginRequestArray;
				foreach (RequestDetails requestDetails in pluginRequestArray)
				{
					ThriftRequestDetails item = requestDetails.ToThrift() as ThriftRequestDetails;
					list.Add(item);
				}
				thriftAggregatePluginRequest.PluginRequestArray = list;
			}
			return thriftAggregatePluginRequest;
		}

		public override TBase NewThrift()
		{
			return new ThriftAggregatePluginRequest();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftAggregatePluginRequest thriftAggregatePluginRequest = (ThriftAggregatePluginRequest)t_;
			if (thriftAggregatePluginRequest.__isset.pluginRequestArray && thriftAggregatePluginRequest.PluginRequestArray != null)
			{
				PluginRequestArray = new RequestDetails[thriftAggregatePluginRequest.PluginRequestArray.Count];
				for (int i = 0; i < thriftAggregatePluginRequest.PluginRequestArray.Count; i++)
				{
					RequestDetails requestDetails = new RequestDetails(thriftAggregatePluginRequest.PluginRequestArray[i]);
					PluginRequestArray[i] = requestDetails;
				}
			}
		}
	}
}
