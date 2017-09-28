using System;

namespace ParsingUtility
{
    public static class PeselParser
    {
        public static Model.PeselData GetPeselData(string pesel)
        {
            if (!IsValid(pesel))
            {
                return null;
            }

            int[] decimalPesel = new int[11];
            for(int i = 0; i < 11; i++)
            {
                decimalPesel[i] = int.Parse(pesel[i].ToString());
            }
            int yearPart = decimalPesel[0] * 10 + decimalPesel[1];
            int monthPart = decimalPesel[2] * 10 + decimalPesel[3];
            int day = decimalPesel[4] * 10 + decimalPesel[5];
            bool male = decimalPesel[9] % 2 != 0;

            int year = GetYear(yearPart, monthPart);
            int month = GetMonth(monthPart);

            return new Model.PeselData(HashCreator.GetHash(pesel), male, new DateTime(year, month, day));
        }

        private static int GetMonth(int monthPart)
        {
            return monthPart - (monthPart / 20) * 20;
        }

        private static int GetYear(int yearPart, int monthPart)
        {
            int year = 1800 + yearPart;
            year += (monthPart / 20) * 100 + 100;
            return year;
        }

        private static bool IsValid(string input)
        {
            bool result = false;
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^\d{11}$");
            if (regex.Match(input).Success)
            {
                int controlSum = CalculateControlSum(input);
                int controlNum = controlSum % 10;
                controlNum = 10 - controlNum;
                if (controlNum == 10)
                {
                    controlNum = 0;
                }
                int lastDigit = int.Parse(input[input.Length - 1].ToString());
                result = controlNum == lastDigit;
            }
            return result;
        }

        private static int CalculateControlSum(string input)
        {
            int[] weights = { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3 };

            int controlSum = 0;
            for (int i = 0; i < input.Length - 1; i++)
            {
                controlSum += weights[i] * int.Parse(input[i].ToString());
            }
            return controlSum;
        }
    }
}
