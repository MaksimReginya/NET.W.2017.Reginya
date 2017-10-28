using System;
using System.Collections;
using NUnit.Framework;
using Algorithm;

namespace Algorithm.NUnitTests
{
    [TestFixture]
    public class ArraySortingTests
    {       
        [Test, TestCaseSource(typeof(TestCasesClass), nameof(TestCasesClass.TestCases))]
        public int[][] BubbleSortByRowElementsSumTest(int[][] array, SortCriterionProvider.SortCriterion criterion, bool descending)
        {            
            ArraySorting.BubbleRowsSort(array, criterion, descending);
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
                    SortCriterionProvider.SortCriterion.RowElementsSum, false).Returns(
                    new[] { new[] { 1, 2, 3, 4 }, new[] { 4, 3, 2, 1, 5 } });

                yield return new TestCaseData(new [] {new [] {4, 3, 2, 1, 5}, new [] {1, 2, 3, 4}},
                    SortCriterionProvider.SortCriterion.RowElementsSum, true).Returns(
                    new [] {new [] {4, 3, 2, 1, 5}, new [] {1, 2, 3, 4}});                

                yield return new TestCaseData(new[] {new[] { -100, 200 }, new[] { -300 }, new[] { 0, 777777777 } },
                    SortCriterionProvider.SortCriterion.MaxRowElement, false).Returns(
                    new[] { new[] { -300 }, new[] { -100, 200 }, new[] { 0, 777777777 } });

                yield return new TestCaseData(new[] { new[] { -100, 200 }, new[] { -300 }, new[] { 0, Int32.MaxValue } },
                    SortCriterionProvider.SortCriterion.MaxRowElement, true).Returns(
                    new[] { new[] { 0, Int32.MaxValue }, new[] { -100, 200 }, new[] { -300 } });

                yield return new TestCaseData(new[] { new[] { 5, 11, 16, 21 }, new[] { 0 }, new[] { Int32.MinValue } },
                    SortCriterionProvider.SortCriterion.MinRowElement, false).Returns(
                    new[] { new[] { Int32.MinValue }, new[] { 0 }, new[] { 5, 11, 16, 21 } });

                yield return new TestCaseData(new[] { new[] { 5, 11, 16, 21 }, new[] { 0 }, new[] { Int32.MinValue } },
                    SortCriterionProvider.SortCriterion.MinRowElement, true).Returns(
                    new[] { new[] { 5, 11, 16, 21 }, new[] { 0 }, new[] { Int32.MinValue } });
            }
        }
    }
}
