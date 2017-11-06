using System;

using BankAccountLogic.AccountTypes;

namespace BankAccountLogic.Predicates
{
    /// <inheritdoc />
    /// <summary>
    /// Predicate that is true if book's title contains specified string
    /// </summary>
    public class IsNumberValid : IPredicate<BankAccount>
    {
        private readonly string _requiredNumber;

        /// <summary>
        /// Initializes a new instance of <see cref="IsNumberValid"/> class
        /// </summary>
        /// <param name="requiredNumber">The required account number</param>
        public IsNumberValid(string requiredNumber)
        {
            _requiredNumber = requiredNumber;
        }

        /// <inheritdoc />
        /// <summary>Describes the state of the predicate</summary>        
        public bool IsTrue(BankAccount account)
        {
            return account.AccountNumber.Equals(_requiredNumber);
        }
    }
}
