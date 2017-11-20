using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace Matrix.NUnitTests
{
    [TestFixture]
    public class DiagonalMatrixNUnitTests
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
        public void EnumeratorTest(int[] elements)
        {
            var result = new DiagonalMatrix<int>(elements.GetLength(0));

            for (int i = 0; i < elements.GetLength(0); i++)
            {                
                result[i, i] = elements[i];                
            }

            EnumeratorTest<int>(result);
        }

        [Test, TestCaseSource(typeof(TestCasesClass), nameof(TestCasesClass.TestCasesString))]
        public void EnumeratorTestString(string[,] elements)
        {            
            var result = new DiagonalMatrix<string>(elements.GetLength(1));

            for (int i = 0; i < elements.GetLength(1); i++)
            {
                result[i, i] = elements[0, i];
            }

            EnumeratorTest<string>(result);
        }

        [Test, TestCaseSource(typeof(TestCasesClass), nameof(TestCasesClass.TestCasesInt))]
        public void EqualityTestInt(int[] elements)
        {
            var lhs = new DiagonalMatrix<int>(elements.GetLength(0));
            var rhs = new DiagonalMatrix<int>(elements.GetLength(0));

            for (int i = 0; i < elements.GetLength(0); i++)
            {                
                lhs[i, i] = elements[i];
                rhs[i, i] = elements[i];                
            }

            Assert.IsTrue(lhs.Equals(rhs));
        }

        [Test, TestCaseSource(typeof(TestCasesClass), nameof(TestCasesClass.TestCasesString))]
        public void EqualityTestString(string[,] elements)
        {
            var lhs = new DiagonalMatrix<string>(elements.GetLength(0));
            var rhs = new DiagonalMatrix<string>(elements.GetLength(0));

            for (int i = 0; i < elements.GetLength(0); i++)
            {
                lhs[i, i] = elements[0, i];
                rhs[i, i] = elements[0, i];
            }

            Assert.IsTrue(lhs.Equals(rhs));
        }

        [Test, TestCaseSource(typeof(TestCasesClass), nameof(TestCasesClass.TestCasesAddTestInt))]
        public int[] AddTestInt(int[] elements)
        {
            var lhs = new DiagonalMatrix<int>(elements.GetLength(0));
            var rhs = new DiagonalMatrix<int>(elements.GetLength(0));

            for (int i = 0; i < elements.GetLength(0); i++)
            {
                lhs[i, i] = elements[i];
                rhs[i, i] = elements[i];
            }

            lhs = lhs.Add(rhs) as DiagonalMatrix<int>;

            for (int i = 0; i < elements.GetLength(0); i++)
            {
                elements[i] = lhs[i, i];
            }

            return elements;
        }

        [Test, TestCaseSource(typeof(TestCasesClass), nameof(TestCasesClass.TestCasesAddDiagonalAndSymmetricalTestInt))]
        public int[,] AddDiagonalAndSymmetricalTestInt(int[,] elements)
        {
            var lhs = new DiagonalMatrix<int>(elements.GetLength(0));
            var rhs = new SymmetricalMatrix<int>(elements.GetLength(0));

            for (int i = 0; i < elements.GetLength(0); i++)
            {
                for (int j = 0; j < elements.GetLength(1); j++)
                {                    
                    rhs[i, j] = elements[i, j];
                }

                lhs[i, i] = elements[i, i];
            }

            rhs = rhs.Add(lhs) as SymmetricalMatrix<int>;

            for (int i = 0; i < elements.GetLength(0); i++)
            {
                for (int j = 0; j < elements.GetLength(1); j++)
                {
                    elements[i, j] = rhs[i, j];
                }
            }

            return elements;
        }        

        [Test, TestCaseSource(typeof(TestCasesClass), nameof(TestCasesClass.TestCasesAddTestString))]
        public string[,] AddTestString(string[,] elements)
        {
            var lhs = new DiagonalMatrix<string>(elements.GetLength(1));
            var rhs = new DiagonalMatrix<string>(elements.GetLength(1));

            for (int i = 0; i < elements.GetLength(1); i++)
            {
                lhs[i, i] = elements[0, i];
                rhs[i, i] = elements[0, i];
            }

            lhs = lhs.Add(rhs) as DiagonalMatrix<string>;

            for (int i = 0; i < elements.GetLength(1); i++)
            {
                elements[0, i] = lhs[i, i];
            }

            return elements;
        }

        private static void ConstructorTest<T>(int order)
        {
            var matrix = new DiagonalMatrix<T>(order);
            Assert.AreEqual(matrix.ColumnCount, order);
            Assert.AreEqual(matrix.RowCount, order);
        }

        private static void EnumeratorTest<T>(DiagonalMatrix<T> matrix)
        {
            foreach (var element in matrix)
            {                
            }
        }

        private class TestCasesClass
        {
            public static IEnumerable TestCasesInt
            {
                get
                {
                    yield return new TestCaseData(new[] { 4, 5, 6 });
                }
            }

            public static IEnumerable TestCasesString
            {
                get
                {
                    yield return new TestCaseData(new[,] { { "4", "5", "6" } });
                }
            }

            public static IEnumerable TestCasesAddTestInt
            {
                get
                {
                    yield return new TestCaseData(new[] { 4, 5, 6 }).Returns(new[] { 8, 10, 12 });
                }
            }

            public static IEnumerable TestCasesAddDiagonalAndSymmetricalTestInt
            {
                get
                {
                    yield return new TestCaseData(new[,] { { 1, 2, 3 }, { 2, 4, 5 }, { 3, 5, 6 } }).Returns(
                        new[,] { { 2, 2, 3 }, { 2, 8, 5 }, { 3, 5, 12 } });
                }
            }            

            public static IEnumerable TestCasesAddTestString
            {
                get
                {
                    yield return new TestCaseData(new[,] { { "4", "5", "6" } }).Returns(new[,] { { "44", "55", "66" } });
                }
            }
        }
    }
}
