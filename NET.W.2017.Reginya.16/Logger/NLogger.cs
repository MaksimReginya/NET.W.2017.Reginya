using NLog;

namespace Logger
{
    /// <inheritdoc />
    /// <summary>
    /// Provides Warn methods of NLog.
    /// </summary>
    public class NLogger : Interfaces.ILogger
    {
        #region Private fields
                
        private readonly NLog.Logger _logger;

        #endregion

        #region Public constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NLogger"/> class.
        /// </summary>
        /// <param name="className">Name of logging class.</param>
        public NLogger(string className)
        {
            _logger = LogManager.GetLogger(className);
        }

        #endregion

        #region ILogger implementation
                
        /// <inheritdoc />
        public void Warn(string message)
        {
            _logger.Warn(message);
        }

        #endregion
    }
}
