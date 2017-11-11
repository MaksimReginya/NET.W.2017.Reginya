using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using BankAccountLogic.AccountTypes;

namespace BankAccountLogic
{
    /// <summary>
    /// Generates unique account number
    /// </summary>
    public static class AccountNumberGenerator
    {
        private const string Template = "00000aaaa00000000000";

        /// <summary>
        /// Performs generation of number
        /// </summary>
        /// <param name="storage">Storage of existing accounts</param>
        /// <returns>Generated number</returns>
        public static string GenerateNumber(IStorage storage)
        {
            var accounts = storage.GetAllAccounts();
            var numbers = GetAccountsNumbers(accounts) as List<string>;

            if (numbers == null || numbers.Count == 0)
            {
                return Template;
            }

            var biggestNumber = numbers[numbers.Count - 1];

            return GetNext(biggestNumber);
        }

        private static string GetNext(string number)
        {             
            string lastPartStr = number.Substring(9, 11);
            long lastPartInt = long.Parse(lastPartStr);            
            string resultStr = (lastPartInt + 1).ToString();
            if (resultStr.Length == 11)
            {
                return number.Replace(lastPartStr, resultStr);
            }

            var stringBuilder = new StringBuilder(resultStr);
            while (stringBuilder.Length < 11)
            {
                stringBuilder.Insert(0, "0");
            }

            return number.Replace(lastPartStr, stringBuilder.ToString());
        }

        private static IEnumerable<string> GetAccountsNumbers(IEnumerable<BankAccount> accounts)
        {
            var numbers = new List<string>();

            foreach (var account in accounts)
            {
                numbers.Add(account.AccountNumber);
            }

            numbers.Sort();

            return numbers;
        }
    }
}
