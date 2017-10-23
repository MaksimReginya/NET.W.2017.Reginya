using System;
using NUnit.Framework;
using Algorithm;

namespace Algorithm.NUnitTests
{
    [TestFixture]
    public class NumberAlgorithmNUnitTests
    {
        [TestCase(15, 15, 0, 0, ExpectedResult = 15)]
        [TestCase(8, 15, 0, 0, ExpectedResult = 9)]
        [TestCase(8, 15, 3, 8, ExpectedResult = 120)]                 
        public int InsertNumberTest(int number1, int number2, int i, int j)
        {
            return NumberAlgorithm.InsertNumber(number1, number2, i, j);            
        }

        [TestCase(8, 15, 3, -8)]
        [TestCase(8, 15, 7, 1)]
        [TestCase(8, -15, 3, 8)]
        public void InsertNumber_ThrowsArgumentException(int number1, int number2, int i, int j)
        {
            Assert.Throws<ArgumentException>(
                () => NumberAlgorithm.InsertNumber(number1, number2, i, j));
        }

        [TestCase(12, ExpectedResult = 21)]
        [TestCase(1234321, ExpectedResult = 1241233)]
        [TestCase(20, ExpectedResult = -1)]        
        public int FindNextBiggerNumberTest(int source)
        {
            return NumberAlgorithm.FindNextBiggerNumber(source);            
        }

        [TestCase(-5)]
        public void FindNextBiggerNumber_ThrowsArgumentException(int source)
        {
            Assert.Throws<ArgumentException>(
                () => NumberAlgorithm.FindNextBiggerNumber(source));
        }
        
        [TestCase(1, 5, 0.0001)]        
        [TestCase(8, 3, 0.0001)]
        [TestCase(0.001, 3, 0.0001)]
        [TestCase(0.04100625, 4, 0.0001)]
        public void FindNthRootTest(double number, int power, double accuracy)
        {
            double expected = Math.Pow(number, 1.0 / power);
            double actual = NumberAlgorithm.FindNthRoot(number, power, accuracy);
            bool result = Math.Abs(expected - actual) < accuracy;
            Assert.IsTrue(result);
        }

        [TestCase(1, 5, -0.0001)]
        [TestCase(1, -5, 0.0001)]        
        [TestCase(0, 5, 0.0001)]
        public void FindNthRoot_ThrowsArgumentException(double number, int power, double accuracy)
        {            
            Assert.Throws<ArgumentException>(
                () => NumberAlgorithm.FindNthRoot(number, power, accuracy));
        }
    }
}
