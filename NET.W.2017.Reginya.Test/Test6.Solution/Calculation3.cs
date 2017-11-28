namespace Test6.Solution
{
    public class Calculation3 : IElementCalculation<double>
    {
        public double CalculateNextElement(double previous, double current)
            => current + (previous / current);
    }
}
