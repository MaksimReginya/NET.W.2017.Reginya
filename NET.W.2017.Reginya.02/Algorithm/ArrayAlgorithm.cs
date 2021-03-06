﻿using System;
using System.Linq;

namespace Algorithm
{
    /// <summary>
    /// Provides methods to make manipulations with number arrays </summary>
    public class ArrayAlgorithm
    {
        #region PublicMethods

        /// <summary>
        /// Filters the array, so that only numbers satisfying the predicate remain on the output. </summary>
        /// <param name="predicate">Predicate to filter array</param>
        /// <param name="numbers">Source numbers</param>        
        /// <exception cref="ArgumentNullException">
        /// Throws when predicate or numbers are null </exception>        
        /// <returns>
        /// Numbers satisfying the predicate or empty array if there are no such numbers. </returns>
        public static int[] FilterDigit(IPredicate<int> predicate, params int[] numbers)
        {
            if (predicate != null)
            {
                return FilterDigit(predicate.IsSuitable, numbers);
            }

            throw new ArgumentNullException(nameof(predicate));
        }

        /// <summary>
        /// Filters the array, so that only numbers satisfying the predicate remain on the output. </summary>
        /// <param name="predicate">Predicate to filter array</param>
        /// <param name="numbers">Source numbers</param>        
        /// <exception cref="ArgumentNullException">
        /// Throws when predicate or numbers are null </exception>        
        /// <returns>
        /// Numbers satisfying the predicate or empty array if there are no such numbers. </returns>
        public static int[] FilterDigit(Predicate<int> predicate, params int[] numbers)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            if (numbers == null)
            {
                throw new ArgumentNullException(nameof(numbers));
            }

            if (numbers.Length == 0)
            {
                return new int[0];
            }

            return numbers.Where(number => predicate(number)).ToArray();
        }

        #endregion
    }
}
