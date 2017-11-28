using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3.Solution
{
    public class Broker
    {        
        public string Name { get; set; }

        public Broker(string name)
        {
            this.Name = name;                        
        }

        public void Subscribe(Stock notifier)
        {
            notifier.Notification += Update;
        }
        
        public void Unsubscribe(Stock notifier)
        {
            notifier.Notification -= Update;
        }

        public void Update(object sender, StockInfoArgs e)
        {            
            if (e.USD > 30)
                Console.WriteLine("Брокер {0} продает доллары;  Курс доллара: {1}", this.Name, e.USD);
            else
                Console.WriteLine("Брокер {0} покупает доллары;  Курс доллара: {1}", this.Name, e.USD);
        }       
    }
}
