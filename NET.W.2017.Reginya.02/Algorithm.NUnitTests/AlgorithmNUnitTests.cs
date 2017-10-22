using System;
using NUnit.Framework;
using Algorithm;

namespace Algorithm.NUnitTests
{
    [TestFixture]
    public class AlgorithmNUnitTests
    {
        [TestCase(15, 15, 0, 0, ExpectedResult = 15)]
        [TestCase(8, 15, 0, 0, ExpectedResult = 9)]
        [TestCase(8, 15, 3, 8, ExpectedResult = 120)]
        [TestCase(8, 15, 3, -8, ExpectedResult = -1)]                
        public int InsertNumberTest(int number1, int number2, int i, int j)
        {
            try
            {
                return Algorithm.InsertNumber(number1, number2, i, j);
            }
            catch (ArgumentException)
            {
                return -1;
            }
        }

        [TestCase(12, ExpectedResult = 21)]
        [TestCase(1234321, ExpectedResult = 1241233)]
        [TestCase(20, ExpectedResult = -1)]
        [TestCase(-5, ExpectedResult = -2)]
        public int FindNextBiggerNumberTest(int source)
        {
            try
            {
                return Algorithm.FindNextBiggerNumber(source);
            }
            catch (ArgumentException)
            {
                return -2;
            }
        }
        
        [TestCase(7, 1, 2, 3, 4, 5, -6, 7, 68, 69, 70, 15, 17, ExpectedResult = new[] { 7, 70, 17 })]
        [TestCase(5, 1, 2, 3, 4, 5, 6, 7, 65, 69, 70, -15, 17, ExpectedResult = new[] { 5, 65, -15 })]
        [TestCase(12, 5, 8, 999, 1212, ExpectedResult = new[] { -1 })]
        [TestCase(1, new[] {1, 11, 22}, ExpectedResult = new[] { 1, 11 })]
        [TestCase(1, ExpectedResult = null)]        
        public int[] FilterDigitTest(int digit, params int[] numbers)
        {
            try
            {
                return Algorithm.FilterDigit(digit, numbers);
            }            
            catch (ArgumentException)
            {
                return new[] { -1 };
            }            
        }
    }
}
