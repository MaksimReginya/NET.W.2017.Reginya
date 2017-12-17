using Task3.Solution;

namespace Task3.Console
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var stock = new Stock();
            var alphaBank = new Bank("Alpha-bank");
            alphaBank.Subscribe(stock);
            var belarusBank = new Bank("Belarus-bank");
            belarusBank.Subscribe(stock);
            var broker1 = new Broker("Mark");
            broker1.Subscribe(stock);
            var broker2 = new Broker("Steve");
            broker2.Subscribe(stock);

            stock.Market();

            alphaBank.Unsubscribe(stock);

            stock.Market();

            System.Console.ReadKey();
        }
    }
}
