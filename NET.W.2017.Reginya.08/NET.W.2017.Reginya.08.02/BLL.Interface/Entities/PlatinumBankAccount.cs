﻿using System;

namespace BLL.Interface.Entities
{
    /// <inheritdoc />
    /// <summary>
    /// Platinum type of bank account
    /// </summary>
    public class PlatinumBankAccount : BankAccount
    {
        #region Public constructors

        /// <inheritdoc />  
        public PlatinumBankAccount(
            string accountNumber,
            string ownerFirstName,
            string ownerLastName,
            string ownerEmail,
            decimal balance = 0m,
            int bonus = 0)
            : base(accountNumber, ownerFirstName, ownerLastName, ownerEmail, balance, bonus)
        {            
        }

        #endregion

        #region Overrided properties of class BankAccount        

        /// <inheritdoc />
        protected override decimal MinBalance => -100;

        /// <inheritdoc />
        protected override int BalanceCost => 10;

        /// <inheritdoc />
        protected override int TransactionCost => 5;

        #endregion
        
        #region Public methods

        /// <inheritdoc />                        
        public override int CalculateBonus(decimal transactionValue)
        {
            return (int)Math.Ceiling((Balance * this.BalanceCost / 100) + (transactionValue * this.TransactionCost / 100));
        }

        #endregion
    }
}
