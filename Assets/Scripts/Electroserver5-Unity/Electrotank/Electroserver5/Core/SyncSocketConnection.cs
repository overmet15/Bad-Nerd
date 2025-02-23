using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Electrotank.Electroserver5.Api;
using Electrotank.Electroserver5.Ionic.Zlib;
using log4net;

namespace Electrotank.Electroserver5.Core
{
	internal class SyncSocketConnection : Connection, IDisposable
	{
		private const int ID_LEN = 4;

		private const int MAX_READ = 1024;

		private readonly MemoryStream readBuffer;

		private readonly BinaryReader reader;

		private bool awaitingCrossdomain = true;

		private byte[] byteBuffer = new byte[1024];

		private bool disposed;

		private ILog log = LogManager.GetLogger(typeof(SyncSocketConnection));

		private Thread readThread;

		public override bool Connected { get; protected set; }

		public override Protocol Protocol
		{
			get
			{
				return Protocol.BinaryTCP;
			}
		}

		public Socket Socket { get; protected set; }

		private EsEngine Engine { get; set; }

		private Server Server { get; set; }

		public SyncSocketConnection(AvailableConnection availableConnection, EsEngine engine, Server server)
			: base(availableConnection)
		{
			Engine = engine;
			Server = server;
			base.SessionKey = -1;
			readBuffer = new MemoryStream();
			reader = new BinaryReader(readBuffer);
			base.PrimaryCapable = true;
		}

		~SyncSocketConnection()
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
					Server.Enqueue(connectionClosedEvent, true, this);
				}
			}
			if (Socket != null && Socket.Connected && Connected)
			{
				Connected = false;
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
			try
			{
				Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				Socket.Connect(new IPEndPoint(Dns.GetHostAddresses(base.AvailableConnection.Host)[0], base.AvailableConnection.Port));
				readThread = new Thread(OnRead);
				Connected = true;
				readThread.Start();
			}
			catch (Exception ex)
			{
				log.Debug("Connection failed: " + ex.Message);
				throw ex;
			}
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		public void Receive(byte[] bytes, int offset, int length)
		{
			readBuffer.Seek(0L, SeekOrigin.End);
			readBuffer.Write(bytes, offset, length);
			readBuffer.Seek(0L, SeekOrigin.Begin);
			if (awaitingCrossdomain && RemainingBytes() > 0)
			{
				ProcessCrossDomain();
			}
			else
			{
				ProcessEsMessages();
			}
		}

		public override void Send(EsMessage message)
		{
			if (base.EncryptionContext != null)
			{
				base.EncryptionContext.HandleOutgoingMessage(message, Engine, this);
			}
			if (message is DHInitiateKeyExchangeRequest || message is DHSharedModulusRequest || message is EncryptionStateChangeEvent)
			{
				message.MessageNumber = -1;
			}
			else
			{
				message.MessageNumber = GetNextOutboundId();
			}
			message.RequestId = Server.ConnectionFor(Protocol).SessionKey;
			byte[] array = MessageTranslator.ToBytes(message);
			byte b = 0;
			if (Engine.MessageSizeCompressionThreshold != -1 && (Engine.MessageSizeCompressionThreshold == 0 || array.Length >= Engine.MessageSizeCompressionThreshold))
			{
				b = (byte)(b | 1u);
				log.DebugFormat("Compressing message from {0}", array.Length);
				ZlibStream zlibStream = new ZlibStream(new MemoryStream(array), CompressionMode.Compress);
				MemoryStream memoryStream = new MemoryStream();
				byte[] buffer = new byte[512];
				try
				{
					int count;
					while ((count = zlibStream.Read(buffer, 0, 512)) != 0)
					{
						memoryStream.Write(buffer, 0, count);
					}
				}
				catch (Exception ex)
				{
					log.Debug(ex.ToString());
				}
				array = memoryStream.GetBuffer();
				log.DebugFormat("  to {0}", array.Length);
			}
			if (base.EncryptionContext != null)
			{
				base.EncryptionContext.HandleOutgoingMessage(message, Engine, this);
				base.EncryptionContext.EncryptOutgoingMessage(array);
			}
			ByteArray byteArray = new ByteArray(new byte[array.Length + 5]);
			byteArray.WriteInteger(array.Length + 1);
			byteArray.WriteByte(b);
			byteArray.Write(array);
			if (log.IsDebugEnabled)
			{
				log.DebugFormat("SEND TCP({0}) MessageType[{1}] {2}", message.MessageNumber, message.MessageType, message.ToThrift());
			}
			if (Socket.Connected)
			{
				Socket.Send(byteArray.RawBytes);
			}
			else
			{
				Console.WriteLine("WARN: Attempt to write to socket while not connected");
			}
		}

		private void Dispose(bool disposing)
		{
			if (!disposed)
			{
				Close();
				disposed = true;
			}
		}

		private void OnRead()
		{
			try
			{
				while (Socket.Connected && Connected)
				{
					Thread.Sleep(0);
					int num = -1;
					num = Socket.Receive(byteBuffer, 0, byteBuffer.Length, SocketFlags.None);
					if (num < 1)
					{
						if (log.IsDebugEnabled)
						{
							log.Debug("Unable to read socket, aborting read thread");
						}
						readThread.Abort();
						break;
					}
					Receive(byteBuffer, 0, num);
				}
			}
			catch (Exception ex)
			{
				if (log.IsDebugEnabled)
				{
					log.Debug("Failure reading socket, disconnecting.. Error: " + ex);
				}
				Close();
			}
		}

		private void ProcessCrossDomain()
		{
			byte[] array = reader.ReadBytes((int)RemainingBytes());
			string @string = Encoding.UTF8.GetString(array);
			if (!@string.StartsWith("<"))
			{
				return;
			}
			int num = -1;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] == 0)
				{
					num = i;
					awaitingCrossdomain = false;
					break;
				}
			}
			if (num > 0)
			{
				int num2 = num + 1;
				int len = array.Length - num2;
				saveLeftovers(array, num2, len);
				ProcessEsMessages();
			}
		}

		private void ProcessEsMessages()
		{
			while (RemainingBytes() > 4)
			{
				int num = IPAddress.NetworkToHostOrder(reader.ReadInt32());
				if (RemainingBytes() >= num)
				{
					byte b = reader.ReadByte();
					byte[] array = reader.ReadBytes(num - 1);
					if (base.EncryptionContext != null)
					{
						base.EncryptionContext.DecryptIncomingMessage(array);
					}
					if (((uint)b & (true ? 1u : 0u)) != 0)
					{
						log.DebugFormat("Decompressing message from {0}", array.Length);
						ZlibStream zlibStream = new ZlibStream(new MemoryStream(array), CompressionMode.Decompress);
						MemoryStream memoryStream = new MemoryStream();
						byte[] buffer = new byte[512];
						try
						{
							int count;
							while ((count = zlibStream.Read(buffer, 0, 512)) != 0)
							{
								memoryStream.Write(buffer, 0, count);
							}
						}
						catch (Exception ex)
						{
							log.Debug(ex.ToString());
						}
						array = memoryStream.GetBuffer();
						log.DebugFormat("  to {0}", array.Length);
					}
					EsMessage esMessage = MessageTranslator.ToMessage(array);
					if (base.EncryptionContext != null)
					{
						base.EncryptionContext.HandleIncomingMessage(esMessage, Engine, this);
					}
					if (base.SessionKey == -1 && esMessage is ConnectionResponse)
					{
						ConnectionResponse connectionResponse = esMessage as ConnectionResponse;
						if (connectionResponse.Successful)
						{
							base.SessionKey = connectionResponse.HashId;
						}
					}
					if (log.IsDebugEnabled)
					{
						log.DebugFormat("RECV TCP({0}) [{1}] {2}", esMessage.MessageNumber, esMessage.MessageType, esMessage.ToThrift());
					}
					if (esMessage.MessageNumber != -1)
					{
						Server.Enqueue(esMessage, false, this);
					}
					else
					{
						Server.Enqueue(esMessage, true, this);
					}
					saveLeftovers();
					continue;
				}
				readBuffer.Position -= 4;
				break;
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
	}
}
