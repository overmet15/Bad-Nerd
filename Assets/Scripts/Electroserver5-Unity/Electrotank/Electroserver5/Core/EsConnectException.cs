using System;

namespace Electrotank.Electroserver5.Core
{
	public class EsConnectException : Exception
	{
		public const string MULTI_SERVER_UNSUPPORTED = "Multiple servers are not currently supported";

		public const string NO_PRIMARY_CONNECTIONS = "No connections with primary transport types defined";

		public const string NO_SERVERS_DEFINED = "No servers are defined";

		public EsConnectException()
		{
		}

		public EsConnectException(string message)
			: base(message)
		{
		}
	}
}
