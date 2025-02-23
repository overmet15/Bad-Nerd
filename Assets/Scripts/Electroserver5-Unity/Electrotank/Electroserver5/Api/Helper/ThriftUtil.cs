using System;
using System.IO;
using Thrift.Protocol;
using Thrift.Transport;

namespace Electrotank.Electroserver5.Api.Helper
{
	public class ThriftUtil : EsObjectCodec
	{
		public static Enum EnumConvert(Type type, Enum enumValue)
		{
			return (Enum)Enum.Parse(type, enumValue.ToString());
		}

		public static byte[] Encode(TBase tbase)
		{
			MemoryStream memoryStream = new MemoryStream();
			TProtocol tProtocol = new TBinaryProtocol(new TStreamTransport(memoryStream, memoryStream));
			tbase.Write(tProtocol);
			return memoryStream.ToArray();
		}

		public static void Decode(byte[] bytes, TBase tbase)
		{
			MemoryStream memoryStream = new MemoryStream(bytes);
			TProtocol tProtocol = new TBinaryProtocol(new TStreamTransport(memoryStream, memoryStream));
			tbase.Read(tProtocol);
		}

		public static string DumpBuffer(byte[] bytes, int offset, int length)
		{
			return HexDumpForBytes(bytes, offset, length);
		}

		public static string HexDumpForBytes(byte[] b)
		{
			return HexDumpForBytes(b, 0, b.Length);
		}

		public static string HexDumpForBytes(byte[] bytes, int offset, int length)
		{
			string text = string.Empty;
			string text2 = string.Empty;
			for (int i = offset; i < length; i++)
			{
				text = text + bytes[i].ToString("x2") + " ";
				char c = (char)bytes[i];
				text2 += ((c < ' ' || c > '~') ? '.' : c);
				if (i % 16 == 15)
				{
					text = text + "  " + text2;
					text += "\n";
					text2 = string.Empty;
				}
			}
			if (length % 16 < 15)
			{
				for (int j = 0; j < 16 - length % 16; j++)
				{
					text += "   ";
				}
				text = text + "  " + text2;
			}
			return text;
		}
	}
}
