using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public abstract class EsMessage : IThriftSerializable
	{
		public MessageType MessageType { get; internal set; }

		public int RequestId { get; internal set; }

		public int MessageNumber { get; internal set; }

		public string ServerId { get; internal set; }

		public abstract TBase ToThrift();

		public abstract void FromThrift(TBase t);

		public abstract TBase NewThrift();
	}
}
