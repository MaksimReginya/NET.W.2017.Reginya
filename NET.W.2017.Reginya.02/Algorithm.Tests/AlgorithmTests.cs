using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Algorithm;

namespace Algorithm.Tests
{
    [TestClass]
    public class AlgorithmTests
    {
        [TestMethod]
        public void InsertNumber_15insertin15from0to0_15returned()
        {
            // Arrange.
            int number1 = 15, number2 = 15;
            int i = 0, j = 0;
            int expected = 15;

            // Act.
            int actual = Algorithm.InsertNumber(number1, number2, i, j);

            // Assert.
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void InsertNumber_8insertin15from0to0_9returned()
        {
            // Arrange.
            int number1 = 8, number2 = 15;
            int i = 0, j = 0;
            int expected = 9;

            // Act.
            int actual = Algorithm.InsertNumber(number1, number2, i, j);

            // Assert.
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void InsertNumber_8insertin15from3to8_120returned()
        {
            // Arrange.
            int number1 = 8, number2 = 15;
            int i = 3, j = 8;
            int expected = 120;

            // Act.
            int actual = Algorithm.InsertNumber(number1, number2, i, j);

            // Assert.
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InsertNumber_1insertnegative2from0to0_exceptionreturned()
        {
            // Arrange.
            int number1 = 1, number2 = -2;
            int i = 0, j = 0;

            // Act.
            Algorithm.InsertNumber(number1, number2, i, j);

            // Assert.
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InsertNumber_8insert15from3tonegative8_exceptionreturned()
        {
            // Arrange.
            int number1 = 8, number2 = 15;
            int i = 3, j = -8;

            // Act.
            Algorithm.InsertNumber(number1, number2, i, j);

            // Assert.
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InsertNumber_8insert15from8to3_exceptionreturned()
        {
            // Arrange.
            int number1 = 8, number2 = 15;
            int i = 8, j = 3;

            // Act.
            Algorithm.InsertNumber(number1, number2, i, j);

            // Assert.
        }
    }
}
