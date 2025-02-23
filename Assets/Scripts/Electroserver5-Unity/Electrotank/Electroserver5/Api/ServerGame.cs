using Electrotank.Electroserver5.Api.Helper;
using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class ServerGame : EsEntity
	{
		private EsObject GameDetails_;

		private int Id_;

		private int RoomId_;

		private int ZoneId_;

		private bool Locked_;

		private bool PasswordProtected_;

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

		public int Id
		{
			get
			{
				return Id_;
			}
			set
			{
				Id_ = value;
				Id_Set_ = true;
			}
		}

		private bool Id_Set_ { get; set; }

		public int RoomId
		{
			get
			{
				return RoomId_;
			}
			set
			{
				RoomId_ = value;
				RoomId_Set_ = true;
			}
		}

		private bool RoomId_Set_ { get; set; }

		public int ZoneId
		{
			get
			{
				return ZoneId_;
			}
			set
			{
				ZoneId_ = value;
				ZoneId_Set_ = true;
			}
		}

		private bool ZoneId_Set_ { get; set; }

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

		public bool PasswordProtected
		{
			get
			{
				return PasswordProtected_;
			}
			set
			{
				PasswordProtected_ = value;
				PasswordProtected_Set_ = true;
			}
		}

		private bool PasswordProtected_Set_ { get; set; }

		public ServerGame()
		{
		}

		public ServerGame(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftServerGame thriftServerGame = new ThriftServerGame();
			if (GameDetails_Set_ && GameDetails != null)
			{
				ThriftFlattenedEsObject gameDetails = EsObjectCodec.FlattenEsObject(GameDetails).ToThrift() as ThriftFlattenedEsObject;
				thriftServerGame.GameDetails = gameDetails;
			}
			if (Id_Set_)
			{
				int id = Id;
				thriftServerGame.Id = id;
			}
			if (RoomId_Set_)
			{
				int roomId = RoomId;
				thriftServerGame.RoomId = roomId;
			}
			if (ZoneId_Set_)
			{
				int zoneId = ZoneId;
				thriftServerGame.ZoneId = zoneId;
			}
			if (Locked_Set_)
			{
				bool locked = Locked;
				thriftServerGame.Locked = locked;
			}
			if (PasswordProtected_Set_)
			{
				bool passwordProtected = PasswordProtected;
				thriftServerGame.PasswordProtected = passwordProtected;
			}
			return thriftServerGame;
		}

		public override TBase NewThrift()
		{
			return new ThriftServerGame();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftServerGame thriftServerGame = (ThriftServerGame)t_;
			if (thriftServerGame.__isset.gameDetails && thriftServerGame.GameDetails != null)
			{
				GameDetails = EsObjectCodec.UnflattenEsObject(new FlattenedEsObject(thriftServerGame.GameDetails));
			}
			if (thriftServerGame.__isset.id)
			{
				Id = thriftServerGame.Id;
			}
			if (thriftServerGame.__isset.roomId)
			{
				RoomId = thriftServerGame.RoomId;
			}
			if (thriftServerGame.__isset.zoneId)
			{
				ZoneId = thriftServerGame.ZoneId;
			}
			if (thriftServerGame.__isset.locked)
			{
				Locked = thriftServerGame.Locked;
			}
			if (thriftServerGame.__isset.passwordProtected)
			{
				PasswordProtected = thriftServerGame.PasswordProtected;
			}
		}
	}
}
