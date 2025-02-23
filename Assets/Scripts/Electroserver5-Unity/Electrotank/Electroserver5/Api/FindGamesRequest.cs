using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class FindGamesRequest : EsRequest
	{
		private SearchCriteria SearchCriteria_;

		public SearchCriteria SearchCriteria
		{
			get
			{
				return SearchCriteria_;
			}
			set
			{
				SearchCriteria_ = value;
				SearchCriteria_Set_ = true;
			}
		}

		private bool SearchCriteria_Set_ { get; set; }

		public FindGamesRequest()
		{
			base.MessageType = MessageType.FindGamesRequest;
		}

		public FindGamesRequest(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftFindGamesRequest thriftFindGamesRequest = new ThriftFindGamesRequest();
			if (SearchCriteria_Set_ && SearchCriteria != null)
			{
				ThriftSearchCriteria searchCriteria = SearchCriteria.ToThrift() as ThriftSearchCriteria;
				thriftFindGamesRequest.SearchCriteria = searchCriteria;
			}
			return thriftFindGamesRequest;
		}

		public override TBase NewThrift()
		{
			return new ThriftFindGamesRequest();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftFindGamesRequest thriftFindGamesRequest = (ThriftFindGamesRequest)t_;
			if (thriftFindGamesRequest.__isset.searchCriteria && thriftFindGamesRequest.SearchCriteria != null)
			{
				SearchCriteria = new SearchCriteria(thriftFindGamesRequest.SearchCriteria);
			}
		}
	}
}
