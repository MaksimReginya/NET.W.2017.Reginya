using System;
using System.Collections.Generic;

namespace BookLogic.Comparers
{
    /// <summary>
    /// Comparer to compare two books by their title
    /// </summary>
    public class TitleComparer : IComparer<Book>
    {
        /// <inheritdoc />
        /// <summary>
        /// Compare two books
        /// </summary>
        /// <param name="lhs"> The first book to compare. </param>
        /// <param name="rhs"> The second book to compare. </param>
        /// <returns>
        /// -1 if the title of first book is less than the second's, 0 if they are equal, 1 if greater.
        /// </returns>
        public int Compare(Book lhs, Book rhs)
        {
            if (ReferenceEquals(lhs, rhs))
            {
                return 0;
            }

            if (ReferenceEquals(lhs, null))
            {
                return -1;
            }

            if (ReferenceEquals(rhs, null))
            {
                return 1;
            }

            return lhs.Title.CompareTo(rhs.Title);
        }
    }
}
