using Electrotank.Electroserver5.Api;
using Electrotank.Electroserver5.Core.Util;

namespace Electrotank.Electroserver5.Core
{
	public abstract class Connection
	{
		internal int outboundId = -1;

		public AvailableConnection AvailableConnection { get; internal set; }

		public abstract bool Connected { get; protected set; }

		public int ConnectionId { get; internal set; }

		public DhAesEncryptionContext EncryptionContext { get; set; }

		public bool PrimaryCapable { get; internal set; }

		public abstract Protocol Protocol { get; }

		public int SessionKey { get; internal set; }

		public string ServerVersion { get; internal set; }

		public Connection(AvailableConnection availableConnection)
		{
			AvailableConnection = availableConnection;
		}

		public abstract void Close();

		public abstract void Connect();

		public abstract void Send(EsMessage request);

		public int GetNextOutboundId()
		{
			outboundId++;
			if (outboundId == 10000)
			{
				outboundId = 0;
			}
			return outboundId;
		}

		public void DecrementOutboundId()
		{
			outboundId--;
		}
	}
}
