using System;

namespace Timer
{
    /// <summary>
    /// Listener that subscribes to the notifications from notifier.
    /// </summary>
    public class Listener
    {
        #region Public methods
                
        /// <summary>
        /// Subscribes to notifications from notifier.
        /// </summary>
        /// <param name="notifier">Notifier that performs notifications.</param>
        public void Subscribe(Notifier notifier)
        {
            notifier.Notification += PrintInfo;
        }

        /// <summary>
        /// Unsubscribes to stop receiving notifications from notifier.
        /// </summary>
        /// <param name="notifier">Notifier that performs notifications.</param>
        public void Unsubscribe(Notifier notifier)
        {
            notifier.Notification -= PrintInfo;
        }

        #endregion

        #region Private methods

        private static void PrintInfo(object sender, TimerEventArgs e)
        {
            Console.WriteLine($"Message: {e.Message}, Occurring time: {e.EventOccurrenceTime.ToLocalTime()}.");
        }

        #endregion
    }
}
