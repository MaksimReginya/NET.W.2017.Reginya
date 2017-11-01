using System;

namespace Algorithm
{
    /// <summary>
    /// Predicate that is true if data contains wanted digit
    /// </summary>
    public class IsDigitInNumberPredicate : IPredicate<int>
    {
        private readonly int _wantedDigit;

        /// <summary>
        /// Initializes a new instance of <see cref="IsDigitInNumberPredicate"/> class
        /// </summary>
        /// <param name="wantedDigit">Digit that will be searched in data</param>
        public IsDigitInNumberPredicate(int wantedDigit)
        {
            _wantedDigit = wantedDigit;
        }

        /// <inheritdoc cref="IPredicate{T}.IsTrue"/>
        public bool IsTrue(int data)
        {
            return data.ToString().Contains(_wantedDigit.ToString());
        }
    }
}
