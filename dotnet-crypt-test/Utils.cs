using System;
using System.Text;

namespace dotnet_crypt_test
{
    public static class Utils
    {
        public static byte[] GetBytes(string s) => Encoding.Default.GetBytes(s);

        public static void PrintBytes(byte[] r) => Console.WriteLine($"{r.Length}" +
                                                                     $" :: {BitConverter.ToInt32(r, 0)}" +
                                                                     $" :: {BitConverter.ToString(r)}" +
                                                                     $" :: {Convert.ToBase64String(r)}");
    }
}