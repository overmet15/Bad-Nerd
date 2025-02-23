using System;
using System.Net;
using System.Net.Sockets;

namespace Thrift.Transport
{
	public class TServerSocket : TServerTransport
	{
		private TcpListener server;

		private int port;

		private int clientTimeout;

		private bool useBufferedSockets;

		public TServerSocket(TcpListener listener)
			: this(listener, 0)
		{
		}

		public TServerSocket(TcpListener listener, int clientTimeout)
		{
			server = listener;
			this.clientTimeout = clientTimeout;
		}

		public TServerSocket(int port)
			: this(port, 0)
		{
		}

		public TServerSocket(int port, int clientTimeout)
			: this(port, clientTimeout, false)
		{
		}

		public TServerSocket(int port, int clientTimeout, bool useBufferedSockets)
		{
			this.port = port;
			this.clientTimeout = clientTimeout;
			this.useBufferedSockets = useBufferedSockets;
			try
			{
				server = new TcpListener(IPAddress.Any, this.port);
			}
			catch (Exception)
			{
				server = null;
				throw new TTransportException("Could not create ServerSocket on port " + port + ".");
			}
		}

		public override void Listen()
		{
			if (server != null)
			{
				try
				{
					server.Start();
				}
				catch (SocketException ex)
				{
					throw new TTransportException("Could not accept on listening socket: " + ex.Message);
				}
			}
		}

		protected override TTransport AcceptImpl()
		{
			if (server == null)
			{
				throw new TTransportException(TTransportException.ExceptionType.NotOpen, "No underlying server socket.");
			}
			try
			{
				TcpClient client = server.AcceptTcpClient();
				TSocket tSocket = new TSocket(client);
				tSocket.Timeout = clientTimeout;
				if (useBufferedSockets)
				{
					return new TBufferedTransport(tSocket);
				}
				return tSocket;
			}
			catch (Exception ex)
			{
				throw new TTransportException(ex.ToString());
			}
		}

		public override void Close()
		{
			if (server != null)
			{
				try
				{
					server.Stop();
				}
				catch (Exception ex)
				{
					throw new TTransportException("WARNING: Could not close server socket: " + ex);
				}
				server = null;
			}
		}
	}
}
