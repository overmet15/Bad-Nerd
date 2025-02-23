using System;
using System.Security;

namespace Org.BouncyCastle.Utilities
{
	internal sealed class Platform
	{
		internal static readonly string NewLine = GetNewLine();

		private Platform()
		{
		}

		internal static Exception CreateNotImplementedException(string message)
		{
			return new NotImplementedException(message);
		}

		internal static string GetEnvironmentVariable(string variable)
		{
			try
			{
				return Environment.GetEnvironmentVariable(variable);
			}
			catch (SecurityException)
			{
				return null;
			}
		}

		private static string GetNewLine()
		{
			return Environment.NewLine;
		}
	}
}
