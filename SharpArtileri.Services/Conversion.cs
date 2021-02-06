using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpArtileri.Services
{
    public class Conversion
    {
        private StringBuilder words;

        private readonly string[] m_Units = new string[10] {
            string.Empty,
            "satu", "dua", "tiga", "empat", "lima",
            "enam", "tujuh", "delapan", "sembilan"
        };

        private readonly string[] m_Thousands = new string[5] {
            string.Empty, " ribu", " juta", " miliar", " triliun"
        };

        public string ConvertMoneyToWords(decimal money)
        {
            words = new StringBuilder(200);

            long number = (long)money;
            if (number == 0L)
            {
                words.Append("Nol ");
            }
            else
            {
                // Hitung jumlah per tiga digit.
                int digits = 0;
                long step = 1L;
                while (step <= number)
                {
                    digits++;
                    step *= 1000L;
                }

                for (int index = (digits - 1); index >= 0; index--)
                {
                    long counter = (long)Math.Pow(1000, index);
                    long temp = number / counter;
                    short remainder = (short)(temp % 1000L);

                    if (remainder > 0)
                    {
                        AddWords(remainder, m_Thousands[index % m_Thousands.Length]);
                        words.Append(" ");
                    }
                }
            }

            words.Append("rupiah");

            // Apakah ada sen ?
            decimal fraction = money - decimal.Truncate(money);
            if (fraction > 0m)
            {
                short cent = (short)(fraction * 100m);

                words.Append(" ");
                AddWords(cent, string.Empty);
                words.Append(" sen");
            }

            words.Append(".");

            // Kapital huruf pertama.
            words.Replace(words[0], char.ToUpper(words[0]), 0, 1);

            return words.ToString();
        }

        private void AddWords(short number, string suffix)
        {
            // Three digits.
            int[] digits = new int[3];
            for (int index = 2; index >= 0; index--)
            {
                digits[index] = number % 10;
                number /= 10;
            }

            bool isLeadingZero = true;

            /* digits[0] == ratusan
             * digits[1] == puluhan
             * digits[2] == satuan
             */
            if (digits[0] > 0)
            {
                if (digits[0] == 1)
                {
                    words.Append("seratus");
                }
                else
                {
                    words.Append(m_Units[digits[0]]).Append(" ratus");
                }

                isLeadingZero = false;
            }

            if (digits[1] > 0)
            {
                if (digits[0] > 0)
                {
                    words.Append(" ");
                }

                if (digits[1] == 1)
                {
                    switch (digits[2])
                    {
                        case 0:
                            words.Append("sepuluh");
                            break;
                        case 1:
                            words.Append("sebelas");
                            break;
                        default:
                            words.Append(m_Units[digits[2]]).Append(" belas");
                            break;
                    }

                    words.Append(suffix);
                    return;
                }

                words.Append(m_Units[digits[1]]).Append(" puluh");
                isLeadingZero = false;

                if (digits[2] == 0)
                {
                    words.Append(suffix);
                    return;
                }

                words.Append(" ");
            }

            if (isLeadingZero && (digits[2] == 1) && (suffix == " ribu"))
            {
                words.Append("seribu");
                return;
            }

            words.Append(m_Units[digits[2]]).Append(suffix);
        }
    }
}
