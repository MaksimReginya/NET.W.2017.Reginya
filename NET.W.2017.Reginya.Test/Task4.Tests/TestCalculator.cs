using System;
using System.Collections.Generic;
using NUnit.Framework;
using Task4;
using Task4.Solution;

namespace Task4.Tests
{
    [TestFixture]
    public class TestCalculator
    {
        private readonly List<double> values = new List<double> { 10, 5, 7, 15, 13, 12, 8, 7, 4, 2, 9 };

        [Test]
        public void TestDelegate_AverageByMean()
        {
            var calculator = new Calculator();

            double expected = 8.3636363;

            double actual = calculator.CalculateAverage(values, new MeanAveragingMethod().CalculateAverageValue);

            Assert.AreEqual(expected, actual, 0.000001);
        }

        [Test]
        public void TestDelegate_AverageByMedian()
        {
            Calculator calculator = new Calculator();

            double expected = 8.0;

            double actual = calculator.CalculateAverage(values, new MedianAveragingMethod().CalculateAverageValue);

            Assert.AreEqual(expected, actual, 0.000001);
        }

        [Test]
        public void TestInterface_AverageByMean()
        {
            var calculator = new Calculator();

            double expected = 8.3636363;

            double actual = calculator.CalculateAverage(values, new MeanAveragingMethod());

            Assert.AreEqual(expected, actual, 0.000001);
        }

        [Test]
        public void TestInterface_AverageByMedian()
        {
            Calculator calculator = new Calculator();

            double expected = 8.0;

            double actual = calculator.CalculateAverage(values, new MedianAveragingMethod());

            Assert.AreEqual(expected, actual, 0.000001);
        }

        [Test]
        public void TestExtension_AverageByMean()
        {            
            double expected = 8.3636363;

            double actual = values.CalculateAverage(new MeanAveragingMethod());

            Assert.AreEqual(expected, actual, 0.000001);
        }

        [Test]
        public void TestExtension_AverageByMedian()
        {            
            double expected = 8.0;

            double actual = values.CalculateAverage(new MedianAveragingMethod());

            Assert.AreEqual(expected, actual, 0.000001);
        }        
    }
}