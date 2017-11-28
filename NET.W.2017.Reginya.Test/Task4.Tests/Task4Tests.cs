using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Task4.Solution;

namespace Task4.Tests
{
    [TestFixture]
    public class Task4Tests
    {
        private readonly List<double> _values = new List<double> { 10, 5, 7, 15, 13, 12, 8, 7, 4, 2, 9 };

        [Test]
        public void TestDelegate_AverageByMean()
        {            
            double expected = 8.3636363;

            double actual = Calculator.CalculateAverage(_values, data => data.Sum() / data.Count);

            Assert.AreEqual(expected, actual, 0.000001);
        }

        [Test]
        public void TestInterface_AverageByMean()
        {            
            double expected = 8.3636363;

            double actual = Calculator.CalculateAverage(_values, new MeanAveragingMethod());

            Assert.AreEqual(expected, actual, 0.000001);
        }

        [Test]
        public void TestDelegate_AverageByMedian()
        {            
            double expected = 8.0;

            double actual = Calculator.CalculateAverage(_values, new MedianAveragingMethod().CalculateAverageValue);

            Assert.AreEqual(expected, actual, 0.000001);
        }
        
        [Test]
        public void TestInterface_AverageByMedian()
        {            
            double expected = 8.0;

            double actual = Calculator.CalculateAverage(_values, new MedianAveragingMethod());

            Assert.AreEqual(expected, actual, 0.000001);
        }

        [Test]
        public void TestExtension_AverageByMean()
        {            
            double expected = 8.3636363;

            double actual = _values.CalculateAverage(new MeanAveragingMethod());

            Assert.AreEqual(expected, actual, 0.000001);
        }

        [Test]
        public void TestExtension_AverageByMedian()
        {            
            double expected = 8.0;

            double actual = _values.CalculateAverage(new MedianAveragingMethod());

            Assert.AreEqual(expected, actual, 0.000001);
        }        
    }
}