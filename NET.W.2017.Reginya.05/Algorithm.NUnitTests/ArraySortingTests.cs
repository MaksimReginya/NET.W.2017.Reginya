using System;
using System.Collections;
using System.Collections.Generic;
using Algorithm.NUnitTests.Comparators;
using NUnit.Framework;

namespace Algorithm.NUnitTests
{
    [TestFixture]
    public class ArraySortingTests
    {       
        [Test, TestCaseSource(typeof(TestCasesClass), nameof(TestCasesClass.TestCases))]
        public int[][] BubbleSortWithInterfaceTest(int[][] array, IComparer<int[]> comparer)
        {            
            ArraySortingWithInterface.BubbleRowsSort(array, comparer);
            return array;
        }

        [Test, TestCaseSource(typeof(TestCasesClass), nameof(TestCasesClass.TestCases))]
        public int[][] BubbleSortWithDelegateTest(int[][] array, IComparer<int[]> comparer)
        {
            ArraySortingWithInterface.BubbleRowsSort(array, comparer.Compare);
            return array;
        }

        private class TestCasesClass
        {
            public static IEnumerable TestCases
            {
                get
                {
                    yield return new TestCaseData(
                        new[] { new[] { 4, 3, 2, 1, 5 }, new[] { 1, 2, 3, 4 } },
                        new SumComparator(false)).Returns(
                        new[] { new[] { 1, 2, 3, 4 }, new[] { 4, 3, 2, 1, 5 } });

                    yield return new TestCaseData(
                        new[] { new[] { 4, 3, 2, 1, 5 }, new[] { 1, 2, 3, 4 } },
                        new SumComparator(true)).Returns(
                        new[] { new[] { 4, 3, 2, 1, 5 }, new[] { 1, 2, 3, 4 } });

                    yield return new TestCaseData(
                        new[] { new[] { -100, 200 }, new[] { -300 }, new[] { 0, 777777777 } },
                        new MaxElementComparator(false)).Returns(
                        new[] { new[] { -300 }, new[] { -100, 200 }, new[] { 0, 777777777 } });

                    yield return new TestCaseData(
                        new[] { new[] { -100, 200 }, new[] { -300 }, new[] { 0, int.MaxValue } },
                        new MaxElementComparator(true)).Returns(
                        new[] { new[] { 0, int.MaxValue }, new[] { -100, 200 }, new[] { -300 } });

                    yield return new TestCaseData(
                        new[] { new[] { 5, 11, 16, 21 }, new[] { 0 }, new[] { int.MinValue } },
                        new MinElementComparator(false)).Returns(
                        new[] { new[] { int.MinValue }, new[] { 0 }, new[] { 5, 11, 16, 21 } });

                    yield return new TestCaseData(
                        new[] { new[] { 5, 11, 16, 21 }, new[] { 0 }, new[] { int.MinValue } },
                        new MinElementComparator(true)).Returns(
                        new[] { new[] { 5, 11, 16, 21 }, new[] { 0 }, new[] { int.MinValue } });
                }
            }
        }
    }
}
