using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Algorithm;
using Algorithm.NUnitTests.Comparators;

namespace Algorithm.NUnitTests
{
    [TestFixture]
    public class ArraySortingTests
    {       
        [Test, TestCaseSource(typeof(TestCasesClass), nameof(TestCasesClass.TestCases))]
        public int[][] BubbleSortTest(int[][] array, IComparer<int []> comparer)
        {            
            ArraySorting.BubbleRowsSort(array, comparer);
            return array;
        }
    }

    public class TestCasesClass
    {
        public static IEnumerable TestCases
        {
            get
            {
                yield return new TestCaseData(new[] { new[] { 4, 3, 2, 1, 5 }, new[] { 1, 2, 3, 4 } },
                    new SumComparator(false)).Returns(
                    new[] { new[] { 1, 2, 3, 4 }, new[] { 4, 3, 2, 1, 5 } });

                yield return new TestCaseData(new [] {new [] {4, 3, 2, 1, 5}, new [] {1, 2, 3, 4}},
                    new SumComparator(true)).Returns(
                    new [] {new [] {4, 3, 2, 1, 5}, new [] {1, 2, 3, 4}});                

                yield return new TestCaseData(new[] {new[] { -100, 200 }, new[] { -300 }, new[] { 0, 777777777 } },
                    new MaxElementComparator(false)).Returns(
                    new[] { new[] { -300 }, new[] { -100, 200 }, new[] { 0, 777777777 } });

                yield return new TestCaseData(new[] { new[] { -100, 200 }, new[] { -300 }, new[] { 0, Int32.MaxValue } },
                    new MaxElementComparator(true)).Returns(
                    new[] { new[] { 0, Int32.MaxValue }, new[] { -100, 200 }, new[] { -300 } });

                yield return new TestCaseData(new[] { new[] { 5, 11, 16, 21 }, new[] { 0 }, new[] { Int32.MinValue } },
                    new MinElementComparator(false)).Returns(
                    new[] { new[] { Int32.MinValue }, new[] { 0 }, new[] { 5, 11, 16, 21 } });

                yield return new TestCaseData(new[] { new[] { 5, 11, 16, 21 }, new[] { 0 }, new[] { Int32.MinValue } },
                    new MinElementComparator(true)).Returns(
                    new[] { new[] { 5, 11, 16, 21 }, new[] { 0 }, new[] { Int32.MinValue } });
            }
        }
    }
}
