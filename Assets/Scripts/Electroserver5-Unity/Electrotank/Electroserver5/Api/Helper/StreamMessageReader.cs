using System;
using System.IO;
using System.Text;

namespace Electrotank.Electroserver5.Api.Helper
{
	public class StreamMessageReader
	{
		private Stream buffer;

		public StreamMessageReader(Stream buffer)
		{
			this.buffer = buffer;
		}

		public bool NextBoolean()
		{
			return NextByte() != 0;
		}

		public int NextInteger()
		{
			byte[] array = new byte[4];
			buffer.Read(array, 0, array.Length);
			return ((array[0] & 0xFF) << 24) | ((array[1] & 0xFF) << 16) | ((array[2] & 0xFF) << 8) | (array[3] & 0xFF);
		}

		public long NextLong()
		{
			byte[] array = new byte[8];
			buffer.Read(array, 0, array.Length);
			return ((long)(array[0] & 0xFF) << 56) | ((long)(array[1] & 0xFF) << 48) | ((long)(array[2] & 0xFF) << 40) | ((long)(array[3] & 0xFF) << 32) | ((long)(array[4] & 0xFF) << 24) | ((long)(array[5] & 0xFF) << 16) | ((long)(array[6] & 0xFF) << 8) | (array[7] & 0xFF);
		}

		public short NextShort()
		{
			byte[] array = new byte[2];
			buffer.Read(array, 0, array.Length);
			return (short)(((short)(array[0] & 0xFF) << 8) | (short)(array[1] & 0xFF));
		}

		public char NextCharacter()
		{
			return (char)NextShort();
		}

		public byte NextByte()
		{
			return (byte)buffer.ReadByte();
		}

		public float NextFloat()
		{
			byte[] array = new byte[4];
			buffer.Read(array, 0, 4);
			if (BitConverter.IsLittleEndian)
			{
				Array.Reverse(array);
			}
			return BitConverter.ToSingle(array, 0);
		}

		public double NextDouble()
		{
			byte[] array = new byte[8];
			buffer.Read(array, 0, 8);
			if (BitConverter.IsLittleEndian)
			{
				Array.Reverse(array);
			}
			return BitConverter.ToDouble(array, 0);
		}

		public string NextString()
		{
			int num = NextLength();
			byte[] bytes = new byte[num];
			buffer.Read(bytes, 0, num);
			return Encoding.UTF8.GetString(bytes);
		}

		public int[] nextIntegerArray()
		{
			int num = NextLength();
			int[] array = new int[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = NextInteger();
			}
			return array;
		}

		public bool[] nextBooleanArray()
		{
			int num = NextLength();
			bool[] array = new bool[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = NextBoolean();
			}
			return array;
		}

		public byte[] NextByteArray()
		{
			int num = NextLength();
			byte[] array = new byte[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = NextByte();
			}
			return array;
		}

		public char[] NextCharacterArray()
		{
			int num = NextLength();
			char[] array = new char[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = NextCharacter();
			}
			return array;
		}

		public double[] NextDoubleArray()
		{
			int num = NextLength();
			double[] array = new double[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = NextDouble();
			}
			return array;
		}

		public float[] NextFloatArray()
		{
			int num = NextLength();
			float[] array = new float[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = NextFloat();
			}
			return array;
		}

		public long[] NextLongArray()
		{
			int num = NextLength();
			long[] array = new long[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = NextLong();
			}
			return array;
		}

		public short[] NextShortArray()
		{
			int num = NextLength();
			short[] array = new short[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = NextShort();
			}
			return array;
		}

		public string[] NextStringArray()
		{
			int num = NextLength();
			string[] array = new string[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = NextString();
			}
			return array;
		}

		public int NextLength()
		{
			byte b = NextByte();
			int num = (b >> 6) & 3;
			int num2 = b & 0x3F;
			for (int i = 0; i < num; i++)
			{
				num2 <<= 8;
				num2 |= NextByte() & 0xFF;
			}
			return num2;
		}
	}
}
