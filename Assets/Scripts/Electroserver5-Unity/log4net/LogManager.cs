using System;

namespace log4net
{
	public class LogManager
	{
		public static bool IsDebugEnabled;

		public static ILog GetLogger(Type t)
		{
			return new ILog();
		}
	}
}
