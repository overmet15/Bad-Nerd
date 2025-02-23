using System.IO;

namespace Electrotank.Electroserver5.Core
{
	internal class PreservingMemoryStream : MemoryStream
	{
		public void Preserve()
		{
			byte[] array = new byte[RemainingBytes()];
			Read(array, 0, array.Length);
			_Preserve(array, 0, array.Length);
		}

		public long RemainingBytes()
		{
			return Length - Position;
		}

		private void _Preserve(byte[] leftover, int offset, int len)
		{
			SetLength(0L);
			Seek(0L, SeekOrigin.Begin);
			Write(leftover, offset, len);
			Seek(0L, SeekOrigin.Begin);
		}
	}
}
