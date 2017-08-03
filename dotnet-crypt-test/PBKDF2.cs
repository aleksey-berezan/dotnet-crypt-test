using System.Security.Cryptography;
using System.Text;
using NUnit.Framework;
using static dotnet_crypt_test.Utils;

namespace dotnet_crypt_test
{
    [TestFixture]
    public class PBKDF2
    {
        private static byte[] ComputeHash(string data, byte[] salt, int numberOfRounds)
        {
            using (var rfc2898 = new Rfc2898DeriveBytes(Encoding.Default.GetBytes(data), salt, numberOfRounds))
            {
                return rfc2898.GetBytes(32);
            }
        }

        [Test]
        public void Sample1()
        {
            int baseRounds = 50_000;
            PrintBytes(Measure(() => ComputeHash("Hello World!", GenerateSalt(128), baseRounds * 1)));
            PrintBytes(Measure(() => ComputeHash("Hello World!", GenerateSalt(128), baseRounds * 2)));
            PrintBytes(Measure(() => ComputeHash("Hello World!", GenerateSalt(128), baseRounds * 3)));
            PrintBytes(Measure(() => ComputeHash("Hello World!", GenerateSalt(128), baseRounds * 4)));
            PrintBytes(Measure(() => ComputeHash("Hello World!", GenerateSalt(128), baseRounds * 5)));
        }
    }
}