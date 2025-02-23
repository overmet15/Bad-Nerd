using System;
using System.Text;
using Thrift.Protocol;

[Serializable]
public class ThriftConnectionAttemptResponse : TBase
{
	[Serializable]
	public struct Isset
	{
		public bool successful;

		public bool connectionId;

		public bool error;
	}

	private bool _successful;

	private int _connectionId;

	private ThriftErrorType _error;

	public Isset __isset;

	public bool Successful
	{
		get
		{
			return _successful;
		}
		set
		{
			__isset.successful = true;
			_successful = value;
		}
	}

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

	public ThriftErrorType Error
	{
		get
		{
			return _error;
		}
		set
		{
			__isset.error = true;
			_error = value;
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
				if (tField.Type == TType.Bool)
				{
					Successful = iprot.ReadBool();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 2:
				if (tField.Type == TType.I32)
				{
					ConnectionId = iprot.ReadI32();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 3:
				if (tField.Type == TType.I32)
				{
					Error = (ThriftErrorType)iprot.ReadI32();
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
		TStruct struc = new TStruct("ThriftConnectionAttemptResponse");
		oprot.WriteStructBegin(struc);
		TField field = default(TField);
		if (__isset.successful)
		{
			field.Name = "successful";
			field.Type = TType.Bool;
			field.ID = 1;
			oprot.WriteFieldBegin(field);
			oprot.WriteBool(Successful);
			oprot.WriteFieldEnd();
		}
		if (__isset.connectionId)
		{
			field.Name = "connectionId";
			field.Type = TType.I32;
			field.ID = 2;
			oprot.WriteFieldBegin(field);
			oprot.WriteI32(ConnectionId);
			oprot.WriteFieldEnd();
		}
		if (__isset.error)
		{
			field.Name = "error";
			field.Type = TType.I32;
			field.ID = 3;
			oprot.WriteFieldBegin(field);
			oprot.WriteI32((int)Error);
			oprot.WriteFieldEnd();
		}
		oprot.WriteFieldStop();
		oprot.WriteStructEnd();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder("ThriftConnectionAttemptResponse(");
		stringBuilder.Append("Successful: ");
		stringBuilder.Append(Successful);
		stringBuilder.Append(",ConnectionId: ");
		stringBuilder.Append(ConnectionId);
		stringBuilder.Append(",Error: ");
		stringBuilder.Append(Error);
		stringBuilder.Append(")");
		return stringBuilder.ToString();
	}
}
