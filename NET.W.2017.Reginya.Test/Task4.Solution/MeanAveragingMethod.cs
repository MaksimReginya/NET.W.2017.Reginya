using System.Collections.Generic;
using System.Linq;

namespace Task4.Solution
{
    public class MeanAveragingMethod : IAveragingMethod
    {
        public double CalculateAverageValue(List<double> values)
            => values.Sum() / values.Count;        
    }
}
