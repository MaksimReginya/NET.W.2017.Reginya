using System;
using NUnit.Framework;
using Algorithm;

namespace Algorithm.NUnitTests
{
    [TestFixture]
    class ArrayAlgorithmNUnitTests
    {
        [TestCase(7, 1, 2, 3, 4, 5, -6, 7, 68, 69, 70, 15, 17, ExpectedResult = new[] { 7, 70, 17 })]
        [TestCase(5, 1, 2, 3, 4, 5, 6, 7, 65, 69, 70, -15, 17, ExpectedResult = new[] { 5, 65, -15 })]
        [TestCase(1, new[] { 1, 11, 22 }, ExpectedResult = new[] { 1, 11 })]
        [TestCase(1, ExpectedResult = null)]
        public int[] FilterDigitTest(int digit, params int[] numbers)
        {
            return ArrayAlgorithm.FilterDigit(digit, numbers);
        }

        [TestCase(12, 5, 8, 999, 1212)]
        public void FilterDigit_ThrowsArgumentException(int digit, params int[] numbers)
        {
            Assert.Throws<ArgumentException>(
                () => ArrayAlgorithm.FilterDigit(digit, numbers));
        }
    }
}
