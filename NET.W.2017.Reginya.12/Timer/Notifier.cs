using System;
using System.Threading;

namespace Timer
{
    /// <summary>
    /// Notifies all listeners that event occurred.
    /// </summary>
    public class Notifier
    {
        #region Events

        /// <summary>
        /// Notifies listeners when that event invoked.
        /// </summary>
        public event EventHandler<TimerEventArgs> Notification = delegate { };

        #endregion        

        #region Public methods

        /// <summary>
        /// Starts the timer and, at it ends, invokes the event.
        /// </summary>
        /// <param name="message">Message to transfer to listeners.</param>
        /// <param name="interval">Timer's interval</param>
        public void NotifyOnTimer(string message, TimeSpan interval)
            => NotifyOnTimer(message, interval.Seconds);

        /// <summary>
        /// Starts the timer and, at it ends, invokes the event.
        /// </summary>
        /// <param name="message">Message to transfer to listeners.</param>
        /// <param name="seconds">Timer's interval in seconds.</param>
        public void NotifyOnTimer(string message, int seconds)
        {            
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentException($"{nameof(message)} can't be null or whitespace.");
            }

            var timerCallback = new TimerCallback(OnTimerCallback);            
            var timer = new System.Threading.Timer(timerCallback, message, seconds * 1000, Timeout.Infinite);
        }

        #endregion

        #region Private methods

        private void OnTimerCallback(object message)
        {
            OnNotification(this, new TimerEventArgs(message as string, DateTime.Now));
        }

        private void OnNotification(object sender, TimerEventArgs args)
        {
            EventHandler<TimerEventArgs> temp = Notification;
            temp?.Invoke(sender, args);
        }        

        #endregion        
    }
}
