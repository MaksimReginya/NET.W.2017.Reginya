using System;
using System.Collections.Generic;

namespace Test6.Solution
{
    public static class Generator<T>
    {
        public static IEnumerable<T> GenerateSequence(T first, T second, int count, Func<T, T, T> elementCalculator)
        {
            VerifyInput(first, second, count, elementCalculator);

            return count == 0 ? new T[0] : Generate(first, second, count, elementCalculator);
        }

        public static IEnumerable<T> GenerateSequence(T first, T second, int count, IElementCalculation<T> elementCalculator)
        {
            if (ReferenceEquals(elementCalculator, null))
            {
                throw new ArgumentNullException(nameof(elementCalculator));
            }

            return GenerateSequence(first, second, count, elementCalculator.CalculateNextElement);
        }

        private static IEnumerable<T> Generate(T first, T second, int count, Func<T, T, T> elementCalculator)
        {
            if (count >= 1)
            {
                yield return first;
            }

            if (count >= 2)
            {
                yield return second;
            }

            for (int i = 3; i <= count; i++)
            {
                var temp = second;
                second = elementCalculator(first, second);
                yield return second;
                first = temp;
            }
        }

        private static void VerifyInput(T first, T second, int count, Func<T, T, T> elementCalculator)
        {
            if (count < 0)
            {
                throw new ArgumentException($"{nameof(count)} must be positive.", nameof(count));
            }

            if (ReferenceEquals(first, null))
            {
                throw new ArgumentNullException(nameof(first));
            }

            if (ReferenceEquals(second, null))
            {
                throw new ArgumentNullException(nameof(second));
            }

            if (ReferenceEquals(elementCalculator, null))
            {
                throw new ArgumentNullException(nameof(elementCalculator));
            }
        }
    }
}
