using Electrotank.Electroserver5.Api.Helper;
using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class SearchCriteria : EsEntity
	{
		private int GameId_;

		private bool Locked_;

		private bool LockedSet_;

		private string GameType_;

		private EsObject GameDetails_;

		public int GameId
		{
			get
			{
				return GameId_;
			}
			set
			{
				GameId_ = value;
				GameId_Set_ = true;
			}
		}

		private bool GameId_Set_ { get; set; }

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

		public bool LockedSet
		{
			get
			{
				return LockedSet_;
			}
			set
			{
				LockedSet_ = value;
				LockedSet_Set_ = true;
			}
		}

		private bool LockedSet_Set_ { get; set; }

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

		public SearchCriteria()
		{
		}

		public SearchCriteria(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftSearchCriteria thriftSearchCriteria = new ThriftSearchCriteria();
			if (GameId_Set_)
			{
				int gameId = GameId;
				thriftSearchCriteria.GameId = gameId;
			}
			if (Locked_Set_)
			{
				bool locked = Locked;
				thriftSearchCriteria.Locked = locked;
			}
			if (LockedSet_Set_)
			{
				bool lockedSet = LockedSet;
				thriftSearchCriteria.LockedSet = lockedSet;
			}
			if (GameType_Set_ && GameType != null)
			{
				string gameType = GameType;
				thriftSearchCriteria.GameType = gameType;
			}
			if (GameDetails_Set_ && GameDetails != null)
			{
				ThriftFlattenedEsObject gameDetails = EsObjectCodec.FlattenEsObject(GameDetails).ToThrift() as ThriftFlattenedEsObject;
				thriftSearchCriteria.GameDetails = gameDetails;
			}
			return thriftSearchCriteria;
		}

		public override TBase NewThrift()
		{
			return new ThriftSearchCriteria();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftSearchCriteria thriftSearchCriteria = (ThriftSearchCriteria)t_;
			if (thriftSearchCriteria.__isset.gameId)
			{
				GameId = thriftSearchCriteria.GameId;
			}
			if (thriftSearchCriteria.__isset.locked)
			{
				Locked = thriftSearchCriteria.Locked;
			}
			if (thriftSearchCriteria.__isset.lockedSet)
			{
				LockedSet = thriftSearchCriteria.LockedSet;
			}
			if (thriftSearchCriteria.__isset.gameType && thriftSearchCriteria.GameType != null)
			{
				GameType = thriftSearchCriteria.GameType;
			}
			if (thriftSearchCriteria.__isset.gameDetails && thriftSearchCriteria.GameDetails != null)
			{
				GameDetails = EsObjectCodec.UnflattenEsObject(new FlattenedEsObject(thriftSearchCriteria.GameDetails));
			}
		}
	}
}
