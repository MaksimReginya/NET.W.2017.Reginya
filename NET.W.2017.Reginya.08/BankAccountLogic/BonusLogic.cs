using System;

namespace BankAccountLogic
{
    internal static class BonusLogic
    {
        internal static decimal CalculateBonus(decimal balance, decimal transactionValue,
            int balanceCost, int transactionCost)
        {
            return balance * balanceCost / 100 + transactionValue * transactionCost / 100;
        }
    }
}
