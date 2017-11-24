using System;

namespace BLL.Interface.ServiceInterface
{
    /// <inheritdoc />
    /// <summary>
    /// Thrown by the <see cref="IBankAccountService"/> when some error occuried in service.
    /// </summary>
    public class BankAccountServiceException : Exception
    {
        /// <inheritdoc />
        public BankAccountServiceException()
        {
        }

        /// <inheritdoc />
        public BankAccountServiceException(string message) :
            base(message)
        {
        }

        /// <inheritdoc />
        public BankAccountServiceException(string message, Exception innerException) :
            base(message, innerException)
        {
        }
    }
}
