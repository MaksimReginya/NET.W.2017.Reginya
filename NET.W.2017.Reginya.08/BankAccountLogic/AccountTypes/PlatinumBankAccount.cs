using System;

namespace BankAccountLogic.AccountTypes
{
    public class PlatinumBankAccount: BankAccount
    {
        #region Constants

        private const int ReplenishBonus = 10;
        private const int WithdrawBonus = -5;

        #endregion

        #region Public constructors

        public PlatinumBankAccount(string accountNumber, string ownerFirstName, string ownerLastName,
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
