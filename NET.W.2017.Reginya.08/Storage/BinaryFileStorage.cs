using System;
using System.Collections.Generic;
using System.IO;

using BookLogic;

namespace Storage
{
    public class BinaryFileStorage : IBookListStorage
    {
        private readonly string _path;

        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryFileStorage"/> class.
        /// </summary>
        /// <param name="path"> The path to the file. </param>
        public BinaryFileStorage(string path)
        {
            _path = path;
        }

        /// <inheritdoc />
        /// <summary>
        /// Saves list of books in the binary file storage.
        /// </summary>
        /// <param name="books"> List of books to save. </param>   
        /// <exception cref="ArgumentNullException">Thrown when book list is null</exception>     
        public void Save(IEnumerable<Book> books)
        {
            if (books == null)
                throw new ArgumentNullException(nameof(books));

            using (var writer = new BinaryWriter(File.Open(_path, FileMode.Create)))
            {
                foreach (var book in books)
                {
                    writer.Write(book.Isbn);
                    writer.Write(book.Author);
                    writer.Write(book.Title);
                    writer.Write(book.Publisher);
                    writer.Write(book.PublishingYear);
                    writer.Write(book.PagesCount);
                    writer.Write(book.Cost);
                }
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Loads list of books from a binary file storage.
        /// </summary>
        /// <returns> List of books from the binary storage.</returns>
        /// <exception cref="FileNotFoundException">Thrown when storage can't be found </exception>
        public IEnumerable<Book> Load()
        {
            if (!File.Exists(_path))
                throw new FileNotFoundException($"Binary file storage can't be found at {_path}.");

            var books = new List<Book>();

            using (var reader = new BinaryReader(File.Open(_path, FileMode.Open)))
            {
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    string isbn = reader.ReadString();
                    string author = reader.ReadString();
                    string title = reader.ReadString();
                    string publisher = reader.ReadString();
                    int publishingYear = reader.ReadInt32();
                    int pagesCount = reader.ReadInt32();
                    double cost = reader.ReadDouble();

                    books.Add(new Book(isbn, author, title, publisher, publishingYear, pagesCount, cost));
                }
            }

            return books;
        }
    }
}
