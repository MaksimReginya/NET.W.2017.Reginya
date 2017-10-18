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
            int[] expexted = new int[] {1, 2, 3, 4, 5};
            int[] actual = new int[] {3, 1, 2, 5, 4};

            //Act
            Sortings.QuickSort(ref actual, 0, actual.Length-1);

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
            Sortings.QuickSort(ref array, 0, 5);         
        }

        [TestMethod]
        public void MergeSort_IntArrayOf5El()
        {
            //Arrange
            int[] expexted = new int[] { 1, 2, 3, 4, 5 };
            int[] actual = new int[] { 3, 1, 2, 5, 4 };

            //Act
            actual = Sortings.MergeSort(actual);

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
