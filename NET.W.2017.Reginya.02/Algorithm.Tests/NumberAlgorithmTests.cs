﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Algorithm.Tests
{
    [TestClass]
    public class NumberAlgorithmTests
    {
        [TestMethod]
        public void InsertNumber_15insertIn15from0to0_15returned()
        {
            // Arrange.
            int number1 = 15, number2 = 15;
            int i = 0, j = 0;
            int expected = 15;

            // Act.
            int actual = NumberAlgorithm.InsertNumber(number1, number2, i, j);

            // Assert.
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void InsertNumber_8insertIn15from0to0_9returned()
        {
            // Arrange.
            int number1 = 8, number2 = 15;
            int i = 0, j = 0;
            int expected = 9;

            // Act.
            int actual = NumberAlgorithm.InsertNumber(number1, number2, i, j);

            // Assert.
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void InsertNumber_8insertIn15from3to8_120returned()
        {
            // Arrange.
            int number1 = 8, number2 = 15;
            int i = 3, j = 8;
            int expected = 120;

            // Act.
            int actual = NumberAlgorithm.InsertNumber(number1, number2, i, j);

            // Assert.
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InsertNumber_1insertNegative2from0to0_exceptionReturned()
        {
            // Arrange.
            int number1 = 1, number2 = -2;
            int i = 0, j = 0;

            // Act.
            NumberAlgorithm.InsertNumber(number1, number2, i, j);

            // Assert.
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InsertNumber_8insert15from3toNegative8_exceptionReturned()
        {
            // Arrange.
            int number1 = 8, number2 = 15;
            int i = 3, j = -8;

            // Act.
            NumberAlgorithm.InsertNumber(number1, number2, i, j);

            // Assert.
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InsertNumber_8insert15from8to3_exceptionReturned()
        {
            // Arrange.
            int number1 = 8, number2 = 15;
            int i = 8, j = 3;

            // Act.
            NumberAlgorithm.InsertNumber(number1, number2, i, j);

            // Assert.
        }        
    }
}
