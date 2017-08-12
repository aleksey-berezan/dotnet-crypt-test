using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace dotnet_crypt_test
{
    public static class Utils
    {
        public static byte[] GetBytes(string s) => Encoding.Default.GetBytes(s);

        public static string GetString(byte[] bytes) => Encoding.Default.GetString(bytes, 0, bytes.Length);

        public static void PrintBytes(byte[] r) => Console.WriteLine(GetBytesString(r));

        public static string GetBytesString(byte[] r) => $"{r.Length}" +
                                                         $" :: {BitConverter.ToInt32(r, 0)}" +
                                                         $" :: {BitConverter.ToString(r).Replace("-", "").ToLower()}" +
                                                         $" :: {Convert.ToBase64String(r)}";

        public static byte[] GenerateSalt(int length = 32)
        {
            const int saltLength = 32;
            using (var rng = new RNGCryptoServiceProvider())
            {
                var bytes = new byte[saltLength];
                rng.GetBytes(bytes);
                return bytes;
            }
        }

        public static byte[] Combine(byte[] data, byte[] salt)
        {
            var r = new byte[data.Length + salt.Length];
            Buffer.BlockCopy(data, 0, r, 0, data.Length);
            Buffer.BlockCopy(salt, 0, r, data.Length, salt.Length);
            return r;
        }

        public static byte[] HashSha256(byte[] data)
        {
            using (var h = SHA256.Create())
            {
                return h.ComputeHash(data);
            }
        }

        public static T Measure<T>(Func<T> f)
        {
            var sw = Stopwatch.StartNew();
            var r = f();
            Console.WriteLine("Elapsed time: {0}", sw.Elapsed);
            return r;
        }
    }
}