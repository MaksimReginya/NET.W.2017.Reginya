namespace Test6.Solution
{
    public class Calculation2 : IElementCalculation<int>
    {
        public int CalculateNextElement(int previous, int current)
            => (6 * current) - (8 * previous);
    }
}
