using System.Collections;
using System.Collections.Generic;

namespace Electrotank.Electroserver5.Api.Helper
{
	public class ByteComparer : IComparer, IEqualityComparer<byte>, IComparer<byte>
	{
		public int Compare(byte x, byte y)
		{
			if (x == y)
			{
				return 0;
			}
			if (x < y)
			{
				return -1;
			}
			return 1;
		}

		public int Compare(object x, object y)
		{
			return Compare((byte)x, (byte)y);
		}

		public bool Equals(byte i1, byte i2)
		{
			if (i1 == i2)
			{
				return true;
			}
			return false;
		}

		public int GetHashCode(byte i)
		{
			return i;
		}
	}
}
