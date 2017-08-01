using System;
using System.Security.Cryptography;
using NUnit.Framework;

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

        private static void PrintBytes(byte[] r)
        {
            Console.WriteLine($"{BitConverter.ToInt32(r, 0)}" +
                              $":{BitConverter.ToString(r)}" +
                              $":{Convert.ToBase64String(r)}");
        }
    }
}
