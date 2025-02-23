using System;

namespace Thrift.Protocol
{
	internal class TProtocolException : Exception
	{
		public const int UNKNOWN = 0;

		public const int INVALID_DATA = 1;

		public const int NEGATIVE_SIZE = 2;

		public const int SIZE_LIMIT = 3;

		public const int BAD_VERSION = 4;

		protected int type_;

		public TProtocolException()
		{
		}

		public TProtocolException(int type)
		{
			type_ = type;
		}

		public TProtocolException(int type, string message)
			: base(message)
		{
			type_ = type;
		}

		public TProtocolException(string message)
			: base(message)
		{
		}

		public int getType()
		{
			return type_;
		}
	}
}
