using System;

namespace BookLogic.Exceptions
{
    /// <inheritdoc />
    /// <summary>
    /// The exception that is thrown when book passed to remove method can't be found in list.
    /// </summary>
    internal class BookNotFoundException: Exception
    {
        #region Public properties

        /// <summary>
        /// The book that can't be found in list.
        /// </summary>
        public Book NotFoundBook { get; }

        #endregion

        #region Public constructors

        /// <inheritdoc />
        /// <summary>
        ///  Initializes a new instance of <see cref="BookNotFoundException"/> class.
        /// </summary>
        /// <param name="book">The book that can't be found in list. </param>
        public BookNotFoundException(Book book) : base($"The book {book} can't be found in list.")
        {
            NotFoundBook = book;
        }

        #endregion
    }
}
