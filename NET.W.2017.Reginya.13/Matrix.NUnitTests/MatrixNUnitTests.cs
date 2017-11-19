using System;
using System.Collections;
using NUnit.Framework;

namespace Matrix.NUnitTests
{
    [TestFixture]
    public class MatrixNUnitTests
    {
        [TestCase(5, 10)]
        [TestCase(10, 10)]
        [TestCase(int.MinValue, int.MaxValue)]
        public void ConstructorTest(int rowCount, int columnCount)
        {
            ConstructorTest<int>(rowCount, columnCount);
            ConstructorTest<string>(rowCount, columnCount);
            ConstructorTest<double>(rowCount, columnCount);
            ConstructorTest<object>(rowCount, columnCount);
        }
                    
        [Test, TestCaseSource(typeof(TestCasesClass), nameof(TestCasesClass.TestCases))]
        public void EnumeratorTest<T>(T[,] elements)
        {
            var result = new Matrix<T>(elements.GetLength(0), elements.GetLength(1));

            for (int i = 0; i < elements.GetLength(0); i++)
            {
                for (int j = 0; j < elements.GetLength(1); j++)
                {
                    result[i, j] = elements[i, j];
                }
            }

            EnumeratorTest<T>(result);
        }
        
        [Test, TestCaseSource(typeof(TestCasesClass), nameof(TestCasesClass.TestCases))]
        public void EqualityTest<T>(T[,] elements)
        {
            var lhs = new Matrix<T>(elements.GetLength(0), elements.GetLength(1));
            var rhs = new Matrix<T>(elements.GetLength(0), elements.GetLength(1));

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
            var lhs = new Matrix<T>(elements.GetLength(0), elements.GetLength(1));
            var rhs = new Matrix<T>(elements.GetLength(0), elements.GetLength(1));

            for (int i = 0; i < elements.GetLength(0); i++)
            {
                for (int j = 0; j < elements.GetLength(1); j++)
                {
                    lhs[i, j] = elements[i, j];
                    rhs[i, j] = elements[i, j];
                }
            }

            lhs = lhs.Add(rhs);
            
            for (int i = 0; i < elements.GetLength(0); i++)
            {
                for (int j = 0; j < elements.GetLength(1); j++)
                {
                    elements[i, j] = lhs[i, j];                    
                }
            }

            return elements;
        }

        private static void ConstructorTest<T>(int rowCount, int columnCount)
        {
            var matrix = new Matrix<T>(rowCount, columnCount);
            Assert.AreEqual(matrix.ColumnCount, columnCount);
            Assert.AreEqual(matrix.RowCount, rowCount);
        }

        private static void EnumeratorTest<T>(Matrix<T> matrix)
        {
            foreach (var element in matrix)
            {
                Assert.IsNotNull(element);
            }
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
