using System.Collections.Generic;

namespace Algorithm.Tests.Comparers
{
    /// <inheritdoc />
    /// <summary>
    /// Compares two string values
    /// </summary>
    public class StringComparer : IComparer<string>
    {
        /// <inheritdoc />        
        public int Compare(string lhs, string rhs)
        {
            return string.CompareOrdinal(lhs, rhs);
        }
    }
}
