using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NarposTestApp
{
    public static class RandomNameHelper
    {
        public static string GenerateRandomName(int length = 10)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz";
            byte[] bytes = new byte[length];

            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(bytes);
            }

            char firstChar = char.ToUpper(chars[bytes[0] % chars.Length]);

            var rest = new char[length - 1];
            for (int i = 0; i < rest.Length; i++)
            {
                rest[i] = chars[bytes[i + 1] % chars.Length];
            }

            if (!rest.Contains('a'))
            {
                int randomIndex = GetRandomInt(0, rest.Length);
                rest[randomIndex] = 'a';
            }

            return firstChar + new string(rest);
        }

        private static int GetRandomInt(int minValue, int maxValue)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] randomNumber = new byte[4];
                rng.GetBytes(randomNumber);
                int result = Math.Abs(BitConverter.ToInt32(randomNumber, 0));
                return minValue + (result % (maxValue - minValue));
            }
        }
    }
}
