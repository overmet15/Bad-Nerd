using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using Electrotank.Electroserver5.Api;

namespace Electrotank.Electroserver5.Core
{
	internal class AsyncReader
	{
		private const int MAX_READ = 512;

		private readonly SocketConnection connection;

		private readonly MemoryStream readBuffer;

		private readonly BinaryReader reader;

		private static int ID_LEN = 4;

		private byte[] byteBuffer = new byte[512];

		public AsyncReader(SocketConnection connection)
		{
			this.connection = connection;
			readBuffer = new MemoryStream();
			reader = new BinaryReader(readBuffer);
		}

		public void OnRead(IAsyncResult asr)
		{
			if (!connection.Socket.Connected || !connection.Connected)
			{
				return;
			}
			try
			{
				int num = connection.Socket.EndReceive(asr);
				if (num < 1)
				{
					connection.Socket.Close();
					return;
				}
				Receive(byteBuffer, 0, num);
				if (connection.Socket.Connected && connection.Connected)
				{
					connection.Socket.BeginReceive(byteBuffer, 0, 512, SocketFlags.None, OnRead, null);
				}
			}
			catch (Exception value)
			{
				Console.WriteLine(value);
			}
		}

		public void Receive(byte[] bytes, int offset, int length)
		{
			readBuffer.Seek(0L, SeekOrigin.End);
			readBuffer.Write(bytes, offset, length);
			readBuffer.Seek(0L, SeekOrigin.Begin);
			if (RemainingBytes() > ID_LEN)
			{
				int num = IPAddress.NetworkToHostOrder(reader.ReadInt32());
				if (RemainingBytes() >= num)
				{
					EsMessage esMessage = MessageTranslator.ToMessage(reader.ReadBytes(num));
					Console.WriteLine("RECV({0}) [{1}] {2}", esMessage.MessageNumber, esMessage.MessageType, esMessage.ToThrift());
					throw new Exception("Restore queueing");
				}
				readBuffer.Position -= ID_LEN;
			}
			byte[] array = reader.ReadBytes((int)RemainingBytes());
			readBuffer.SetLength(0L);
			readBuffer.Write(array, 0, array.Length);
		}

		public void Start()
		{
			connection.Socket.BeginReceive(byteBuffer, 0, 512, SocketFlags.None, OnRead, null);
		}

		public void Start(byte[] buf, int offset, int len)
		{
			Receive(buf, offset, len);
			Start();
		}

		private long RemainingBytes()
		{
			return readBuffer.Length - readBuffer.Position;
		}
	}
}
