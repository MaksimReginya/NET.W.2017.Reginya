using System;

namespace Sortings
{
    /// <summary>
    /// Provides methods to sort single dimensioned array of integer numbers in ascending order
    /// </summary>
    public class Sortings
    {
        #region Public methods

        /// <summary>
        /// Sorts array using quick sort algorithm
        /// </summary>
        /// <param name="array">Source array to sort</param>
        /// <exception cref="ArgumentNullException">
        /// Throws if array is null
        /// </exception>
        public static void QuickSort(int[] array)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (array.Length < 2)
            {
                return;
            }

            RecursiveQuickSort(array, 0, array.Length - 1);
        }

        /// <summary>
        /// Sorts array using merge sort algorithm
        /// </summary>
        /// <param name="array">Source array to sort</param>
        /// <exception cref="ArgumentNullException">
        /// Throws if array is null
        /// </exception>        
        public static void MergeSort(int[] array)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            RecursiveMergeSort(array, 0, array.Length);
        }

        #endregion

        #region Private methods

        private static void RecursiveQuickSort(int[] array, int start, int end)
        {
            int index = start + ((end - start) / 2);
            int pivot = array[index];

            int i = start;
            int j = end;
            while (i <= j)
            {
                while (array[i] < pivot && i <= end)
                {
                    ++i;
                }

                while (array[j] > pivot && j >= start)
                {
                    --j;
                }

                if (i <= j)
                {
                    int temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;
                    i++;
                    j--;
                }
            }

            if (i < end)
            {
                RecursiveQuickSort(array, i, end);
            }

            if (j > start)
            {
                RecursiveQuickSort(array, start, j);
            }
        }

        private static void RecursiveMergeSort(int[] array, int left, int right)
        {
            if (left + 1 >= right)
            {
                return;
            }

            int middle = (right + left) / 2;

            RecursiveMergeSort(array, left, middle);
            RecursiveMergeSort(array, middle, right);

            Merge(array, left, middle, right);
        }

        private static void Merge(int[] array, int left, int middle, int right)
        {
            int currentLeft = left;
            int currentRight = middle;
            int currentBufferIndex = 0;

            int[] buffer = new int[right - left];

            while (currentLeft < middle && currentRight < right)
            {
                if (array[currentLeft] <= array[currentRight])
                {
                    buffer[currentBufferIndex++] = array[currentLeft++];
                }
                else
                {
                    buffer[currentBufferIndex++] = array[currentRight++];
                }
            }

            if (currentRight == right)
            {
                while (currentLeft < middle)
                {
                    buffer[currentBufferIndex++] = array[currentLeft++];
                }
            }
            else
            {
                while (currentRight < right)
                {
                    buffer[currentBufferIndex++] = array[currentRight++];
                }
            }

            currentBufferIndex = 0;
            for (int i = left; i < right; i++)
            {
                array[i] = buffer[currentBufferIndex++];
            }
        }

        #endregion
    }
}
