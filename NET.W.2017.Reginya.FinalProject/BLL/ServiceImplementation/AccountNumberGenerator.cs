using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.Interface.Entities;
using BLL.Interface.ServiceInterface;

namespace BLL.ServiceImplementation
{
    /// <inheritdoc />
    public class AccountNumberGenerator : IAccountNumberGenerator
    {
        #region Private constants
        
        private const string Template = "00000aaaa00000000000";

        #endregion

        #region IAccountNumberGenerator implementation
        
        /// <inheritdoc />
        public string CreateNumber(IEnumerable<BankAccount> accounts)
        {            
            var numbers = GetAccountsNumbers(accounts) as List<string>;
            if (numbers == null || numbers.Count == 0)
            {
                return Template;
            }

            var biggestNumber = numbers[numbers.Count - 1];
            return GetNext(biggestNumber);
        }

        #endregion

        #region Private methods
                
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
            var numbers = accounts.Select(account => account.AccountNumber).ToList();
            numbers.Sort();
            return numbers;
        }

        #endregion
    }
}
