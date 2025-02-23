using System.Collections;
using System.Collections.Generic;

namespace Thrift.Collections
{
	public class THashSet<T> : IEnumerable<T>, ICollection<T>, IEnumerable
	{
		private HashSet<T> set = new HashSet<T>();

		public int Count
		{
			get
			{
				return set.Count;
			}
		}

		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return ((IEnumerable<T>)set).GetEnumerator();
		}

		public void Add(T item)
		{
			set.Add(item);
		}

		public void Clear()
		{
			set.Clear();
		}

		public bool Contains(T item)
		{
			return set.Contains(item);
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			set.CopyTo(array, arrayIndex);
		}

		public IEnumerator GetEnumerator()
		{
			return set.GetEnumerator();
		}

		public bool Remove(T item)
		{
			return set.Remove(item);
		}
	}
}
