using System.Collections;
using System.Collections.Generic;

namespace Electrotank.Electroserver5.Api.Helper
{
	public class LongComparer : IComparer, IEqualityComparer<long>, IComparer<long>
	{
		public int Compare(long x, long y)
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
			return Compare((long)x, (long)y);
		}

		public bool Equals(long i1, long i2)
		{
			if (i1 == i2)
			{
				return true;
			}
			return false;
		}

		public int GetHashCode(long i)
		{
			return (int)i ^ (int)(i >> 32);
		}
	}
}
