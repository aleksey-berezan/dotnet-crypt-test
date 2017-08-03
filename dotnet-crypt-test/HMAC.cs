using System.Security.Cryptography;
using NUnit.Framework;
using static dotnet_crypt_test.Utils;

namespace dotnet_crypt_test
{
    [TestFixture]
    public class HMAC
    {
        private static byte[] GenerateRandomBytes(int bytesCount)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var randomBytes = new byte[bytesCount];
                rng.GetBytes(randomBytes);
                return randomBytes;
            }
        }

        private static byte[] ComputeHMACSHA512(string data, byte[] key)
        {
            using (var hmac = new HMACSHA512(key))
            {
                return hmac.ComputeHash(GetBytes(data));
            }
        }

        [Test]
        public static void Sample1()
        {
            byte[] key = GenerateRandomBytes(512);
            PrintBytes(ComputeHMACSHA512("Hello World!", key));
            PrintBytes(ComputeHMACSHA512("Hello World!2", key));
        }
    }
}