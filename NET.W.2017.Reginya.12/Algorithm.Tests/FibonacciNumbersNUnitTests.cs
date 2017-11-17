using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using NUnit.Framework;

namespace Algorithm.Tests
{
    public class FibonacciNumbersNUnitTests
    {
        [Test, TestCaseSource(typeof(TestCasesClass), nameof(TestCasesClass.TestCases))]
        public IEnumerable<BigInteger> FibonacciNumbersTest(int length)
        {
            return FibonacciNumbers.GetNumbers(length);
        }

        [TestCase(-5)]
        public void FibonacciNumbersArgumentExceptionThrown(int length)
        {
            Assert.Throws<ArgumentException>(
                () => FibonacciNumbers.GetNumbers(length));
        }

        private class TestCasesClass
        {
            public static IEnumerable TestCases
            {
                get
                {
                    yield return new TestCaseData(0).Returns(new List<BigInteger>());
                    yield return new TestCaseData(1).Returns(new List<BigInteger> { 1 });
                    yield return new TestCaseData(2).Returns(new List<BigInteger> { 1, 1 });
                    yield return new TestCaseData(19).Returns(new List<BigInteger>
                    {
                        1, 1, 2, 3, 5, 8, 13, 21, 34, 55, 89, 144, 233, 377, 610, 987, 1597, 2584, 4181
                    });
                }
            }
        }
    }
}
