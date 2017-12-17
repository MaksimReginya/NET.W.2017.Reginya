using System;

namespace Task3.Solution
{
    public class Stock
    {
        public event EventHandler<StockInfoArgs> Notification = delegate { };

        private readonly StockInfoArgs _stocksInfo;        

        public Stock()
        {            
            _stocksInfo = new StockInfoArgs();
        }
                                
        public void Market()
        {
            var rnd = new Random();
            _stocksInfo.USD = rnd.Next(20, 40);
            _stocksInfo.Euro = rnd.Next(30, 50);
            OnNotification(this, _stocksInfo);
        }

        protected virtual void OnNotification(object sender, StockInfoArgs args)
        {
            EventHandler<StockInfoArgs> temp = Notification;
            temp?.Invoke(sender, args);
        }
    }
}
