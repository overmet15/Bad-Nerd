using System;
using System.Text;
using Thrift.Protocol;

[Serializable]
public class ThriftLogOutRequest : TBase
{
	[Serializable]
	public struct Isset
	{
		public bool dropConnection;

		public bool dropAllConnections;
	}

	private bool _dropConnection;

	private bool _dropAllConnections;

	public Isset __isset;

	public bool DropConnection
	{
		get
		{
			return _dropConnection;
		}
		set
		{
			__isset.dropConnection = true;
			_dropConnection = value;
		}
	}

	public bool DropAllConnections
	{
		get
		{
			return _dropAllConnections;
		}
		set
		{
			__isset.dropAllConnections = true;
			_dropAllConnections = value;
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
					DropConnection = iprot.ReadBool();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
				break;
			case 2:
				if (tField.Type == TType.Bool)
				{
					DropAllConnections = iprot.ReadBool();
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
		TStruct struc = new TStruct("ThriftLogOutRequest");
		oprot.WriteStructBegin(struc);
		TField field = default(TField);
		if (__isset.dropConnection)
		{
			field.Name = "dropConnection";
			field.Type = TType.Bool;
			field.ID = 1;
			oprot.WriteFieldBegin(field);
			oprot.WriteBool(DropConnection);
			oprot.WriteFieldEnd();
		}
		if (__isset.dropAllConnections)
		{
			field.Name = "dropAllConnections";
			field.Type = TType.Bool;
			field.ID = 2;
			oprot.WriteFieldBegin(field);
			oprot.WriteBool(DropAllConnections);
			oprot.WriteFieldEnd();
		}
		oprot.WriteFieldStop();
		oprot.WriteStructEnd();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder("ThriftLogOutRequest(");
		stringBuilder.Append("DropConnection: ");
		stringBuilder.Append(DropConnection);
		stringBuilder.Append(",DropAllConnections: ");
		stringBuilder.Append(DropAllConnections);
		stringBuilder.Append(")");
		return stringBuilder.ToString();
	}
}
