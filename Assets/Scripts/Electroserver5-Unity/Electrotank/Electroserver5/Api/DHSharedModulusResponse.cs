using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class DHSharedModulusResponse : EsResponse
	{
		private string Number_;

		public string Number
		{
			get
			{
				return Number_;
			}
			set
			{
				Number_ = value;
				Number_Set_ = true;
			}
		}

		private bool Number_Set_ { get; set; }

		public DHSharedModulusResponse()
		{
			base.MessageType = MessageType.DHSharedModulusResponse;
		}

		public DHSharedModulusResponse(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftDHSharedModulusResponse thriftDHSharedModulusResponse = new ThriftDHSharedModulusResponse();
			if (Number_Set_ && Number != null)
			{
				string number = Number.ToString();
				thriftDHSharedModulusResponse.Number = number;
			}
			return thriftDHSharedModulusResponse;
		}

		public override TBase NewThrift()
		{
			return new ThriftDHSharedModulusResponse();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftDHSharedModulusResponse thriftDHSharedModulusResponse = (ThriftDHSharedModulusResponse)t_;
			if (thriftDHSharedModulusResponse.__isset.number && thriftDHSharedModulusResponse.Number != null)
			{
				Number = thriftDHSharedModulusResponse.Number;
			}
		}
	}
}
