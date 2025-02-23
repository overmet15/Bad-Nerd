using System;
using System.Text;
using Thrift.Protocol;

[Serializable]
public class ThriftGatewayKickUserRequest : TBase
{
	[Serializable]
	public struct Isset
	{
		public bool clientId;

		public bool error;

		public bool esObject;
	}

	private long _clientId;

	private ThriftErrorType _error;

	private ThriftFlattenedEsObjectRO _esObject;

	public Isset __isset;

	public long ClientId
	{
		get
		{
			return _clientId;
		}
		set
		{
			__isset.clientId = true;
			_clientId = value;
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

	public ThriftFlattenedEsObjectRO EsObject
	{
		get
		{
			return _esObject;
		}
		set
		{
			__isset.esObject = true;
			_esObject = value;
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
					ClientId = iprot.ReadI64();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 2:
				if (tField.Type == TType.I32)
				{
					Error = (ThriftErrorType)iprot.ReadI32();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 3:
				if (tField.Type == TType.Struct)
				{
					EsObject = new ThriftFlattenedEsObjectRO();
					EsObject.Read(iprot);
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
		TStruct struc = new TStruct("ThriftGatewayKickUserRequest");
		oprot.WriteStructBegin(struc);
		TField field = default(TField);
		if (__isset.clientId)
		{
			field.Name = "clientId";
			field.Type = TType.I64;
			field.ID = 1;
			oprot.WriteFieldBegin(field);
			oprot.WriteI64(ClientId);
			oprot.WriteFieldEnd();
		}
		if (__isset.error)
		{
			field.Name = "error";
			field.Type = TType.I32;
			field.ID = 2;
			oprot.WriteFieldBegin(field);
			oprot.WriteI32((int)Error);
			oprot.WriteFieldEnd();
		}
		if (EsObject != null && __isset.esObject)
		{
			field.Name = "esObject";
			field.Type = TType.Struct;
			field.ID = 3;
			oprot.WriteFieldBegin(field);
			EsObject.Write(oprot);
			oprot.WriteFieldEnd();
		}
		oprot.WriteFieldStop();
		oprot.WriteStructEnd();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder("ThriftGatewayKickUserRequest(");
		stringBuilder.Append("ClientId: ");
		stringBuilder.Append(ClientId);
		stringBuilder.Append(",Error: ");
		stringBuilder.Append(Error);
		stringBuilder.Append(",EsObject: ");
		stringBuilder.Append((EsObject != null) ? EsObject.ToString() : "<null>");
		stringBuilder.Append(")");
		return stringBuilder.ToString();
	}
}
