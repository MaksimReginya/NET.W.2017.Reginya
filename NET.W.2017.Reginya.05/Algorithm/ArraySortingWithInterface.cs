using System;
using System.Collections.Generic;

namespace Algorithm
{
    /// <summary>
    /// Sorts rows in jagged array with bubble sort algorithm
    /// </summary>
    public static class ArraySortingWithInterface
    {
        #region Public methods

        /// <summary>
        /// Performs bubble sort of array rows depending on the sort criterion
        /// </summary>
        /// <param name="array">Source array which rows we need to sort</param>        
        /// <param name="comparer">Comparer to compare rows of array</param>
        /// <exception cref="ArgumentNullException">
        /// Throws if the source array or comparer are null
        /// </exception>
        public static void BubbleRowsSort(int[][] array, IComparer<int[]> comparer)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (comparer == null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }

            for (int i = 1; i < array.Length; i++)
            {
                for (int j = 0; j < array.Length - i; j++)
                {
                    int comparison = comparer.Compare(array[j], array[j + 1]);

                    if (comparison > 0)
                    {
                        Swap(ref array[j], ref array[j + 1]);
                    }
                }
            }
        }

        /// <summary>
        /// Performs bubble sort of array rows depending on the sort criterion
        /// </summary>
        /// <param name="array">Source array which rows we need to sort</param>        
        /// <param name="comparer">Comparison delegate to compare rows of array</param>
        /// <exception cref="ArgumentNullException">
        /// Throws if the source array is null
        /// </exception>
        public static void BubbleRowsSort(int[][] array, Comparison<int[]> comparer)
            => BubbleRowsSort(array, new DelegateToInterfaceAdapter(comparer));

        #endregion

        #region Private methods

        /// <summary>
        /// Swaps two single dimensional arrays
        /// </summary>
        /// <param name="lhs">First array</param>
        /// <param name="rhs">Second array</param>
        private static void Swap(ref int[] lhs, ref int[] rhs)
        {
            int[] temp = lhs;
            lhs = rhs;
            rhs = temp;
        }   
        
        #endregion
    }
}
