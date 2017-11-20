﻿using System.Collections;
using NUnit.Framework;

namespace Matrix.NUnitTests
{
    [TestFixture]
    public class SquareMatrixNUnitTests
    {
        [TestCase(5)]
        [TestCase(10)]        
        public void ConstructorTest(int order)
        {
            ConstructorTest<int>(order);
            ConstructorTest<string>(order);
            ConstructorTest<double>(order);
            ConstructorTest<object>(order);
        }
        
        [Test, TestCaseSource(typeof(TestCasesClass), nameof(TestCasesClass.TestCasesInt))]
        public void EnumeratorTest(int[,] elements)
        {
            var result = new SquareMatrix<int>(elements.GetLength(0));

            for (int i = 0; i < elements.GetLength(0); i++)
            {
                for (int j = 0; j < elements.GetLength(1); j++)
                {
                    result[i, j] = elements[i, j];
                }
            }

            EnumeratorTest<int>(result);
        }

        [Test, TestCaseSource(typeof(TestCasesClass), nameof(TestCasesClass.TestCasesString))]
        public void EnumeratorTestString(string[,] elements)
        {
            var result = new SquareMatrix<string>(elements.GetLength(0));

            for (int i = 0; i < elements.GetLength(0); i++)
            {
                for (int j = 0; j < elements.GetLength(1); j++)
                {
                    result[i, j] = elements[i, j];
                }
            }

            EnumeratorTest<string>(result);
        }

        [Test, TestCaseSource(typeof(TestCasesClass), nameof(TestCasesClass.TestCasesInt))]
        public void EqualityTestInt(int[,] elements)
        {
            var lhs = new SquareMatrix<int>(elements.GetLength(0));
            var rhs = new SquareMatrix<int>(elements.GetLength(0));

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

        [Test, TestCaseSource(typeof(TestCasesClass), nameof(TestCasesClass.TestCasesString))]
        public void EqualityTestString(string[,] elements)
        {
            var lhs = new SquareMatrix<string>(elements.GetLength(0));
            var rhs = new SquareMatrix<string>(elements.GetLength(0));

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

        [Test, TestCaseSource(typeof(TestCasesClass), nameof(TestCasesClass.TestCasesAddTestInt))]
        public int[,] AddTestInt(int[,] elements)
        {
            var lhs = new SquareMatrix<int>(elements.GetLength(0));
            var rhs = new SquareMatrix<int>(elements.GetLength(0));

            for (int i = 0; i < elements.GetLength(0); i++)
            {
                for (int j = 0; j < elements.GetLength(1); j++)
                {
                    lhs[i, j] = elements[i, j];
                    rhs[i, j] = elements[i, j];
                }
            }

            lhs = lhs.Add(rhs) as SquareMatrix<int>;

            for (int i = 0; i < elements.GetLength(0); i++)
            {
                for (int j = 0; j < elements.GetLength(1); j++)
                {
                    elements[i, j] = lhs[i, j];
                }
            }

            return elements;
        }

        [Test, TestCaseSource(typeof(TestCasesClass), nameof(TestCasesClass.TestCasesAddSquareAndSymmetricalTestInt))]
        public int[,] AddSquareAndSymmetricalTestInt(int[,] elements)
        {
            var lhs = new SquareMatrix<int>(elements.GetLength(0));
            var rhs = new SymmetricalMatrix<int>(elements.GetLength(0));

            for (int i = 0; i < elements.GetLength(0); i++)
            {
                for (int j = 0; j < elements.GetLength(1); j++)
                {
                    lhs[i, j] = elements[i, j];
                    rhs[i, j] = elements[i, j];
                }
            }

            lhs = lhs.Add(rhs) as SquareMatrix<int>;

            for (int i = 0; i < elements.GetLength(0); i++)
            {
                for (int j = 0; j < elements.GetLength(1); j++)
                {
                    elements[i, j] = lhs[i, j];
                }
            }

            return elements;
        }

        [Test, TestCaseSource(typeof(TestCasesClass), nameof(TestCasesClass.TestCasesAddSquareAndDiagonalTestInt))]
        public int[,] AddSquareAndDiagonalTestInt(int[,] elements)
        {
            var lhs = new SquareMatrix<int>(elements.GetLength(0));
            var rhs = new DiagonalMatrix<int>(elements.GetLength(0));

            for (int i = 0; i < elements.GetLength(0); i++)
            {
                for (int j = 0; j < elements.GetLength(1); j++)
                {
                    lhs[i, j] = elements[i, j];                    
                }

                rhs[i, i] = elements[i, i];
            }

            lhs = lhs.Add(rhs) as SquareMatrix<int>;

            for (int i = 0; i < elements.GetLength(0); i++)
            {
                for (int j = 0; j < elements.GetLength(1); j++)
                {
                    elements[i, j] = lhs[i, j];
                }
            }

            return elements;
        }

        [Test, TestCaseSource(typeof(TestCasesClass), nameof(TestCasesClass.TestCasesAddTestString))]
        public string[,] AddTestString(string[,] elements)
        {
            var lhs = new SquareMatrix<string>(elements.GetLength(0));
            var rhs = new SquareMatrix<string>(elements.GetLength(0));

            for (int i = 0; i < elements.GetLength(0); i++)
            {
                for (int j = 0; j < elements.GetLength(1); j++)
                {
                    lhs[i, j] = elements[i, j];
                    rhs[i, j] = elements[i, j];
                }
            }

            lhs = lhs.Add(rhs) as SquareMatrix<string>;

            for (int i = 0; i < elements.GetLength(0); i++)
            {
                for (int j = 0; j < elements.GetLength(1); j++)
                {
                    elements[i, j] = lhs[i, j];
                }
            }

            return elements;
        }

        private static void ConstructorTest<T>(int order)
        {
            var matrix = new SquareMatrix<T>(order);
            Assert.AreEqual(matrix.ColumnCount, order);
            Assert.AreEqual(matrix.RowCount, order);
        }

        private static void EnumeratorTest<T>(SquareMatrix<T> matrix)
        {
            foreach (var element in matrix)
            {
                Assert.IsNotNull(element);
            }
        }

        private class TestCasesClass
        {
            public static IEnumerable TestCasesInt
            {
                get
                {                                        
                    yield return new TestCaseData(new[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } });                    
                }
            }

            public static IEnumerable TestCasesString
            {
                get
                {
                    yield return new TestCaseData(new[,] { { "1", "2", "3" }, { "4", "5", "6" }, { "7", "8", "9" } });
                }
            }

            public static IEnumerable TestCasesAddTestInt
            {
                get
                {
                    yield return new TestCaseData(new[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } }).Returns(
                        new[,] { { 2, 4, 6 }, { 8, 10, 12 }, { 14, 16, 18 } });                 
                }
            }

            public static IEnumerable TestCasesAddSquareAndSymmetricalTestInt
            {
                get
                {
                    yield return new TestCaseData(new[,] { { 1, 2, 3 }, { 2, 4, 5 }, { 3, 5, 6 } }).Returns(
                        new[,] { { 2, 4, 6 }, { 4, 8, 10 }, { 6, 10, 12 } });
                }
            }

            public static IEnumerable TestCasesAddSquareAndDiagonalTestInt
            {
                get
                {
                    yield return new TestCaseData(new[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } }).Returns(
                        new[,] { { 2, 2, 3 }, { 4, 10, 6 }, { 7, 8, 18 } });
                }
            }

            public static IEnumerable TestCasesAddTestString
            {
                get
                {                    
                    yield return new TestCaseData(new[,] { { "1", "2", "3" }, { "4", "5", "6" }, { "7", "8", "9" } }).Returns(
                        new[,] { { "11", "22", "33" }, { "44", "55", "66" }, { "77", "88", "99" } });
                }
            }
        }
    }
}
