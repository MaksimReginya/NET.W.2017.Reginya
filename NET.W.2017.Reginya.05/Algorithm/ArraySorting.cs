using System;

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
        /// <param name="sortCriterion">The criterion by which sorting should be done</param>
        /// <param name="descending">Indicates that rows should be sorted in descending/ascending order</param>
        /// <exception cref="ArgumentNullException">
        /// Throws if the source array is null
        /// </exception>
        public static void BubbleRowsSort(int[][] array, SortCriterionProvider.SortCriterion sortCriterion =
            SortCriterionProvider.SortCriterion.RowElementsSum, bool descending = false)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            var criterionProvider = new SortCriterionProvider();
            var getCriterion = criterionProvider.GetCriterionDelegate(sortCriterion);
            for (int i = 1; i < array.Length; i++)            
                for (int j = 0; j < array.Length - i; j++)
                    if (IsNeedSwap(getCriterion(array[j]), getCriterion(array[j + 1]), descending))
                        Swap(ref array[j], ref array[j + 1]);            
        }

        #endregion

        #region Private methods

        private static void Swap(ref int[] row1, ref int[] row2)
        {
            int[] temp = row1;
            row1 = row2;
            row2 = temp;
        }

        private static bool IsNeedSwap(int el1, int el2, bool descending)
        {
            if (descending)
                return el1 < el2;

            return el1 > el2;
        }

        #endregion
    }
}
