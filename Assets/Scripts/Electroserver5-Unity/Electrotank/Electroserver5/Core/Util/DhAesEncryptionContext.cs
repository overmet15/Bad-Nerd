using System;
using System.Security.Cryptography;
using System.Text;
using Electrotank.Electroserver5.Api;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using log4net;

namespace Electrotank.Electroserver5.Core.Util
{
	public class DhAesEncryptionContext
	{
		private static int DH_KEY_SIZE = 1024;

		private static Random rng = new Random();

		private BigInteger dhBase;

		private BigInteger dhPrime;

		private BigInteger dhSecret;

		private BigInteger dhSharedSecret;

		private IBlockCipher inputCipher;

		private bool inputEnabled;

		private ILog log = LogManager.GetLogger(typeof(DhAesEncryptionContext));

		private IBlockCipher outputCipher;

		private bool outputEnabled;

		public void DecryptIncomingMessage(byte[] b)
		{
			if (inputEnabled)
			{
				for (int i = 0; i < b.Length; i++)
				{
					inputCipher.ProcessBlock(b, i, b, i);
				}
			}
		}

		public bool EncryptionEnabled()
		{
			return inputEnabled && outputEnabled;
		}

		public void EncryptOutgoingMessage(byte[] b)
		{
			if (outputEnabled)
			{
				for (int i = 0; i < b.Length; i++)
				{
					outputCipher.ProcessBlock(b, i, b, i);
				}
			}
		}

		public void HandleIncomingMessage(EsMessage message, EsEngine engine, Connection con)
		{
			if (message is DHPublicNumbersResponse)
			{
				DHPublicNumbersResponse dHPublicNumbersResponse = (DHPublicNumbersResponse)message;
				dhBase = new BigInteger(dHPublicNumbersResponse.BaseNumber, 10);
				dhPrime = new BigInteger(dHPublicNumbersResponse.PrimeNumber, 10);
				dhSecret = new BigInteger(DH_KEY_SIZE, rng).Add(BigInteger.One);
				BigInteger bigInteger = dhBase.ModPow(dhSecret, dhPrime);
				DHSharedModulusRequest dHSharedModulusRequest = new DHSharedModulusRequest();
				dHSharedModulusRequest.Number = bigInteger.ToString(10);
				engine.Send(dHSharedModulusRequest, con);
			}
			else if (message is DHSharedModulusResponse)
			{
				DHSharedModulusResponse dHSharedModulusResponse = (DHSharedModulusResponse)message;
				dhSharedSecret = new BigInteger(dHSharedModulusResponse.Number, 10).ModPow(dhSecret, dhPrime);
				if (inputCipher == null)
				{
					PrepareCiphers();
				}
				EncryptionStateChangeEvent encryptionStateChangeEvent = new EncryptionStateChangeEvent();
				encryptionStateChangeEvent.EncryptionOn = true;
				engine.Send(encryptionStateChangeEvent, con);
				outputEnabled = true;
			}
			else
			{
				if (!(message is EncryptionStateChangeEvent))
				{
					return;
				}
				EncryptionStateChangeEvent encryptionStateChangeEvent2 = (EncryptionStateChangeEvent)message;
				if (encryptionStateChangeEvent2.EncryptionOn)
				{
					if (!outputEnabled)
					{
						throw new Exception("Got out of order encryption enable message from server.");
					}
					inputEnabled = true;
				}
				else
				{
					if (!inputEnabled)
					{
						throw new Exception("Got out of order encryption disable message from server.");
					}
					inputEnabled = false;
				}
			}
		}

		public void HandleOutgoingMessage(EsMessage message, EsEngine engine, Connection con)
		{
		}

		public void PrepareCiphers()
		{
			string text = dhSharedSecret.ToString(16);
			string text2 = text.Substring(0, 32);
			string text3 = text.Substring(32, 32);
			string text4 = text.Substring(64, 32);
			string text5 = text.Substring(96, 32);
			if (log.IsDebugEnabled)
			{
				log.Debug("sharedSecret: " + dhSharedSecret);
				log.Debug("outputKeyHex: " + text2);
				log.Debug("outputIvHex: " + text3);
				log.Debug("inputKeyHex: " + text4);
				log.Debug("inputIvHex: " + text5);
			}
			MD5 mD = new MD5CryptoServiceProvider();
			inputCipher = new CfbBlockCipher(new AesEngine(), 8);
			outputCipher = new CfbBlockCipher(new AesEngine(), 8);
			inputCipher.Init(false, new ParametersWithIV(new KeyParameter(mD.ComputeHash(Encoding.UTF8.GetBytes(text4))), mD.ComputeHash(Encoding.UTF8.GetBytes(text5))));
			outputCipher.Init(true, new ParametersWithIV(new KeyParameter(mD.ComputeHash(Encoding.UTF8.GetBytes(text2))), mD.ComputeHash(Encoding.UTF8.GetBytes(text3))));
		}

		public void SetEnabled(bool enabled, EsEngine engine, Connection con)
		{
			if (enabled)
			{
				if (dhBase == null)
				{
					DHInitiateKeyExchangeRequest request = new DHInitiateKeyExchangeRequest();
					engine.Send(request, con);
					return;
				}
				EncryptionStateChangeEvent encryptionStateChangeEvent = new EncryptionStateChangeEvent();
				encryptionStateChangeEvent.EncryptionOn = true;
				engine.Send(encryptionStateChangeEvent, con);
				outputEnabled = true;
			}
			else if (inputEnabled)
			{
				EncryptionStateChangeEvent encryptionStateChangeEvent2 = new EncryptionStateChangeEvent();
				encryptionStateChangeEvent2.EncryptionOn = false;
				engine.Send(encryptionStateChangeEvent2, con);
				outputEnabled = false;
			}
		}
	}
}
