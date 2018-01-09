using System;

namespace BLL.Interface.ServiceInterface
{
    /// <inheritdoc />
    /// <summary>
    /// Thrown by the <see cref="IBankManageService"/> when some error occuried in service.
    /// </summary>
    public class BankManageServiceException : Exception
    {
        /// <inheritdoc />
        public BankManageServiceException()
        {
        }

        /// <inheritdoc />
        public BankManageServiceException(string message) :
            base(message)
        {
        }

        /// <inheritdoc />
        public BankManageServiceException(string message, Exception innerException) :
            base(message, innerException)
        {
        }
    }
}
