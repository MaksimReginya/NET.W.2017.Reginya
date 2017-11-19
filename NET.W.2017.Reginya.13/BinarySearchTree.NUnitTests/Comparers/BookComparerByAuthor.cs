using System.Collections.Generic;
using BookLogic;

namespace BinarySearchTree.NUnitTests.Comparers
{
    public class BookComparerByAuthor : IComparer<Book>
    {
        public int Compare(Book lhs, Book rhs)
        {
            return string.CompareOrdinal(lhs.Author, rhs.Author);
        }
    }
}
