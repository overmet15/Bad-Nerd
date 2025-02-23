using System.Collections.Generic;
using log4net;

namespace Electrotank.Electroserver5.Core
{
	public class UserManager
	{
		protected ILog log = LogManager.GetLogger(typeof(EsEngine));

		private readonly IDictionary<string, User> buddies = new Dictionary<string, User>();

		private readonly IDictionary<string, int> references = new Dictionary<string, int>();

		private readonly IDictionary<string, User> usersByName = new Dictionary<string, User>();

		public ICollection<User> Buddies
		{
			get
			{
				return buddies.Values;
			}
		}

		public User Me { get; internal set; }

		public ICollection<User> Users
		{
			get
			{
				return usersByName.Values;
			}
		}

		internal UserManager()
		{
		}

		public User AddBuddy(User user)
		{
			User user2 = AddUser(user);
			user2.BuddyVariable = user.BuddyVariable;
			user2.IsBuddy = true;
			if (!buddies.ContainsKey(user.UserName))
			{
				buddies.Add(user.UserName, user2);
			}
			return user2;
		}

		public bool DoesUserExist(string name)
		{
			return usersByName.ContainsKey(name);
		}

		public void RemoveBuddy(User user)
		{
			if (!buddies.ContainsKey(user.UserName))
			{
				buddies.Remove(user.UserName);
			}
			user.IsBuddy = false;
		}

		public User UserByName(string name)
		{
			User value;
			usersByName.TryGetValue(name, out value);
			return value;
		}

		internal User AddUser(User u)
		{
			if (u.IsMe)
			{
				Me = u;
			}
			if (!usersByName.ContainsKey(u.UserName))
			{
				usersByName.Add(u.UserName, u);
				references[u.UserName] = 0;
			}
			else
			{
				u = UserByName(u.UserName);
			}
			IDictionary<string, int> dictionary;
			IDictionary<string, int> dictionary2 = (dictionary = references);
			string userName;
			string key = (userName = u.UserName);
			int num = dictionary[userName];
			dictionary2[key] = num + 1;
			return u;
		}

		internal void RemoveUser(string name)
		{
			User user = UserByName(name);
			if (user != null)
			{
				IDictionary<string, int> dictionary;
				IDictionary<string, int> dictionary2 = (dictionary = references);
				string userName;
				string key = (userName = user.UserName);
				int num = dictionary[userName];
				dictionary2[key] = num - 1;
			}
			if (references[user.UserName] == 0 && !user.IsMe && !user.IsBuddy)
			{
				references.Remove(user.UserName);
				usersByName.Remove(user.UserName);
			}
		}
	}
}
