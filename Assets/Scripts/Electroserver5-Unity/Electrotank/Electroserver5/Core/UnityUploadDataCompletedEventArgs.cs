using System;

namespace Electrotank.Electroserver5.Core
{
	public class UnityUploadDataCompletedEventArgs : EventArgs
	{
		private byte[] result;

		public byte[] Result
		{
			get
			{
				return result;
			}
		}

		internal UnityUploadDataCompletedEventArgs(byte[] result)
		{
			this.result = result;
		}
	}
}
