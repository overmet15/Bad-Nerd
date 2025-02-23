using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Threading;
using Electrotank.Electroserver5.Api;
using log4net;

namespace Electrotank.Electroserver5.Core
{
	internal class NetHttpConnection : Connection
	{
		private const string SEND_URI = "/s/";

		private const string SESSION_KEY_URI = "/connect/binary";

		private readonly PreservingMemoryStream readBuffer;

		private readonly BinaryReader reader;

		private Thread listenThread;

		private ILog log = LogManager.GetLogger(typeof(NetHttpConnection));

		private Queue requestQueue = new Queue();

		public override bool Connected { get; protected set; }

		public override Protocol Protocol
		{
			get
			{
				return Protocol.BinaryTCP;
			}
		}

		public new string SessionKey { get; set; }

		private new AvailableConnection AvailableConnection { get; set; }

		private EsEngine Engine { get; set; }

		private Server Server { get; set; }

		public NetHttpConnection(AvailableConnection availableConnection, EsEngine engine, Server server)
			: base(availableConnection)
		{
			Engine = engine;
			Server = server;
			Connected = false;
			SessionKey = null;
			readBuffer = new PreservingMemoryStream();
			reader = new BinaryReader(readBuffer);
			ThreadPool.SetMaxThreads(1, 2);
		}

		public override void Close()
		{
		}

		public override void Connect()
		{
			Rec(_Connect());
		}

		public override void Send(EsMessage request)
		{
			Console.WriteLine("################### BEGIN SEND");
			if (SessionKey == null)
			{
				throw new InvalidOperationException("Cannot send a message until the connection process is complete: Invalid Session Key");
			}
			lock (this)
			{
				request.MessageNumber = GetNextOutboundId();
				requestQueue.Enqueue(request);
			}
			Console.WriteLine("##############################   Send() : " + request.MessageType);
			StartWork();
		}

		private static void OnContinue(int something, WebHeaderCollection col)
		{
			Console.WriteLine("ON CONTINUE@!@#!@#!@#@!!!!!!!!!!!!!!!!!!!!!!!!!");
		}

		private void ProcessListener(object state, bool cancelled)
		{
			AutoResetEvent autoResetEvent = state as AutoResetEvent;
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://" + AvailableConnection.Host + ":" + AvailableConnection.Port + "/s/" + SessionKey);
			httpWebRequest.Method = "POST";
			httpWebRequest.ContentType = "application/x-www-form-urlencoded";
			httpWebRequest.ReadWriteTimeout = 1000;
			httpWebRequest.Timeout = 1000;
			Stream requestStream = httpWebRequest.GetRequestStream();
			EsMessage esMessage = requestQueue.Dequeue() as EsMessage;
			Console.WriteLine("ProcessListener MEssage: " + esMessage);
			if (esMessage != null)
			{
				byte[] array = MessageTranslator.ToBytes(esMessage);
				ByteArray byteArray = new ByteArray(new byte[array.Length + 4]);
				byteArray.WriteByteArray(array);
				Console.WriteLine("SEND({0}) MessageType[{1}] {2}", esMessage.MessageNumber, esMessage.MessageType, esMessage.ToThrift());
				requestStream.Write(byteArray.RawBytes, 0, byteArray.RawBytes.Length);
			}
			else
			{
				Console.WriteLine("#############  Polling listener");
				requestStream.Write(new byte[1], 0, 1);
			}
			requestStream.Close();
			using (HttpWebResponse httpWebResponse = httpWebRequest.GetResponse() as HttpWebResponse)
			{
				MemoryStream memoryStream = new MemoryStream();
				BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
				BinaryReader binaryReader = new BinaryReader(memoryStream);
				int num = 0;
				Stream responseStream = httpWebResponse.GetResponseStream();
				while ((num = responseStream.ReadByte()) != -1)
				{
					binaryWriter.Write((byte)num);
				}
				responseStream.Close();
				memoryStream.Seek(0L, SeekOrigin.Begin);
				Rec(binaryReader.ReadBytes((int)memoryStream.Length));
				Console.WriteLine("ProcessListener Completed..");
			}
			autoResetEvent.Set();
			StartWork();
		}

		private void Rec(byte[] data)
		{
			readBuffer.Seek(0L, SeekOrigin.End);
			readBuffer.Write(data, 0, data.Length);
			readBuffer.Seek(0L, SeekOrigin.Begin);
			while (readBuffer.RemainingBytes() > 4)
			{
				int num = IPAddress.NetworkToHostOrder(reader.ReadInt32());
				if (readBuffer.RemainingBytes() >= num)
				{
					EsMessage esMessage = MessageTranslator.ToMessage(reader.ReadBytes(num));
					if (log.IsDebugEnabled)
					{
						log.DebugFormat("RECV({0}) [{1}] {2}", esMessage.MessageNumber, esMessage.MessageType, esMessage.ToThrift());
					}
					if (esMessage is ConnectionResponse)
					{
						ConnectionResponse connectionResponse = esMessage as ConnectionResponse;
						if (connectionResponse.Successful)
						{
							SessionKey = connectionResponse.HashId.ToString();
							Connected = true;
						}
					}
					Server.Enqueue(esMessage, false, this);
					continue;
				}
				readBuffer.Position -= 4;
				break;
			}
			readBuffer.Preserve();
		}

		private void StartWork()
		{
			Console.WriteLine("StartWork Called");
			AutoResetEvent autoResetEvent = new AutoResetEvent(false);
			ThreadPool.RegisterWaitForSingleObject(autoResetEvent, ProcessListener, autoResetEvent, 2000, false);
		}

		private byte[] _Connect()
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://" + AvailableConnection.Host + ":" + AvailableConnection.Port + "/connect/binary");
			httpWebRequest.Method = "POST";
			httpWebRequest.ContentType = "application/x-www-form-urlencoded";
			httpWebRequest.ContinueDelegate = (HttpContinueDelegate)Delegate.Combine(httpWebRequest.ContinueDelegate, new HttpContinueDelegate(OnContinue));
			using (Stream stream = httpWebRequest.GetRequestStream())
			{
				stream.Write(new byte[1], 0, 1);
			}
			HttpWebResponse httpWebResponse = httpWebRequest.GetResponse() as HttpWebResponse;
			MemoryStream memoryStream = new MemoryStream();
			BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
			using (BinaryReader binaryReader = new BinaryReader(memoryStream))
			{
				int num = 0;
				Stream responseStream = httpWebResponse.GetResponseStream();
				while ((num = responseStream.ReadByte()) != -1)
				{
					binaryWriter.Write((byte)num);
				}
				memoryStream.Seek(0L, SeekOrigin.Begin);
				return binaryReader.ReadBytes((int)memoryStream.Length);
			}
		}
	}
}
