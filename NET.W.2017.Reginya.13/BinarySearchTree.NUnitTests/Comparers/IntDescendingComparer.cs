using System.Collections.Generic;

namespace BinarySearchTree.NUnitTests.Comparers
{
    public class IntDescendingComparer : IComparer<int>
    {
        public int Compare(int lhs, int rhs)
        {
            return lhs < rhs ? 1 : lhs > rhs ? -1 : 0;
        }
    }
}
