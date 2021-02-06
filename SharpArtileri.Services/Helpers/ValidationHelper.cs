using System;

namespace SharpArtileri.Services.Helpers
{
    public static class ValidationHelper
    {
        public static bool IsValidCreditCardNumber(string number)
        {
            var deltas = new[] {0, 1, 2, 3, 4, -4, -3, -2, -1, 0};
            int checksum = 0;
            char[] chars = number.ToCharArray();
            for (int i = chars.Length - 1 ;i > -1 ;i--)
            {
                int j = chars[i] - 48;
                checksum += j;
                if (((i - chars.Length) % 2) == 0)
                    checksum += deltas[j];
            }

            return ((checksum % 10) == 0);
        }
    }
}