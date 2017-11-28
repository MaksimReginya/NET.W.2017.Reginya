using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4.Solution
{
    public static class AveragingMethodEx
    {
        public static double CalculateAverage(this List<double> values, IAveragingMethod averagingMethod)
            => averagingMethod.CalculateAverageValue(values);
    }
}
