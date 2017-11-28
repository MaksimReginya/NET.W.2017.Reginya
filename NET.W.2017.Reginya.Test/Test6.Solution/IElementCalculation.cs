namespace Test6.Solution
{
    public interface IElementCalculation<T>
    {
        T CalculateNextElement(T previous, T current);
    }
}
