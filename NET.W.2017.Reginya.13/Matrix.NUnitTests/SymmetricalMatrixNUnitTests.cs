using System.Collections;
using System.Collections.Generic;
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
            var result = new SymmetricalMatrix<int>(elements);            
            EnumeratorTest<int>(result);
        }

        [Test, TestCaseSource(typeof(TestCasesClass), nameof(TestCasesClass.TestCasesString))]
        public void EnumeratorTestString(string[,] elements)
        {
            var result = new SymmetricalMatrix<string>(elements);           
            EnumeratorTest<string>(result);
        }

        [Test, TestCaseSource(typeof(TestCasesClass), nameof(TestCasesClass.TestCasesInt))]
        public void EqualityTestInt(int[,] elements)
        {
            var lhs = new SymmetricalMatrix<int>(elements);
            var rhs = new SymmetricalMatrix<int>(elements);            
            Assert.IsTrue(lhs.Equals(rhs));
        }

        [Test, TestCaseSource(typeof(TestCasesClass), nameof(TestCasesClass.TestCasesString))]
        public void EqualityTestString(string[,] elements)
        {
            var lhs = new SymmetricalMatrix<string>(elements);
            var rhs = new SymmetricalMatrix<string>(elements);
            Assert.IsTrue(lhs.Equals(rhs));
        }

        [Test, TestCaseSource(typeof(TestCasesClass), nameof(TestCasesClass.TestCasesAddTestInt))]
        public int[,] AddTestInt(int[,] elements)
        {
            var lhs = new SymmetricalMatrix<int>(elements);
            var rhs = new SymmetricalMatrix<int>(elements);            

            lhs = lhs.Add(rhs) as SymmetricalMatrix<int>;
            
            return lhs?.ToArray();
        }
        
        [Test, TestCaseSource(typeof(TestCasesClass), nameof(TestCasesClass.TestCasesAddTestString))]
        public string[,] AddTestString(string[,] elements)
        {
            var lhs = new SymmetricalMatrix<string>(elements);
            var rhs = new SymmetricalMatrix<string>(elements);            

            lhs = lhs.Add(rhs) as SymmetricalMatrix<string>;
            
            return lhs?.ToArray();
        }

        private static void ConstructorTest<T>(int order)
        {
            var matrix = new SymmetricalMatrix<T>(order);
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
