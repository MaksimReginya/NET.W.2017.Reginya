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
        public void EnumeratorTest(int[,] elements)
        {
            var result = new DiagonalMatrix<int>(elements);            
            EnumeratorTest<int>(result);
        }

        [Test, TestCaseSource(typeof(TestCasesClass), nameof(TestCasesClass.TestCasesString))]
        public void EnumeratorTestString(string[,] elements)
        {            
            var result = new DiagonalMatrix<string>(elements);            
            EnumeratorTest<string>(result);
        }

        [Test, TestCaseSource(typeof(TestCasesClass), nameof(TestCasesClass.TestCasesInt))]
        public void EqualityTestInt(int[,] elements)
        {
            var lhs = new DiagonalMatrix<int>(elements);
            var rhs = new DiagonalMatrix<int>(elements);
            Assert.IsTrue(lhs.Equals(rhs));
        }

        [Test, TestCaseSource(typeof(TestCasesClass), nameof(TestCasesClass.TestCasesString))]
        public void EqualityTestString(string[,] elements)
        {
            var lhs = new DiagonalMatrix<string>(elements);
            var rhs = new DiagonalMatrix<string>(elements);            
            Assert.IsTrue(lhs.Equals(rhs));
        }

        [Test, TestCaseSource(typeof(TestCasesClass), nameof(TestCasesClass.TestCasesAddTestInt))]
        public int[,] AddTestInt(int[,] elements)
        {
            var lhs = new DiagonalMatrix<int>(elements);
            var rhs = new DiagonalMatrix<int>(elements);         

            lhs = lhs.Add(rhs) as DiagonalMatrix<int>;
            
            return lhs?.ToArray();
        }

        [Test, TestCaseSource(typeof(TestCasesClass), nameof(TestCasesClass.TestCasesAddDiagonalAndSymmetricalTestInt))]
        public int[,] AddDiagonalAndSymmetricalTestInt(int[,] elements)
        {
            var lhs = new DiagonalMatrix<int>(elements);
            var rhs = new SymmetricalMatrix<int>(elements);
            
            rhs = rhs.Add(lhs) as SymmetricalMatrix<int>;
            
            return rhs?.ToArray();
        }        

        [Test, TestCaseSource(typeof(TestCasesClass), nameof(TestCasesClass.TestCasesAddTestString))]
        public string[,] AddTestString(string[,] elements)
        {
            var lhs = new DiagonalMatrix<string>(elements);
            var rhs = new DiagonalMatrix<string>(elements);

            lhs = lhs.Add(rhs, (x, y) => x + y) as DiagonalMatrix<string>;
            
            return lhs?.ToArray();
        }

        private static void ConstructorTest<T>(int order)
        {
            var matrix = new DiagonalMatrix<T>(order);
            Assert.AreEqual(matrix.Order, order);
        }

        private static void EnumeratorTest<T>(IEnumerable<T> matrix)
        {
            foreach (var element in matrix)
            {                 
                Assert.IsTrue(element != null || default(T) == null);
            }
        }

        private class TestCasesClass
        {
            public static IEnumerable TestCasesInt
            {
                get
                {
                    yield return new TestCaseData(new[,] { { 1, 0, 0 }, { 0, 5, 0 }, { 0, 0, 9 } });
                }
            }

            public static IEnumerable TestCasesString
            {
                get
                {
                    yield return new TestCaseData(new[,] { { "1", null, null }, { null, "5", null }, { null, null, "9" } });
                }
            }

            public static IEnumerable TestCasesAddTestInt
            {
                get
                {
                    yield return new TestCaseData(new[,] { { 1, 0, 0 }, { 0, 5, 0 }, { 0, 0, 9 } }).Returns(
                        new[,] { { 2, 0, 0 }, { 0, 10, 0 }, { 0, 0, 18 } });
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
                    yield return new TestCaseData(new[,] { { "1", null, null }, { null, "5", null }, { null, null, "9" } }).Returns(
                        new[,] { { "11", null, null }, { null, "55", null }, { null, null, "99" } });
                }
            }
        }
    }
}
