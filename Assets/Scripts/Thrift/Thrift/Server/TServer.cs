using System;
using Thrift.Protocol;
using Thrift.Transport;

namespace Thrift.Server
{
	public abstract class TServer
	{
		public delegate void LogDelegate(string str);

		protected TProcessor processor;

		protected TServerTransport serverTransport;

		protected TTransportFactory inputTransportFactory;

		protected TTransportFactory outputTransportFactory;

		protected TProtocolFactory inputProtocolFactory;

		protected TProtocolFactory outputProtocolFactory;

		protected LogDelegate logDelegate;

		public TServer(TProcessor processor, TServerTransport serverTransport)
			: this(processor, serverTransport, new TTransportFactory(), new TTransportFactory(), new TBinaryProtocol.Factory(), new TBinaryProtocol.Factory(), DefaultLogDelegate)
		{
		}

		public TServer(TProcessor processor, TServerTransport serverTransport, LogDelegate logDelegate)
			: this(processor, serverTransport, new TTransportFactory(), new TTransportFactory(), new TBinaryProtocol.Factory(), new TBinaryProtocol.Factory(), DefaultLogDelegate)
		{
		}

		public TServer(TProcessor processor, TServerTransport serverTransport, TTransportFactory transportFactory)
			: this(processor, serverTransport, transportFactory, transportFactory, new TBinaryProtocol.Factory(), new TBinaryProtocol.Factory(), DefaultLogDelegate)
		{
		}

		public TServer(TProcessor processor, TServerTransport serverTransport, TTransportFactory transportFactory, TProtocolFactory protocolFactory)
			: this(processor, serverTransport, transportFactory, transportFactory, protocolFactory, protocolFactory, DefaultLogDelegate)
		{
		}

		public TServer(TProcessor processor, TServerTransport serverTransport, TTransportFactory inputTransportFactory, TTransportFactory outputTransportFactory, TProtocolFactory inputProtocolFactory, TProtocolFactory outputProtocolFactory, LogDelegate logDelegate)
		{
			this.processor = processor;
			this.serverTransport = serverTransport;
			this.inputTransportFactory = inputTransportFactory;
			this.outputTransportFactory = outputTransportFactory;
			this.inputProtocolFactory = inputProtocolFactory;
			this.outputProtocolFactory = outputProtocolFactory;
			this.logDelegate = logDelegate;
		}

		public abstract void Serve();

		public abstract void Stop();

		protected static void DefaultLogDelegate(string s)
		{
			Console.Error.WriteLine(s);
		}
	}
}
