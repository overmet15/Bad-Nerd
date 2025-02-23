using Electrotank.Electroserver5.Api;

namespace Electrotank.Electroserver5.Core
{
	public class QueuedMessage
	{
		public Connection Connection { get; internal set; }

		public EsMessage Message { get; internal set; }

		public QueuedMessage(EsMessage message, Connection connection)
		{
			Message = message;
			Connection = connection;
		}
	}
}
