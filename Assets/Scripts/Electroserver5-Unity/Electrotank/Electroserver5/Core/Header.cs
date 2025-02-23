namespace Electrotank.Electroserver5.Core
{
	internal class Header
	{
		public string Key { get; set; }

		public string Value { get; set; }

		public Header(string key, string val)
		{
			Key = key;
			Value = val;
		}

		public override string ToString()
		{
			return string.Format("{0}: {1}\r\n", Key, Value);
		}
	}
}
