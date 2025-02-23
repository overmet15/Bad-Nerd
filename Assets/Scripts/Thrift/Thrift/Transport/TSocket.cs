using System.Net.Sockets;

namespace Thrift.Transport
{
	public class TSocket : TStreamTransport
	{
		private TcpClient client;

		private string host;

		private int port;

		private int timeout;

		public int Timeout
		{
			set
			{
				TcpClient tcpClient = client;
				int num = (timeout = value);
				client.SendTimeout = num;
				tcpClient.ReceiveTimeout = num;
			}
		}

		public TcpClient TcpClient
		{
			get
			{
				return client;
			}
		}

		public string Host
		{
			get
			{
				return host;
			}
		}

		public int Port
		{
			get
			{
				return port;
			}
		}

		public override bool IsOpen
		{
			get
			{
				if (client == null)
				{
					return false;
				}
				return client.Connected;
			}
		}

		public TSocket(TcpClient client)
		{
			this.client = client;
			if (IsOpen)
			{
				inputStream = client.GetStream();
				outputStream = client.GetStream();
			}
		}

		public TSocket(string host, int port)
			: this(host, port, 0)
		{
		}

		public TSocket(string host, int port, int timeout)
		{
			this.host = host;
			this.port = port;
			this.timeout = timeout;
			InitSocket();
		}

		private void InitSocket()
		{
			client = new TcpClient();
			TcpClient tcpClient = client;
			int num = timeout;
			client.SendTimeout = num;
			tcpClient.ReceiveTimeout = num;
		}

		public override void Open()
		{
			if (IsOpen)
			{
				throw new TTransportException(TTransportException.ExceptionType.AlreadyOpen, "Socket already connected");
			}
			if (string.IsNullOrEmpty(host))
			{
				throw new TTransportException(TTransportException.ExceptionType.NotOpen, "Cannot open null host");
			}
			if (port <= 0)
			{
				throw new TTransportException(TTransportException.ExceptionType.NotOpen, "Cannot open without port");
			}
			if (client == null)
			{
				InitSocket();
			}
			client.Connect(host, port);
			inputStream = client.GetStream();
			outputStream = client.GetStream();
		}

		public override void Close()
		{
			base.Close();
			if (client != null)
			{
				client.Close();
				client = null;
			}
		}
	}
}
