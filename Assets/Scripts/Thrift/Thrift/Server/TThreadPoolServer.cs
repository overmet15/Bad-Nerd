using System;
using System.Threading;
using Thrift.Protocol;
using Thrift.Transport;

namespace Thrift.Server
{
	public class TThreadPoolServer : TServer
	{
		private const int DEFAULT_MIN_THREADS = 10;

		private const int DEFAULT_MAX_THREADS = 100;

		private volatile bool stop;

		public TThreadPoolServer(TProcessor processor, TServerTransport serverTransport)
			: this(processor, serverTransport, new TTransportFactory(), new TTransportFactory(), new TBinaryProtocol.Factory(), new TBinaryProtocol.Factory(), 10, 100, TServer.DefaultLogDelegate)
		{
		}

		public TThreadPoolServer(TProcessor processor, TServerTransport serverTransport, LogDelegate logDelegate)
			: this(processor, serverTransport, new TTransportFactory(), new TTransportFactory(), new TBinaryProtocol.Factory(), new TBinaryProtocol.Factory(), 10, 100, logDelegate)
		{
		}

		public TThreadPoolServer(TProcessor processor, TServerTransport serverTransport, TTransportFactory transportFactory, TProtocolFactory protocolFactory)
			: this(processor, serverTransport, transportFactory, transportFactory, protocolFactory, protocolFactory, 10, 100, TServer.DefaultLogDelegate)
		{
		}

		public TThreadPoolServer(TProcessor processor, TServerTransport serverTransport, TTransportFactory inputTransportFactory, TTransportFactory outputTransportFactory, TProtocolFactory inputProtocolFactory, TProtocolFactory outputProtocolFactory, int minThreadPoolThreads, int maxThreadPoolThreads, LogDelegate logDel)
			: base(processor, serverTransport, inputTransportFactory, outputTransportFactory, inputProtocolFactory, outputProtocolFactory, logDel)
		{
			if (!ThreadPool.SetMinThreads(minThreadPoolThreads, minThreadPoolThreads))
			{
				throw new Exception("Error: could not SetMinThreads in ThreadPool");
			}
			if (!ThreadPool.SetMaxThreads(maxThreadPoolThreads, maxThreadPoolThreads))
			{
				throw new Exception("Error: could not SetMaxThreads in ThreadPool");
			}
		}

		public override void Serve()
		{
			try
			{
				serverTransport.Listen();
			}
			catch (TTransportException ex)
			{
				logDelegate("Error, could not listen on ServerTransport: " + ex);
				return;
			}
			while (!stop)
			{
				int num = 0;
				try
				{
					TTransport state = serverTransport.Accept();
					ThreadPool.QueueUserWorkItem(Execute, state);
				}
				catch (TTransportException ex2)
				{
					if (stop)
					{
						logDelegate("TThreadPoolServer was shutting down, caught " + ex2.GetType().Name);
						continue;
					}
					num++;
					logDelegate(ex2.ToString());
				}
			}
			if (stop)
			{
				try
				{
					serverTransport.Close();
				}
				catch (TTransportException ex3)
				{
					logDelegate("TServerTransport failed on close: " + ex3.Message);
				}
				stop = false;
			}
		}

		private void Execute(object threadContext)
		{
			TTransport trans = (TTransport)threadContext;
			TTransport tTransport = null;
			TTransport tTransport2 = null;
			TProtocol tProtocol = null;
			TProtocol tProtocol2 = null;
			try
			{
				tTransport = inputTransportFactory.GetTransport(trans);
				tTransport2 = outputTransportFactory.GetTransport(trans);
				tProtocol = inputProtocolFactory.GetProtocol(tTransport);
				tProtocol2 = outputProtocolFactory.GetProtocol(tTransport2);
				while (processor.Process(tProtocol, tProtocol2))
				{
				}
			}
			catch (TTransportException)
			{
			}
			catch (Exception ex2)
			{
				logDelegate("Error: " + ex2);
			}
			if (tTransport != null)
			{
				tTransport.Close();
			}
			if (tTransport2 != null)
			{
				tTransport2.Close();
			}
		}

		public override void Stop()
		{
			stop = true;
			serverTransport.Close();
		}
	}
}
