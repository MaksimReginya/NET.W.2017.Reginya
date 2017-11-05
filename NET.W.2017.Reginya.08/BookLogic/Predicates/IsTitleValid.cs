using System;

namespace BookLogic.Predicates
{
    /// <inheritdoc />
    /// <summary>
    /// Predicate that is true if book's title contains specified string
    /// </summary>
    public class IsTitleValid : IPredicate<Book>
    {
        private readonly string _specifiedString;

        /// <summary>
        /// Initializes a new instance of <see cref="IsTitleValid"/> class
        /// </summary>
        /// <param name="specifiedString">String that will be searched in book's title</param>
        public IsTitleValid(string specifiedString)
        {
            _specifiedString = specifiedString;
        }

        /// <inheritdoc />
        /// <summary>Describes the state of the predicate</summary>        
        public bool IsTrue(Book book)
        {
            return book.Title.Contains(_specifiedString);
        }
    }
}
