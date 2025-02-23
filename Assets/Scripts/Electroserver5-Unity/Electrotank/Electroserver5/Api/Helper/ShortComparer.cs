using System.Collections;
using System.Collections.Generic;

namespace Electrotank.Electroserver5.Api.Helper
{
	public class ShortComparer : IComparer, IEqualityComparer<short>, IComparer<short>
	{
		public int Compare(short x, short y)
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
			return Compare((short)x, (short)y);
		}

		public bool Equals(short i1, short i2)
		{
			if (i1 == i2)
			{
				return true;
			}
			return false;
		}

		public int GetHashCode(short i)
		{
			return i;
		}
	}
}
