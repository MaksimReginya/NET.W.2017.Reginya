namespace Test6.Solution
{
    public class Calculation1 : IElementCalculation<int>
    {
        public int CalculateNextElement(int previous, int current)
            => current + previous;
    }
}
