using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.NUnitTests.Comparators
{
    /// <summary>
    /// Compares two arrays by their max elements
    /// </summary>
    internal class MaxElementComparator: IComparer<int []>
    {
        private readonly bool _descending;

        /// <summary>
        /// Initializes a new instance of <see cref="MaxElementComparator"/> class
        /// </summary>
        /// <param name="descending">Order of comparation</param>
        public MaxElementComparator(bool descending)
        {
            _descending = descending;
        }

        /// <summary>
        /// Compares two sz-arrays
        /// </summary>
        /// <param name="lhs">First array to compare</param>
        /// <param name="rhs">Second array to compare</param>
        /// <returns>
        /// Result of comparation
        /// </returns>
        public int Compare(int[] lhs, int[] rhs)
        {
            if (ReferenceEquals(lhs, rhs)) return 0;
            if (lhs == null)
                if (_descending) return 1;
                else return -1;

            if (rhs == null)
                if (_descending) return -1;
                else return 1;

            int max1 = lhs.Max();
            int max2 = rhs.Max();

            if (max1 == max2) return 0;

            if (max1 > max2)
                if (_descending) return -1;
                else return 1;
            if (_descending)
                return 1;
            return -1;
        }
    }
}
