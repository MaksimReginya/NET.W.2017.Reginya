﻿using System;

namespace BankAccountLogic.AccountTypes
{
    /// <inheritdoc />
    /// <summary>
    /// Type of bank account
    /// </summary>
    public class BaseBankAccount : BankAccount
    {
        #region Overrided properties of class BankAccount        

        /// <inheritdoc />
        protected override int BalanceCost => 3;
        /// <inheritdoc />
        protected override int TransactionCost => 1;

        #endregion

        #region Public constructors

        /// <summary>        
        /// Initializes a new instance of the <see cref="BaseBankAccount"/> class.
        /// </summary>
        /// <param name="accountNumber"> Number of account</param>
        /// <param name="ownerFirstName">Owner's first name</param>
        /// <param name="ownerLastName">Owner's last name</param>
        /// <param name="balance">Balance of account</param>
        /// <param name="bonus">Bonus on account</param>
        public BaseBankAccount(string accountNumber, string ownerFirstName, string ownerLastName,
            decimal balance = 0m, decimal bonus = 0m)
        {
            AccountNumber = accountNumber;
            OwnerFirstName = ownerFirstName;
            OwnerLastName = ownerLastName;
            Balance = balance;
            Bonus = bonus;
        }

        #endregion        
    }
}
