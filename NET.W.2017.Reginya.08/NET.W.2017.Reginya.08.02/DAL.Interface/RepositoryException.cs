using System;

namespace DAL.Interface
{
    /// <inheritdoc />
    /// <summary>
    /// Thrown by the <see cref="IBankAccountRepository"/> when some error occuried in repository.
    /// </summary>
    public class RepositoryException : Exception
    {
        /// <inheritdoc />
        public RepositoryException()
        {
        }

        /// <inheritdoc />
        public RepositoryException(string message) :
            base(message)
        {
        }

        /// <inheritdoc />
        public RepositoryException(string message, Exception innerException) :
            base(message, innerException)
        {
        }
    }
}
