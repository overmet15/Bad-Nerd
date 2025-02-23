using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class EsUnknownMessage : EsMessage
	{
		public override TBase ToThrift()
		{
			return null;
		}

		public override void FromThrift(TBase t)
		{
		}

		public override TBase NewThrift()
		{
			return null;
		}
	}
}
