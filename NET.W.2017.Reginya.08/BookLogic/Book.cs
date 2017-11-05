using System;
using System.Text.RegularExpressions;

namespace BookLogic
{
    public class Book : IComparable<Book>, IEquatable<Book>
    {
        #region Private fields

        private string _isbn;
        private string _author;
        private string _title;
        private string _publisher;
        private int _publishingYear;
        private int _pagesCount;
        private double _cost;

        #endregion

        #region Public properties
                
        public string Isbn
        {
            get => _isbn;
            set
            {
                if (IsCorrectIsbn(value))
                    _isbn = string.Copy(value);
            }
        }
        public string Author
        {
            get => _author;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(nameof(Author) + " can not be empty.");
                _author = value;
            }
        }
        public string Title
        {
            get => _title;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(nameof(Title) + " can not be empty.");
                _title = value;
            }
        }
        public string Publisher
        {
            get => _publisher;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(nameof(Publisher) + " can not be empty.");
                _publisher = value;
            }
        }
        public int PublishingYear
        {
            get => _publishingYear;
            set
            {
                if (value < 0 || value > DateTime.Now.Year)
                    throw new ArgumentException(nameof(PublishingYear) + " can not be negative or in future.");
                _publishingYear = value;
            }
        }
        public int PagesCount
        {
            get => _pagesCount;
            set
            {
                if (value <= 0)
                    throw new ArgumentException(nameof(PagesCount) + " can must be positive.");
                _pagesCount = value;
            }
        }
        public double Cost
        {
            get => _cost;
            set
            {
                if (value <= 0)
                    throw new ArgumentException(nameof(Cost) + " can must be positive.");
                _cost = value;
            }
        }

        #endregion

        #region Public constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Book"/> class.
        /// </summary>        
        public Book(string isbn, string author, string title, string publisher,
                    int publishingYear, int pagesCount, double cost)
        {
            Isbn = isbn;
            Author = author;
            Title = title;
            Publisher = publisher;
            PublishingYear = publishingYear;
            PagesCount = pagesCount;
            Cost = cost;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Determines if value is valid ISBN code
        /// </summary>
        /// <param name="value">Code to check</param>
        /// <returns>
        /// True if code is valid, false - if not.
        /// </returns>
        public bool IsCorrectIsbn(string value)
        {
            if (value.Length != 17) return false;
            var regex = new Regex(@"[0-9]+-");
            return regex.IsMatch(value);
        }
        
        #endregion

        #region Overloaded methods of class Object 

        /// <summary>
        /// Determines whether the specified instance of <see cref="Book"/>
        /// class is equal to the current instance
        /// </summary>
        /// <param name="other">Book to compare with the current instance</param>
        /// <returns>Result of comparasion</returns>
        public override bool Equals(object other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            if (other.GetType() != GetType()) return false;

            return Equals((Book)other);
        }

        /// <summary>
        /// Returns the hash code for this instance of <see cref="Book"/> class
        /// </summary>
        /// <returns>The hash code for this instance</returns>
        public override int GetHashCode() => Isbn.GetHashCode();

        /// <summary>
        /// Converts the value of this instance to a <see cref="string"/>
        /// </summary>
        /// <returns>String representation of book</returns>
        public override string ToString()
        {
            return $"Title: {Title}, Author: {Author}, Publisher: {Publisher}, Publishing year: {PublishingYear}, " +
                   $"ISBN: {Isbn}, Pages count: {PagesCount}, Cost: {Cost}.";
        }

        #endregion

        #region Interfaces implementation

        /// <inheritdoc />
        /// <summary>
        /// Compares two books on equality
        /// </summary>
        /// <param name="other">Book to compare with the current instance</param>  
        /// <returns>Result of comparasion</returns>
        public bool Equals(Book other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return Isbn.Equals(other.Isbn) && Author.Equals(other.Author) &&
                   Publisher.Equals(other.Publisher) && PublishingYear.Equals(other.PublishingYear) &&
                   PagesCount.Equals(other.PagesCount) && Cost.Equals(other.Cost) &&
                   Title.Equals(other.Title);
        }

        /// <inheritdoc />
        /// <summary>
        /// Compares two books by title ignoring case.
        /// </summary>
        /// <param name="other">Book for comparison.</param>
        /// <returns>Greater than 0, if the current book is larger, 0 if equal, -1 if less.</returns>
        public int CompareTo(Book other)
        {            
           return ReferenceEquals(other, null) ? 1 :
                string.Compare(Title, other.Title, StringComparison.OrdinalIgnoreCase);
        }

        #endregion
        
    }
}
