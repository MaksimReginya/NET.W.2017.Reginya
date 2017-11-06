using System;

namespace BankAccountLogic.AccountTypes
{
    public class PlatinumBankAccount: BankAccount
    {
        #region Overrided properties of class BankAccount        

        protected override int BalanceCost => 10;
        protected override int TransactionCost => 5;

        #endregion

        #region Public constructors

        public PlatinumBankAccount(string accountNumber, string ownerFirstName, string ownerLastName,
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
