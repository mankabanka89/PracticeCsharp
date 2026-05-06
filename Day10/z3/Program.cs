using System;
using System.Collections.Generic;

namespace ObserverPatternTask
{
    public interface IStockObserver
    {
        void Update(string stockSymbol, decimal newPrice);
    }

    public class StockMarket
    {
        private readonly Dictionary<string, decimal> _stocks = new Dictionary<string, decimal>();
        private readonly List<IStockObserver> _observers = new List<IStockObserver>();

        public void RegisterObserver(IStockObserver observer)
        {
            _observers.Add(observer);
        }

        public void RemoveObserver(IStockObserver observer)
        {
            _observers.Remove(observer);
        }

        public void UpdatePrice(string stockSymbol, decimal newPrice)
        {
            _stocks[stockSymbol] = newPrice;
            NotifyObservers(stockSymbol, newPrice);
        }

        private void NotifyObservers(string stockSymbol, decimal newPrice)
        {
            foreach (var observer in _observers)
            {
                observer.Update(stockSymbol, newPrice);
            }
        }
    }

    public class Investor : IStockObserver
    {
        private readonly string _name;

        public Investor(string name)
        {
            _name = name;
        }

        public void Update(string stockSymbol, decimal newPrice)
        {
            Console.WriteLine($"Инвестор {_name} уведомлен: цена акции {stockSymbol} изменилась до {newPrice:C}");
        }
    }

    class Program
    {
        static void Main()
        {
            StockMarket stockMarket = new StockMarket();

            Investor investor1 = new Investor("Алексей");
            Investor investor2 = new Investor("Мария");

            stockMarket.RegisterObserver(investor1);
            stockMarket.RegisterObserver(investor2);

            stockMarket.UpdatePrice("AAPL", 150.25m);
            stockMarket.UpdatePrice("MSFT", 300.50m);

            stockMarket.RemoveObserver(investor1);

            Console.WriteLine("\nПосле отписки Алексея:");
            stockMarket.UpdatePrice("TSLA", 700.00m);
        }
    }
}
