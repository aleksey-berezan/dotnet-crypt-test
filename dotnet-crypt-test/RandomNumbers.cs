using System;
using System.Security.Cryptography;
using NUnit.Framework;

namespace dotnet_crypt_test
{
    [TestFixture]
    public class RandomNumbers
    {
        private static byte[] GenerateRandomNumber(int bytesCount)
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
            byte[] r = null;
            r = GenerateRandomNumber(4); Console.WriteLine(BitConverter.ToString(r) + ":" + Convert.ToBase64String(r));
            r = GenerateRandomNumber(4); Console.WriteLine(BitConverter.ToString(r) + ":" + Convert.ToBase64String(r));
            r = GenerateRandomNumber(4); Console.WriteLine(BitConverter.ToString(r) + ":" + Convert.ToBase64String(r));
            r = GenerateRandomNumber(4); Console.WriteLine(BitConverter.ToString(r) + ":" + Convert.ToBase64String(r));
            r = GenerateRandomNumber(4); Console.WriteLine(BitConverter.ToString(r) + ":" + Convert.ToBase64String(r));
            r = GenerateRandomNumber(4); Console.WriteLine(BitConverter.ToString(r) + ":" + Convert.ToBase64String(r));
        }
    }
}
