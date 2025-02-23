using System;
using UnityEngine;

namespace log4net
{
	public class ILog
	{
		public bool IsDebugEnabled
		{
			get
			{
				return LogManager.IsDebugEnabled;
			}
		}

		public void Debug(string s)
		{
			if (IsDebugEnabled)
			{
				Console.WriteLine(s);
				UnityEngine.Debug.Log(s);
			}
		}

		public void DebugFormat(string s, params object[] p)
		{
			if (IsDebugEnabled)
			{
				string text = string.Format(s, p);
				Console.WriteLine(text);
				UnityEngine.Debug.Log(text);
			}
		}

		public void Error(string s)
		{
			if (IsDebugEnabled)
			{
				Console.WriteLine(s);
				UnityEngine.Debug.LogError(s);
			}
		}

		public void ErrorFormat(string s, params object[] p)
		{
			if (IsDebugEnabled)
			{
				string text = string.Format(s, p);
				Console.WriteLine(text);
				UnityEngine.Debug.LogError(text);
			}
		}
	}
}
