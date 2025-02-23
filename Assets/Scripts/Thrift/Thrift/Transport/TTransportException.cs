using System;

namespace Thrift.Transport
{
	public class TTransportException : Exception
	{
		public enum ExceptionType
		{
			Unknown = 0,
			NotOpen = 1,
			AlreadyOpen = 2,
			TimedOut = 3,
			EndOfFile = 4
		}

		protected ExceptionType type;

		public ExceptionType Type
		{
			get
			{
				return type;
			}
		}

		public TTransportException()
		{
		}

		public TTransportException(ExceptionType type)
			: this()
		{
			this.type = type;
		}

		public TTransportException(ExceptionType type, string message)
			: base(message)
		{
			this.type = type;
		}

		public TTransportException(string message)
			: base(message)
		{
		}
	}
}
