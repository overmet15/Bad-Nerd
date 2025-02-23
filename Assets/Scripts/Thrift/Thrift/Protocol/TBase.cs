namespace Thrift.Protocol
{
	public interface TBase
	{
		void Read(TProtocol tProtocol);

		void Write(TProtocol tProtocol);
	}
}
