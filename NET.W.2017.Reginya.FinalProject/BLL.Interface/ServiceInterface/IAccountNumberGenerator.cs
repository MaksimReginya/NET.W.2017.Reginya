using System.Collections.Generic;
using BLL.Interface.Entities;

namespace BLL.Interface.ServiceInterface
{
    /// <summary>
    /// Interface of generator that creates unique account number.
    /// </summary>
    public interface IAccountNumberGenerator
    {
        /// <summary>
        /// Performs creation of account number.
        /// </summary>
        /// <param name="accounts">Existing accounts.</param>
        /// <returns>Created unique number.</returns>
        string CreateNumber(IEnumerable<BankAccount> accounts);                
    }
}
