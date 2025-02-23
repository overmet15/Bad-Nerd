namespace Thrift.Transport
{
	public abstract class TServerTransport
	{
		public abstract void Listen();

		public abstract void Close();

		protected abstract TTransport AcceptImpl();

		public TTransport Accept()
		{
			TTransport tTransport = AcceptImpl();
			if (tTransport == null)
			{
				throw new TTransportException("accept() may not return NULL");
			}
			return tTransport;
		}
	}
}
