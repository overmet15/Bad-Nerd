using Electrotank.Electroserver5.Api.Helper;
using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class UserVariable : EsEntity
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

		public UserVariable()
		{
		}

		public UserVariable(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftUserVariable thriftUserVariable = new ThriftUserVariable();
			if (Name_Set_ && Name != null)
			{
				string name = Name;
				thriftUserVariable.Name = name;
			}
			if (Value_Set_ && Value != null)
			{
				ThriftFlattenedEsObject value = EsObjectCodec.FlattenEsObject(Value).ToThrift() as ThriftFlattenedEsObject;
				thriftUserVariable.Value = value;
			}
			return thriftUserVariable;
		}

		public override TBase NewThrift()
		{
			return new ThriftUserVariable();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftUserVariable thriftUserVariable = (ThriftUserVariable)t_;
			if (thriftUserVariable.__isset.name && thriftUserVariable.Name != null)
			{
				Name = thriftUserVariable.Name;
			}
			if (thriftUserVariable.__isset.value && thriftUserVariable.Value != null)
			{
				Value = EsObjectCodec.UnflattenEsObject(new FlattenedEsObject(thriftUserVariable.Value));
			}
		}
	}
}
