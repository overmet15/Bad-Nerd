using System;
using System.Text;
using Thrift.Protocol;

[Serializable]
public class ThriftGenericErrorResponse : TBase
{
	[Serializable]
	public struct Isset
	{
		public bool requestMessageType;

		public bool errorType;

		public bool extraData;
	}

	private ThriftMessageType _requestMessageType;

	private ThriftErrorType _errorType;

	private ThriftFlattenedEsObject _extraData;

	public Isset __isset;

	public ThriftMessageType RequestMessageType
	{
		get
		{
			return _requestMessageType;
		}
		set
		{
			__isset.requestMessageType = true;
			_requestMessageType = value;
		}
	}

	public ThriftErrorType ErrorType
	{
		get
		{
			return _errorType;
		}
		set
		{
			__isset.errorType = true;
			_errorType = value;
		}
	}

	public ThriftFlattenedEsObject ExtraData
	{
		get
		{
			return _extraData;
		}
		set
		{
			__isset.extraData = true;
			_extraData = value;
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
				if (tField.Type == TType.I32)
				{
					RequestMessageType = (ThriftMessageType)iprot.ReadI32();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 2:
				if (tField.Type == TType.I32)
				{
					ErrorType = (ThriftErrorType)iprot.ReadI32();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 3:
				if (tField.Type == TType.Struct)
				{
					ExtraData = new ThriftFlattenedEsObject();
					ExtraData.Read(iprot);
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
		TStruct struc = new TStruct("ThriftGenericErrorResponse");
		oprot.WriteStructBegin(struc);
		TField field = default(TField);
		if (__isset.requestMessageType)
		{
			field.Name = "requestMessageType";
			field.Type = TType.I32;
			field.ID = 1;
			oprot.WriteFieldBegin(field);
			oprot.WriteI32((int)RequestMessageType);
			oprot.WriteFieldEnd();
		}
		if (__isset.errorType)
		{
			field.Name = "errorType";
			field.Type = TType.I32;
			field.ID = 2;
			oprot.WriteFieldBegin(field);
			oprot.WriteI32((int)ErrorType);
			oprot.WriteFieldEnd();
		}
		if (ExtraData != null && __isset.extraData)
		{
			field.Name = "extraData";
			field.Type = TType.Struct;
			field.ID = 3;
			oprot.WriteFieldBegin(field);
			ExtraData.Write(oprot);
			oprot.WriteFieldEnd();
		}
		oprot.WriteFieldStop();
		oprot.WriteStructEnd();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder("ThriftGenericErrorResponse(");
		stringBuilder.Append("RequestMessageType: ");
		stringBuilder.Append(RequestMessageType);
		stringBuilder.Append(",ErrorType: ");
		stringBuilder.Append(ErrorType);
		stringBuilder.Append(",ExtraData: ");
		stringBuilder.Append((ExtraData != null) ? ExtraData.ToString() : "<null>");
		stringBuilder.Append(")");
		return stringBuilder.ToString();
	}
}
