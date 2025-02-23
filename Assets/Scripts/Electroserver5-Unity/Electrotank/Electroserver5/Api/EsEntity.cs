using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public abstract class EsEntity : IThriftSerializable
	{
		public abstract TBase ToThrift();

		public abstract void FromThrift(TBase t);

		public abstract TBase NewThrift();
	}
}
