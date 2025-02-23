using System;
using System.Text;
using Thrift.Protocol;

[Serializable]
public class ThriftGatewayStatistics : TBase
{
	[Serializable]
	public struct Isset
	{
		public bool bytesInTotal;

		public bool bytesOutTotal;

		public bool messagesInTotal;

		public bool messagesOutTotal;
	}

	private long _bytesInTotal;

	private long _bytesOutTotal;

	private long _messagesInTotal;

	private long _messagesOutTotal;

	public Isset __isset;

	public long BytesInTotal
	{
		get
		{
			return _bytesInTotal;
		}
		set
		{
			__isset.bytesInTotal = true;
			_bytesInTotal = value;
		}
	}

	public long BytesOutTotal
	{
		get
		{
			return _bytesOutTotal;
		}
		set
		{
			__isset.bytesOutTotal = true;
			_bytesOutTotal = value;
		}
	}

	public long MessagesInTotal
	{
		get
		{
			return _messagesInTotal;
		}
		set
		{
			__isset.messagesInTotal = true;
			_messagesInTotal = value;
		}
	}

	public long MessagesOutTotal
	{
		get
		{
			return _messagesOutTotal;
		}
		set
		{
			__isset.messagesOutTotal = true;
			_messagesOutTotal = value;
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
			switch (tField.ID)
			{
			case 1:
				if (tField.Type == TType.I64)
				{
					BytesInTotal = iprot.ReadI64();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 2:
				if (tField.Type == TType.I64)
				{
					BytesOutTotal = iprot.ReadI64();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 3:
				if (tField.Type == TType.I64)
				{
					MessagesInTotal = iprot.ReadI64();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 4:
				if (tField.Type == TType.I64)
				{
					MessagesOutTotal = iprot.ReadI64();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			default:
				TProtocolUtil.Skip(iprot, tField.Type);
				break;
			}
			iprot.ReadFieldEnd();
		}
		iprot.ReadStructEnd();
	}

	public void Write(TProtocol oprot)
	{
		TStruct struc = new TStruct("ThriftGatewayStatistics");
		oprot.WriteStructBegin(struc);
		TField field = default(TField);
		if (__isset.bytesInTotal)
		{
			field.Name = "bytesInTotal";
			field.Type = TType.I64;
			field.ID = 1;
			oprot.WriteFieldBegin(field);
			oprot.WriteI64(BytesInTotal);
			oprot.WriteFieldEnd();
		}
		if (__isset.bytesOutTotal)
		{
			field.Name = "bytesOutTotal";
			field.Type = TType.I64;
			field.ID = 2;
			oprot.WriteFieldBegin(field);
			oprot.WriteI64(BytesOutTotal);
			oprot.WriteFieldEnd();
		}
		if (__isset.messagesInTotal)
		{
			field.Name = "messagesInTotal";
			field.Type = TType.I64;
			field.ID = 3;
			oprot.WriteFieldBegin(field);
			oprot.WriteI64(MessagesInTotal);
			oprot.WriteFieldEnd();
		}
		if (__isset.messagesOutTotal)
		{
			field.Name = "messagesOutTotal";
			field.Type = TType.I64;
			field.ID = 4;
			oprot.WriteFieldBegin(field);
			oprot.WriteI64(MessagesOutTotal);
			oprot.WriteFieldEnd();
		}
		oprot.WriteFieldStop();
		oprot.WriteStructEnd();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder("ThriftGatewayStatistics(");
		stringBuilder.Append("BytesInTotal: ");
		stringBuilder.Append(BytesInTotal);
		stringBuilder.Append(",BytesOutTotal: ");
		stringBuilder.Append(BytesOutTotal);
		stringBuilder.Append(",MessagesInTotal: ");
		stringBuilder.Append(MessagesInTotal);
		stringBuilder.Append(",MessagesOutTotal: ");
		stringBuilder.Append(MessagesOutTotal);
		stringBuilder.Append(")");
		return stringBuilder.ToString();
	}
}
