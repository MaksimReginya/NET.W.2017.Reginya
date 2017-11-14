using NLog;

namespace BookLogic.Loggers
{
    /// <inheritdoc />
    /// <summary>
    /// Provides Error and Info methods of NLog
    /// </summary>
    public class NLogger : ILogger
    {
        #region Private fields
                
        private readonly Logger _logger;

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
        public void Error(string message)
        {
            _logger.Error(message);
        }

        /// <inheritdoc />
        public void Info(string message)
        {
            _logger.Info(message);
        }

        #endregion
    }
}
