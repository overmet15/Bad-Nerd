using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Crypto
{
	public interface IBasicAgreement
	{
		void Init(ICipherParameters parameters);

		BigInteger CalculateAgreement(ICipherParameters pubKey);
	}
}
