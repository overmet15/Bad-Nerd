using System;
using System.Text;
using Thrift.Protocol;

[Serializable]
public class ThriftPingResponse : TBase
{
	[Serializable]
	public struct Isset
	{
		public bool globalResponseRequested;

		public bool pingRequestId;
	}

	private bool _globalResponseRequested;

	private int _pingRequestId;

	public Isset __isset;

	public bool GlobalResponseRequested
	{
		get
		{
			return _globalResponseRequested;
		}
		set
		{
			__isset.globalResponseRequested = true;
			_globalResponseRequested = value;
		}
	}

	public int PingRequestId
	{
		get
		{
			return _pingRequestId;
		}
		set
		{
			__isset.pingRequestId = true;
			_pingRequestId = value;
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
					GlobalResponseRequested = iprot.ReadBool();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 2:
				if (tField.Type == TType.I32)
				{
					PingRequestId = iprot.ReadI32();
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
		TStruct struc = new TStruct("ThriftPingResponse");
		oprot.WriteStructBegin(struc);
		TField field = default(TField);
		if (__isset.globalResponseRequested)
		{
			field.Name = "globalResponseRequested";
			field.Type = TType.Bool;
			field.ID = 1;
			oprot.WriteFieldBegin(field);
			oprot.WriteBool(GlobalResponseRequested);
			oprot.WriteFieldEnd();
		}
		if (__isset.pingRequestId)
		{
			field.Name = "pingRequestId";
			field.Type = TType.I32;
			field.ID = 2;
			oprot.WriteFieldBegin(field);
			oprot.WriteI32(PingRequestId);
			oprot.WriteFieldEnd();
		}
		oprot.WriteFieldStop();
		oprot.WriteStructEnd();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder("ThriftPingResponse(");
		stringBuilder.Append("GlobalResponseRequested: ");
		stringBuilder.Append(GlobalResponseRequested);
		stringBuilder.Append(",PingRequestId: ");
		stringBuilder.Append(PingRequestId);
		stringBuilder.Append(")");
		return stringBuilder.ToString();
	}
}
