using System;
using System.Text;
using Thrift.Protocol;

[Serializable]
public class ThriftConnectionClosedEvent : TBase
{
	[Serializable]
	public struct Isset
	{
		public bool connectionId;
	}

	private int _connectionId;

	public Isset __isset;

	public int ConnectionId
	{
		get
		{
			return _connectionId;
		}
		set
		{
			__isset.connectionId = true;
			_connectionId = value;
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
					ConnectionId = iprot.ReadI32();
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
		TStruct struc = new TStruct("ThriftConnectionClosedEvent");
		oprot.WriteStructBegin(struc);
		TField field = default(TField);
		if (__isset.connectionId)
		{
			field.Name = "connectionId";
			field.Type = TType.I32;
			field.ID = 1;
			oprot.WriteFieldBegin(field);
			oprot.WriteI32(ConnectionId);
			oprot.WriteFieldEnd();
		}
		oprot.WriteFieldStop();
		oprot.WriteStructEnd();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder("ThriftConnectionClosedEvent(");
		stringBuilder.Append("ConnectionId: ");
		stringBuilder.Append(ConnectionId);
		stringBuilder.Append(")");
		return stringBuilder.ToString();
	}
}
