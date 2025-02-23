using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Electrotank.Electroserver5.Api;
using log4net;

namespace Electrotank.Electroserver5.Core
{
	internal class SyncSocketHttpConnection : Connection, IDisposable
	{
		private const int ID_LEN = 4;

		private const int MAX_READ = 8192;

		private const string SEND_URI = "/s/";

		private const string SESSION_KEY_URI = "/connect/binary";

		private readonly MemoryStream readBuffer;

		private readonly BinaryReader reader;

		private readonly PreservingMemoryStream responseBuffer;

		private readonly StreamReader responseReader;

		private readonly Queue sendQueue = new Queue();

		private byte[] byteBuffer = new byte[8192];

		private bool disposed;

		private ILog log = LogManager.GetLogger(typeof(SyncSocketHttpConnection));

		private int messageNumberSequence = -1;

		private Thread readThread;

		public override bool Connected { get; protected set; }

		public override Protocol Protocol
		{
			get
			{
				return Protocol.BinaryHTTP;
			}
		}

		public Socket Socket { get; protected set; }

		private EsEngine Engine { get; set; }

		private Server Server { get; set; }

		private new string SessionKey { get; set; }

		public SyncSocketHttpConnection(AvailableConnection availableConnection, EsEngine engine, Server server)
			: base(availableConnection)
		{
			base.AvailableConnection = availableConnection;
			Engine = engine;
			Server = server;
			readBuffer = new MemoryStream();
			reader = new BinaryReader(readBuffer);
			responseBuffer = new PreservingMemoryStream();
			responseReader = new StreamReader(responseBuffer);
			Engine.AddEventListener(MessageType.ConnectionResponse, OnConnectResponse);
		}

		~SyncSocketHttpConnection()
		{
			Dispose(false);
		}

		public override void Close()
		{
			if (Connected)
			{
				lock (this)
				{
					ConnectionClosedEvent connectionClosedEvent = new ConnectionClosedEvent();
					connectionClosedEvent.ConnectionId = base.ConnectionId;
					Connected = false;
					Server.Enqueue(connectionClosedEvent, true, this);
				}
			}
			if (Socket != null && Socket.Connected)
			{
				try
				{
					Socket.Shutdown(SocketShutdown.Both);
					Socket.Close();
				}
				catch (Exception ex)
				{
					Console.WriteLine("Exception closing socket " + ex.Message);
				}
			}
			Server.UpdateConnectionStatus();
		}

		public override void Connect()
		{
			StartReadThread();
			Connected = true;
			_Connect();
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		public void Receive(byte[] bytes)
		{
			readBuffer.Seek(0L, SeekOrigin.End);
			readBuffer.Write(bytes, 0, bytes.Length);
			readBuffer.Seek(0L, SeekOrigin.Begin);
			ProcessEsMessages();
		}

		public override void Send(EsMessage message)
		{
			message.MessageNumber = Interlocked.Increment(ref messageNumberSequence);
			sendQueue.Enqueue(message);
		}

		private int ChompResponseHeaders()
		{
			while (responseReader.Peek() >= 0)
			{
				string text = responseReader.ReadLine();
				Console.WriteLine("RESHEADER: " + text);
				if (string.Empty.Equals(text))
				{
					Console.WriteLine("found the end of headers, brokeified");
					return (int)responseBuffer.Position;
				}
			}
			return -1;
		}

		private void Dispose(bool disposing)
		{
			if (!disposed)
			{
				Close();
				disposed = true;
			}
		}

		private void OnConnectResponse(EsMessage message)
		{
			ConnectionResponse connectionResponse = message as ConnectionResponse;
			if (connectionResponse.Successful)
			{
				SessionKey = connectionResponse.HashId.ToString();
			}
			else
			{
				Console.WriteLine("HTTPConnection failed: " + connectionResponse.Error);
			}
		}

		private void OnRead2()
		{
			try
			{
				while (Socket.Connected)
				{
					Thread.Sleep(0);
					_Send();
					int num = -1;
					num = Socket.Receive(byteBuffer, 0, byteBuffer.Length, SocketFlags.None);
					if (num < 1)
					{
						if (log.IsDebugEnabled)
						{
							log.Debug("Unable to read socket, aborting read thread");
						}
						break;
					}
					ReceiveHttpResponse(byteBuffer, 0, num);
				}
			}
			catch (Exception ex)
			{
				if (log.IsDebugEnabled)
				{
					log.Debug("Failure reading socket, disconnecting.. Error: " + ex);
				}
			}
		}

		private void OnUserWorkQueue(object state)
		{
			OnRead2();
			StartReadThread();
		}

		private void ProcessEsMessages()
		{
			while (RemainingBytes() > 4)
			{
				int num = IPAddress.NetworkToHostOrder(reader.ReadInt32());
				if (RemainingBytes() >= num)
				{
					EsMessage esMessage = MessageTranslator.ToMessage(reader.ReadBytes(num));
					if (log.IsDebugEnabled)
					{
						log.DebugFormat("RECV({0}) [{1}] {2}", esMessage.MessageNumber, esMessage.MessageType, esMessage.ToThrift());
					}
					Server.Enqueue(esMessage, false, this);
					saveLeftovers();
					continue;
				}
				readBuffer.Position -= 4;
				break;
			}
		}

		private void ReceiveHttpResponse(byte[] bytes, int offset, int length)
		{
			responseBuffer.Seek(0L, SeekOrigin.End);
			responseBuffer.Write(bytes, 0, length);
			responseBuffer.Seek(0L, SeekOrigin.Begin);
			if (ChompResponseHeaders() != -1)
			{
				Receive(Encoding.UTF8.GetBytes(responseReader.ReadToEnd()));
			}
			else
			{
				Console.WriteLine("CHGIMP HEADER -1");
			}
		}

		private long RemainingBytes()
		{
			return readBuffer.Length - readBuffer.Position;
		}

		private void saveLeftovers()
		{
			byte[] array = reader.ReadBytes((int)RemainingBytes());
			saveLeftovers(array, 0, array.Length);
		}

		private void saveLeftovers(byte[] leftover, int offset, int len)
		{
			readBuffer.SetLength(0L);
			readBuffer.Seek(0L, SeekOrigin.Begin);
			readBuffer.Write(leftover, offset, len);
			readBuffer.Seek(0L, SeekOrigin.Begin);
		}

		private void StartReadThread()
		{
			Console.WriteLine("STARRT READ THREAD");
			Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			Socket.Connect(new IPEndPoint(Dns.GetHostAddresses(base.AvailableConnection.Host)[0], base.AvailableConnection.Port));
			ThreadPool.QueueUserWorkItem(OnUserWorkQueue, null);
		}

		private void _Connect()
		{
			string text = "POST /connect/binary HTTP/1.1\r\n";
			List<Header> list = new List<Header>();
			list.Add(new Header("Content-Type", "application/x-www-form-urlencoded"));
			list.Add(new Header("Content-Length", "1"));
			list.Add(new Header("Connection", "keep-alive"));
			foreach (Header item in list)
			{
				text += item;
			}
			text += "\r\n";
			byte[] bytes = Encoding.UTF8.GetBytes(text);
			Socket.Send(bytes);
			Socket.Send(new byte[1]);
		}

		private bool _Send()
		{
			if (sendQueue.Count > 0)
			{
				EsMessage esMessage = sendQueue.Dequeue() as EsMessage;
				byte[] array = MessageTranslator.ToBytes(esMessage);
				ByteArray byteArray = new ByteArray(new byte[array.Length + 4]);
				byteArray.WriteByteArray(array);
				if (log.IsDebugEnabled)
				{
					Console.WriteLine("SEND({0}) MessageType[{1}] {2}", esMessage.MessageNumber, esMessage.MessageType, esMessage.ToThrift());
				}
				if (Socket.Connected)
				{
					string text = "POST /s/" + SessionKey + " HTTP/1.1\r\n";
					List<Header> list = new List<Header>();
					list.Add(new Header("Content-Type", "application/x-www-form-urlencoded"));
					list.Add(new Header("Content-Length", byteArray.RawBytes.Length.ToString()));
					list.Add(new Header("Expect", "100-continue"));
					list.Add(new Header("Host", "localhost:8989"));
					foreach (Header item in list)
					{
						text += item;
					}
					text += "\r\n";
					Console.WriteLine(text);
					byte[] bytes = Encoding.UTF8.GetBytes(text);
					MemoryStream memoryStream = new MemoryStream();
					memoryStream.Write(bytes, 0, bytes.Length);
					memoryStream.Write(byteArray.RawBytes, 0, byteArray.RawBytes.Length);
					memoryStream.Seek(0L, SeekOrigin.Begin);
					byte[] array2 = new byte[memoryStream.Length];
					memoryStream.Read(array2, 0, array2.Length);
					Console.WriteLine(array2);
					Socket.Send(array2);
					return true;
				}
				Console.WriteLine("WARN: Attempt to write to socket while not connected");
			}
			return false;
		}
	}
}
