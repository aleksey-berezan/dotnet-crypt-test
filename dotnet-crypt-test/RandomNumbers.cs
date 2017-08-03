using System.Security.Cryptography;
using NUnit.Framework;
using static dotnet_crypt_test.Utils;

namespace dotnet_crypt_test
{
    [TestFixture]
    public class RandomNumbers
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

        [Test]
        public void Sample1()
        {
            PrintBytes(GenerateRandomBytes(8));
            PrintBytes(GenerateRandomBytes(8));
            PrintBytes(GenerateRandomBytes(8));
            PrintBytes(GenerateRandomBytes(8));
            PrintBytes(GenerateRandomBytes(8));
            PrintBytes(GenerateRandomBytes(8));
            PrintBytes(GenerateRandomBytes(8));
            PrintBytes(GenerateRandomBytes(8));
        }
    }
}
