using System.Collections.Generic;
using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class RemoveBuddiesResponse : EsResponse
	{
		private List<string> BuddiesRemoved_;

		private List<string> BuddiesNotRemoved_;

		public List<string> BuddiesRemoved
		{
			get
			{
				return BuddiesRemoved_;
			}
			set
			{
				BuddiesRemoved_ = value;
				BuddiesRemoved_Set_ = true;
			}
		}

		private bool BuddiesRemoved_Set_ { get; set; }

		public List<string> BuddiesNotRemoved
		{
			get
			{
				return BuddiesNotRemoved_;
			}
			set
			{
				BuddiesNotRemoved_ = value;
				BuddiesNotRemoved_Set_ = true;
			}
		}

		private bool BuddiesNotRemoved_Set_ { get; set; }

		public RemoveBuddiesResponse()
		{
			base.MessageType = MessageType.RemoveBuddiesResponse;
		}

		public RemoveBuddiesResponse(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftRemoveBuddiesResponse thriftRemoveBuddiesResponse = new ThriftRemoveBuddiesResponse();
			if (BuddiesRemoved_Set_ && BuddiesRemoved != null)
			{
				List<string> list = new List<string>();
				foreach (string item3 in BuddiesRemoved)
				{
					string item = item3;
					list.Add(item);
				}
				thriftRemoveBuddiesResponse.BuddiesRemoved = list;
			}
			if (BuddiesNotRemoved_Set_ && BuddiesNotRemoved != null)
			{
				List<string> list2 = new List<string>();
				foreach (string item4 in BuddiesNotRemoved)
				{
					string item2 = item4;
					list2.Add(item2);
				}
				thriftRemoveBuddiesResponse.BuddiesNotRemoved = list2;
			}
			return thriftRemoveBuddiesResponse;
		}

		public override TBase NewThrift()
		{
			return new ThriftRemoveBuddiesResponse();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftRemoveBuddiesResponse thriftRemoveBuddiesResponse = (ThriftRemoveBuddiesResponse)t_;
			if (thriftRemoveBuddiesResponse.__isset.buddiesRemoved && thriftRemoveBuddiesResponse.BuddiesRemoved != null)
			{
				BuddiesRemoved = new List<string>();
				foreach (string item3 in thriftRemoveBuddiesResponse.BuddiesRemoved)
				{
					string item = item3;
					BuddiesRemoved.Add(item);
				}
			}
			if (!thriftRemoveBuddiesResponse.__isset.buddiesNotRemoved || thriftRemoveBuddiesResponse.BuddiesNotRemoved == null)
			{
				return;
			}
			BuddiesNotRemoved = new List<string>();
			foreach (string item4 in thriftRemoveBuddiesResponse.BuddiesNotRemoved)
			{
				string item2 = item4;
				BuddiesNotRemoved.Add(item2);
			}
		}
	}
}
