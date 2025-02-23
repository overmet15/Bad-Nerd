using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class JoinGameRequest : EsRequest
	{
		private int GameId_;

		private string Password_;

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

		public JoinGameRequest()
		{
			base.MessageType = MessageType.JoinGameRequest;
		}

		public JoinGameRequest(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftJoinGameRequest thriftJoinGameRequest = new ThriftJoinGameRequest();
			if (GameId_Set_)
			{
				int gameId = GameId;
				thriftJoinGameRequest.GameId = gameId;
			}
			if (Password_Set_ && Password != null)
			{
				string password = Password;
				thriftJoinGameRequest.Password = password;
			}
			return thriftJoinGameRequest;
		}

		public override TBase NewThrift()
		{
			return new ThriftJoinGameRequest();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftJoinGameRequest thriftJoinGameRequest = (ThriftJoinGameRequest)t_;
			if (thriftJoinGameRequest.__isset.gameId)
			{
				GameId = thriftJoinGameRequest.GameId;
			}
			if (thriftJoinGameRequest.__isset.password && thriftJoinGameRequest.Password != null)
			{
				Password = thriftJoinGameRequest.Password;
			}
		}
	}
}
