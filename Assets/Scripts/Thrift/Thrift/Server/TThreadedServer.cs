using System;
using System.Collections.Generic;
using System.Threading;
using Thrift.Collections;
using Thrift.Protocol;
using Thrift.Transport;

namespace Thrift.Server
{
	public class TThreadedServer : TServer
	{
		private const int DEFAULT_MAX_THREADS = 100;

		private volatile bool stop;

		private readonly int maxThreads;

		private Queue<TTransport> clientQueue;

		private THashSet<Thread> clientThreads;

		private object clientLock;

		private Thread workerThread;

		public TThreadedServer(TProcessor processor, TServerTransport serverTransport)
			: this(processor, serverTransport, new TTransportFactory(), new TTransportFactory(), new TBinaryProtocol.Factory(), new TBinaryProtocol.Factory(), 100, TServer.DefaultLogDelegate)
		{
		}

		public TThreadedServer(TProcessor processor, TServerTransport serverTransport, LogDelegate logDelegate)
			: this(processor, serverTransport, new TTransportFactory(), new TTransportFactory(), new TBinaryProtocol.Factory(), new TBinaryProtocol.Factory(), 100, logDelegate)
		{
		}

		public TThreadedServer(TProcessor processor, TServerTransport serverTransport, TTransportFactory transportFactory, TProtocolFactory protocolFactory)
			: this(processor, serverTransport, transportFactory, transportFactory, protocolFactory, protocolFactory, 100, TServer.DefaultLogDelegate)
		{
		}

		public TThreadedServer(TProcessor processor, TServerTransport serverTransport, TTransportFactory inputTransportFactory, TTransportFactory outputTransportFactory, TProtocolFactory inputProtocolFactory, TProtocolFactory outputProtocolFactory, int maxThreads, LogDelegate logDel)
			: base(processor, serverTransport, inputTransportFactory, outputTransportFactory, inputProtocolFactory, outputProtocolFactory, logDel)
		{
			this.maxThreads = maxThreads;
			clientQueue = new Queue<TTransport>();
			clientLock = new object();
			clientThreads = new THashSet<Thread>();
		}

		public override void Serve()
		{
			try
			{
				workerThread = new Thread(Execute);
				workerThread.Start();
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
					TTransport item = serverTransport.Accept();
					lock (clientLock)
					{
						clientQueue.Enqueue(item);
						Monitor.Pulse(clientLock);
					}
				}
				catch (TTransportException ex2)
				{
					if (stop)
					{
						logDelegate("TThreadPoolServer was shutting down, caught " + ex2);
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
					logDelegate("TServeTransport failed on close: " + ex3.Message);
				}
				stop = false;
			}
		}

		private void Execute()
		{
			while (!stop)
			{
				TTransport parameter;
				Thread thread;
				lock (clientLock)
				{
					while (clientThreads.Count >= maxThreads)
					{
						Monitor.Wait(clientLock);
					}
					while (clientQueue.Count == 0)
					{
						Monitor.Wait(clientLock);
					}
					parameter = clientQueue.Dequeue();
					thread = new Thread(ClientWorker);
					clientThreads.Add(thread);
				}
				thread.Start(parameter);
			}
		}

		private void ClientWorker(object context)
		{
			TTransport trans = (TTransport)context;
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
			lock (clientLock)
			{
				clientThreads.Remove(Thread.CurrentThread);
				Monitor.Pulse(clientLock);
			}
		}

		public override void Stop()
		{
			stop = true;
			serverTransport.Close();
			workerThread.Abort();
			foreach (Thread clientThread in clientThreads)
			{
				clientThread.Abort();
			}
		}
	}
}
