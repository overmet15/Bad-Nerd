using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class DHSharedModulusRequest : EsRequest
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

		public DHSharedModulusRequest()
		{
			base.MessageType = MessageType.DHSharedModulusRequest;
		}

		public DHSharedModulusRequest(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftDHSharedModulusRequest thriftDHSharedModulusRequest = new ThriftDHSharedModulusRequest();
			if (Number_Set_ && Number != null)
			{
				string number = Number.ToString();
				thriftDHSharedModulusRequest.Number = number;
			}
			return thriftDHSharedModulusRequest;
		}

		public override TBase NewThrift()
		{
			return new ThriftDHSharedModulusRequest();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftDHSharedModulusRequest thriftDHSharedModulusRequest = (ThriftDHSharedModulusRequest)t_;
			if (thriftDHSharedModulusRequest.__isset.number && thriftDHSharedModulusRequest.Number != null)
			{
				Number = thriftDHSharedModulusRequest.Number;
			}
		}
	}
}
