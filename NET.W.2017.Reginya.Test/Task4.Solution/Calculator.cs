using System;
using System.Collections.Generic;
using System.Linq;
using Task4.Solution;

namespace Task4.Solution
{
    public class Calculator
    {
        public double CalculateAverage(List<double> values, Func<List<double>, double> averagingMethod)
        {
            if (values == null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            return averagingMethod(values);
        }

        public double CalculateAverage(List<double> values, IAveragingMethod averagingMethod)
            => CalculateAverage(values, averagingMethod.CalculateAverageValue);
    }
}
