using System;
using System.Collections;
using NUnit.Framework;

namespace Matrix.NUnitTests
{
    [TestFixture]
    public class SquareMatrixNUnitTests
    {
        [TestCase(5)]
        [TestCase(10)]
        [TestCase(int.MinValue)]
        [TestCase(int.MaxValue)]
        public void ConstructorTest(int order)
        {
            ConstructorTest<int>(order);
            ConstructorTest<string>(order);
            ConstructorTest<double>(order);
            ConstructorTest<object>(order);
        }

        private static void ConstructorTest<T>(int order)
        {
            var matrix = new SquareMatrix<T>(order);
            Assert.AreEqual(matrix.ColumnCount, order);
            Assert.AreEqual(matrix.RowCount, order);
        }

        [Test, TestCaseSource(typeof(TestCasesClass), nameof(TestCasesClass.TestCases))]
        public void EnumeratorTest<T>(T[,] elements)
        {
            var result = new SquareMatrix<T>(elements.GetLength(0));

            for (int i = 0; i < elements.GetLength(0); i++)
            {
                for (int j = 0; j < elements.GetLength(1); j++)
                {
                    result[i, j] = elements[i, j];
                }
            }

            EnumeratorTest<T>(result);
        }

        private static void EnumeratorTest<T>(SquareMatrix<T> matrix)
        {
            foreach (var element in matrix)
            {
                Assert.IsNotNull(element);
            }
        }

        [Test, TestCaseSource(typeof(TestCasesClass), nameof(TestCasesClass.TestCases))]
        public void EqualityTest<T>(T[,] elements)
        {
            var lhs = new SquareMatrix<T>(elements.GetLength(0));
            var rhs = new SquareMatrix<T>(elements.GetLength(0));

            for (int i = 0; i < elements.GetLength(0); i++)
            {
                for (int j = 0; j < elements.GetLength(1); j++)
                {
                    lhs[i, j] = elements[i, j];
                    rhs[i, j] = elements[i, j];
                }
            }

            Assert.IsTrue(lhs.Equals(rhs));
        }

        [Test, TestCaseSource(typeof(TestCasesClass), nameof(TestCasesClass.TestCasesAddTest))]
        public T[,] AddTest<T>(T[,] elements)
        {
            var lhs = new SquareMatrix<T>(elements.GetLength(0));
            var rhs = new SquareMatrix<T>(elements.GetLength(0));

            for (int i = 0; i < elements.GetLength(0); i++)
            {
                for (int j = 0; j < elements.GetLength(1); j++)
                {
                    lhs[i, j] = elements[i, j];
                    rhs[i, j] = elements[i, j];
                }
            }

            lhs = lhs.Add(rhs) as SquareMatrix<T>;

            for (int i = 0; i < elements.GetLength(0); i++)
            {
                for (int j = 0; j < elements.GetLength(1); j++)
                {
                    elements[i, j] = lhs[i, j];
                }
            }

            return elements;
        }

        private class TestCasesClass
        {
            public static IEnumerable TestCases
            {
                get
                {
                    yield return new TestCaseData(new[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } });
                    yield return new TestCaseData(new[,] { { "1", "2", "3" }, { "4", "5", "6" }, { "7", "8", "9" } });
                    yield return new TestCaseData(new[,] { { 1d, 2d, 3d }, { 4d, 5d, 6d }, { 7d, 8d, 9d } });
                    yield return new TestCaseData(new[,] { { new object(), new object() }, { new object(), new object() } });
                }
            }

            public static IEnumerable TestCasesAddTest
            {
                get
                {
                    yield return new TestCaseData(new[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } }).Returns(
                        new[,] { { 2, 4, 6 }, { 8, 10, 12 }, { 14, 16, 18 } });
                    yield return new TestCaseData(new[,] { { "1", "2", "3" }, { "4", "5", "6" }, { "7", "8", "9" } }).Returns(
                        new[,] { { "11", "22", "33" }, { "44", "55", "66" }, { "77", "88", "99" } });
                }
            }
        }
    }
}
