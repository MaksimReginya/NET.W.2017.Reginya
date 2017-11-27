namespace Logger.Interfaces
{
    /// <summary>
    /// Provides methods to log warn messages.
    /// </summary>
    public interface ILogger
    {       
        /// <summary>
        /// Logs warn message.
        /// </summary>
        /// <param name="message">Message to log.</param>
        void Warn(string message);
    }
}
