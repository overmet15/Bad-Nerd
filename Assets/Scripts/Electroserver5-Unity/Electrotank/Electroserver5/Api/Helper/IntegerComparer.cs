using System.Collections;
using System.Collections.Generic;

namespace Electrotank.Electroserver5.Api.Helper
{
	public class IntegerComparer : IComparer, IEqualityComparer<int>, IComparer<int>
	{
		public int Compare(int x, int y)
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
			return Compare((int)x, (int)y);
		}

		public bool Equals(int i1, int i2)
		{
			if (i1 == i2)
			{
				return true;
			}
			return false;
		}

		public int GetHashCode(int i)
		{
			return i;
		}
	}
}
