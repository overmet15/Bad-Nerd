using Electrotank.Electroserver5.Api.Helper;
using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class UserServerVariable : EsEntity
	{
		private string Name_;

		private EsObject Value_;

		public string Name
		{
			get
			{
				return Name_;
			}
			set
			{
				Name_ = value;
				Name_Set_ = true;
			}
		}

		private bool Name_Set_ { get; set; }

		public EsObject Value
		{
			get
			{
				return Value_;
			}
			set
			{
				Value_ = value;
				Value_Set_ = true;
			}
		}

		private bool Value_Set_ { get; set; }

		public UserServerVariable()
		{
		}

		public UserServerVariable(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftUserServerVariable thriftUserServerVariable = new ThriftUserServerVariable();
			if (Name_Set_ && Name != null)
			{
				string name = Name;
				thriftUserServerVariable.Name = name;
			}
			if (Value_Set_ && Value != null)
			{
				ThriftFlattenedEsObject value = EsObjectCodec.FlattenEsObject(Value).ToThrift() as ThriftFlattenedEsObject;
				thriftUserServerVariable.Value = value;
			}
			return thriftUserServerVariable;
		}

		public override TBase NewThrift()
		{
			return new ThriftUserServerVariable();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftUserServerVariable thriftUserServerVariable = (ThriftUserServerVariable)t_;
			if (thriftUserServerVariable.__isset.name && thriftUserServerVariable.Name != null)
			{
				Name = thriftUserServerVariable.Name;
			}
			if (thriftUserServerVariable.__isset.value && thriftUserServerVariable.Value != null)
			{
				Value = EsObjectCodec.UnflattenEsObject(new FlattenedEsObject(thriftUserServerVariable.Value));
			}
		}
	}
}
