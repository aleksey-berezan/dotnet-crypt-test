using System.Security.Cryptography;
using NUnit.Framework;

namespace dotnet_crypt_test.Hashing
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
                return hmac.ComputeHash(Utils.GetBytes(data));
            }
        }

        [Test]
        public static void Sample1()
        {
            byte[] key = GenerateRandomBytes(512);
            Utils.PrintBytes(ComputeHMACSHA512("Hello World!", key));
            Utils.PrintBytes(ComputeHMACSHA512("Hello World!2", key));
        }
    }
}