using System;

namespace BookLogic
{
    /// <summary>
    /// Provides additional way to format the instance of Book class.
    /// </summary>
    public class CustomBookFormatter : IFormatProvider, ICustomFormatter
    {
        #region Private fields

        private const string SupportedFormat = "IAT";

        #endregion

        #region IFormatProvider implementation

        /// <inheritdoc />
        public object GetFormat(Type formatType) =>
            formatType == typeof(ICustomFormatter) ? this : null;

        #endregion

        #region ICustomFormatter implementation

        /// <inheritdoc />
        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            if (!formatProvider.Equals(this))
            {
                return null;
            }

            if (format.Trim().ToUpper() != SupportedFormat)
            {
                return null;
            }

            var book = arg as Book;
            if (ReferenceEquals(book, null))
            {
                return null;
            }

            return $"ISBN 13: {book.Isbn}, {book.Author}, {book.Title}";
        }

        #endregion         
    }
}
