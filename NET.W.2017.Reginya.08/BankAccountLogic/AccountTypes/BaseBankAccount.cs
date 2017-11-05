using System;

namespace BankAccountLogic.AccountTypes
{
    public class BaseBankAccount : BankAccount
    {
        #region Constants
                
        private const int ReplenishBonus = 2;
        private const int WithdrawBonus = -1;

        #endregion

        #region Public constructors

        public BaseBankAccount(string accountNumber, string ownerFirstName, string ownerLastName,
            double balance = 0d, double bonus = 0d)
        {
            AccountNumber = accountNumber;
            OwnerFirstName = ownerFirstName;
            OwnerLastName = ownerLastName;
            Balance = balance;
            Bonus = bonus;
        }

        #endregion

        #region Overrided methods of class BankAccount

        protected override void DecreaseBonus()
        {
            if (Bonus + WithdrawBonus < 0)
                Bonus = 0;
            else
                Bonus += WithdrawBonus;
        }

        protected override void IncreaseBonus()
        {
            if (Bonus >= double.MaxValue - ReplenishBonus)
                Bonus = double.MaxValue;
            else
                Bonus += ReplenishBonus;
        }

        #endregion
    }
}
