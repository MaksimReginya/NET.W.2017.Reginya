using System;
using System.Threading;
using Timer;

namespace TimerUI
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var notifier = new Notifier();
            var firstListener = new Listener();
            firstListener.Subscribe(notifier);
            notifier.NotifyOnTimer("Should be printed one time", 0); 
            Thread.Sleep(1000);

            var secondListener = new Listener();
            secondListener.Subscribe(notifier);
            var thirdListener = new Listener();
            thirdListener.Subscribe(notifier);
            notifier.NotifyOnTimer("Should be printed three times", new TimeSpan(0, 0, 0, 5));
            Thread.Sleep(6000);

            secondListener.Unsubscribe(notifier);
            notifier.NotifyOnTimer("Should be printed two times", 5);    
            
            Console.ReadKey();
        }
    }
}
