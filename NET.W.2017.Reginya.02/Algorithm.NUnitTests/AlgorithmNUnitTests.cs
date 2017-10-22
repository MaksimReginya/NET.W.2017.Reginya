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
    }
}
