using System.Collections.Generic;

namespace Electrotank.Electroserver5.Core
{
	internal class MultipleIndexStore<T> where T : IEntity
	{
		private IDictionary<string, T> byId = new Dictionary<string, T>();

		private IDictionary<string, T> byName = new Dictionary<string, T>();

		public ICollection<T> Values
		{
			get
			{
				return byId.Values;
			}
		}

		public void Add(T val)
		{
			if (!byId.ContainsKey(val.Id.ToString()))
			{
				byId.Add(val.Id.ToString(), val);
				byName.Add(val.Name, val);
			}
		}

		public T ById(int id)
		{
			T value;
			byId.TryGetValue(id.ToString(), out value);
			return value;
		}

		public T ByName(string name)
		{
			T value;
			byName.TryGetValue(name, out value);
			return value;
		}

		public void Remove(int valId)
		{
			T val = ById(valId);
			if (val != null)
			{
				byId.Remove(val.Id.ToString());
				byName.Remove(val.Name);
			}
		}
	}
}
