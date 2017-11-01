using System;
using System.Collections;
using NUnit.Framework;
using Algorithm;

namespace Algorithm.NUnitTests
{
    [TestFixture]
    public class PolynomialTests
    {
        #region Tests on indexer
                    
        [TestCase(1, 8.1, 15.21, 0d, 1.22, 2.0, ExpectedResult = 15.21)]
        [TestCase(100, -8.9, 18.9889, 6d, 32d, 2.555, 8.6, ExpectedResult = 0.0)]
        [TestCase(1, -9.2, -27d, 0d, 81d, 9d, -81d, -27d, ExpectedResult = -27d)]
        public double PolynomialIndexerTest(int degree, params double[] coefficients)
        {
            var polynomial = new Polynomial(coefficients);
            return polynomial[degree];
        }
        
        [TestCase(-1, 8.1, 15.21, 0d, 1.22, 2.0)]
        public void PolynomialIndexerTest_ThrowsArgumentOutOfRangeException(int degree, params double[] coefficients)
        {
            var polynomial = new Polynomial(coefficients);

            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                double res = polynomial[degree];
            });            
        }

        #endregion

        #region Tests on overloaded operators

        [TestCase(new[] { 1.0, 3.0, 2.0 }, new[] { 5.0, 5.0 }, ExpectedResult = new[] { 6.0, 8.0, 2.0 })]
        [TestCase(new[] { 1.0, -3.0, 2.0 }, new[] { 5.0, 5.0 }, ExpectedResult = new[] { 6.0, 2.0, 2.0 })]        
        public double[] PolynomialAdditionTest(double[] coefficientsFirst, double[] coefficientsSecond)
        {
            var first = new Polynomial(coefficientsFirst);
            var second = new Polynomial(coefficientsSecond);            
            return (first + second).Coefficients;
        }

        [TestCase(null, new[] { 5.0, 5.0 })]
        public void PolynomialAdditionTest_ThrowsArgumentNullException(double[] coefficientsFirst, double[] coefficientsSecond)
        {
            Polynomial first = null;
            var second = new Polynomial(coefficientsSecond);
            Assert.Throws<ArgumentNullException>(() =>
            {
                var result = first + second;
            });
        }

        [TestCase(new[] { 1.0, 3.0, 2.0 }, new[] { 5.0, 5.0 }, ExpectedResult = new[] { -4.0, -2.0, 2.0 })]
        [TestCase(new[] { 1.0, -3.0, 2.0, 3.1 }, new[] { 5.222 }, ExpectedResult = new[] { -4.222, -3.0, 2.0, 3.1 })]
        public double[] PolynomialSubtractionTest(double[] coefficientsFirst, double[] coefficientsSecond)
        {
            var first = new Polynomial(coefficientsFirst);
            var second = new Polynomial(coefficientsSecond);
            return (first - second).Coefficients;
        }

        [TestCase(null, new[] { 5.0, 5.0 })]
        public void PolynomialSubtractionTest_ThrowsArgumentNullException(double[] coefficientsFirst, double[] coefficientsSecond)
        {
            Polynomial first = null;
            var second = new Polynomial(coefficientsSecond);
            Assert.Throws<ArgumentNullException>(() =>
            {
                var result = first - second;
            });
        }

        [TestCase(new[] { 1.0, 3.0, 2.0 }, 5.0, new[] { 5.0, 15.0, 10.0 }, ExpectedResult = true)]
        [TestCase(new[] { 1.0, -3.0, 2.0, 3.1 }, 0.1, new[] { 0.1, -0.3, 0.2, 0.31 }, ExpectedResult = true)]
        public bool PolynomialAndNumberMultiplicationTest(double[] coefficients, double multiplier, double[] expected)
        {
            var polynomial = new Polynomial(coefficients);
            var expectedPolynomial = new Polynomial(expected);            
            return (multiplier * polynomial).ToString().Equals(expectedPolynomial.ToString());
        }

        [TestCase]
        public void PolynomialAndNumberMultiplicationTest_ThrowsArgumentNullException()
        {
            Polynomial polynomial = null;            
            Assert.Throws<ArgumentNullException>(() =>
            {
                var result = polynomial * 7;
            });
        }

        [TestCase(new[] { 1.0, 3.0, 2.0 }, new[] { 5.0, 5.0 }, new[] { 5.0, 20.0, 25.0, 10.0 }, ExpectedResult = true)]
        [TestCase(new[] { 1.0, -3.0, 2.0, 3.1 }, new[] { 0.1 }, new[] { 0.1, -0.3, 0.2, 0.31 }, ExpectedResult = true)]
        public bool PolynomialsMultiplicationTest(double[] coefficientsFirst, double[] coefficientsSecond, double[] expected)
        {
            var first = new Polynomial(coefficientsFirst);
            var second = new Polynomial(coefficientsSecond);
            var expectedPolynomial = new Polynomial(expected);
            return (first * second).ToString().Equals(expectedPolynomial.ToString());
        }

        [TestCase(null, new[] { 5.0, 5.0 })]
        public void PolynomialsMultiplicationTest_ThrowsArgumentNullException(double[] coefficientsFirst, double[] coefficientsSecond)
        {
            Polynomial first = null;
            var second = new Polynomial(coefficientsSecond);
            Assert.Throws<ArgumentNullException>(() =>
            {
                var result = first * second;
            });
        }

        [TestCase(new[] { 1.0, 3.0, 2.0 }, 5.0, ExpectedResult = new[] { 0.2, 0.6, 0.4 })]
        [TestCase(new[] { 1.0, -3.0, 2.0, 3.1 }, 0.1, ExpectedResult = new[] { 10.0, -30.0, 20.0, 31 })]
        public double[] PolynomialDivisionTest(double[] coefficients, double divider)
        {
            var polynomial = new Polynomial(coefficients);
            return (polynomial / divider).Coefficients;
        }

        [TestCase]
        public void PolynomialDivisionTest_ThrowsArgumentNullException()
        {
            Polynomial polynomial = null;
            Assert.Throws<ArgumentNullException>(() =>
            {
                var result = polynomial / 5;
            });
        }

        [TestCase(new[] { 1.0, 3.0, 2.0 }, 0.0)]
        public void PolynomialDivisionTest_ThrowsArgumentException(double[] coefficients, double divider)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                PolynomialDivisionTest(coefficients, divider);
            });
        }

        [TestCase(new[] { 1.0, 3.0, 2.0 }, new[] { 1.0, 3.0, 2.0 }, ExpectedResult = true)]
        [TestCase(new[] { 1.0, 3.0, 2.0 }, new[] { 0.1 }, ExpectedResult = false)]        
        public bool PolynomialsEqualityOperatorTest(double[] coefficientsFirst, double[] coefficientsSecond)
        {
            var first = new Polynomial(coefficientsFirst);
            var second = new Polynomial(coefficientsSecond);
            return first == second;
        }        

        [TestCase(new[] { 1.0, 3.0, 2.0 }, new[] { 1.0, 3.0, 2.0 }, ExpectedResult = false)]
        [TestCase(new[] { 1.0, 3.0, 2.0 }, new[] { 0.1 }, ExpectedResult = true)]
        public bool PolynomialsNotEqualityOperatorTest(double[] coefficientsFirst, double[] coefficientsSecond)
        {
            var first = new Polynomial(coefficientsFirst);
            var second = new Polynomial(coefficientsSecond);
            return first != second;
        }

        #endregion

        #region Tests on public methods

        [TestCase(new[] { 1.0, 3.0, 2.0 }, 1.0, 6.0, ExpectedResult = true)]
        [TestCase(new[] { 0.0, 1.0, -4.0 }, -2.1, -19.74, ExpectedResult = true)]
        public bool PolynomialCalculateTest(double[] coefficients, double arg, double expected)
        {
            var polynomial = new Polynomial(coefficients);
            double eps = 0.0001d;
            return polynomial.Calculate(arg) - expected < eps;
        }

        #endregion

        #region Tests on overloaded methods of class Object

        [TestCase(new[] { 1.0, 3.0, 2.0 }, new[] { 1.0, 3.0, 2.0 }, ExpectedResult = true)]
        [TestCase(new[] { 1.0, 3.0, 2.0 }, new[] { 0.1 }, ExpectedResult = false)]
        public bool PolynomialsEqualsTest(double[] coefficientsFirst, double[] coefficientsSecond)
        {
            var first = new Polynomial(coefficientsFirst);
            var second = new Polynomial(coefficientsSecond);
            return first.Equals(second);
        }

        [TestCase(new[] { 1.1, -3.2, 2.3 }, ExpectedResult = "1,1 -3,2*x^1 + 2,3*x^2")]
        [TestCase(new[] { 0.1 }, ExpectedResult = "0,1")]
        public string PolynomialsToStringTest(double[] coefficients)
        {
            var polynomial = new Polynomial(coefficients);
            return polynomial.ToString();
        }

        [TestCase(new[] { 1.0, 3.0, 2.0 }, new[] { 1.0, 3.0, 2.0 }, ExpectedResult = true)]
        [TestCase(new[] { 1.0, 3.0, 2.0 }, new[] { 0.1 }, ExpectedResult = false)]
        public bool PolynomialsGetHashCodeTest(double[] coefficientsFirst, double[] coefficientsSecond)
        {
            var first = new Polynomial(coefficientsFirst);
            var second = new Polynomial(coefficientsSecond);
            return first.GetHashCode() == second.GetHashCode();
        }

        #endregion
    }
}
