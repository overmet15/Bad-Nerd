using Electrotank.Electroserver5.Api.Helper;
using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class PluginListEntry : EsEntity
	{
		private string ExtensionName_;

		private string PluginName_;

		private string PluginHandle_;

		private int PluginId_;

		private EsObject Parameters_;

		public string ExtensionName
		{
			get
			{
				return ExtensionName_;
			}
			set
			{
				ExtensionName_ = value;
				ExtensionName_Set_ = true;
			}
		}

		private bool ExtensionName_Set_ { get; set; }

		public string PluginName
		{
			get
			{
				return PluginName_;
			}
			set
			{
				PluginName_ = value;
				PluginName_Set_ = true;
			}
		}

		private bool PluginName_Set_ { get; set; }

		public string PluginHandle
		{
			get
			{
				return PluginHandle_;
			}
			set
			{
				PluginHandle_ = value;
				PluginHandle_Set_ = true;
			}
		}

		private bool PluginHandle_Set_ { get; set; }

		public int PluginId
		{
			get
			{
				return PluginId_;
			}
			set
			{
				PluginId_ = value;
				PluginId_Set_ = true;
			}
		}

		private bool PluginId_Set_ { get; set; }

		public EsObject Parameters
		{
			get
			{
				return Parameters_;
			}
			set
			{
				Parameters_ = value;
				Parameters_Set_ = true;
			}
		}

		private bool Parameters_Set_ { get; set; }

		public PluginListEntry()
		{
		}

		public PluginListEntry(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftPluginListEntry thriftPluginListEntry = new ThriftPluginListEntry();
			if (ExtensionName_Set_ && ExtensionName != null)
			{
				string extensionName = ExtensionName;
				thriftPluginListEntry.ExtensionName = extensionName;
			}
			if (PluginName_Set_ && PluginName != null)
			{
				string pluginName = PluginName;
				thriftPluginListEntry.PluginName = pluginName;
			}
			if (PluginHandle_Set_ && PluginHandle != null)
			{
				string pluginHandle = PluginHandle;
				thriftPluginListEntry.PluginHandle = pluginHandle;
			}
			if (PluginId_Set_)
			{
				int pluginId = PluginId;
				thriftPluginListEntry.PluginId = pluginId;
			}
			if (Parameters_Set_ && Parameters != null)
			{
				ThriftFlattenedEsObject parameters = EsObjectCodec.FlattenEsObject(Parameters).ToThrift() as ThriftFlattenedEsObject;
				thriftPluginListEntry.Parameters = parameters;
			}
			return thriftPluginListEntry;
		}

		public override TBase NewThrift()
		{
			return new ThriftPluginListEntry();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftPluginListEntry thriftPluginListEntry = (ThriftPluginListEntry)t_;
			if (thriftPluginListEntry.__isset.extensionName && thriftPluginListEntry.ExtensionName != null)
			{
				ExtensionName = thriftPluginListEntry.ExtensionName;
			}
			if (thriftPluginListEntry.__isset.pluginName && thriftPluginListEntry.PluginName != null)
			{
				PluginName = thriftPluginListEntry.PluginName;
			}
			if (thriftPluginListEntry.__isset.pluginHandle && thriftPluginListEntry.PluginHandle != null)
			{
				PluginHandle = thriftPluginListEntry.PluginHandle;
			}
			if (thriftPluginListEntry.__isset.pluginId)
			{
				PluginId = thriftPluginListEntry.PluginId;
			}
			if (thriftPluginListEntry.__isset.parameters && thriftPluginListEntry.Parameters != null)
			{
				Parameters = EsObjectCodec.UnflattenEsObject(new FlattenedEsObject(thriftPluginListEntry.Parameters));
			}
		}
	}
}
