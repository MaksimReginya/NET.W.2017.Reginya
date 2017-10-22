using System;
using System.Collections;

namespace Algorithm
{
    public class Algorithm
    {
        /// <summary>
        /// Inserts first number bits from the jth to the ith position into second number
        /// so that the bits of second number occupy positions from bit j to bit i. </summary>        
        /// <param name="number1">First number</param>
        /// <param name="number2">Second number</param>
        /// <param name="i">Start position</param>
        /// <param name="j">End position</param>
        /// <exception cref="ArgumentException">
        /// Throws when i is greater than j or
        /// (i &lt; 0) || (i &gt; 31) || (j &lt; 0) || (j &gt; 31) or
        /// (number1 &lt; 0) || (number2 &lt; 0) </exception>        
        /// <returns> 
        /// Result of bit insertion </returns>
        public static int InsertNumber(int number1, int number2, int i, int j)
        {
            if (i > j)
            {
                throw new ArgumentException(nameof(i) + " must be less than " + nameof(j));
            }

            if (i < 0 || i > 31 || j < 0 || j > 31)
            {
                throw new ArgumentException(nameof(i) + " and " + nameof(j) + " must be greater than 0 and less than 32");
            }

            if (number1 < 0 || number2 < 0)
            {
                throw new ArgumentException(nameof(number1) + " and " + nameof(number2) + " must not be negative");
            }

            var number1BitArray = new BitArray(new[] { number1 });
            var number2BitArray = new BitArray(new[] { number2 });

            int l = 0;
            for (int k = i; k <= j; k++)
                number1BitArray.Set(k, number2BitArray.Get(l++));

            // Convert bit array to single int
            var result = new int[1];            
            number1BitArray.CopyTo(result, 0);
            return result[0];
        }
        
    }
}
