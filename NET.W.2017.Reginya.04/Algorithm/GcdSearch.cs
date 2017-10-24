using System;
using System.Diagnostics;

namespace Algorithm
{
    /// <summary>
    /// Calculates greatest common divisor of numbers in array by different methods
    /// (Stein's and Euclidean algorithms)
    /// </summary>
    public static class GcdSearch
    {
        #region Public methods GdcSearch        
        /// <summary>
        /// Finds the greatest common divisor of numbers array by the Euclidean algorithm
        /// </summary>
        /// <param name="numbers">Array of numbers to find gcd</param>
        /// <param name="operationTime">Time spent on the operation</param>    
        /// <exception cref="ArgumentNullException">
        /// Throws if numbers array is null
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Throws if numbers array length is smaller than 2
        /// </exception>
        /// <returns> The greatest common divisor of numbers array </returns>
        public static int EuclideanAlgorithm(out TimeSpan operationTime, params int[] numbers)
        {
            if (numbers == null)
                throw new ArgumentNullException(nameof(numbers));

            if (numbers.Length < 2)
                throw new ArgumentException(nameof(numbers));

            var watch = new Stopwatch();
            watch.Start();

            int neighboorsGcd = GetEuclideanGcdFor2Numbers(numbers[0], numbers[1]);
            for (int i = 2; i < numbers.Length; i++)
            {
                neighboorsGcd = GetEuclideanGcdFor2Numbers(neighboorsGcd, numbers[i]);
            }

            watch.Stop();
            operationTime = watch.Elapsed;

            return neighboorsGcd;            
        }

        /// <summary>
        /// Finds the greatest common divisor of two numbers by the Euclidean algorithm
        /// </summary>
        /// <param name="num1">One of numbers to find gcd</param>
        /// <param name="num2">One of numbers to find gcd</param>  
        /// <param name="operationTime">Time spent on the operation</param>      
        /// <returns> The greatest common divisor of two numbers </returns>
        public static int EuclideanAlgorithm(out TimeSpan operationTime, int num1, int num2)
        {
            var watch = new Stopwatch();
            watch.Start();
            
            int result = GetEuclideanGcdFor2Numbers(num1, num2);

            watch.Stop();
            operationTime = watch.Elapsed;

            return result;
        }

        /// <summary>
        /// Finds the greatest common divisor of three numbers by the Euclidean algorithm
        /// </summary>
        /// <param name="num1">One of numbers to find gcd</param>
        /// <param name="num2">One of numbers to find gcd</param>
        /// <param name="num3">One of numbers to find gcd</param>
        /// <param name="operationTime">Time spent on the operation</param>
        /// <returns> The greatest common divisor of three numbers </returns>
        public static int EuclideanAlgorithm(out TimeSpan operationTime, int num1, int num2, int num3)
        {
            var watch = new Stopwatch();
            watch.Start();

            int temp = GetEuclideanGcdFor2Numbers(num1, num2);
            int result = GetEuclideanGcdFor2Numbers(temp, num3);

            watch.Stop();
            operationTime = watch.Elapsed;

            return result;
        }
        
       /* /// <summary>
        /// Finds the greatest common divisor of numbers array by the Stein's algorithm
        /// </summary>
        /// <param name="numbers">Array of numbers to find gcd</param>
        /// <returns> The greatest common divisor of numbers array </returns>
        public static int SteinsAlgorithm(params int[] numbers)
        {

        }*/
        #endregion

        #region Private methods GdcSearch        
        private static int GetEuclideanGcdFor2Numbers(int num1, int num2)
        {
            if (num1 == 0)
                return num2;

            if (num2 == 0)
                return num1;

            num1 = Math.Abs(num1);
            num2 = Math.Abs(num2);

            while (num1 != num2)
            {
                if (num1 > num2)
                    num1 -= num2;
                else
                    num2 -= num1;
            }
            return num1;
        }        
        #endregion
    }
}
