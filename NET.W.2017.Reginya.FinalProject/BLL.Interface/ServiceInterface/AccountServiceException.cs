using System;

namespace BLL.Interface.ServiceInterface
{
    /// <inheritdoc />
    /// <summary>
    /// Thrown by the <see cref="IAccountService"/> when some error occuried in service.
    /// </summary>
    public class AccountServiceException : Exception
    {
        /// <inheritdoc />
        public AccountServiceException()
        {
        }

        /// <inheritdoc />
        public AccountServiceException(string message) :
            base(message)
        {
        }

        /// <inheritdoc />
        public AccountServiceException(string message, Exception innerException) :
            base(message, innerException)
        {
        }
    }
}
