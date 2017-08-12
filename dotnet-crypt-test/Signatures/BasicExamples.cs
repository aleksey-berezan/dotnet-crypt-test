using System;
using System.Security.Cryptography;
using NUnit.Framework;

namespace dotnet_crypt_test.Signatures
{
    [TestFixture]
    public class BasicExamples
    {
        private const int KeySize = 2048;

        private static RSACryptoServiceProvider CreateRsaCryptoServiceProvider() => new RSACryptoServiceProvider(KeySize) { PersistKeyInCsp = false };

        private (RSAParameters PublicKey, RSAParameters PrivateKey) GenerateKeys()
        {
            using (var rsa = CreateRsaCryptoServiceProvider())
            {
                return (PublicKey: rsa.ExportParameters(includePrivateParameters: false),
                        PrivateKey: rsa.ExportParameters(includePrivateParameters: true));
            }
        }

        private static byte[] SignData(string data, RSAParameters privateKey)
        {
            using (var rsa = CreateRsaCryptoServiceProvider())
            {
                rsa.ImportParameters(privateKey);
                var formatter = new RSAPKCS1SignatureFormatter(rsa);
                formatter.SetHashAlgorithm(HashAlgorithmName.SHA256.Name);
                return formatter.CreateSignature(Utils.HashSha256(Utils.GetBytes(data)));
            }
        }

        private static bool VerifySignature(string data, RSAParameters publicKey, byte[] signature)
        {
            using (var rsa = CreateRsaCryptoServiceProvider())
            {
                rsa.ImportParameters(publicKey);
                var deformatter = new RSAPKCS1SignatureDeformatter(rsa);
                deformatter.SetHashAlgorithm(HashAlgorithmName.SHA256.Name);
                return deformatter.VerifySignature(Utils.HashSha256(Utils.GetBytes(data)), signature);
            }
        }

        [Test]
        public void Sample1()
        {
            void Run(string data, string otherData)
            {
                Console.WriteLine($"      Data: {data}");
                Console.WriteLine($"Other Data: {otherData}");
                (RSAParameters publicKey, RSAParameters privateKey) = GenerateKeys();
                byte[] signature = SignData(data, privateKey);
                Console.WriteLine($"Signature: {Utils.GetBytesString(signature)}");
                bool isValid = VerifySignature(otherData, publicKey, signature);
                Console.WriteLine($"Data match: {isValid}");
                Console.WriteLine("+---------------------------------------+");
            }

            Run("Hello world", "Hello world");
            Run("Hello world", "hello world");
        }
    }
}