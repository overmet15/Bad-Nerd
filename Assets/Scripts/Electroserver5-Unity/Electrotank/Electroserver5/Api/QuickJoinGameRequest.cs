using Electrotank.Electroserver5.Api.Helper;
using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class QuickJoinGameRequest : EsRequest
	{
		private string GameType_;

		private string ZoneName_;

		private string Password_;

		private bool Locked_;

		private bool Hidden_;

		private bool CreateOnly_;

		private EsObject GameDetails_;

		private SearchCriteria Criteria_;

		public string GameType
		{
			get
			{
				return GameType_;
			}
			set
			{
				GameType_ = value;
				GameType_Set_ = true;
			}
		}

		private bool GameType_Set_ { get; set; }

		public string ZoneName
		{
			get
			{
				return ZoneName_;
			}
			set
			{
				ZoneName_ = value;
				ZoneName_Set_ = true;
			}
		}

		private bool ZoneName_Set_ { get; set; }

		public string Password
		{
			get
			{
				return Password_;
			}
			set
			{
				Password_ = value;
				Password_Set_ = true;
			}
		}

		private bool Password_Set_ { get; set; }

		public bool Locked
		{
			get
			{
				return Locked_;
			}
			set
			{
				Locked_ = value;
				Locked_Set_ = true;
			}
		}

		private bool Locked_Set_ { get; set; }

		public bool Hidden
		{
			get
			{
				return Hidden_;
			}
			set
			{
				Hidden_ = value;
				Hidden_Set_ = true;
			}
		}

		private bool Hidden_Set_ { get; set; }

		public bool CreateOnly
		{
			get
			{
				return CreateOnly_;
			}
			set
			{
				CreateOnly_ = value;
				CreateOnly_Set_ = true;
			}
		}

		private bool CreateOnly_Set_ { get; set; }

		public EsObject GameDetails
		{
			get
			{
				return GameDetails_;
			}
			set
			{
				GameDetails_ = value;
				GameDetails_Set_ = true;
			}
		}

		private bool GameDetails_Set_ { get; set; }

		public SearchCriteria Criteria
		{
			get
			{
				return Criteria_;
			}
			set
			{
				Criteria_ = value;
				Criteria_Set_ = true;
			}
		}

		private bool Criteria_Set_ { get; set; }

		public QuickJoinGameRequest()
		{
			base.MessageType = MessageType.CreateOrJoinGameRequest;
		}

		public QuickJoinGameRequest(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftQuickJoinGameRequest thriftQuickJoinGameRequest = new ThriftQuickJoinGameRequest();
			if (GameType_Set_ && GameType != null)
			{
				string gameType = GameType;
				thriftQuickJoinGameRequest.GameType = gameType;
			}
			if (ZoneName_Set_ && ZoneName != null)
			{
				string zoneName = ZoneName;
				thriftQuickJoinGameRequest.ZoneName = zoneName;
			}
			if (Password_Set_ && Password != null)
			{
				string password = Password;
				thriftQuickJoinGameRequest.Password = password;
			}
			if (Locked_Set_)
			{
				bool locked = Locked;
				thriftQuickJoinGameRequest.Locked = locked;
			}
			if (Hidden_Set_)
			{
				bool hidden = Hidden;
				thriftQuickJoinGameRequest.Hidden = hidden;
			}
			if (CreateOnly_Set_)
			{
				bool createOnly = CreateOnly;
				thriftQuickJoinGameRequest.CreateOnly = createOnly;
			}
			if (GameDetails_Set_ && GameDetails != null)
			{
				ThriftFlattenedEsObject gameDetails = EsObjectCodec.FlattenEsObject(GameDetails).ToThrift() as ThriftFlattenedEsObject;
				thriftQuickJoinGameRequest.GameDetails = gameDetails;
			}
			if (Criteria_Set_ && Criteria != null)
			{
				ThriftSearchCriteria criteria = Criteria.ToThrift() as ThriftSearchCriteria;
				thriftQuickJoinGameRequest.Criteria = criteria;
			}
			return thriftQuickJoinGameRequest;
		}

		public override TBase NewThrift()
		{
			return new ThriftQuickJoinGameRequest();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftQuickJoinGameRequest thriftQuickJoinGameRequest = (ThriftQuickJoinGameRequest)t_;
			if (thriftQuickJoinGameRequest.__isset.gameType && thriftQuickJoinGameRequest.GameType != null)
			{
				GameType = thriftQuickJoinGameRequest.GameType;
			}
			if (thriftQuickJoinGameRequest.__isset.zoneName && thriftQuickJoinGameRequest.ZoneName != null)
			{
				ZoneName = thriftQuickJoinGameRequest.ZoneName;
			}
			if (thriftQuickJoinGameRequest.__isset.password && thriftQuickJoinGameRequest.Password != null)
			{
				Password = thriftQuickJoinGameRequest.Password;
			}
			if (thriftQuickJoinGameRequest.__isset.locked)
			{
				Locked = thriftQuickJoinGameRequest.Locked;
			}
			if (thriftQuickJoinGameRequest.__isset.hidden)
			{
				Hidden = thriftQuickJoinGameRequest.Hidden;
			}
			if (thriftQuickJoinGameRequest.__isset.createOnly)
			{
				CreateOnly = thriftQuickJoinGameRequest.CreateOnly;
			}
			if (thriftQuickJoinGameRequest.__isset.gameDetails && thriftQuickJoinGameRequest.GameDetails != null)
			{
				GameDetails = EsObjectCodec.UnflattenEsObject(new FlattenedEsObject(thriftQuickJoinGameRequest.GameDetails));
			}
			if (thriftQuickJoinGameRequest.__isset.criteria && thriftQuickJoinGameRequest.Criteria != null)
			{
				Criteria = new SearchCriteria(thriftQuickJoinGameRequest.Criteria);
			}
		}
	}
}
