using System.Collections.Generic;
using Electrotank.Electroserver5.Api.Helper;
using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class AddBuddiesRequest : EsRequest
	{
		private List<string> BuddyNames_;

		private EsObject EsObject_;

		private bool SkipInitialLoggedOutEvents_;

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

		public EsObject EsObject
		{
			get
			{
				return EsObject_;
			}
			set
			{
				EsObject_ = value;
				EsObject_Set_ = true;
			}
		}

		private bool EsObject_Set_ { get; set; }

		public bool SkipInitialLoggedOutEvents
		{
			get
			{
				return SkipInitialLoggedOutEvents_;
			}
			set
			{
				SkipInitialLoggedOutEvents_ = value;
				SkipInitialLoggedOutEvents_Set_ = true;
			}
		}

		private bool SkipInitialLoggedOutEvents_Set_ { get; set; }

		public AddBuddiesRequest()
		{
			base.MessageType = MessageType.AddBuddiesRequest;
			SkipInitialLoggedOutEvents = false;
			SkipInitialLoggedOutEvents_Set_ = true;
		}

		public AddBuddiesRequest(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftAddBuddiesRequest thriftAddBuddiesRequest = new ThriftAddBuddiesRequest();
			if (BuddyNames_Set_ && BuddyNames != null)
			{
				List<string> list = new List<string>();
				foreach (string buddyName in BuddyNames)
				{
					string item = buddyName;
					list.Add(item);
				}
				thriftAddBuddiesRequest.BuddyNames = list;
			}
			if (EsObject_Set_ && EsObject != null)
			{
				ThriftFlattenedEsObject esObject = EsObjectCodec.FlattenEsObject(EsObject).ToThrift() as ThriftFlattenedEsObject;
				thriftAddBuddiesRequest.EsObject = esObject;
			}
			if (SkipInitialLoggedOutEvents_Set_)
			{
				bool skipInitialLoggedOutEvents = SkipInitialLoggedOutEvents;
				thriftAddBuddiesRequest.SkipInitialLoggedOutEvents = skipInitialLoggedOutEvents;
			}
			return thriftAddBuddiesRequest;
		}

		public override TBase NewThrift()
		{
			return new ThriftAddBuddiesRequest();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftAddBuddiesRequest thriftAddBuddiesRequest = (ThriftAddBuddiesRequest)t_;
			if (thriftAddBuddiesRequest.__isset.buddyNames && thriftAddBuddiesRequest.BuddyNames != null)
			{
				BuddyNames = new List<string>();
				foreach (string buddyName in thriftAddBuddiesRequest.BuddyNames)
				{
					string item = buddyName;
					BuddyNames.Add(item);
				}
			}
			if (thriftAddBuddiesRequest.__isset.esObject && thriftAddBuddiesRequest.EsObject != null)
			{
				EsObject = EsObjectCodec.UnflattenEsObject(new FlattenedEsObject(thriftAddBuddiesRequest.EsObject));
			}
			if (thriftAddBuddiesRequest.__isset.skipInitialLoggedOutEvents)
			{
				SkipInitialLoggedOutEvents = thriftAddBuddiesRequest.SkipInitialLoggedOutEvents;
			}
		}
	}
}
