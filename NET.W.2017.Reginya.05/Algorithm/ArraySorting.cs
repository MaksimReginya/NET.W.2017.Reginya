using System;
using System.Collections.Generic;

namespace Algorithm
{
    /// <summary>
    /// Sorts rows in jagged array with bubble sort algorithm
    /// </summary>
    public static class ArraySorting
    {
        #region Public methods
        /// <summary>
        /// Performs bubble sort of array rows depending on the sort criterion
        /// </summary>
        /// <param name="array">Source array which rows we need to sort</param>        
        /// <param name="comparer">Comparer to compare rows of array</param>
        /// <exception cref="ArgumentNullException">
        /// Throws if the source array is null
        /// </exception>
        public static void BubbleRowsSort(int[][] array, IComparer<int[]> comparer)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

                        
            for (int i = 1; i < array.Length; i++)
                for (int j = 0; j < array.Length - i; j++)
                {
                    int comparison = comparer.Compare(array[j], array[j + 1]);

                    if (comparison > 0)                    
                        Swap(ref array[j], ref array[j + 1]);                    
                }                
        }

        #endregion

        #region Private methods

        private static void Swap(ref int[] row1, ref int[] row2)
        {
            int[] temp = row1;
            row1 = row2;
            row2 = temp;
        }       
        #endregion
    }
}
