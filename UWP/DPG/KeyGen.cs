using System;
using System.Diagnostics;

namespace DPG
{
	static class KeyGen
	{
		// NOTE: The iteration count should
		// be as high as possible without causing
		// unreasonable delay.  Note also that the password
		// and salt are byte arrays, not strings.  After use,
		// the password and salt should be cleared (with Array.Clear)
		public static byte[] PBKDF2Sha256GetBytes(int dklen, byte[] password, byte[] salt, int iterationCount)
		{
			using (var hmac = new System.Security.Cryptography.HMACSHA512(password))
			{
				var hashLength = hmac.HashSize / 8;
				if ((hmac.HashSize & 7) != 0)
				{
					hashLength++;
				}

				var keyLength = dklen / hashLength;
				if (dklen > (0xFFFFFFFFL * hashLength) || dklen < 0)
				{
					throw new ArgumentOutOfRangeException("dklen");
				}

				if (dklen % hashLength != 0)
				{
					keyLength++;
				}

				var extendedkey = new byte[salt.Length + 4];
				Buffer.BlockCopy(salt, 0, extendedkey, 0, salt.Length);
				using (var ms = new System.IO.MemoryStream())
				{
					for (var i = 0; i < keyLength; i++)
					{
						extendedkey[salt.Length] = (byte)(((i + 1) >> 24) & 0xFF);
						extendedkey[salt.Length + 1] = (byte)(((i + 1) >> 16) & 0xFF);
						extendedkey[salt.Length + 2] = (byte)(((i + 1) >> 8) & 0xFF);
						extendedkey[salt.Length + 3] = (byte)(((i + 1)) & 0xFF);

						var u = hmac.ComputeHash(extendedkey);
						Array.Clear(extendedkey, salt.Length, 4);
						var f = u;
						for (var j = 1; j < iterationCount; j++)
						{
							u = hmac.ComputeHash(u);
							for (var k = 0; k < f.Length; k++)
							{
								f[k] ^= u[k];
							}
						}
						ms.Write(f, 0, f.Length);
						Array.Clear(u, 0, u.Length);
						Array.Clear(f, 0, f.Length);
					}
					var dk = new byte[dklen];
					ms.Position = 0;
					ms.Read(dk, 0, dklen);
					ms.Position = 0;
					for (long i = 0; i < ms.Length; i++)
					{
						ms.WriteByte(0);
					}

					Array.Clear(extendedkey, 0, extendedkey.Length);
					return dk;
				}
			}
		}
	}
}
