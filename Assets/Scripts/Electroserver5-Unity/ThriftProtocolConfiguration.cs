using System;
using System.Text;
using Thrift.Protocol;

[Serializable]
public class ThriftProtocolConfiguration : TBase
{
	[Serializable]
	public struct Isset
	{
		public bool messageCompressionThreshold;
	}

	private int _messageCompressionThreshold;

	public Isset __isset;

	public int MessageCompressionThreshold
	{
		get
		{
			return _messageCompressionThreshold;
		}
		set
		{
			__isset.messageCompressionThreshold = true;
			_messageCompressionThreshold = value;
		}
	}

	public void Read(TProtocol iprot)
	{
		iprot.ReadStructBegin();
		while (true)
		{
			TField tField = iprot.ReadFieldBegin();
			if (tField.Type == TType.Stop)
			{
				break;
			}
			short iD = tField.ID;
			if (iD == 1)
			{
				if (tField.Type == TType.I32)
				{
					MessageCompressionThreshold = iprot.ReadI32();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
			}
			else
			{
				TProtocolUtil.Skip(iprot, tField.Type);
			}
			iprot.ReadFieldEnd();
		}
		iprot.ReadStructEnd();
	}

	public void Write(TProtocol oprot)
	{
		TStruct struc = new TStruct("ThriftProtocolConfiguration");
		oprot.WriteStructBegin(struc);
		TField field = default(TField);
		if (__isset.messageCompressionThreshold)
		{
			field.Name = "messageCompressionThreshold";
			field.Type = TType.I32;
			field.ID = 1;
			oprot.WriteFieldBegin(field);
			oprot.WriteI32(MessageCompressionThreshold);
			oprot.WriteFieldEnd();
		}
		oprot.WriteFieldStop();
		oprot.WriteStructEnd();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder("ThriftProtocolConfiguration(");
		stringBuilder.Append("MessageCompressionThreshold: ");
		stringBuilder.Append(MessageCompressionThreshold);
		stringBuilder.Append(")");
		return stringBuilder.ToString();
	}
}
