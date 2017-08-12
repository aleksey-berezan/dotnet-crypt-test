using System.Security.Cryptography;
using NUnit.Framework;

namespace dotnet_crypt_test.RandomNumbers
{
    [TestFixture]
    public class BasicExamples
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
            Utils.PrintBytes(GenerateRandomBytes(8));
            Utils.PrintBytes(GenerateRandomBytes(8));
            Utils.PrintBytes(GenerateRandomBytes(8));
            Utils.PrintBytes(GenerateRandomBytes(8));
            Utils.PrintBytes(GenerateRandomBytes(8));
            Utils.PrintBytes(GenerateRandomBytes(8));
            Utils.PrintBytes(GenerateRandomBytes(8));
            Utils.PrintBytes(GenerateRandomBytes(8));
        }
    }
}
