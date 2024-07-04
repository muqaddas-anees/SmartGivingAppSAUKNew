using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Text;
//using java.util;
using Random = System.Random;
using ArrayList = System.Collections.ArrayList;
using System.Linq;

namespace Deffinity
{
    public class Utility
    {
        private static Random random = new Random();

        private static readonly Random _random = new Random();
        private const string _chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private static readonly HashSet<string> _generatedCodes = new HashSet<string>();

        public static string GenerateUniqueCode(IEnumerable<string> existingCodes, int length = 6)
        {
            string newCode;
            do
            {
                newCode = new string(Enumerable.Repeat(_chars, length)
                                                .Select(s => s[_random.Next(s.Length)]).ToArray());
            } while (existingCodes.Contains(newCode) || _generatedCodes.Contains(newCode));

            _generatedCodes.Add(newCode);
            return newCode;
        }

        public static double GetNetAmount(double amount, double chargePercent, double fixedFee)
        {
            double percentageFee = amount * (chargePercent / 100);
            double totalFee = percentageFee + fixedFee;
            double netAmount = amount - totalFee;
            return netAmount;
        }

        public static double GetAmountCharges(double amount, double chargePercent)
        {
            double percentageFee = amount * (chargePercent / 100);
            double totalFee = percentageFee ;
            double netAmount =  totalFee;
            return netAmount;
        }
        public static string GetSevenCharRandomString(int length = 7)
        {
            string retval = "";

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            StringBuilder stringBuilder = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                stringBuilder.Append(chars[random.Next(chars.Length)]);
            }

            return stringBuilder.ToString();

            //   retval = GenerateUniqueRandomAlphaNumericString(7, 100000);

            // return retval;

        }

        public static string GenerateUniqueRandomAlphaNumericString(int length, int maxAttempts)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();

            HashSet<string> generatedStrings = new HashSet<string>();

            for (int attempt = 0; attempt < maxAttempts; attempt++)
            {
                StringBuilder randomString = new StringBuilder(length);

                for (int i = 0; i < length; i++)
                {
                    randomString.Append(chars[random.Next(chars.Length)]);
                }

                string generatedString = randomString.ToString();

                if (!generatedStrings.Contains(generatedString))
                {
                    generatedStrings.Add(generatedString);
                    return generatedString;
                }
            }

            throw new InvalidOperationException("Unable to generate a unique random string within the specified number of attempts.");
        }
        public static string TruncateString(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            if (input.Length <= 100)
                return input;

            return input.Substring(0, 100) + "...";
        }
        public static string GetCurrencySymbol()
        {
            CultureInfo sa_culture =
              new CultureInfo("af-ZA");

            return sessionKeys.Currency;// sa_culture.NumberFormat.CurrencySymbol;
        }
        public static string RemoveHTMLTags(string value)
        {
            Regex regex = new Regex("\\<[^\\>]*\\>");
            value = regex.Replace(value, String.Empty);
            return value;
        }
        public static DateTime EndOfDay( DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 23, 59, 59, 999);
        }

        public static DateTime StartOfDay( DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 0);
        }
        public static DateTime EndDateOfMonth(DateTime _Date)
        {
            if (_Date != null)
                return new DateTime(_Date.Year, _Date.Month, 1).AddMonths(1).AddDays(-1);
            else
                return _Date;
        }
        public static DateTime StartDateOfMonth(DateTime _Date)
        {
            if (_Date != null)
                return new DateTime(_Date.Year, _Date.Month, 1); 
            else 
                return _Date;
        }

        public static int DateDifference(DateTime StartDate,DateTime EndDate)
        {
            DateTime d1 = StartDate;
            DateTime d2 = EndDate;

            TimeSpan t1 = d2.Subtract(d1);

            return t1.Days;

        }

        public static int MonthDifference(DateTime fromDate, DateTime toDate)
        {
           // int M ;//Math.Abs((toDate.Month - fromDate.Month));

           //M= (toDate.Year * 12 + toDate.Month) - (fromDate.Year * 12 + fromDate.Month);
            //int months=((M*12)+Math.Abs((toDate.Month-fromDate.Month)));
//            int monthsApart = 12 * (toDate.Year - fromDate.Year) +
//toDate.Month - fromDate.Month;
            //int months;
            //if (toDate.Month < fromDate.Month)
            //{
            //    // example: March 2010 (3) and January 2011 (1); this should be 10 monts
            //    // 12 - 3 + 1 = 10
            //    // Take the 12 months of a year into account
            //    M = 12 - fromDate.Month + toDate.Month;
            //}
            //else
            //{
            //    M = toDate.Month - fromDate.Month;
            //}
            int M = System.Data.Linq.SqlClient.SqlMethods.DateDiffMonth(fromDate, toDate);
            return M;
        }

        public static ArrayList MonthlyDiff(DateTime fromDate, DateTime toDate)
        {
           ArrayList dates=new ArrayList(100);
           //DateTime[] MyArray = new DateTime[6];
           while (fromDate.Date < toDate.Date)
            {
                
                dates.Add(fromDate.ToShortDateString());
               fromDate=fromDate.AddMonths(1);
            }
           dates.Add(toDate.ToShortDateString());
            //if (fromDate.Date >= toDate.Date)
            //{
            //    dates.Add(toDate.ToShortDateString());
            //}
            return dates;
        }
        public static ArrayList QtrlyDiff(DateTime fromDate, DateTime toDate)
        {
            ArrayList dates = new ArrayList(100);
            //DateTime[] MyArray = new DateTime[6];
            DateTime dateT = fromDate.AddMonths(3);
            int M = System.Data.Linq.SqlClient.SqlMethods.DateDiffMonth(fromDate, toDate);
            while (M >3)
            {
                if (M < 2)
                {
                    dates.Add(toDate.ToShortDateString());
                }
                else
                {
                    fromDate = fromDate.AddMonths(3);
                    dates.Add(fromDate.ToShortDateString());
                }
               M = M - 3;
            }
            if (M<=3)
            {
                dates.Add(toDate.ToShortDateString());
            }
            
            return dates;
        }




    }
}
