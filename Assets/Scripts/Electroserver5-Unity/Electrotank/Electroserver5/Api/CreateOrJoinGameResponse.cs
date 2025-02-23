using System;
using Electrotank.Electroserver5.Api.Helper;
using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class CreateOrJoinGameResponse : EsResponse
	{
		private bool Successful_;

		private ErrorType? Error_;

		private int ZoneId_;

		private int RoomId_;

		private int GameId_;

		private EsObjectRO GameDetails_;

		public bool Successful
		{
			get
			{
				return Successful_;
			}
			set
			{
				Successful_ = value;
				Successful_Set_ = true;
			}
		}

		private bool Successful_Set_ { get; set; }

		public ErrorType? Error
		{
			get
			{
				return Error_;
			}
			set
			{
				Error_ = value;
				Error_Set_ = true;
			}
		}

		private bool Error_Set_ { get; set; }

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

		public EsObjectRO GameDetails
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

		public CreateOrJoinGameResponse()
		{
			base.MessageType = MessageType.CreateOrJoinGameResponse;
		}

		public CreateOrJoinGameResponse(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftCreateOrJoinGameResponse thriftCreateOrJoinGameResponse = new ThriftCreateOrJoinGameResponse();
			if (Successful_Set_)
			{
				bool successful = Successful;
				thriftCreateOrJoinGameResponse.Successful = successful;
			}
			if (Error_Set_ && Error.HasValue)
			{
				ThriftErrorType error = (ThriftErrorType)(object)ThriftUtil.EnumConvert(typeof(ThriftErrorType), (Enum)(object)Error);
				thriftCreateOrJoinGameResponse.Error = error;
			}
			if (ZoneId_Set_)
			{
				int zoneId = ZoneId;
				thriftCreateOrJoinGameResponse.ZoneId = zoneId;
			}
			if (RoomId_Set_)
			{
				int roomId = RoomId;
				thriftCreateOrJoinGameResponse.RoomId = roomId;
			}
			if (GameId_Set_)
			{
				int gameId = GameId;
				thriftCreateOrJoinGameResponse.GameId = gameId;
			}
			if (GameDetails_Set_ && GameDetails != null)
			{
				ThriftFlattenedEsObjectRO gameDetails = EsObjectCodec.FlattenEsObject(GameDetails).ToThrift() as ThriftFlattenedEsObjectRO;
				thriftCreateOrJoinGameResponse.GameDetails = gameDetails;
			}
			return thriftCreateOrJoinGameResponse;
		}

		public override TBase NewThrift()
		{
			return new ThriftCreateOrJoinGameResponse();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftCreateOrJoinGameResponse thriftCreateOrJoinGameResponse = (ThriftCreateOrJoinGameResponse)t_;
			if (thriftCreateOrJoinGameResponse.__isset.successful)
			{
				Successful = thriftCreateOrJoinGameResponse.Successful;
			}
			if (thriftCreateOrJoinGameResponse.__isset.error)
			{
				Error = (ErrorType)(object)ThriftUtil.EnumConvert(Nullable.GetUnderlyingType(typeof(ErrorType?)), thriftCreateOrJoinGameResponse.Error);
			}
			if (thriftCreateOrJoinGameResponse.__isset.zoneId)
			{
				ZoneId = thriftCreateOrJoinGameResponse.ZoneId;
			}
			if (thriftCreateOrJoinGameResponse.__isset.roomId)
			{
				RoomId = thriftCreateOrJoinGameResponse.RoomId;
			}
			if (thriftCreateOrJoinGameResponse.__isset.gameId)
			{
				GameId = thriftCreateOrJoinGameResponse.GameId;
			}
			if (thriftCreateOrJoinGameResponse.__isset.gameDetails && thriftCreateOrJoinGameResponse.GameDetails != null)
			{
				GameDetails = EsObjectCodec.UnflattenEsObjectRO(new FlattenedEsObjectRO(thriftCreateOrJoinGameResponse.GameDetails));
			}
		}
	}
}
