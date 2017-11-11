using System.Collections.Generic;
using System.Linq;

namespace Algorithm.NUnitTests.Comparators
{
    /// <summary>
    /// Compares two arrays by their min elements
    /// </summary>
    internal class MinElementComparator : IComparer<int[]>
    {
        private readonly bool _descending;

        /// <summary>
        /// Initializes a new instance of <see cref="MinElementComparator"/> class
        /// </summary>
        /// <param name="descending">Order of comparison</param>
        public MinElementComparator(bool descending)
        {
            _descending = descending;
        }

        /// <summary>
        /// Compares two single dimensional arrays
        /// </summary>
        /// <param name="lhs">First array to compare</param>
        /// <param name="rhs">Second array to compare</param>
        /// <returns>
        /// Result of comparison
        /// </returns>
        public int Compare(int[] lhs, int[] rhs)
        {
            if (ReferenceEquals(lhs, rhs))
            {
                return 0;
            }

            if (lhs == null)
            {
                if (_descending)
                {
                    return 1;
                }

                return -1;                
            }

            if (rhs == null)
            {
                if (_descending)
                {
                    return -1;
                }

                return 1;
            }

            int min1 = lhs.Min();
            int min2 = rhs.Min();

            if (min1 == min2)
            {
                return 0;
            }

            if (min1 > min2)
            {
                if (_descending)
                {
                    return -1;
                }

                return 1;                
            }

            if (_descending)
            {
                return 1;
            }

            return -1;
        }
    }
}
