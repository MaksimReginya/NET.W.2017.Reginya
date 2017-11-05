using System;
using System.Collections.Generic;

namespace BookLogic
{
    /// <summary>
    /// Interface to work with book storage
    /// </summary>
    public interface IBookListStorage
    {
        /// <summary>
        /// Saves list of books in specified storage.
        /// </summary>
        /// <param name="books"> List of books to save. </param>
        void Save(IEnumerable<Book> books);

        /// <summary>
        /// Loads list of books from a specified storage.
        /// </summary>
        /// <returns> Loaded list of books. </returns>        
        IEnumerable<Book> Load();
    }
}
