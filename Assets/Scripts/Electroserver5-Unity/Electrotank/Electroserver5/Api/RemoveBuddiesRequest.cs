using System.Collections.Generic;
using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class RemoveBuddiesRequest : EsRequest
	{
		private List<string> BuddyNames_;

		public List<string> BuddyNames
		{
			get
			{
				return BuddyNames_;
			}
			set
			{
				BuddyNames_ = value;
				BuddyNames_Set_ = true;
			}
		}

		private bool BuddyNames_Set_ { get; set; }

		public RemoveBuddiesRequest()
		{
			base.MessageType = MessageType.RemoveBuddiesRequest;
		}

		public RemoveBuddiesRequest(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftRemoveBuddiesRequest thriftRemoveBuddiesRequest = new ThriftRemoveBuddiesRequest();
			if (BuddyNames_Set_ && BuddyNames != null)
			{
				List<string> list = new List<string>();
				foreach (string buddyName in BuddyNames)
				{
					string item = buddyName;
					list.Add(item);
				}
				thriftRemoveBuddiesRequest.BuddyNames = list;
			}
			return thriftRemoveBuddiesRequest;
		}

		public override TBase NewThrift()
		{
			return new ThriftRemoveBuddiesRequest();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftRemoveBuddiesRequest thriftRemoveBuddiesRequest = (ThriftRemoveBuddiesRequest)t_;
			if (!thriftRemoveBuddiesRequest.__isset.buddyNames || thriftRemoveBuddiesRequest.BuddyNames == null)
			{
				return;
			}
			BuddyNames = new List<string>();
			foreach (string buddyName in thriftRemoveBuddiesRequest.BuddyNames)
			{
				string item = buddyName;
				BuddyNames.Add(item);
			}
		}
	}
}
