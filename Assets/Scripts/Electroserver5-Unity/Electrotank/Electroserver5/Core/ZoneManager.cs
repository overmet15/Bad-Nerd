using System.Collections.Generic;

namespace Electrotank.Electroserver5.Core
{
	public class ZoneManager
	{
		private readonly MultipleIndexStore<Zone> zoneStore = new MultipleIndexStore<Zone>();

		public ICollection<Zone> Zones
		{
			get
			{
				return zoneStore.Values;
			}
		}

		public Zone ZoneById(int zoneId)
		{
			return zoneStore.ById(zoneId);
		}

		public Zone ZoneByName(string name)
		{
			return zoneStore.ByName(name);
		}

		internal void AddZone(Zone zone)
		{
			zoneStore.Add(zone);
		}

		internal void RemoveZone(int zoneId)
		{
			zoneStore.Remove(zoneId);
		}
	}
}
