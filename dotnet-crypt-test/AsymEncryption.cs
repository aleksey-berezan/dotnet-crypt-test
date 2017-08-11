using System.Security.Cryptography;
using NUnit.Framework;
using static System.Console;
using static dotnet_crypt_test.Utils;

namespace dotnet_crypt_test
{
    [TestFixture]
    public class AsymEncryption
    {
        private const int KeySize = 2048;

        private (RSAParameters PublicKey, RSAParameters PrivateKey) GenerateKeys()
        {
            using (var rsa = new RSACryptoServiceProvider(KeySize))
            {
                rsa.PersistKeyInCsp = false;
                RSAParameters publicKey = rsa.ExportParameters(includePrivateParameters: false);
                RSAParameters privateKey = rsa.ExportParameters(includePrivateParameters: true);
                return (publicKey, privateKey);
            }
        }

        private byte[] Encrypt(string data, RSAParameters parameters)
        {
            using (var rsa = new RSACryptoServiceProvider(KeySize))
            {
                rsa.ImportParameters(parameters);
                return rsa.Encrypt(GetBytes(data), RSAEncryptionPadding.Pkcs1);
            }
        }

        private string Decrypt(byte[] bytes, RSAParameters parameters)
        {
            using (var rsa = new RSACryptoServiceProvider(dwKeySize: KeySize))
            {
                rsa.ImportParameters(parameters);
                byte[] decrypted = rsa.Decrypt(bytes, RSAEncryptionPadding.Pkcs1);
                return GetString(decrypted);
            }
        }

        [Test]
        public void Sample1()
        {
            void Run(string s)
            {
                (RSAParameters publicKey, RSAParameters privateKey) = GenerateKeys();

                WriteLine($"Input: {s}");
                byte[] encrypted = Encrypt(s, publicKey);
                WriteLine($"Encrypted: {GetBytesString(encrypted)}");
                string decrypted = Decrypt(encrypted, privateKey);
                WriteLine($"Decrypted: {decrypted}");
                WriteLine("+------------------------------------------------+");
            }

            Run("Hello world");
            Run("Hello world2");
        }
    }
}