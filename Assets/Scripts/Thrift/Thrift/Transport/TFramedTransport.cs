using System.IO;

namespace Thrift.Transport
{
	public class TFramedTransport : TTransport
	{
		public class Factory : TTransportFactory
		{
			public override TTransport GetTransport(TTransport trans)
			{
				return new TFramedTransport(trans);
			}
		}

		protected TTransport transport;

		protected MemoryStream writeBuffer = new MemoryStream(1024);

		protected MemoryStream readBuffer;

		public override bool IsOpen
		{
			get
			{
				return transport.IsOpen;
			}
		}

		public TFramedTransport(TTransport transport)
		{
			this.transport = transport;
		}

		public override void Open()
		{
			transport.Open();
		}

		public override void Close()
		{
			transport.Close();
		}

		public override int Read(byte[] buf, int off, int len)
		{
			if (readBuffer != null)
			{
				int num = readBuffer.Read(buf, off, len);
				if (num > 0)
				{
					return num;
				}
			}
			ReadFrame();
			return readBuffer.Read(buf, off, len);
		}

		private void ReadFrame()
		{
			byte[] array = new byte[4];
			transport.ReadAll(array, 0, 4);
			int num = ((array[0] & 0xFF) << 24) | ((array[1] & 0xFF) << 16) | ((array[2] & 0xFF) << 8) | (array[3] & 0xFF);
			byte[] array2 = new byte[num];
			transport.ReadAll(array2, 0, num);
			readBuffer = new MemoryStream(array2);
		}

		public override void Write(byte[] buf, int off, int len)
		{
			writeBuffer.Write(buf, off, len);
		}

		public override void Flush()
		{
			byte[] buffer = writeBuffer.GetBuffer();
			int num = (int)writeBuffer.Length;
			writeBuffer = new MemoryStream(writeBuffer.Capacity);
			byte[] buf = new byte[4]
			{
				(byte)(0xFFu & (uint)(num >> 24)),
				(byte)(0xFFu & (uint)(num >> 16)),
				(byte)(0xFFu & (uint)(num >> 8)),
				(byte)(0xFFu & (uint)num)
			};
			transport.Write(buf, 0, 4);
			transport.Write(buffer, 0, num);
			transport.Flush();
		}
	}
}
