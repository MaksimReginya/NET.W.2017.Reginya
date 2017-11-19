using System.Collections.Generic;

namespace BinarySearchTree.NUnitTests.Comparers
{
    public class PointComparer : IComparer<Point>
    {
        public int Compare(Point lhs, Point rhs)
        {
            return string.CompareOrdinal(lhs.ToString(), rhs.ToString());
        }
    }
}
