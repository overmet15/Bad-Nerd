using System;
using System.Collections;
using System.IO;
using System.Net;
using Electrotank.Electroserver5.Api;
using log4net;

namespace Electrotank.Electroserver5.Core
{
	internal class HttpConnection : Connection
	{
		private const string SEND_URI = "/s/";

		private const string SESSION_KEY_URI = "/connect/binary";

		private ILog log = LogManager.GetLogger(typeof(HttpConnection));

		private UnityWebClient listener;

		private Queue messageQueue = new Queue();

		public override bool Connected { get; protected set; }

		public override Protocol Protocol
		{
			get
			{
				return Protocol.BinaryHTTP;
			}
		}

		public new string SessionKey { get; set; }

		private EsEngine Engine { get; set; }

		private Server Server { get; set; }

		public HttpConnection(AvailableConnection availableConnection, EsEngine engine, Server server)
			: base(availableConnection)
		{
			log.Debug("OOGA BOOGA!");
			Engine = engine;
			Server = server;
			Connected = false;
			listener = new UnityWebClient();
			listener.UploadDataCompleted += ListenerUploadDataCompleted;
			base.PrimaryCapable = true;
		}

		public override void Close()
		{
			listener.UploadDataCompleted -= ListenerUploadDataCompleted;
			listener.CancelAsync();
		}

		public override void Connect()
		{
			PostAsync("/connect/binary", new byte[1]);
		}

		public override void Send(EsMessage message)
		{
			message.MessageNumber = GetNextOutboundId();
			messageQueue.Enqueue(message);
			log.Debug("SendMessageQueue: " + messageQueue.Count);
			listener.CancelAsync();
		}

		private void ListenerUploadDataCompleted(object o, UnityUploadDataCompletedEventArgs e)
		{
			if (e.Result != null)
			{
				Rec(e.Result);
			}
			if (SessionKey == null)
			{
				log.Debug("No SessionKey found after first connection.");
				ConnectionAttemptResponse connectionAttemptResponse = new ConnectionAttemptResponse();
				connectionAttemptResponse.ConnectionId = base.ConnectionId;
				connectionAttemptResponse.Successful = false;
				connectionAttemptResponse.Error = ErrorType.ConnectionFailed;
				PreProcessMessage(connectionAttemptResponse);
			}
			if (messageQueue.Count > 0)
			{
				EsMessage esMessage = messageQueue.Dequeue() as EsMessage;
				byte[] array = MessageTranslator.ToBytes(esMessage);
				ByteArray byteArray = new ByteArray(new byte[array.Length + 4]);
				byteArray.WriteByteArray(array);
				log.DebugFormat("SEND({0}) MessageType[{1}] {2}", esMessage.MessageNumber, esMessage.MessageType, esMessage.ToThrift());
				PostAsync("/s/" + SessionKey, byteArray.RawBytes);
			}
			else
			{
				PostAsync("/s/" + SessionKey, new byte[0]);
			}
		}

		private void PostAsync(string uri, byte[] sendData)
		{
			string text = "http://" + base.AvailableConnection.Host + ":" + base.AvailableConnection.Port + uri;
			log.DebugFormat("fullUri: {0}", text);
			listener.UploadDataAsync(new Uri(text), sendData);
		}

		private void Rec(byte[] data)
		{
			MemoryStream memoryStream = new MemoryStream(data);
			BinaryReader binaryReader = new BinaryReader(memoryStream);
			memoryStream.Seek(0L, SeekOrigin.Begin);
			while (memoryStream.Position < data.Length)
			{
				int count = IPAddress.NetworkToHostOrder(binaryReader.ReadInt32());
				EsMessage message = MessageTranslator.ToMessage(binaryReader.ReadBytes(count));
				PreProcessMessage(message);
			}
		}

		private void PreProcessMessage(EsMessage message)
		{
			log.DebugFormat("RECV({0}) [{1}] {2}", message.MessageNumber, message.MessageType, message.ToThrift());
			if (message is ConnectionResponse)
			{
				ConnectionResponse connectionResponse = message as ConnectionResponse;
				if (connectionResponse.Successful)
				{
					SessionKey = connectionResponse.HashId.ToString();
					Connected = true;
				}
			}
			Server.Enqueue(message, false, this);
		}
	}
}
