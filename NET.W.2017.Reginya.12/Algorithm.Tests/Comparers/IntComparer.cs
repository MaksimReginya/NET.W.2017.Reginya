using System.Collections.Generic;

namespace Algorithm.Tests.Comparers
{
    /// <inheritdoc />
    /// <summary>
    /// Compares two integer values
    /// </summary>
    public class IntComparer : IComparer<int>
    {
        /// <inheritdoc />
        public int Compare(int lhs, int rhs)
        {
            return lhs.CompareTo(rhs);
        }
    }
}
