using System;
using System.IO;
using System.Text;

namespace Electrotank.Electroserver5.Api.Helper
{
	public class StreamMessageWriter
	{
		private Stream buffer;

		public StreamMessageWriter(Stream buffer)
		{
			this.buffer = buffer;
		}

		public void WriteBoolean(bool b)
		{
			WriteByte((byte)(b ? 1u : 0u));
		}

		public void WriteInteger(int i32)
		{
			buffer.WriteByte((byte)(0xFFu & (uint)(i32 >> 24)));
			buffer.WriteByte((byte)(0xFFu & (uint)(i32 >> 16)));
			buffer.WriteByte((byte)(0xFFu & (uint)(i32 >> 8)));
			buffer.WriteByte((byte)(0xFFu & (uint)i32));
		}

		public void WriteLong(long longIn)
		{
			buffer.WriteByte((byte)(0xFF & (longIn >> 56)));
			buffer.WriteByte((byte)(0xFF & (longIn >> 48)));
			buffer.WriteByte((byte)(0xFF & (longIn >> 40)));
			buffer.WriteByte((byte)(0xFF & (longIn >> 32)));
			buffer.WriteByte((byte)(0xFF & (longIn >> 24)));
			buffer.WriteByte((byte)(0xFF & (longIn >> 16)));
			buffer.WriteByte((byte)(0xFF & (longIn >> 8)));
			buffer.WriteByte((byte)(0xFF & longIn));
		}

		public void WriteDouble(double doubleIn)
		{
			byte[] bytes = BitConverter.GetBytes(doubleIn);
			if (BitConverter.IsLittleEndian)
			{
				Array.Reverse(bytes);
			}
			buffer.Write(bytes, 0, bytes.Length);
		}

		public void WriteCharacter(char character)
		{
			WriteShort((short)character);
		}

		public void WriteShort(short shortIn)
		{
			buffer.WriteByte((byte)(0xFFu & (uint)(shortIn >> 8)));
			buffer.WriteByte((byte)(0xFFu & (uint)shortIn));
		}

		public void WriteByte(byte byteIn)
		{
			buffer.WriteByte(byteIn);
		}

		public void WriteFloat(float floatIn)
		{
			byte[] bytes = BitConverter.GetBytes(floatIn);
			if (BitConverter.IsLittleEndian)
			{
				Array.Reverse(bytes);
			}
			buffer.Write(bytes, 0, bytes.Length);
		}

		public void WriteString(string s)
		{
			if (s == null)
			{
				s = string.Empty;
			}
			byte[] bytes = Encoding.UTF8.GetBytes(s);
			WriteLength(bytes.Length);
			buffer.Write(bytes, 0, bytes.Length);
		}

		public void WriteIntegerArray(int[] array)
		{
			int num = array.Length;
			WriteLength(num);
			for (int i = 0; i < num; i++)
			{
				WriteInteger(array[i]);
			}
		}

		public void WriteBooleanArray(bool[] array)
		{
			int num = array.Length;
			WriteLength(num);
			for (int i = 0; i < num; i++)
			{
				WriteBoolean(array[i]);
			}
		}

		public void WriteByteArray(byte[] array)
		{
			int num = array.Length;
			WriteLength(num);
			for (int i = 0; i < num; i++)
			{
				WriteByte(array[i]);
			}
		}

		public void WriteCharacterArray(char[] array)
		{
			int num = array.Length;
			WriteLength(num);
			for (int i = 0; i < num; i++)
			{
				WriteCharacter(array[i]);
			}
		}

		public void WriteDoubleArray(double[] array)
		{
			int num = array.Length;
			WriteLength(num);
			for (int i = 0; i < num; i++)
			{
				WriteDouble(array[i]);
			}
		}

		public void WriteFloatArray(float[] array)
		{
			int num = array.Length;
			WriteLength(num);
			for (int i = 0; i < num; i++)
			{
				WriteFloat(array[i]);
			}
		}

		public void WriteLongArray(long[] array)
		{
			int num = array.Length;
			WriteLength(num);
			for (int i = 0; i < num; i++)
			{
				WriteLong(array[i]);
			}
		}

		public void WriteShortArray(short[] array)
		{
			int num = array.Length;
			WriteLength(num);
			for (int i = 0; i < num; i++)
			{
				WriteShort(array[i]);
			}
		}

		public void WriteStringArray(string[] array)
		{
			int num = array.Length;
			WriteLength(num);
			for (int i = 0; i < num; i++)
			{
				WriteString(array[i]);
			}
		}

		public void WriteLength(int length)
		{
			if (((uint)length & 0x40000000u) != 0)
			{
				throw new Exception("writeLength(): length too large.");
			}
			int num = 1;
			if (length > 4194303)
			{
				num = 4;
			}
			else if (length > 16383)
			{
				num = 3;
			}
			else if (length > 63)
			{
				num = 2;
			}
			byte b = (byte)(num - 1 << 6);
			for (int num2 = num - 1; num2 >= 0; num2--)
			{
				b |= (byte)((uint)(length >> num2 * 8) & 0xFFu);
				WriteByte(b);
				b = 0;
			}
		}
	}
}
