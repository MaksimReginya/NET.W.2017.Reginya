using System;
using NUnit.Framework;
using Algorithm;

namespace Algorithm.NUnitTests
{
    [TestFixture]
    public class GcdSearchNUnitTests
    {        
        [TestCase(8, 15, 0, 1, 2, ExpectedResult = 1)]
        [TestCase(-8, 18, 6, 32, 2, 8, ExpectedResult = 2)]
        [TestCase(-9, 27, 0, 81, 9, -81, -27, ExpectedResult = 9)]
        public int EuclideanAlgorithmTest(params int[] numbers)
        {            
            return GcdSearch.EuclideanAlgorithm(out var time, numbers);
        }

        [TestCase]
        [TestCase(1)]
        public void EuclideanAlgorithm_ThrowsArgumentException(params int[] numbers)
        {
            Assert.Throws<ArgumentException>(
                () => GcdSearch.EuclideanAlgorithm(out var time, numbers));
        }

        [TestCase(15, 15, ExpectedResult = 15)]
        [TestCase(5, 0, ExpectedResult = 5)]
        [TestCase(0, 15, ExpectedResult = 15)]
        [TestCase(-5, 10, ExpectedResult = 5)]
        public int EuclideanAlgorithmTest(int num1, int num2)
        {
            return GcdSearch.EuclideanAlgorithm(out var time, num1, num2);
        }

        [TestCase(25, 15, 10, ExpectedResult = 5)]
        [TestCase(5, 0, 100, ExpectedResult = 5)]
        [TestCase(-50, 100, 200, ExpectedResult = 50)]
        [TestCase(99, 11, 121, ExpectedResult = 11)]
        public int EuclideanAlgorithmTest(int num1, int num2, int num3)
        {
            return GcdSearch.EuclideanAlgorithm(out var time, num1, num2, num3);
        }

        [TestCase(8, 15, 0, 1, 2, ExpectedResult = 1)]
        [TestCase(-8, 18, 6, 32, 2, 8, ExpectedResult = 2)]
        [TestCase(-9, 27, 0, 81, 9, -81, -27, ExpectedResult = 9)]
        public int SteinsAlgorithmTest(params int[] numbers)
        {
            return GcdSearch.SteinsAlgorithm(out var time, numbers);
        }

        [TestCase]
        [TestCase(1)]
        public void SteinsAlgorithm_ThrowsArgumentException(params int[] numbers)
        {
            Assert.Throws<ArgumentException>(
                () => GcdSearch.SteinsAlgorithm(out var time, numbers));
        }

        [TestCase(15, 15, ExpectedResult = 15)]
        [TestCase(5, 0, ExpectedResult = 5)]
        [TestCase(0, 15, ExpectedResult = 15)]
        [TestCase(-5, 10, ExpectedResult = 5)]
        public int SteinsAlgorithmTest(int num1, int num2)
        {
            return GcdSearch.SteinsAlgorithm(out var time, num1, num2);
        }

        [TestCase(25, 15, 10, ExpectedResult = 5)]
        [TestCase(5, 0, 100, ExpectedResult = 5)]
        [TestCase(-50, 100, 200, ExpectedResult = 50)]
        [TestCase(99, 11, 121, ExpectedResult = 11)]
        public int SteinsAlgorithmTest(int num1, int num2, int num3)
        {
            return GcdSearch.SteinsAlgorithm(out var time, num1, num2, num3);
        }
    }
}
