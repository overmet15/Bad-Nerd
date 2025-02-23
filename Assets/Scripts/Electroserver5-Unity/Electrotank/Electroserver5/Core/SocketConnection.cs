using System;
using System.Net;
using System.Net.Sockets;
using Electrotank.Electroserver5.Api;

namespace Electrotank.Electroserver5.Core
{
	internal class SocketConnection : Connection, IDisposable
	{
		public override bool Connected { get; protected set; }

		public EsEngine Engine { get; protected set; }

		public override Protocol Protocol
		{
			get
			{
				return Protocol.BinaryTCP;
			}
		}

		public Socket Socket { get; protected set; }

		public SocketConnection(AvailableConnection availableConnection, EsEngine engine)
			: base(availableConnection)
		{
			Engine = engine;
			Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		}

		public override void Close()
		{
			Connected = false;
			try
			{
				Socket.Shutdown(SocketShutdown.Both);
				Socket.Close();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				throw ex;
			}
			Socket = null;
		}

		public override void Connect()
		{
			IPEndPoint remoteEP = new IPEndPoint(Dns.GetHostAddresses(base.AvailableConnection.Host)[0], base.AvailableConnection.Port);
			try
			{
				Socket.Connect(remoteEP);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				throw ex;
			}
			Connected = true;
			new CrossDomainReader(this).Read();
		}

		public void Dispose()
		{
			Close();
		}

		public override void Send(EsMessage message)
		{
			message.MessageNumber = GetNextOutboundId();
			byte[] array = MessageTranslator.ToBytes(message);
			ByteArray byteArray = new ByteArray(new byte[array.Length + 4]);
			byteArray.WriteByteArray(array);
			Console.WriteLine("SEND({0}) MessageType[{1}] {2}", message.MessageNumber, message.MessageType, message.ToThrift());
			Socket.Send(byteArray.RawBytes);
		}
	}
}
