using System;

namespace StoreSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
                        
            Order[] orders = new Order[5];

            orders[0] = new Order(1, "Иван Петров", 1500.50m);
            orders[1] = new Order(2, "Мария Сидорова", 3500.00m);
            orders[2] = new Order(3, "Иван Петров", 500.75m);
            orders[3] = new Order(4, "Алексей Иванов", 2800.30m);
            orders[4] = new Order(5, "Мария Сидорова", 1200.00m);
                        
            Store store = new Store(orders);
                        
            store.ShowAllOrders();

            Order[] expensiveOrders = store.GetHighValueOrders(2000);
            store.ShowOrders(expensiveOrders, "Заказы дороже 2000 рублей");
                        
            Order[] customerOrders = store.GetOrdersByCustomer("Иван Петров");
            store.ShowOrders(customerOrders, "Заказы клиента Иван Петров");

            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}