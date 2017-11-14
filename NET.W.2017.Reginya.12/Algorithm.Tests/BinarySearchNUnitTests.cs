using System;
using System.Collections;
using System.Collections.Generic;
using Algorithm.Tests.Comparers;
using NUnit.Framework;

namespace Algorithm.Tests
{
    [TestFixture]
    public class BinarySearchNUnitTests
    {
        [Test, TestCaseSource(typeof(TestCasesClass), nameof(TestCasesClass.TestCasesInt))]
        public int BinarySearchIntTest(int[] array, int element, IComparer<int> comparer)
        {
            return BinarySearch.SearchElement(array, element, comparer);            
        }

        [Test, TestCaseSource(typeof(TestCasesClass), nameof(TestCasesClass.TestCasesIntArgumentNullExceptionThrown))]
        public void BinarySearchIntArgumentNullExceptionThrown(int[] array, int element, IComparer<int> comparer)
        {
            Assert.Throws<ArgumentNullException>(
                () => BinarySearch.SearchElement(array, element, comparer));
        }

        [Test, TestCaseSource(typeof(TestCasesClass), nameof(TestCasesClass.TestCasesString))]
        public int BinarySearchStringTest(string[] array, string element, IComparer<string> comparer)
        {
            return BinarySearch.SearchElement(array, element, comparer);            
        }

        [Test, TestCaseSource(typeof(TestCasesClass), nameof(TestCasesClass.TestCasesStringArgumentNullExceptionThrown))]
        public void BinarySearchStringArgumentNullExceptionThrown(string[] array, string element, IComparer<string> comparer)
        {
            Assert.Throws<ArgumentNullException>(
                () => BinarySearch.SearchElement(array, element, comparer));
        }

        private class TestCasesClass
        {
            public static IEnumerable TestCasesInt
            {
                get
                {
                    yield return new TestCaseData(new[] { 1, 2, 3, 4, 5 }, 3, new IntComparer()).Returns(2);
                    yield return new TestCaseData(new[] { -5, -4, -3, -2, -1 }, -3, new IntComparer()).Returns(2);
                    yield return new TestCaseData(new[] { int.MinValue, 2, 3, 4, int.MaxValue }, 4, new IntComparer()).Returns(3);
                    yield return new TestCaseData(new[] { 1, 2, 3, 4, 5 }, 200, new IntComparer()).Returns(-1);                                        
                }
            }

            public static IEnumerable TestCasesIntArgumentNullExceptionThrown
            {
                get
                {
                    yield return new TestCaseData(null, 3, new IntComparer());                    
                    yield return new TestCaseData(new[] { int.MinValue, 2, 3, 4, int.MaxValue }, 4, null);                    
                }
            }

            public static IEnumerable TestCasesString
            {
                get
                {
                    yield return new TestCaseData(new[] { "a", "b", "c", "d" }, "c", new Comparers.StringComparer()).Returns(2);
                    yield return new TestCaseData(new[] { "Ad", "Bc", "Cb", "Da" }, "Bc", new Comparers.StringComparer()).Returns(1);
                    yield return new TestCaseData(new[] { string.Empty, "wqdqwd", "wqdqww" }, "wqdqw", new Comparers.StringComparer()).Returns(-1);                    
                }
            }

            public static IEnumerable TestCasesStringArgumentNullExceptionThrown
            {
                get
                {
                    yield return new TestCaseData(null, "c", new Comparers.StringComparer());
                    yield return new TestCaseData(new[] { "Ad", "Bc", "Cb", "Da" }, null, new Comparers.StringComparer());
                    yield return new TestCaseData(new[] { string.Empty, "wqdqwd", "wqdqww" }, "wqdqw", null);
                }
            }
        }
    }
}
