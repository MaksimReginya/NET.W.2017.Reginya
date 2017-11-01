using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sorting;

namespace SortingsTest
{
    [TestClass]
    public class SortingsTest
    {
        [TestMethod]
        public void QuickSort_IntArrayOf5El()
        {            
            //Arrange
            var expexted = new [] {1, 2, 3, 4, 5};
            var actual = new [] {3, 1, 2, 5, 4};

            //Act
            Sortings.QuickSort(actual);

            //Assert
            Assert.IsTrue(expexted.SequenceEqual(actual));
        }

        [TestMethod]
        public void QuickSort_IntArrayOfNegativeAndBigEl()
        {
            //Arrange
            var expexted = new[] { int.MinValue, -333, 0, 1, 9999, int.MaxValue };
            var actual = new[] { 9999, int.MaxValue, -333, int.MinValue, 0, 1 };

            //Act
            Sortings.QuickSort(actual);

            //Assert
            Assert.IsTrue(expexted.SequenceEqual(actual));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void QuickSort_NullArray()
        {
            //Arrange
            int[] array = null;

            //Act
            Sortings.QuickSort(array);         
        }

        [TestMethod]
        public void MergeSort_IntArrayOf5El()
        {
            //Arrange
            var expexted = new [] { 1, 2, 3, 4, 5 };
            var actual = new [] { 3, 1, 2, 5, 4 };

            //Act
            Sortings.MergeSort(actual);

            //Assert            
            Assert.IsTrue(expexted.SequenceEqual(actual));
        }

        [TestMethod]
        public void MergeSort_IntArrayOfNegativeAndBigEl()
        {
            //Arrange
            var expexted = new[] { int.MinValue, -333, 0, 1, 9999, int.MaxValue };
            var actual = new[] { 9999, int.MaxValue, -333, int.MinValue, 0, 1 };

            //Act
            Sortings.MergeSort(actual);

            //Assert
            Assert.IsTrue(expexted.SequenceEqual(actual));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MergeSort_NullArray()
        {
            //Arrange
            int[] array = null;

            //Act
            Sortings.MergeSort(array);
        }
    }
}
