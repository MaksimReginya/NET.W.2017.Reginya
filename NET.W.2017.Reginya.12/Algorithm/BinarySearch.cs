using System;
using System.Collections.Generic;

namespace Algorithm
{
    /// <summary>
    /// Provides methods for searching the element in the array with binary search algorithm
    /// </summary>
    public static class BinarySearch
    {
        #region Public methods

        /// <summary>
        /// Searches for the element in the array
        /// </summary>
        /// <typeparam name="T">Type of elements of array</typeparam>
        /// <param name="array">Source array with elements in ascending order</param>
        /// <param name="element">Searched element</param>
        /// <param name="comparer">Compares two elements of the array.</param>
        /// <returns>Index of <paramref name="element"/>, or -1 if element can't be found</returns>
        public static int SearchElement<T>(T[] array, T element, IComparer<T> comparer)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            if (comparer == null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }

            return SearchElement(array, element, 0, array.Length - 1, comparer);
        }

        #endregion

        #region Private methods

        private static int SearchElement<T>(T[] array, T element, int left, int right, IComparer<T> comparer)
        {
            int mid = (left + (right - left)) / 2;
            if (left >= right)
            {
                return -1;
            }

            if (mid < left)
            {
                mid = left;
            }

            if (comparer.Compare(array[mid], element) == 0)
            {
                return mid;
            }

            if (comparer.Compare(array[mid], element) > 0)
            {
                return SearchElement(array, element, left, mid, comparer);
            }

            return SearchElement(array, element, mid + 1, right, comparer);
        }

        #endregion
    }
}
