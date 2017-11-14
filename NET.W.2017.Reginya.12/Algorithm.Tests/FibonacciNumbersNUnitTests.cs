using System;
using System.Collections;
using NUnit.Framework;

namespace Algorithm.Tests
{
    public class FibonacciNumbersNUnitTests
    {
        [Test, TestCaseSource(typeof(TestCasesClass), nameof(TestCasesClass.TestCases))]
        public int[] FibonacciNumbersTest(int length)
        {
            return FibonacciNumbers.GetRow(length);
        }

        [TestCase(-5)]
        public void FibonacciNumbersArgumentExceptionThrown(int length)
        {
            Assert.Throws<ArgumentException>(
                () => FibonacciNumbers.GetRow(length));
        }

        private class TestCasesClass
        {
            public static IEnumerable TestCases
            {
                get
                {
                    yield return new TestCaseData(0).Returns(new int[] { });
                    yield return new TestCaseData(1).Returns(new[] {0});
                    yield return new TestCaseData(2).Returns(new[] {0, 1});
                    yield return new TestCaseData(20).Returns(new[]
                        {0, 1, 1, 2, 3, 5, 8, 13, 21, 34, 55, 89, 144, 233, 377, 610, 987, 1597, 2584, 4181});
                }
            }
        }
    }
}
