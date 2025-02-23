using System.Collections.Generic;
using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class FindGamesResponse : EsResponse
	{
		private ServerGame[] Games_;

		public ServerGame[] Games
		{
			get
			{
				return Games_;
			}
			set
			{
				Games_ = value;
				Games_Set_ = true;
			}
		}

		private bool Games_Set_ { get; set; }

		public FindGamesResponse()
		{
			base.MessageType = MessageType.FindGamesResponse;
		}

		public FindGamesResponse(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftFindGamesResponse thriftFindGamesResponse = new ThriftFindGamesResponse();
			if (Games_Set_ && Games != null)
			{
				List<ThriftServerGame> list = new List<ThriftServerGame>();
				ServerGame[] games = Games;
				foreach (ServerGame serverGame in games)
				{
					ThriftServerGame item = serverGame.ToThrift() as ThriftServerGame;
					list.Add(item);
				}
				thriftFindGamesResponse.Games = list;
			}
			return thriftFindGamesResponse;
		}

		public override TBase NewThrift()
		{
			return new ThriftFindGamesResponse();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftFindGamesResponse thriftFindGamesResponse = (ThriftFindGamesResponse)t_;
			if (thriftFindGamesResponse.__isset.games && thriftFindGamesResponse.Games != null)
			{
				Games = new ServerGame[thriftFindGamesResponse.Games.Count];
				for (int i = 0; i < thriftFindGamesResponse.Games.Count; i++)
				{
					ServerGame serverGame = new ServerGame(thriftFindGamesResponse.Games[i]);
					Games[i] = serverGame;
				}
			}
		}
	}
}
