using System.Collections.Generic;

namespace Task4.Solution
{
    public interface IAveragingMethod
    {
        double CalculateAverageValue(List<double> values);
    }
}