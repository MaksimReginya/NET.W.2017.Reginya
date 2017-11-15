using System;

namespace Timer
{
    /// <inheritdoc />
    /// <summary>
    /// Class that contains event data for Notification event of Notifier.
    /// </summary>
    public sealed class TimerEventArgs : EventArgs
    {
        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="TimerEventArgs"/> class.
        /// </summary>
        /// <param name="message">Message to send </param>
        /// <param name="eventOccurrenceTime">The time when event occurred.</param>
        /// <exception cref="ArgumentException">
        /// Thrown when message is null or whitespace.
        /// </exception>
        public TimerEventArgs(string message, DateTime eventOccurrenceTime)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentException(nameof(message) + "must not be null or whitespace.");
            }

            if (eventOccurrenceTime == null)
            {
                throw new ArgumentException(nameof(eventOccurrenceTime) + "must not be null.");
            }

            this.Message = message;
            this.EventOccurrenceTime = eventOccurrenceTime;
        }

        /// <summary>
        /// Message to send.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// The time when event occurred.
        /// </summary>
        public DateTime EventOccurrenceTime { get; }
    }
}
