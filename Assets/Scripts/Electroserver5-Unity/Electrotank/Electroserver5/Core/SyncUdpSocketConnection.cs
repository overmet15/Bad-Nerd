using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Electrotank.Electroserver5.Api;
using Electrotank.Electroserver5.Ionic.Zlib;
using log4net;

namespace Electrotank.Electroserver5.Core
{
	internal class SyncUdpSocketConnection : Connection, IDisposable
	{
		private const int ID_LEN = 4;

		private const int MAX_READ = 1024;

		private readonly MemoryStream readBuffer;

		private readonly BinaryReader reader;

		private byte[] byteBuffer = new byte[1024];

		private bool disposed;

		private EndPoint localEndPoint;

		private ILog log = LogManager.GetLogger(typeof(SyncUdpSocketConnection));

		private Thread readThread;

		private EndPoint remoteEndPoint;

		public override bool Connected { get; protected set; }

		public override Protocol Protocol
		{
			get
			{
				return Protocol.BinaryUDP;
			}
		}

		public Socket Socket { get; protected set; }

		private EsEngine Engine { get; set; }

		private Server Server { get; set; }

		public SyncUdpSocketConnection(AvailableConnection availableConnection, EsEngine engine, Server server)
			: base(availableConnection)
		{
			Engine = engine;
			Server = server;
			readBuffer = new MemoryStream();
			reader = new BinaryReader(readBuffer);
			base.PrimaryCapable = false;
		}

		~SyncUdpSocketConnection()
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
					Socket.Close();
				}
				catch (Exception ex)
				{
					Console.WriteLine("Exception closing socket " + ex);
				}
			}
			Server.UpdateConnectionStatus();
		}

		public override void Connect()
		{
			try
			{
				localEndPoint = new IPEndPoint(IPAddress.Any, base.AvailableConnection.LocalPort);
				Socket = new Socket(localEndPoint.AddressFamily, SocketType.Dgram, ProtocolType.Udp);
				Socket.Bind(localEndPoint);
				IPEndPoint iPEndPoint = Socket.LocalEndPoint as IPEndPoint;
				remoteEndPoint = new IPEndPoint(Dns.GetHostAddresses(base.AvailableConnection.Host)[0], base.AvailableConnection.Port);
				Connected = true;
				readThread = new Thread(OnRead);
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
			ProcessEsMessages();
		}

		public override void Send(EsMessage message)
		{
			message.GetType().GetProperty("SessionKey").SetValue(message, base.SessionKey, null);
			message.MessageNumber = -1;
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
			ByteArray byteArray = new ByteArray(new byte[array.Length + 5]);
			byteArray.WriteInteger(array.Length + 1);
			byteArray.WriteByte(b);
			byteArray.Write(array);
			if (log.IsDebugEnabled)
			{
				log.DebugFormat("SEND UDP({0}) MessageType[{1}] {2} {3}", message.MessageNumber, message.MessageType, base.SessionKey, message.ToThrift());
			}
			Socket.SendTo(byteArray.RawBytes, remoteEndPoint);
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
			while (true)
			{
				Thread.Sleep(0);
				int num = -1;
				try
				{
					num = Socket.ReceiveFrom(byteBuffer, ref remoteEndPoint);
					Receive(byteBuffer, 0, num);
				}
				catch (ObjectDisposedException)
				{
					break;
				}
				catch (Exception ex2)
				{
					log.Debug(ex2.Message.ToString());
				}
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
					byte[] array = reader.ReadBytes(num);
					if (((uint)b & (true ? 1u : 0u)) != 0)
					{
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
					}
					EsMessage esMessage = MessageTranslator.ToMessage(array);
					if (log.IsDebugEnabled)
					{
						log.DebugFormat("RECV UDP({0}) [{1}] {2}", esMessage.MessageNumber, esMessage.MessageType, esMessage.ToThrift());
					}
					Server.Enqueue(esMessage, true, this);
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
