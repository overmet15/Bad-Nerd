using System.Collections.Generic;
using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class AddBuddiesResponse : EsResponse
	{
		private List<string> BuddiesAdded_;

		private List<string> BuddiesNotAdded_;

		public List<string> BuddiesAdded
		{
			get
			{
				return BuddiesAdded_;
			}
			set
			{
				BuddiesAdded_ = value;
				BuddiesAdded_Set_ = true;
			}
		}

		private bool BuddiesAdded_Set_ { get; set; }

		public List<string> BuddiesNotAdded
		{
			get
			{
				return BuddiesNotAdded_;
			}
			set
			{
				BuddiesNotAdded_ = value;
				BuddiesNotAdded_Set_ = true;
			}
		}

		private bool BuddiesNotAdded_Set_ { get; set; }

		public AddBuddiesResponse()
		{
			base.MessageType = MessageType.AddBuddiesResponse;
		}

		public AddBuddiesResponse(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftAddBuddiesResponse thriftAddBuddiesResponse = new ThriftAddBuddiesResponse();
			if (BuddiesAdded_Set_ && BuddiesAdded != null)
			{
				List<string> list = new List<string>();
				foreach (string item3 in BuddiesAdded)
				{
					string item = item3;
					list.Add(item);
				}
				thriftAddBuddiesResponse.BuddiesAdded = list;
			}
			if (BuddiesNotAdded_Set_ && BuddiesNotAdded != null)
			{
				List<string> list2 = new List<string>();
				foreach (string item4 in BuddiesNotAdded)
				{
					string item2 = item4;
					list2.Add(item2);
				}
				thriftAddBuddiesResponse.BuddiesNotAdded = list2;
			}
			return thriftAddBuddiesResponse;
		}

		public override TBase NewThrift()
		{
			return new ThriftAddBuddiesResponse();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftAddBuddiesResponse thriftAddBuddiesResponse = (ThriftAddBuddiesResponse)t_;
			if (thriftAddBuddiesResponse.__isset.buddiesAdded && thriftAddBuddiesResponse.BuddiesAdded != null)
			{
				BuddiesAdded = new List<string>();
				foreach (string item3 in thriftAddBuddiesResponse.BuddiesAdded)
				{
					string item = item3;
					BuddiesAdded.Add(item);
				}
			}
			if (!thriftAddBuddiesResponse.__isset.buddiesNotAdded || thriftAddBuddiesResponse.BuddiesNotAdded == null)
			{
				return;
			}
			BuddiesNotAdded = new List<string>();
			foreach (string item4 in thriftAddBuddiesResponse.BuddiesNotAdded)
			{
				string item2 = item4;
				BuddiesNotAdded.Add(item2);
			}
		}
	}
}
