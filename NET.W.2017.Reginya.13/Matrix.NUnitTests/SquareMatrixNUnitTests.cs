using System.Collections;
using System.Collections.Generic;
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
            var result = new SquareMatrix<int>(elements);            
            EnumeratorTest<int>(result);
        }

        [Test, TestCaseSource(typeof(TestCasesClass), nameof(TestCasesClass.TestCasesString))]
        public void EnumeratorTestString(string[,] elements)
        {
            var result = new SquareMatrix<string>(elements);            
            EnumeratorTest<string>(result);
        }

        [Test, TestCaseSource(typeof(TestCasesClass), nameof(TestCasesClass.TestCasesInt))]
        public void EqualityTestInt(int[,] elements)
        {
            var lhs = new SquareMatrix<int>(elements);
            var rhs = new SquareMatrix<int>(elements);
           
            Assert.IsTrue(lhs.Equals(rhs));
        }

        [Test, TestCaseSource(typeof(TestCasesClass), nameof(TestCasesClass.TestCasesString))]
        public void EqualityTestString(string[,] elements)
        {
            var lhs = new SquareMatrix<string>(elements);
            var rhs = new SquareMatrix<string>(elements);

            Assert.IsTrue(lhs.Equals(rhs));
        }

        [Test, TestCaseSource(typeof(TestCasesClass), nameof(TestCasesClass.TestCasesAddTestInt))]
        public int[,] AddTestInt(int[,] elements)
        {
            var lhs = new SquareMatrix<int>(elements);
            var rhs = new SquareMatrix<int>(elements);            

            lhs = lhs.Add(rhs) as SquareMatrix<int>;
            
            return lhs?.ToArray();
        }

        [Test, TestCaseSource(typeof(TestCasesClass), nameof(TestCasesClass.TestCasesAddSquareAndSymmetricalTestInt))]
        public int[,] AddSquareAndSymmetricalTestInt(int[,] elements)
        {
            var lhs = new SquareMatrix<int>(elements);
            var rhs = new SymmetricalMatrix<int>(elements);
            
            lhs = lhs.Add(rhs) as SquareMatrix<int>;            

            return lhs?.ToArray();
        }

        [Test, TestCaseSource(typeof(TestCasesClass), nameof(TestCasesClass.TestCasesAddSquareAndDiagonalTestInt))]
        public int[,] AddSquareAndDiagonalTestInt(int[,] elements)
        {
            var lhs = new SquareMatrix<int>(elements);
            var rhs = new DiagonalMatrix<int>(elements);          

            lhs = lhs.Add(rhs) as SquareMatrix<int>;            

            return lhs?.ToArray();
        }

        [Test, TestCaseSource(typeof(TestCasesClass), nameof(TestCasesClass.TestCasesAddTestString))]
        public string[,] AddTestString(string[,] elements)
        {
            var lhs = new SquareMatrix<string>(elements);
            var rhs = new SquareMatrix<string>(elements);
          
            lhs = lhs.Add(rhs) as SquareMatrix<string>;
           
            return lhs?.ToArray();
        }

        private static void ConstructorTest<T>(int order)
        {
            var matrix = new SquareMatrix<T>(order);
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
