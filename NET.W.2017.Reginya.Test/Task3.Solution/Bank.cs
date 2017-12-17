using System;

namespace Task3.Solution
{
    public class Bank
    {        
        public string Name { get; set; }

        public Bank(string name)
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
            if (e.Euro > 40)
                Console.WriteLine("Банк {0} продает евро;  Курс евро: {1}", this.Name, e.Euro);
            else
                Console.WriteLine("Банк {0} покупает евро;  Курс евро: {1}", this.Name, e.Euro);
        }
    }
}
