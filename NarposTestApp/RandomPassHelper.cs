using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NarposTestApp
{
    public class RandomPassHelper
    {
        public static string GenPass(int length = 8)
        {

            const string kucukHarfler = "abcdefghijklmnopqrstuvwxyz";
            const string buyukHarfler = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string rakamlar = "0123456789";
            string tumKarakterler = kucukHarfler + buyukHarfler + rakamlar;

            Random rnd = new Random();
            StringBuilder sifre = new StringBuilder();

            sifre.Append(kucukHarfler[rnd.Next(1, kucukHarfler.Length)]);
            sifre.Append(buyukHarfler[rnd.Next(1, buyukHarfler.Length)]);
            sifre.Append(rakamlar[rnd.Next(1, rakamlar.Length)]);

            for (int i = 3; i < length; i++)
            {
                sifre.Append(tumKarakterler[rnd.Next(tumKarakterler.Length)]);
            }

            return MixChars(sifre.ToString(), rnd);
        }

        public static string MixChars(string input, Random rnd)
        {
            char[] array = input.ToCharArray();
            for (int i = array.Length - 1; i > 0; i--)
            {
                int j = rnd.Next(i + 1);
                (array[i], array[j]) = (array[j], array[i]);
            }
            return new string(array);
        }


    }
}
