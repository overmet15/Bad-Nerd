using System;
using System.Text;
using Thrift.Protocol;

[Serializable]
public class ThriftFindGamesRequest : TBase
{
	[Serializable]
	public struct Isset
	{
		public bool searchCriteria;
	}

	private ThriftSearchCriteria _searchCriteria;

	public Isset __isset;

	public ThriftSearchCriteria SearchCriteria
	{
		get
		{
			return _searchCriteria;
		}
		set
		{
			__isset.searchCriteria = true;
			_searchCriteria = value;
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
				if (tField.Type == TType.Struct)
				{
					SearchCriteria = new ThriftSearchCriteria();
					SearchCriteria.Read(iprot);
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
		TStruct struc = new TStruct("ThriftFindGamesRequest");
		oprot.WriteStructBegin(struc);
		TField field = default(TField);
		if (SearchCriteria != null && __isset.searchCriteria)
		{
			field.Name = "searchCriteria";
			field.Type = TType.Struct;
			field.ID = 1;
			oprot.WriteFieldBegin(field);
			SearchCriteria.Write(oprot);
			oprot.WriteFieldEnd();
		}
		oprot.WriteFieldStop();
		oprot.WriteStructEnd();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder("ThriftFindGamesRequest(");
		stringBuilder.Append("SearchCriteria: ");
		stringBuilder.Append((SearchCriteria != null) ? SearchCriteria.ToString() : "<null>");
		stringBuilder.Append(")");
		return stringBuilder.ToString();
	}
}
