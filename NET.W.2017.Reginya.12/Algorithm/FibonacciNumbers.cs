using System;

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
        /// <param name="length">Length of the row of Fibonacci numbers</param>
        /// <returns>The array of Fibonacci numbers</returns>
        /// <exception cref="ArgumentException">
        /// Thrown if length is negative.
        /// </exception>
        public static int[] GetRow(int length)
        {
            if (length < 0)
            {
                throw new ArgumentException(nameof(length) + " must not be negative.");
            }

            if (length == 0)
            {
                return new int[] { };
            }

            if (length == 1)
            {
                return new[] { 0 };
            }

            var array = new int[length];
            array[0] = 0;
            array[1] = 1;
            if (length > 2)
            {
                for (int i = 2; i < length; i++)
                {
                    array[i] = array[i - 1] + array[i - 2];
                }
            }

            return array;
        }
    }
}
