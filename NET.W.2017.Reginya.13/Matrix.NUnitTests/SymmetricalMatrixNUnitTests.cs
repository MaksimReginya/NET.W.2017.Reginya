using System.Collections;
using NUnit.Framework;

namespace Matrix.NUnitTests
{
    [TestFixture]
    public class SymmetricalMatrixNUnitTests
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
            var result = new SymmetricalMatrix<int>(elements.GetLength(0));

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
            var result = new SymmetricalMatrix<string>(elements.GetLength(0));

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
            var lhs = new SymmetricalMatrix<int>(elements.GetLength(0));
            var rhs = new SymmetricalMatrix<int>(elements.GetLength(0));

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
            var lhs = new SymmetricalMatrix<string>(elements.GetLength(0));
            var rhs = new SymmetricalMatrix<string>(elements.GetLength(0));

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
            var lhs = new SymmetricalMatrix<int>(elements.GetLength(0));
            var rhs = new SymmetricalMatrix<int>(elements.GetLength(0));

            for (int i = 0; i < elements.GetLength(0); i++)
            {
                for (int j = 0; j < elements.GetLength(1); j++)
                {
                    lhs[i, j] = elements[i, j];
                    rhs[i, j] = elements[i, j];
                }
            }

            lhs = lhs.Add(rhs) as SymmetricalMatrix<int>;

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
            var lhs = new SymmetricalMatrix<string>(elements.GetLength(0));
            var rhs = new SymmetricalMatrix<string>(elements.GetLength(0));

            for (int i = 0; i < elements.GetLength(0); i++)
            {
                for (int j = 0; j < elements.GetLength(1); j++)
                {
                    lhs[i, j] = elements[i, j];
                    rhs[i, j] = elements[i, j];
                }
            }

            lhs = lhs.Add(rhs) as SymmetricalMatrix<string>;

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
            var matrix = new SymmetricalMatrix<T>(order);
            Assert.AreEqual(matrix.ColumnCount, order);
            Assert.AreEqual(matrix.RowCount, order);
        }

        private static void EnumeratorTest<T>(SymmetricalMatrix<T> matrix)
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
                    yield return new TestCaseData(new[,] { { 1, 2, 3 }, { 2, 4, 5 }, { 3, 5, 6 } });
                }
            }

            public static IEnumerable TestCasesString
            {
                get
                {
                    yield return new TestCaseData(new[,] { { "1", "2", "3" }, { "2", "4", "5" }, { "3", "5", "6" } });
                }
            }

            public static IEnumerable TestCasesAddTestInt
            {
                get
                {
                    yield return new TestCaseData(new[,] { { 1, 2, 3 }, { 2, 4, 5 }, { 3, 5, 6 } }).Returns(
                        new[,] { { 2, 4, 6 }, { 4, 8, 10 }, { 6, 10, 12 } });
                }
            }            

            public static IEnumerable TestCasesAddTestString
            {
                get
                {
                    yield return new TestCaseData(new[,] { { "1", "2", "3" }, { "2", "4", "5" }, { "3", "5", "6" } }).Returns(
                        new[,] { { "11", "22", "33" }, { "22", "44", "55" }, { "33", "55", "66" } });
                }
            }
        }
    }
}
