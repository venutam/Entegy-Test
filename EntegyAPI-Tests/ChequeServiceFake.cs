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

            string err_msg = CheckInputAmt(amt, out bool inputAmtAccepted);

            if (!inputAmtAccepted)
                return err_msg;

            if (amt.Contains("."))
            {
                string[] parts = amt.Split('.');
                integral_part = parts[0];
                fractional_part = parts[1].Length < 2 ? parts[1] + "0" : parts[1].Substring(0, 2);
            }
            else
            {
                integral_part = amt;
            }

            Int64 max_allowed_integral_part = 999999999999999;

            if (Int64.Parse(integral_part) > max_allowed_integral_part)
                return "Sorry the maximum value that can be processed is 999999999999999.99";

            string strWords = ConvertToWord(integral_part);

            strWords = strWords == "" ? "" : strWords + (Int64.Parse(integral_part) > 1 ? " DOLLARS" : " DOLLAR");

            if (fractional_part.Length > 0)
            {
                string fraction = ConvertToWord(fractional_part);
                fraction = (fraction == "") ? "" : fraction + (int.Parse(fractional_part) > 1 ? " CENTS" : " CENT");
                if (!(fraction == ""))
                    strWords = (strWords == "") ? fraction + " ONLY" : strWords + " AND " + fraction;
            }

            strWords = strWords.Replace("  ", " ").Trim(); //TO REMOVE EXTRA SPACES

            if (strWords == "")
                return "ZERO DOLLAR";

            return strWords;
        }

        private string CheckInputAmt(string amt, out bool inputAmtAccepted)
        {
            decimal amount;
            if (!Decimal.TryParse(amt, out amount))
            {
                inputAmtAccepted = false;
                return "The input is not a number";
            }
            else
            {
                if (amount < 0)
                {
                    inputAmtAccepted = false;
                    return "The input is negative";
                }
                else if (amount == 0)
                {
                    inputAmtAccepted = false;
                    return "The input is zero";
                }
                else
                {
                    inputAmtAccepted = true;
                    return "";
                }
            }
        }

        private string ConvertToWord(string number)
        {
            string[] units = { "", " THOUSAND ", " MILLION ", " BILLION ", " TRILLION " };

            int counter = 0;

            string words = "";

            while (number.Length > 0)
            {
                string next_3digits = number.Length < 3 ? number : number.Substring(number.Length - 3);
                number = number.Length < 3 ? "" : number.Remove(number.Length - 3);

                int num = int.Parse(next_3digits);

                next_3digits = ConvertToWordLast3Digits(num);

                next_3digits = next_3digits == "" ? "" : next_3digits += units[counter];

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

            bool tens_check = false;

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
                tens_check = true;
            }

            if (num > 0 && num < 20)
            {
                word = tens_check ? word + "-" + Ones[num - 1] : word + " " + Ones[num - 1];
            }

            return word;
        }
    }
}
