using System;
using System.Collections.Generic;

namespace BinarySearchTree.NUnitTests.Comparers
{
    public class StringComparerIgnoreCase : IComparer<string>
    {
        public int Compare(string lhs, string rhs)
        {            
            return string.Compare(lhs, 0, rhs, 0, lhs.Length, StringComparison.OrdinalIgnoreCase);
        }
    }
}
