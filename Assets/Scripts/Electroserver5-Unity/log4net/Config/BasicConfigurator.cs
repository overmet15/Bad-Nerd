namespace log4net.Config
{
	public class BasicConfigurator
	{
		public static void Configure()
		{
			LogManager.IsDebugEnabled = true;
		}
	}
}
