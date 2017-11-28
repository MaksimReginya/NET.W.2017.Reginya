using System;
using System.Collections.Generic;

namespace Task4.Solution
{
    public static class AveragingMethodEx
    {
        public static double CalculateAverage(this List<double> values, IAveragingMethod averagingMethod)
            => Calculator.CalculateAverage(values, averagingMethod);

        public static double CalculateAverage(this List<double> values, Func<List<double>, double> averagingMethod)
            => Calculator.CalculateAverage(values, averagingMethod);
    }
}
