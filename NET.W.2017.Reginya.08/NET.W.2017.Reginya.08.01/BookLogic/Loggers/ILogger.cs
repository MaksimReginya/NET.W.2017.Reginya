namespace BookLogic.Loggers 
{
    /// <summary>
    /// Provides methods to log info and error messages
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Logs error message
        /// </summary>
        /// <param name="message">Message to log</param>
        void Error(string message);

        /// <summary>
        /// Logs info message
        /// </summary>
        /// <param name="message">Message to log</param>
        void Info(string message);
    }
}
