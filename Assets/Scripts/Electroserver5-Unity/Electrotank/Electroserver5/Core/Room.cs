using System.Collections.Generic;
using Electrotank.Electroserver5.Api;

namespace Electrotank.Electroserver5.Core
{
	public class Room : IEntity
	{
		private IDictionary<string, RoomVariable> roomVariables = new Dictionary<string, RoomVariable>();

		private IDictionary<string, User> usersByName = new Dictionary<string, User>();

		public int Capacity { get; set; }

		public string Description { get; set; }

		public bool HasPassword { get; set; }

		public int Id { get; internal set; }

		public bool IsHidden { get; set; }

		public bool IsJoined { get; internal set; }

		public string Name { get; internal set; }

		public string Password { get; set; }

		public int UserCount { get; set; }

		public ICollection<User> Users
		{
			get
			{
				return usersByName.Values;
			}
		}

		public int ZoneId { get; set; }

		public List<RoomVariable> RoomVariables()
		{
			List<RoomVariable> list = new List<RoomVariable>();
			foreach (RoomVariable value in roomVariables.Values)
			{
				list.Add(value);
			}
			return list;
		}

		public RoomVariable RoomVariableByName(string name)
		{
			RoomVariable value;
			roomVariables.TryGetValue(name, out value);
			return value;
		}

		public override string ToString()
		{
			return string.Format("[Room: Name={0}, Id={1}, ZoneId={2}, Users={3}, UserCount={4}, HasPassword={5}, Description={6}, Capacity={7}, IsHidden={8}, Password={9}, IsJoined={10}]", Name, Id, ZoneId, Users, UserCount, HasPassword, Description, Capacity, IsHidden, Password, IsJoined);
		}

		internal void AddRoomVariable(RoomVariable rv)
		{
			if (!roomVariables.ContainsKey(rv.Name))
			{
				roomVariables.Add(rv.Name, rv);
			}
			else
			{
				roomVariables[rv.Name] = rv;
			}
		}

		internal void AddUser(User u)
		{
			if (!usersByName.ContainsKey(u.UserName))
			{
				usersByName.Add(u.UserName, u);
				if (u.IsMe)
				{
					IsJoined = true;
				}
				UserCount = Users.Count;
			}
		}

		internal void PurgeRoomVariables()
		{
			roomVariables.Clear();
		}

		internal void PurgeUsers()
		{
			usersByName.Clear();
			IsJoined = false;
		}

		internal void RemoveRoomVariable(string name)
		{
			roomVariables.Remove(name);
		}

		internal void RemoveUser(string name)
		{
			if (usersByName.ContainsKey(name))
			{
				User user = usersByName[name];
				usersByName.Remove(name);
				if (user.IsMe)
				{
					IsJoined = false;
				}
				UserCount = Users.Count;
			}
		}
	}
}
