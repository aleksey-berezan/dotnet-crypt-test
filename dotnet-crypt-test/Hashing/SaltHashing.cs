using System.Security.Cryptography;
using System.Text;
using NUnit.Framework;

namespace dotnet_crypt_test.Hashing
{
    [TestFixture]
    public class SaltHashing
    {
        private static byte[] ComputeHash(string data, byte[] salt, HashAlgorithm h)
        {
            byte[] combined = Utils.Combine(Encoding.Default.GetBytes(data), salt);
            using (h)
            {
                return h.ComputeHash(combined);
            }
        }

        public static byte[] ComputeHashMD5(string data) => ComputeHash(data, Utils.GenerateSalt(), MD5.Create());
        public static byte[] ComputeHashSHA256(string data) => ComputeHash(data, Utils.GenerateSalt(), SHA256.Create());
        public static byte[] ComputeHashSHA512(string data) => ComputeHash(data, Utils.GenerateSalt(), SHA512.Create());
        public static byte[] ComputeHashSHA512Cng(string data) => ComputeHash(data, Utils.GenerateSalt(), SHA512Cng.Create());
        public static byte[] ComputeHashSHA512Managed(string data) => ComputeHash(data, Utils.GenerateSalt(), SHA512Managed.Create());

        [Test]
        public void Sample1()
        {
            Utils.PrintBytes(ComputeHashMD5("Hello World!"));
            Utils.PrintBytes(ComputeHashSHA256("Hello World!"));
            Utils.PrintBytes(ComputeHashSHA512("Hello World!"));
            Utils.PrintBytes(ComputeHashSHA512Cng("Hello World!"));
            Utils.PrintBytes(ComputeHashSHA512Managed("Hello World!"));
        }
    }
}