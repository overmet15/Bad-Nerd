using System;
using System.Collections.Generic;
using System.Text;
using Thrift.Protocol;

[Serializable]
public class ThriftAggregatePluginRequest : TBase
{
	[Serializable]
	public struct Isset
	{
		public bool pluginRequestArray;
	}

	private List<ThriftRequestDetails> _pluginRequestArray;

	public Isset __isset;

	public List<ThriftRequestDetails> PluginRequestArray
	{
		get
		{
			return _pluginRequestArray;
		}
		set
		{
			__isset.pluginRequestArray = true;
			_pluginRequestArray = value;
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
				if (tField.Type == TType.List)
				{
					PluginRequestArray = new List<ThriftRequestDetails>();
					TList tList = iprot.ReadListBegin();
					for (int i = 0; i < tList.Count; i++)
					{
						ThriftRequestDetails thriftRequestDetails = new ThriftRequestDetails();
						thriftRequestDetails = new ThriftRequestDetails();
						thriftRequestDetails.Read(iprot);
						PluginRequestArray.Add(thriftRequestDetails);
					}
					iprot.ReadListEnd();
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
		TStruct struc = new TStruct("ThriftAggregatePluginRequest");
		oprot.WriteStructBegin(struc);
		TField field = default(TField);
		if (PluginRequestArray != null && __isset.pluginRequestArray)
		{
			field.Name = "pluginRequestArray";
			field.Type = TType.List;
			field.ID = 1;
			oprot.WriteFieldBegin(field);
			oprot.WriteListBegin(new TList(TType.Struct, PluginRequestArray.Count));
			foreach (ThriftRequestDetails item in PluginRequestArray)
			{
				item.Write(oprot);
				oprot.WriteListEnd();
			}
			oprot.WriteFieldEnd();
		}
		oprot.WriteFieldStop();
		oprot.WriteStructEnd();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder("ThriftAggregatePluginRequest(");
		stringBuilder.Append("PluginRequestArray: ");
		stringBuilder.Append(PluginRequestArray);
		stringBuilder.Append(")");
		return stringBuilder.ToString();
	}
}
