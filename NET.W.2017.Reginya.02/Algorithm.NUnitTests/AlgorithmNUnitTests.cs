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
        public int InsertNumberTest(int number1, int number2, int i, int j)
        {
            return Algorithm.InsertNumber(number1, number2, i, j);            
        }

        [TestCase(8, 15, 3, -8)]
        [TestCase(8, 15, 7, 1)]
        [TestCase(8, -15, 3, 8)]
        public void InsertNumber_ThrowsArgumentException(int number1, int number2, int i, int j)
        {
            Assert.Throws<ArgumentException>(
                () => Algorithm.InsertNumber(number1, number2, i, j));
        }

        [TestCase(12, ExpectedResult = 21)]
        [TestCase(1234321, ExpectedResult = 1241233)]
        [TestCase(20, ExpectedResult = -1)]        
        public int FindNextBiggerNumberTest(int source)
        {
            return Algorithm.FindNextBiggerNumber(source);            
        }

        [TestCase(-5)]
        public void FindNextBiggerNumber_ThrowsArgumentException(int source)
        {
            Assert.Throws<ArgumentException>(
                () => Algorithm.FindNextBiggerNumber(source));
        }

        [TestCase(7, 1, 2, 3, 4, 5, -6, 7, 68, 69, 70, 15, 17, ExpectedResult = new[] { 7, 70, 17 })]
        [TestCase(5, 1, 2, 3, 4, 5, 6, 7, 65, 69, 70, -15, 17, ExpectedResult = new[] { 5, 65, -15 })]        
        [TestCase(1, new[] {1, 11, 22}, ExpectedResult = new[] { 1, 11 })]
        [TestCase(1, ExpectedResult = null)]        
        public int[] FilterDigitTest(int digit, params int[] numbers)
        {                        
            return Algorithm.FilterDigit(digit, numbers);                    
        }
        
        [TestCase(12, 5, 8, 999, 1212)]        
        public void FilterDigit_ThrowsArgumentException(int digit, params int[] numbers)
        {
            Assert.Throws<ArgumentException>(
                () => Algorithm.FilterDigit(digit, numbers));
        }

        [TestCase(1, 5, 0.0001)]        
        [TestCase(8, 3, 0.0001)]
        [TestCase(0.001, 3, 0.0001)]
        [TestCase(0.04100625, 4, 0.0001)]
        public void FindNthRootTest(double number, int power, double accuracy)
        {
            double expected = Math.Pow(number, 1.0 / power);
            double actual = Algorithm.FindNthRoot(number, power, accuracy);
            bool result = Math.Abs(expected - actual) < accuracy;
            Assert.IsTrue(result);
        }

        [TestCase(1, 5, -0.0001)]
        [TestCase(1, -5, 0.0001)]        
        [TestCase(0, 5, 0.0001)]
        public void FindNthRoot_ThrowsArgumentException(double number, int power, double accuracy)
        {            
            Assert.Throws<ArgumentException>(
                () => Algorithm.FindNthRoot(number, power, accuracy));
        }
    }
}
