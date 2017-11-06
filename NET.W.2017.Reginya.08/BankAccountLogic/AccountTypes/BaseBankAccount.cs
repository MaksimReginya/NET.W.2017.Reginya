using System;

namespace BankAccountLogic.AccountTypes
{
    public class BaseBankAccount : BankAccount
    {
        #region Overrided properties of class BankAccount        

        protected override int BalanceCost => 3;
        protected override int TransactionCost => 1;

        #endregion

        #region Public constructors

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
