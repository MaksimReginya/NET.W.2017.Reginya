using System;

namespace BLL.Interface.Entities
{
    /// <inheritdoc />
    /// <summary>
    /// Base type of bank account
    /// </summary>
    public class BaseBankAccount : BankAccount
    {
        #region Public constructors

        /// <inheritdoc />  
        public BaseBankAccount(string accountNumber, string ownerFirstName, string ownerLastName, decimal balance = 0m, int bonus = 0)
            : base(accountNumber, ownerFirstName, ownerLastName, balance, bonus)
        {
        }

        #endregion

        #region Overrided properties of class BankAccount        

        /// <inheritdoc />
        protected override decimal MinBalance => 0;
                
        /// <inheritdoc />
        protected override int BalanceCost => 3;
        
        /// <inheritdoc />
        protected override int TransactionCost => 1;

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
