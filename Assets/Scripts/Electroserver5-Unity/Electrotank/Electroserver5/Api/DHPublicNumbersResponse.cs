using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class DHPublicNumbersResponse : EsResponse
	{
		private string BaseNumber_;

		private string PrimeNumber_;

		public string BaseNumber
		{
			get
			{
				return BaseNumber_;
			}
			set
			{
				BaseNumber_ = value;
				BaseNumber_Set_ = true;
			}
		}

		private bool BaseNumber_Set_ { get; set; }

		public string PrimeNumber
		{
			get
			{
				return PrimeNumber_;
			}
			set
			{
				PrimeNumber_ = value;
				PrimeNumber_Set_ = true;
			}
		}

		private bool PrimeNumber_Set_ { get; set; }

		public DHPublicNumbersResponse()
		{
			base.MessageType = MessageType.DHPublicNumbers;
		}

		public DHPublicNumbersResponse(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftDHPublicNumbersResponse thriftDHPublicNumbersResponse = new ThriftDHPublicNumbersResponse();
			if (BaseNumber_Set_ && BaseNumber != null)
			{
				string baseNumber = BaseNumber.ToString();
				thriftDHPublicNumbersResponse.BaseNumber = baseNumber;
			}
			if (PrimeNumber_Set_ && PrimeNumber != null)
			{
				string primeNumber = PrimeNumber.ToString();
				thriftDHPublicNumbersResponse.PrimeNumber = primeNumber;
			}
			return thriftDHPublicNumbersResponse;
		}

		public override TBase NewThrift()
		{
			return new ThriftDHPublicNumbersResponse();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftDHPublicNumbersResponse thriftDHPublicNumbersResponse = (ThriftDHPublicNumbersResponse)t_;
			if (thriftDHPublicNumbersResponse.__isset.baseNumber && thriftDHPublicNumbersResponse.BaseNumber != null)
			{
				BaseNumber = thriftDHPublicNumbersResponse.BaseNumber;
			}
			if (thriftDHPublicNumbersResponse.__isset.primeNumber && thriftDHPublicNumbersResponse.PrimeNumber != null)
			{
				PrimeNumber = thriftDHPublicNumbersResponse.PrimeNumber;
			}
		}
	}
}
