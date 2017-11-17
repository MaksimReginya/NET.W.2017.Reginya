using System;
using System.Collections.Generic;
using BookLogic.Exceptions;
using BookLogic.Loggers;
using BookLogic.Predicates;

namespace BookLogic
{
    /// <summary>
    /// Provides methods to work with list of books
    /// </summary>
    public class BookListService
    {
        #region Private fields
        
        private List<Book> _books;        

        #endregion

        #region Public constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BookListService"/> class.
        /// </summary>        
        /// <param name="books">The list of books</param>
        public BookListService(IEnumerable<Book> books = null)
        {
            _books = books == null ? new List<Book>() : new List<Book>(books);
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Logger to log all errors occurred in service
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// Count of books in service
        /// </summary>
        public int BooksCount => _books.Count;

        #endregion
        
        #region Public methods

        /// <summary>
        /// Adds the book to the book list.
        /// </summary>
        /// <param name="book"> The book to add to list. </param>
        /// <exception cref="ArgumentNullException">Thrown when book is null</exception>
        /// <exception cref="BookAlreadyInListException">Thrown when book is already in list</exception>
        public void AddBook(Book book)
        {
            if (book == null)
            {
                Logger?.Info("Unhandled ArgumentNullException:");
                Logger?.Error("Argument \"book\" in AddBook method can't be null.");
                throw new ArgumentNullException(nameof(book));
            }
            
            if (_books.Contains(book))
            {
                Logger?.Info("Unhandled BookAlreadyInListException:");
                Logger?.Error("The book passed to AddBook is already in list.");
                throw new BookAlreadyInListException(book);
            }

            _books.Add(book);
        }

        /// <summary>
        /// Removes the first occurrence of the book from the book list.
        /// </summary>
        /// <param name="book"> The book to remove from the book list. </param>
        /// <exception cref="ArgumentNullException">Thrown when book is null</exception>
        /// <exception cref="BookNotFoundException">Thrown when book can't be found in list</exception>
        public void RemoveBook(Book book)
        {
            if (book == null)
            {
                Logger?.Info("Unhandled ArgumentNullException:");
                Logger?.Error("Argument \"book\" in RemoveBook method can't be null.");
                throw new ArgumentNullException(nameof(book));
            }

            if (!_books.Remove(book))
            {
                Logger?.Info("Unhandled BookNotFoundException:");
                Logger?.Error("The book passed to RemoveBook can't be found in list.");
                throw new BookNotFoundException(book);
            }
        }

        /// <summary>
        /// Searches for the first element that matches the passed condition.
        /// </summary>
        /// <param name="condition">
        ///  The <see cref="IPredicate{Book}"/> instance that defines the condition.
        /// </param>
        /// <returns> The first element that matches the passed condition. </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when condition is null
        /// </exception>
        public Book FindBookByTag(IPredicate<Book> condition)
        {
            if (condition == null)
            {
                Logger?.Info("Unhandled ArgumentNullException:");
                Logger?.Error("Argument \"condition\" in FindBookByTag method can't be null.");
                throw new ArgumentNullException(nameof(condition));
            }

            return _books.Find(condition.IsSuitable);
        }

        /// <summary>
        /// Sorts the elements using the specified comparer.
        /// </summary>
        /// <param name="comparer" >
        ///  The <see cref="System.Collections.Generic.IComparer{Book}"/> instance that defines the comparer.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when comparer is null
        /// </exception>                
        public void SortBooksByTag(IComparer<Book> comparer)
        {
            if (comparer == null)
            {
                Logger?.Info("Unhandled ArgumentNullException:");
                Logger?.Error("Argument \"comparer\" in SortBooksByTag method can't be null.");
                throw new ArgumentNullException(nameof(comparer));
            }

            _books.Sort(comparer);
        }

        /// <summary>
        /// Saves list of books into <see cref="storage"/>.
        /// </summary>
        /// <param name="storage"> Storage of the list of books. </param>
        /// <exception cref="ArgumentNullException">Thrown when storage is null</exception>
        public void Save(IBookListStorage storage)
        {
            if (storage == null)
            {
                Logger?.Info("Unhandled ArgumentNullException:");
                Logger?.Error("Argument \"storage\" in Save method can't be null.");
                throw new ArgumentNullException(nameof(storage));
            }

            try
            {
                storage.Save(_books);
            }
            catch (Exception ex)
            {
                Logger?.Info("Unhandled Exception:");
                Logger?.Error("Some error while saving books to storage occuried.");
                throw;
            }
        }

        /// <summary>
        /// Load list of books from <see cref="storage"/>.
        /// </summary>
        /// <param name="storage"> Storage to load a list of books. </param>
        /// <exception cref="ArgumentNullException">Thrown when storage is null </exception>
        /// <exception cref="ArgumentException">Thrown when book list can't be loaded from storage</exception>
        public void Load(IBookListStorage storage)
        {
            if (storage == null)
            {
                Logger?.Info("Unhandled ArgumentNullException:");
                Logger?.Error("Argument \"storage\" in Load method can't be null.");
                throw new ArgumentNullException(nameof(storage));
            }

            try
            {
                _books = storage.Load() as List<Book>;
            }
            catch (Exception ex)
            {
                Logger?.Info("Unhandled Exception:");
                Logger?.Error("Some error while loading books from storage occuried.");
                throw;
            }            

            if (_books == null)
            {
                Logger?.Info("Unhandled ArgumentException:");
                Logger?.Error($"Can't load book list from {nameof(storage)}.");
                throw new ArgumentException($"Can't load book list from {nameof(storage)}.");
            }
        }

        /// <summary>
        /// Returns list of books.
        /// </summary>
        /// <returns> Returns <see cref="IEnumerable{Book}"/> of books. </returns>
        public IEnumerable<Book> GetBooks() => new List<Book>(_books);
        
        #endregion
    }
}
