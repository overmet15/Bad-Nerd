namespace Electrotank.Electroserver5.Core
{
	public class AvailableConnection
	{
		public enum TransportType
		{
			BinaryTCP = 0,
			BinaryHTTP = 1,
			BinaryUDP = 2
		}

		public string Host { get; protected set; }

		public string LocalIP { get; protected set; }

		public int LocalPort { get; protected set; }

		public int Port { get; protected set; }

		public TransportType Transport { get; protected set; }

		internal string ServerId { get; set; }

		public AvailableConnection(string host, int port, TransportType transport)
		{
			Host = host;
			Port = port;
			Transport = transport;
			LocalIP = "0.0.0.0";
			LocalPort = 0;
		}

		public AvailableConnection(string host, int port, TransportType transport, string localIp, int localPort)
		{
			Host = host;
			Port = port;
			Transport = transport;
			LocalIP = localIp;
			LocalPort = localPort;
		}
	}
}
