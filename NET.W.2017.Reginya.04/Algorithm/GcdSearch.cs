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
        #region Public methods Euclidean algorithm        
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
        public static int EuclideanAlgorithm(out long operationTime, params int[] numbers)
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
            operationTime = watch.Elapsed.Ticks;

            return neighboorsGcd;            
        }

        /// <summary>
        /// Finds the greatest common divisor of two numbers by the Euclidean algorithm
        /// </summary>
        /// <param name="num1">One of numbers to find gcd</param>
        /// <param name="num2">One of numbers to find gcd</param>  
        /// <param name="operationTime">Time spent on the operation</param>      
        /// <returns> The greatest common divisor of two numbers </returns>
        public static int EuclideanAlgorithm(out long operationTime, int num1, int num2)
        {
            var watch = new Stopwatch();
            watch.Start();
            
            int result = GetEuclideanGcdFor2Numbers(num1, num2);

            watch.Stop();
            operationTime = watch.Elapsed.Ticks;

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
        public static int EuclideanAlgorithm(out long operationTime, int num1, int num2, int num3)
        {
            var watch = new Stopwatch();
            watch.Start();

            int temp = GetEuclideanGcdFor2Numbers(num1, num2);
            int result = GetEuclideanGcdFor2Numbers(temp, num3);

            watch.Stop();
            operationTime = watch.Elapsed.Ticks;

            return result;
        }
        #endregion

        #region Public methods Stein's algorithm        
        /// <summary>
        /// Finds the greatest common divisor of numbers array by the Stein's algorithm
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
        public static int SteinsAlgorithm(out long operationTime, params int[] numbers)
        {
            if (numbers == null)
                throw new ArgumentNullException(nameof(numbers));

            if (numbers.Length < 2)
                throw new ArgumentException(nameof(numbers));

            var watch = new Stopwatch();
            watch.Start();

            int neighboorsGcd = GetSteinsGcdFor2Numbers(numbers[0], numbers[1]);
            for (int i = 2; i < numbers.Length; i++)
            {
                neighboorsGcd = GetSteinsGcdFor2Numbers(neighboorsGcd, numbers[i]);
            }

            watch.Stop();
            operationTime = watch.Elapsed.Ticks;

            return neighboorsGcd;
        }

        /// <summary>
        /// Finds the greatest common divisor of two numbers by the Stein's algorithm
        /// </summary>
        /// <param name="num1">One of numbers to find gcd</param>
        /// <param name="num2">One of numbers to find gcd</param>  
        /// <param name="operationTime">Time spent on the operation</param>      
        /// <returns> The greatest common divisor of two numbers </returns>
        public static int SteinsAlgorithm(out long operationTime, int num1, int num2)
        {
            var watch = new Stopwatch();
            watch.Start();

            int result = GetSteinsGcdFor2Numbers(num1, num2);

            watch.Stop();
            operationTime = watch.Elapsed.Ticks;

            return result;
        }

        /// <summary>
        /// Finds the greatest common divisor of three numbers by the Stein's algorithm
        /// </summary>
        /// <param name="num1">One of numbers to find gcd</param>
        /// <param name="num2">One of numbers to find gcd</param>
        /// <param name="num3">One of numbers to find gcd</param>
        /// <param name="operationTime">Time spent on the operation</param>
        /// <returns> The greatest common divisor of three numbers </returns>
        public static int SteinsAlgorithm(out long operationTime, int num1, int num2, int num3)
        {
            var watch = new Stopwatch();
            watch.Start();

            int temp = GetSteinsGcdFor2Numbers(num1, num2);
            int result = GetSteinsGcdFor2Numbers(temp, num3);

            watch.Stop();
            operationTime = watch.Elapsed.Ticks;

            return result;
        }
        #endregion

        #region Private methods Euclidean algorithm               
        private static int GetEuclideanGcdFor2Numbers(int num1, int num2)
        {
            if (num1 == 0 || num2 == 1)
                return num2;

            if (num2 == 0 || num1 == 1)
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

        #region Private methods Stein's algorithm       
        private static int GetSteinsGcdFor2Numbers(int num1, int num2)
        {
            num1 = Math.Abs(num1);
            num2 = Math.Abs(num2);

            int GetNextNum(int number1, int number2)
            {
                if (number1 == number2)
                    return number1;

                if (number2 == 0 || number1 == 1)
                    return number1;

                if (number1 == 0 || number2 == 1)
                    return number2;

                if ((number1 & 1) == 0)
                {
                    if ((number2 & 1) != 0)
                        return GetNextNum(number1 >> 1, number2);
                    else
                        return GetNextNum(number1 >> 1, number2 >> 1) << 1;
                }
                if ((number2 & 1) == 0)
                    return GetNextNum(number1, number2 >> 1);
                if (number1 > number2)
                    return GetNextNum((number1 - number2) >> 1, number2);
                return GetNextNum((number2 - number1) >> 1, number1);
            }

            return GetNextNum(num1, num2);
        }
        #endregion
    }
}
