using System;

namespace BankAccountLogic
{
    /// <summary>
    /// Provides methods to calculate bonus of account
    /// </summary>
    internal static class BonusLogic
    {
        /// <summary>
        /// Calculate bonus of account
        /// </summary>
        /// <param name="balance">Balance of account</param>
        /// <param name="transactionValue">Transaction value</param>
        /// <param name="balanceCost">Balance "cost"</param>
        /// <param name="transactionCost">Transaction "cost"</param>
        /// <returns></returns>
        internal static decimal CalculateBonus(decimal balance, decimal transactionValue,
            int balanceCost, int transactionCost)
        {
            return balance * balanceCost / 100 + transactionValue * transactionCost / 100;
        }
    }
}
