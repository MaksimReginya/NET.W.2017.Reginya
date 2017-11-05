using System;

namespace BookLogic.Exceptions
{
    /// <inheritdoc />
    /// <summary>
    /// The exception that is thrown when book passed to add method is already in list.
    /// </summary>
    internal class BookAlreadyInListException: Exception
    {
        #region Public properties

        /// <summary>
        /// The book that is already in list.
        /// </summary>
        public Book DuplicatingBook { get; }

        #endregion

        #region Public constructors

        /// <inheritdoc />
        /// <summary>
        ///  Initializes a new instance of <see cref="BookAlreadyInListException"/> class.
        /// </summary>
        /// <param name="book">The book that is already in list. </param>
        public BookAlreadyInListException(Book book) : base($"The book {book} is already in list.")
        {
            DuplicatingBook = book;
        }        
        
        #endregion
    }
}
