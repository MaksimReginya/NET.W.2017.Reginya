using System;
using NUnit.Framework;

namespace Algorithm.NUnitTests
{
    [TestFixture]
    internal class ArrayAlgorithmNUnitTests
    {
        [TestCase(7, 1, 2, 3, 4, 5, -6, 7, 68, 69, 70, 15, 17, ExpectedResult = new[] { 7, 70, 17 })]
        [TestCase(5, 1, 2, 3, 4, 5, 6, 7, 65, 69, 70, -15, 17, ExpectedResult = new[] { 5, 65, -15 })]
        [TestCase(1, new[] { 1, 11, 22 }, ExpectedResult = new[] { 1, 11 })]
        [TestCase(1, ExpectedResult = new int[] { })]
        public int[] FilterDigitTestWithInterface(int digit, params int[] numbers)
        {
            return ArrayAlgorithm.FilterDigit(new IsDigitInNumberPredicate(digit), numbers);
        }

        [TestCase(1, 2, 3, 4, 5, -6, 7, 68, 69, 70, 15, 17, ExpectedResult = new[] { 2, 4, -6, 68, 70 })]
        [TestCase(int.MinValue, 2, 3, 4, 5, 6, 7, 65, 69, 70, -15, int.MaxValue, ExpectedResult = new[] { int.MinValue, 2, 4, 6, 70 })]
        [TestCase(new[] { 1, 11, 22 }, ExpectedResult = new[] { 22 })]
        [TestCase(ExpectedResult = new int[] { })]
        public int[] FilterDigitTestOnlyEven(params int[] numbers)
        {
            return ArrayAlgorithm.FilterDigit(el => el % 2 == 0, numbers);
        }

        public void FilterDigit_ThrowsArgumentException(int digit)
        {
            Assert.Throws<ArgumentException>(
                () => ArrayAlgorithm.FilterDigit(new IsDigitInNumberPredicate(digit), null));
        }
    }
}
