using System;
using System.Collections.Generic;

using BankAccountLogic.AccountTypes;

namespace BankAccountLogic
{
    /// <summary>
    /// Interface to work with accounts storage
    /// </summary>
    public interface IAccountsStorage
    {
        /// <summary>
        /// Saves accounts in specified storage.
        /// </summary>
        /// <param name="accounts"> Accounts to save. </param>
        void Save(IEnumerable<BankAccount> accounts);

        /// <summary>
        /// Loads accounts from a specified storage.
        /// </summary>
        /// <returns> Loaded accounts. </returns>        
        IEnumerable<BankAccount> Load();
    }
}
