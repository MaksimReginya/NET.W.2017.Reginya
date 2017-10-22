using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

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
                throw new ArgumentException(nameof(i) + " must be less than " + nameof(j));            

            if (i < 0 || i > 31 || j < 0 || j > 31)            
                throw new ArgumentException(nameof(i) + " and " + nameof(j) + " can be not negative and less than 32");            

            if (number1 < 0 || number2 < 0)            
                throw new ArgumentException(nameof(number1) + " and " + nameof(number2) + " can not be negative");            

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
                throw new ArgumentException(nameof(source) + " must be positive");            
            
            var numerals = source.ToString().ToCharArray();
            var list = new List<char>();
            int i = 0;
            for (i = numerals.Length - 1; i > 0; i --)
            {
                if (numerals[i] > numerals[i - 1])
                {
                    list.Add(numerals[i - 1]);

                    char temp = numerals[i];
                    numerals[i] = numerals[i - 1];
                    numerals[i - 1] = temp;    
                    
                    break;
                }
                else                
                    list.Add(numerals[i]);               
            }

            if (i == 0)            
                return -1;            

            list.Sort();
            foreach (var item in list)
            {
                numerals[i] = item;
                i++;
            }


            return Int32.Parse(new String(numerals));
        }

        /// <summary>
        /// Finds the nearest largest integer that consists of digits of the original number. </summary>
        /// <param name="source">Source number</param>    
        /// <param name="operationTime">Time spent on the operation</param>  
        /// <param name="operationTime1">Time spent on the operation</param>  
        /// <exception cref="ArgumentException">Throws if source number is not positive</exception>
        /// <returns>
        /// Nearest largest integer consisting of digits of the original number.
        /// Or -1 if a required number does not exist. </returns>        
        public static int FindNextBiggerNumber(int source, out TimeSpan operationTime, out TimeSpan operationTime1)
        {
            var watch = new Stopwatch();
            watch.Start();
            int result = FindNextBiggerNumber(source);
            watch.Stop();
            operationTime = watch.Elapsed;

            var startTime = DateTime.Now;
            result = FindNextBiggerNumber(source);
            operationTime1 = DateTime.Now - startTime;
            
            return result;
        }

        /// <summary>
        /// Filters the array, so that only numbers containing the given digit remain on the output. </summary>
        /// <param name="digit">Target digit</param>
        /// <param name="numbers">Source numbers</param>        
        /// <exception cref="ArgumentException">
        /// Throws when (digit &lt; 0) || (digit &gt; 9) </exception>
        /// <exception cref="ArgumentNullException">
        /// Throws when numbers equals null </exception>
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
            for (int i = 0; i < numbers.Length; i++)
            {                
                if (numbers[i].ToString().Contains(digitStr))                
                    result.Add(numbers[i]);
            }

            return result.ToArray();
        }

        /// <summary>
        /// Calculates the root of the nth power from the number by the Newton method with a given accuracy. </summary>
        /// <param name="number">Source number</param>
        /// <param name="power">Power of root</param>
        /// <param name="accuracy">Accuracy</param>
        /// <exception cref="ArgumentException">
        /// Throws if one of the arguments is negative </exception>
        /// <returns>
        /// The root of the nth power from the number</returns>
        public static double FindNthRoot(double number, int power, double accuracy = 0.001)
        {
            if (power <= 0)            
                throw new ArgumentException(nameof(power) + " must be positive");            

            if (number < 0 && power % 2 == 0)            
                throw new ArgumentException("The root of even degree from negative number is not defined.");            
                
            if (number.Equals(0))            
                throw new ArgumentException(nameof(number) + " can not be zero");            

            if (accuracy <= 0)            
                throw new ArgumentException(nameof(accuracy) +" must be positive");            

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

        private static double GetNextNumber(double number, int power, double x0)
        {
            return 1.0 / power * ((power - 1) * x0 + number / Math.Pow(x0, power - 1));
        }
    }
}
