using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using NUnit.Framework;
using static dotnet_crypt_test.Utils;

namespace dotnet_crypt_test
{
    [TestFixture]
    public class Encryption
    {
        private static Rfc2898DeriveBytes GenerateKey(byte[] keyBytes, byte[] salt) => new Rfc2898DeriveBytes(keyBytes, salt, 1000);

        private static RijndaelManaged GetRijndael(byte[] key, byte[] salt, int keySize, int blockSize)
        {
            Rfc2898DeriveBytes deriveKeyBytes = GenerateKey(key, salt);
            return new RijndaelManaged
            {
                KeySize = keySize,
                BlockSize = blockSize,
                Key = deriveKeyBytes.GetBytes(keySize / 8),
                IV = deriveKeyBytes.GetBytes(blockSize / 8),
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7,
            };
        }

        private static byte[] Encrypt(string data, byte[] key, byte[] salt, int keySize, int blockSize)
        {
            RijndaelManaged aes = GetRijndael(key, salt, keySize, blockSize);
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    byte[] bytes = GetBytes(data);
                    cs.Write(bytes, 0, bytes.Length);
                    cs.FlushFinalBlock();
                    return ms.ToArray();
                }
            }
        }

        private static string Decrypt(byte[] data, byte[] key, byte[] salt, int keySize, int blockSize)
        {
            RijndaelManaged aes = GetRijndael(key, salt, keySize, blockSize);
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(data, 0, data.Length);
                    cs.FlushFinalBlock();
                    byte[] bytes = ms.ToArray();
                    return Encoding.Default.GetString(bytes);
                }
            }
        }

        [Test]
        public void Sample1()
        {
            const int keySize = 256;
            const int blockSize = 128;
            byte[] salt = GenerateSalt(keySize);
            byte[] key = GetBytes("mypassword");

            void Run(string s)
            {
                Console.WriteLine($"Original: {s}");

                byte[] encrypted = Encrypt(s, key, salt, keySize, blockSize);
                Console.WriteLine($"Encrypted: {GetBytesString(encrypted)}");

                string decrypted = Decrypt(encrypted, key, salt, keySize, blockSize);
                Console.WriteLine($"Decrypted: {decrypted}");
                Console.WriteLine("+-------------------------------------------------------+");
            }

            Run("Hello world");
            Run("Hello world2");
        }

    }
}