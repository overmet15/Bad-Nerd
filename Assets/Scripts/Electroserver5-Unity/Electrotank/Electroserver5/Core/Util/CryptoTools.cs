using System;
using System.Security.Cryptography;
using System.Text;

namespace Electrotank.Electroserver5.Core.Util
{
	public class CryptoTools
	{
		public string generateAndEncodeHash(byte[] dataToHash)
		{
			byte[] inArray = generateHash(dataToHash);
			return Convert.ToBase64String(inArray);
		}

		public byte[] generateHash(byte[] dataToHash)
		{
			SHA1 sHA = new SHA1CryptoServiceProvider();
			return sHA.ComputeHash(dataToHash);
		}

		public string generatePasswordHash(string visiblePassword, int hashId)
		{
			string s = visiblePassword + "___" + hashId;
			return generateAndEncodeHash(Encoding.UTF8.GetBytes(s));
		}
	}
}
