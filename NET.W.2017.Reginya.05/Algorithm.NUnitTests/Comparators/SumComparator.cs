using System.Collections.Generic;
using System.Linq;

namespace Algorithm.NUnitTests.Comparators
{
    /// <summary>
    /// Compares two arrays by their elements sums
    /// </summary>
    internal class SumComparator : IComparer<int[]>
    {
        private readonly bool _descending;

        /// <summary>
        /// Initializes a new instance of <see cref="SumComparator"/> class
        /// </summary>
        /// <param name="descending">Order of comparison</param>
        public SumComparator(bool descending)
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

            int sum1 = lhs.Sum();
            int sum2 = rhs.Sum();

            if (sum1 == sum2)
            {
                return 0;
            }

            if (sum1 > sum2)
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
