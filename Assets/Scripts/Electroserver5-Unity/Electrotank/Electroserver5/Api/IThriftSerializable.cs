using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public interface IThriftSerializable
	{
		TBase ToThrift();

		void FromThrift(TBase t);

		TBase NewThrift();
	}
}
