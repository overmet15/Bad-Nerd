using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class EsNumber : EsEntity
	{
		private double Value_;

		public double Value
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

		public EsNumber()
		{
		}

		public EsNumber(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftEsNumber thriftEsNumber = new ThriftEsNumber();
			if (Value_Set_)
			{
				double value = Value;
				thriftEsNumber.Value = value;
			}
			return thriftEsNumber;
		}

		public override TBase NewThrift()
		{
			return new ThriftEsNumber();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftEsNumber thriftEsNumber = (ThriftEsNumber)t_;
			if (thriftEsNumber.__isset.value)
			{
				Value = thriftEsNumber.Value;
			}
		}
	}
}
