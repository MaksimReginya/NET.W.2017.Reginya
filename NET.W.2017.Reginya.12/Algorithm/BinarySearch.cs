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
            => SearchElement(array, element, 0, array?.Length - 1 ?? -1, comparer == null ? GetDefaultComparer<T>() : comparer.Compare);

        /// <summary>
        /// Searches for the element in the array
        /// </summary>
        /// <typeparam name="T">Type of elements of array</typeparam>
        /// <param name="array">Source array with elements in ascending order</param>
        /// <param name="element">Searched element</param>
        /// <param name="left">Start index from which element is finding</param>
        /// <param name="right">End index up to which element is finding</param>
        /// <param name="comparer">Compares two elements of the array.</param>
        /// <returns>Index of <paramref name="element"/>, or -1 if element can't be found</returns>
        public static int SearchElement<T>(T[] array, T element, int left, int right, IComparer<T> comparer)
            => SearchElement(array, element, left, right, comparer == null ? GetDefaultComparer<T>() : comparer.Compare);        

        /// <summary>
        /// Searches for the element in the array
        /// </summary>
        /// <typeparam name="T">Type of elements of array</typeparam>
        /// <param name="array">Source array with elements in ascending order</param>
        /// <param name="element">Searched element</param>
        /// <param name="comparer">Compares two elements of the array.</param>
        /// <returns>Index of <paramref name="element"/>, or -1 if element can't be found</returns>
        public static int SearchElement<T>(T[] array, T element, Comparison<T> comparer)
            => SearchElement(array, element, 0, array?.Length - 1 ?? -1, comparer);

        /// <summary>
        /// Searches for the element in the array
        /// </summary>
        /// <typeparam name="T">Type of elements of array</typeparam>
        /// <param name="array">Source array with elements in ascending order</param>
        /// <param name="element">Searched element</param>
        /// <param name="left">Start index from which element is finding</param>
        /// <param name="right">End index up to which element is finding</param>
        /// <param name="comparer">Compares two elements of the array.</param>
        /// <returns>Index of <paramref name="element"/>, or -1 if element can't be found</returns>
        public static int SearchElement<T>(T[] array, T element, int left, int right, Comparison<T> comparer)
        {
            VerifyInput(array, element, left, right, comparer);

            return RecursiveSearch<T>(array, element, left, right, comparer);            
        }

        #endregion

        #region Private methods

        private static int RecursiveSearch<T>(T[] array, T element, int left, int right, Comparison<T> comparer)
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

            if (comparer(array[mid], element) == 0)
            {
                return mid;
            }

            if (comparer(array[mid], element) > 0)
            {
                return RecursiveSearch(array, element, left, mid, comparer);
            }

            return RecursiveSearch(array, element, mid + 1, right, comparer);
        }

        private static void VerifyInput<T>(T[] array, T element, int left, int right, Comparison<T> comparer)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }          

            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            if (left < 0)
            {
                throw new ArgumentException($"{nameof(left)} must be >= 0", nameof(left));
            }

            if (right < 0)
            {
                throw new ArgumentException($"{nameof(right)} must be >= 0", nameof(right));
            }

            if (left > right)
            {
                throw new ArgumentException($"{nameof(left)} must be <= {nameof(right)}");
            }

            if (right >= array.Length)
            {
                throw new ArgumentException($"{nameof(right)} must be lower than array length", nameof(right));
            }

            if (comparer == null)
            {
                comparer = GetDefaultComparer<T>();
            }
        }

        private static Comparison<T> GetDefaultComparer<T>()
        {
            if (typeof(T) == typeof(string))
            {
                return (StringComparer.CurrentCulture as IComparer<T>).Compare;
            }

            if (typeof(T).GetInterface("IComparable`1") != null || typeof(T).GetInterface("IComparable") != null)
            {
                return Comparer<T>.Default.Compare;
            }
            
            throw new ArgumentNullException("comparer", "Comparer must be specified for types without IComparable.");            
        }           

        #endregion
    }
}
