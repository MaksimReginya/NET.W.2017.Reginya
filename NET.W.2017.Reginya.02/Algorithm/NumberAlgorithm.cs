using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Algorithm
{
    /// <summary>
    /// Provides methods to make manipulations with numbers </summary>
    public class NumberAlgorithm
    {
        #region Public Methods  
        
        /// <summary>
        /// Inserts first number bits from the j to the i position into second number
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
                throw new ArgumentException(nameof(i) + " and " + nameof(j) + " can be not negative and less than 32");
            }

            if (number1 < 0 || number2 < 0)
            {
                throw new ArgumentException(nameof(number1) + " and " + nameof(number2) + " can not be negative");
            }

            int mask = (2 << (j - i)) - 1;
            mask <<= i;
            number2 = (number2 << i) & mask;
            number1 = number1 & ~mask;
            return number1 | number2;
        }

        /// <summary>
        /// Finds the nearest largest integer that consists of digits of the original number. </summary>
        /// <param name="source">Source number</param>       
        /// <exception cref="ArgumentException">Throws if source number is not positive</exception>
        /// <returns>
        /// Nearest largest integer consisting of digits of the original number.
        /// Or -1 if a required number does not exist. </returns>        
        public static int FindNextBiggerNumber(int source)
        {
            if (source < 1)
            {
                throw new ArgumentException(nameof(source) + " must be positive");
            }

            var numerals = source.ToString().ToCharArray();

            if (IsDescendingOrder(numerals))
            {
                return -1;
            }

            var list = new List<char>();
            int i;
            for (i = numerals.Length - 1; i > 0; i--)
            {
                if (numerals[i] > numerals[i - 1])
                {
                    list.Add(numerals[i - 1]);

                    char temp = numerals[i];
                    numerals[i] = numerals[i - 1];
                    numerals[i - 1] = temp;
                    
                    break;
                }
                
                list.Add(numerals[i]);
            }

            if (i == 0)
            {
                return -1;
            }

            list.Sort();
            foreach (var item in list)
            {
                numerals[i] = item;
                i++;
            }

            if (!int.TryParse(new string(numerals), out int res))
            {
                return source;
            }

            return res;
        }
        
        /// <summary>
        /// Finds the nearest largest integer that consists of digits of the original number. </summary>
        /// <param name="source">Source number</param>    
        /// <param name="operationTime">Time spent on the operation</param>          
        /// <exception cref="ArgumentException">Throws if source number is not positive</exception>
        /// <returns>
        /// Nearest largest integer consisting of digits of the original number.
        /// Or -1 if a required number does not exist. </returns>        
        public static int FindNextBiggerNumber(int source, out TimeSpan operationTime)
        {
            var watch = new Stopwatch();
            watch.Start();

            int result = FindNextBiggerNumber(source);

            watch.Stop();
            operationTime = watch.Elapsed;

            return result;            
        }
       
        /// <summary>
        /// Calculates the root of the nth power from the number by the Newton method with a given accuracy. </summary>
        /// <param name="number">Source number</param>
        /// <param name="power">Power of root</param>
        /// <param name="accuracy">Accuracy</param>
        /// <exception cref="ArgumentException">
        /// Throws if power or accuracy is not positive, or
        /// number equals zero, or
        /// number is negative and power is even </exception>
        /// <returns>
        /// The root of the nth power from the number</returns>
        public static double FindNthRoot(double number, int power, double accuracy = 0.001)
        {
            if (power <= 0)
            {
                throw new ArgumentException(nameof(power) + " must be positive");
            }

            if (number < 0 && power % 2 == 0)
            { 
                throw new ArgumentException("The root of even degree from negative number is not defined.");
            }

            if (number.Equals(0))
            {
                throw new ArgumentException(nameof(number) + " can not be zero");
            }

            if (accuracy <= 0)
            {
                throw new ArgumentException(nameof(accuracy) + " must be positive");
            }

            double x0 = number / power;
            double x1 = GetNextNumber(number, power, x0);
            double delta = accuracy * 2;

            while (delta > accuracy)
            {
                x0 = x1;
                x1 = GetNextNumber(number, power, x0);
                delta = Math.Abs(x0 - x1);
            }

            return x1;
        }
        
        #endregion

        #region Private Methods

        private static double GetNextNumber(double number, int power, double x0) =>
            (1.0 / power) * (((power - 1) * x0) + (number / Math.Pow(x0, power - 1)));        

        private static bool IsDescendingOrder(char[] array)
        {
            bool result = true;
            for (int i = array.Length - 1; i > 0; i--)
            {
                if (array[i] > array[i - 1])
                {
                    result = false;
                    break;
                }
            }

            return result;
        } 
        
        #endregion
    }    
}
