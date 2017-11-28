using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3.Solution
{
    public class Stock
    {
        public event EventHandler<StockInfoArgs> Notification = delegate { };

        private StockInfoArgs stocksInfo;        

        public Stock()
        {            
            stocksInfo = new StockInfoArgs();
        }
                                
        public void Market()
        {
            Random rnd = new Random();
            stocksInfo.USD = rnd.Next(20, 40);
            stocksInfo.Euro = rnd.Next(30, 50);
            OnNotification(this, stocksInfo);
        }

        protected virtual void OnNotification(object sender, StockInfoArgs args)
        {
            EventHandler<StockInfoArgs> temp = Notification;
            temp?.Invoke(sender, args);
        }
    }
}
