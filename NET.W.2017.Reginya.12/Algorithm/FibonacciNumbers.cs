using System;
using System.Collections.Generic;
using System.Numerics;

namespace Algorithm
{
    /// <summary>
    /// Provides method of calculating array of Fibonacci numbers
    /// </summary>
    public class FibonacciNumbers
    {
        /// <summary>
        /// Gets row of the specified length of Fibonacci numbers
        /// </summary>
        /// <param name="count">Length of the row of Fibonacci numbers</param>
        /// <returns>The array of Fibonacci numbers</returns>
        /// <exception cref="ArgumentException">
        /// Thrown if length is negative.
        /// </exception>
        public static IEnumerable<BigInteger> GetNumbers(int count)
        {
            if (count < 0)
            {
                throw new ArgumentException(nameof(count) + " must not be negative.");
            }

            return FibonacciCore(count);

            IEnumerable<BigInteger> FibonacciCore(int length)
            {
                BigInteger num1 = 1, num2 = 1;
                for (int i = 0; i < length; i++)
                {
                    yield return num1;
                    var temp = num2;
                    num2 += num1;
                    num1 = temp;
                }
            }            
        }
    }
}
