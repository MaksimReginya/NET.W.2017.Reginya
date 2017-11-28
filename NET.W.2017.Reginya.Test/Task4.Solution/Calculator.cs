using System;
using System.Collections.Generic;

namespace Task4.Solution
{
    public static class Calculator
    {
        public static double CalculateAverage(List<double> values, Func<List<double>, double> averagingMethod)
        {
            VerifyInput(values, averagingMethod);

            return averagingMethod(values);
        }

        public static double CalculateAverage(List<double> values, IAveragingMethod averagingMethod)
        {
            if (averagingMethod == null)
            {
                throw new ArgumentNullException(nameof(averagingMethod));
            }

            return CalculateAverage(values, averagingMethod.CalculateAverageValue);
        }

        private static void VerifyInput(List<double> values, Func<List<double>, double> averagingMethod)
        {
            if (values == null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            if (averagingMethod == null)
            {
                throw new ArgumentNullException(nameof(averagingMethod));
            }
        }
    }
}
