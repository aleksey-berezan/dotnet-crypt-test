using System.Security.Cryptography;
using System.Text;
using NUnit.Framework;

namespace dotnet_crypt_test.Hash
{
    [TestFixture]
    public class HashingExamples
    {
        private static byte[] ComputeHash(string data, HashAlgorithm h) => h.ComputeHash(Encoding.Default.GetBytes(data));
        public static byte[] ComputeHashMD5(string data) => ComputeHash(data, MD5.Create());
        public static byte[] ComputeHashSHA256(string data) => ComputeHash(data, SHA256.Create());
        public static byte[] ComputeHashSHA512(string data) => ComputeHash(data, SHA512.Create());
        public static byte[] ComputeHashSHA512Cng(string data) => ComputeHash(data, SHA512Cng.Create());
        public static byte[] ComputeHashSHA512Managed(string data) => ComputeHash(data, SHA512Managed.Create());

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