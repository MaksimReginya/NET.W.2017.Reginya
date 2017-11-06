using System;

namespace BankAccountLogic.AccountTypes
{
    public class GoldBankAccount: BankAccount
    {
        #region Overrided properties of class BankAccount        

        protected override int BalanceCost => 6;
        protected override int TransactionCost => 3;

        #endregion

        #region Public constructors
        
        public GoldBankAccount(string accountNumber, string ownerFirstName, string ownerLastName,
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
