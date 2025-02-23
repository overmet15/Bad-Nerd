using System.Collections.Generic;
using Electrotank.Electroserver5.Api;

namespace Electrotank.Electroserver5.Core
{
	public class User
	{
		private readonly IDictionary<string, UserVariable> userVariables = new Dictionary<string, UserVariable>();

		public EsObject BuddyVariable { get; internal set; }

		public bool IsBuddy { get; internal set; }

		public bool IsLoggedIn { get; internal set; }

		public bool IsMe { get; internal set; }

		public string UserName { get; internal set; }

		public ICollection<UserVariable> UserVariables
		{
			get
			{
				return userVariables.Values;
			}
		}

		public User()
		{
			IsMe = false;
			IsBuddy = false;
			IsLoggedIn = false;
		}

		public override string ToString()
		{
			return string.Format("[User: UserName={0}, IsMe={1}, IsLoggedIn={2}, IsBuddy={3}, BuddyVariable={4}, UserVariables={5}]", UserName, IsMe, IsLoggedIn, IsBuddy, BuddyVariable, UserVariables);
		}

		public UserVariable UserVariableByName(string name)
		{
			UserVariable value = null;
			userVariables.TryGetValue(name, out value);
			return value;
		}

		internal void AddUserVariable(UserVariable v)
		{
			if (!userVariables.ContainsKey(v.Name))
			{
				userVariables.Add(v.Name, v);
			}
			else
			{
				userVariables[v.Name] = v;
			}
		}

		internal void RemoveUserVaraible(string name)
		{
			userVariables.Remove(name);
		}
	}
}
