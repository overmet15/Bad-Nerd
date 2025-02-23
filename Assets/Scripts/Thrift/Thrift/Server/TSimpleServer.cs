using System;
using Thrift.Protocol;
using Thrift.Transport;

namespace Thrift.Server
{
	public class TSimpleServer : TServer
	{
		private bool stop;

		public TSimpleServer(TProcessor processor, TServerTransport serverTransport)
			: base(processor, serverTransport, new TTransportFactory(), new TTransportFactory(), new TBinaryProtocol.Factory(), new TBinaryProtocol.Factory(), TServer.DefaultLogDelegate)
		{
		}

		public TSimpleServer(TProcessor processor, TServerTransport serverTransport, LogDelegate logDel)
			: base(processor, serverTransport, new TTransportFactory(), new TTransportFactory(), new TBinaryProtocol.Factory(), new TBinaryProtocol.Factory(), logDel)
		{
		}

		public TSimpleServer(TProcessor processor, TServerTransport serverTransport, TTransportFactory transportFactory)
			: base(processor, serverTransport, transportFactory, transportFactory, new TBinaryProtocol.Factory(), new TBinaryProtocol.Factory(), TServer.DefaultLogDelegate)
		{
		}

		public TSimpleServer(TProcessor processor, TServerTransport serverTransport, TTransportFactory transportFactory, TProtocolFactory protocolFactory)
			: base(processor, serverTransport, transportFactory, transportFactory, protocolFactory, protocolFactory, TServer.DefaultLogDelegate)
		{
		}

		public override void Serve()
		{
			try
			{
				serverTransport.Listen();
			}
			catch (TTransportException ex)
			{
				logDelegate(ex.ToString());
				return;
			}
			while (!stop)
			{
				TTransport tTransport = null;
				TTransport tTransport2 = null;
				TTransport tTransport3 = null;
				TProtocol tProtocol = null;
				TProtocol tProtocol2 = null;
				try
				{
					tTransport = serverTransport.Accept();
					if (tTransport != null)
					{
						tTransport2 = inputTransportFactory.GetTransport(tTransport);
						tTransport3 = outputTransportFactory.GetTransport(tTransport);
						tProtocol = inputProtocolFactory.GetProtocol(tTransport2);
						tProtocol2 = outputProtocolFactory.GetProtocol(tTransport3);
						while (processor.Process(tProtocol, tProtocol2))
						{
						}
					}
				}
				catch (TTransportException ex2)
				{
					if (stop)
					{
						logDelegate("TSimpleServer was shutting down, caught " + ex2.GetType().Name);
					}
				}
				catch (Exception ex3)
				{
					logDelegate(ex3.ToString());
				}
				if (tTransport2 != null)
				{
					tTransport2.Close();
				}
				if (tTransport3 != null)
				{
					tTransport3.Close();
				}
			}
			if (stop)
			{
				try
				{
					serverTransport.Close();
				}
				catch (TTransportException ex4)
				{
					logDelegate("TServerTranport failed on close: " + ex4.Message);
				}
				stop = false;
			}
		}

		public override void Stop()
		{
			stop = true;
			serverTransport.Close();
		}
	}
}
