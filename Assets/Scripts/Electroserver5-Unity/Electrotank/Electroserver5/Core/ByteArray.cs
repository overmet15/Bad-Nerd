using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Electrotank.Electroserver5.Core
{
	internal class ByteArray
	{
		private delegate T Read<T>();

		private delegate ByteArray Write2<T>(T value);

		private byte[] buffer;

		private int length;

		private int position;

		public byte[] RawBytes
		{
			get
			{
				return buffer;
			}
		}

		internal ByteArray(byte[] buffer, int position, int length)
		{
			this.buffer = buffer;
			this.position = position;
			this.length = length;
		}

		internal ByteArray(byte[] buffer)
		{
			this.buffer = buffer;
			position = 0;
			length = buffer.Length;
		}

		internal bool ReadBoolean()
		{
			bool result = buffer[position] == 1;
			Advance(1);
			return result;
		}

		internal bool[] ReadBooleanArray()
		{
			return ReadArray(ReadBoolean);
		}

		internal byte ReadByte()
		{
			byte result = buffer[position];
			Advance(1);
			return result;
		}

		internal byte[] ReadRemaining()
		{
			int num = length;
			byte[] array = new byte[num];
			Buffer.BlockCopy(buffer, position, array, 0, num);
			Advance(num);
			return array;
		}

		internal byte[] ReadByteArray()
		{
			int num = ReadInteger();
			byte[] array = new byte[num];
			Buffer.BlockCopy(buffer, position, array, 0, num);
			Advance(num);
			return array;
		}

		internal char ReadChar()
		{
			char c = BitConverter.ToChar(buffer, position);
			Advance(2);
			return (char)IPAddress.NetworkToHostOrder((short)c);
		}

		internal double ReadDouble()
		{
			double value = BitConverter.ToDouble(buffer, position);
			Advance(8);
			byte[] bytes = BitConverter.GetBytes(value);
			Array.Reverse(bytes, 0, bytes.Length);
			return BitConverter.ToDouble(bytes, 0);
		}

		internal float ReadFloat()
		{
			float result = BitConverter.ToSingle(buffer, position);
			Advance(4);
			return result;
		}

		internal float[] ReadFloatArray()
		{
			return ReadArray(ReadFloat);
		}

		internal int ReadInteger()
		{
			int network = BitConverter.ToInt32(buffer, position);
			Advance(4);
			return IPAddress.NetworkToHostOrder(network);
		}

		internal int[] ReadIntegerArray()
		{
			return ReadArray(ReadInteger);
		}

		internal long ReadLong()
		{
			long network = BitConverter.ToInt64(buffer, position);
			Advance(8);
			return IPAddress.NetworkToHostOrder(network);
		}

		internal short ReadShort()
		{
			short network = BitConverter.ToInt16(buffer, position);
			Advance(2);
			return IPAddress.NetworkToHostOrder(network);
		}

		internal string ReadString()
		{
			short num = ReadShort();
			if (num == 0)
			{
				return null;
			}
			Encoding uTF = Encoding.UTF8;
			string @string = uTF.GetString(buffer, position, num);
			Advance(num);
			return @string;
		}

		internal string[] ReadStringArray()
		{
			return ReadArray(ReadString);
		}

		internal IList<ArraySegment<byte>> ToArray()
		{
			IList<ArraySegment<byte>> list = new List<ArraySegment<byte>>(1);
			list.Add(new ArraySegment<byte>(buffer, 0, position));
			return list;
		}

		internal ByteArray Write(byte[] bytes)
		{
			Buffer.BlockCopy(bytes, 0, buffer, position, bytes.Length);
			Advance(bytes.Length);
			return this;
		}

		internal ByteArray WriteBoolean(bool b)
		{
			Write(new byte[1] { (byte)(b ? 1 : 0) });
			return this;
		}

		internal ByteArray WriteBooleanArray(bool[] values)
		{
			return WriteArray(WriteBoolean, values);
		}

		internal ByteArray WriteByte(byte b)
		{
			Write(new byte[1] { b });
			return this;
		}

		internal ByteArray WriteByteArray(byte[] values)
		{
			return WriteInteger(values.Length).Write(values);
		}

		internal ByteArray WriteChar(char c)
		{
			Write(BitConverter.GetBytes((char)IPAddress.HostToNetworkOrder((short)c)));
			return this;
		}

		internal ByteArray WriteDouble(double d)
		{
			byte[] bytes = BitConverter.GetBytes(d);
			Array.Reverse(bytes, 0, bytes.Length);
			Write(bytes);
			return this;
		}

		internal ByteArray WriteFloat(float f)
		{
			Write(BitConverter.GetBytes(f));
			return this;
		}

		internal ByteArray WriteFloatArray(float[] values)
		{
			return WriteArray(WriteFloat, values);
		}

		internal ByteArray WriteInteger(int i)
		{
			Write(BitConverter.GetBytes(IPAddress.HostToNetworkOrder(i)));
			return this;
		}

		internal ByteArray WriteIntegerArray(int[] values)
		{
			return WriteArray(WriteInteger, values);
		}

		internal ByteArray WriteLong(long l)
		{
			Write(BitConverter.GetBytes(IPAddress.HostToNetworkOrder(l)));
			return this;
		}

		internal ByteArray WriteShort(short s)
		{
			Write(BitConverter.GetBytes(IPAddress.HostToNetworkOrder(s)));
			return this;
		}

		internal ByteArray WriteString(string s)
		{
			if (s == null || s.Length == 0)
			{
				return WriteShort(0);
			}
			Encoding uTF = Encoding.UTF8;
			byte[] bytes = uTF.GetBytes(s);
			WriteShort((short)bytes.Length);
			Write(bytes);
			return this;
		}

		internal ByteArray WriteStringArray(string[] values)
		{
			return WriteArray(WriteString, values);
		}

		private void Advance(int count)
		{
			position += count;
			length -= count;
		}

		private T[] ReadArray<T>(Read<T> read)
		{
			int num = ReadInteger();
			T[] array = new T[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = read();
			}
			return array;
		}

		private ByteArray WriteArray<T>(Write2<T> write, T[] values)
		{
			WriteInteger(values.Length);
			foreach (T value in values)
			{
				write(value);
			}
			return this;
		}
	}
}
