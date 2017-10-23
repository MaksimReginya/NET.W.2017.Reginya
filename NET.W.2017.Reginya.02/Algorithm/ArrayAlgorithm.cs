using System;
using System.Collections.Generic;

namespace Algorithm
{
    /// <summary>
    /// Provides methods to make manipulations with number arrays </summary>
    public class ArrayAlgorithm
    {
        #region PublicMethods
        /// <summary>
        /// Filters the array, so that only numbers containing the given digit remain on the output. </summary>
        /// <param name="digit">Target digit</param>
        /// <param name="numbers">Source numbers</param>        
        /// <exception cref="ArgumentException">
        /// Throws when (digit &lt; 0) || (digit &gt; 9) </exception>        
        /// <returns>
        /// Numbers containing a given digit or null if there are no such numbers. </returns>
        public static int[] FilterDigit(int digit, params int[] numbers)
        {
            if (digit < 0 || digit > 9)
                throw new ArgumentException(nameof(digit) + " must be from 0 to 9");

            if (numbers.Length == 0)
                return null;

            var result = new List<int>();
            var digitStr = digit.ToString();
            foreach (var value in numbers)
            {
                if (value.ToString().Contains(digitStr))
                    result.Add(value);
            }            

            return result.ToArray();
        }
        #endregion
    }
}
