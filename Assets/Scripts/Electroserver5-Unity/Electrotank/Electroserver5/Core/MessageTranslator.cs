using System;
using System.Collections.Generic;
using Electrotank.Electroserver5.Api;
using Electrotank.Electroserver5.Api.Helper;
using Thrift.Protocol;

namespace Electrotank.Electroserver5.Core
{
	public class MessageTranslator
	{
		public static byte[] ToBytes(EsMessage message)
		{
			byte[] array = ThriftUtil.Encode(message.ToThrift());
			ByteArray byteArray = new ByteArray(new byte[array.Length + 6]);
			byteArray.WriteShort(Utility.getMessageTypeIndicator(message.MessageType));
			byteArray.WriteInteger(message.MessageNumber);
			byteArray.Write(array);
			return byteArray.RawBytes;
		}

		public static EsMessage ToMessage(byte[] bytes)
		{
			ByteArray byteArray = new ByteArray(bytes);
			short messageTypeIndicator = byteArray.ReadShort();
			int messageNumber = byteArray.ReadInteger();
			EsMessage esMessage;
			try
			{
				MessageType messageType = Utility.getMessageType(messageTypeIndicator);
				esMessage = Activator.CreateInstance(Utility.getApiClass(messageType)) as EsMessage;
				TBase tBase = esMessage.NewThrift();
				byte[] bytes2 = byteArray.ReadRemaining();
				ThriftUtil.Decode(bytes2, tBase);
				esMessage.FromThrift(tBase);
			}
			catch (KeyNotFoundException)
			{
				esMessage = new EsUnknownMessage();
				esMessage.MessageType = MessageType.Unknown;
			}
			esMessage.MessageNumber = messageNumber;
			return esMessage;
		}
	}
}
