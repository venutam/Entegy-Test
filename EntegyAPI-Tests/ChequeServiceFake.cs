using System;
using System.Collections.Generic;
using System.Text;
using EntegyAPI.Interface;

namespace EntegyAPI_Tests
{
    class ChequeServiceFake : IChequeService
    {
        public ChequeServiceFake()
        {

        }

        public string ConvertDecimalToWord(string amt)
        {
            string integral_part = "";
            string fractional_part = "";

            if (amt.Contains("."))
            {
                string[] parts = amt.Split('.');
                integral_part = parts[0];
                fractional_part = parts[1];
            }
            else
            {
                integral_part = amt;
            }            

            string strWords = ConvertToWord(integral_part) + " DOLLARS ";

            if (fractional_part.Length > 0)
            {
                string fraction = ConvertToWord(fractional_part);
                strWords = strWords + fraction + " CENTS";
            }

            return strWords;
        }

        private string ConvertToWord(string number)
        {
            string[] units = { "", " THOUSAND ", " MILLION ", " BILLION ", " TRILLION" };

            int counter = 0;

            string words = "";

            while (number.Length > 0)
            {
                string next_3digits = number.Length < 3 ? number : number.Substring(number.Length - 3);
                number = number.Length < 3 ? "" : number.Remove(number.Length - 3);

                int num = int.Parse(next_3digits);

                next_3digits = ConvertToWordLast3Digits(num);

                next_3digits += units[counter];

                words = next_3digits + words;

                counter++;

            }
            return words;
        }

        private string ConvertToWordLast3Digits(int num)
        {
            string[] Ones = { "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE", "TEN", "ELEVEN",
                                "TWELVE", "THIRTEEN", "FOURTEEN", "FIFTEEN", "SIXTEEN", "SEVENTEEN", "EIGHTEEN", "NINTEEN" };

            string[] Tens = { "TEN", "TWENTY", "THIRTY", "FOURTY", "FIFTY", "SIXTY", "SEVENTY", "EIGHTY", "NINTY" };

            string word = "";

            if (num > 99 && num < 1000)
            {
                int i = num / 100;
                word = word + Ones[i - 1] + " HUNDRED ";
                num = num % 100;
            }

            if (num > 19 && num < 100)
            {
                int i = num / 10;
                word = word + Tens[i - 1];
                num = num % 10;
            }

            if (num > 0 && num < 20)
            {
                word += " " + Ones[num - 1];
            }

            return word;
        }
    }
}
