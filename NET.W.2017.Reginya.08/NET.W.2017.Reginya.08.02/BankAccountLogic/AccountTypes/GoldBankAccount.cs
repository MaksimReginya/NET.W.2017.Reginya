using System;

namespace BankAccountLogic.AccountTypes
{
    /// <inheritdoc />
    /// <summary>
    /// Gold type of bank account
    /// </summary>
    public class GoldBankAccount : BankAccount
    {
        #region Public constructors

        /// <inheritdoc />  
        public GoldBankAccount(string accountNumber, string ownerFirstName, string ownerLastName, decimal balance = 0m, int bonus = 0)
            : base(accountNumber, ownerFirstName, ownerLastName, balance, bonus)
        {            
        }

        #endregion

        #region Overrided properties of class BankAccount        

        /// <inheritdoc />
        protected override decimal MinBalance => -50;                

        /// <inheritdoc />
        protected override int BalanceCost => 6;

        /// <inheritdoc />
        protected override int TransactionCost => 3;

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
