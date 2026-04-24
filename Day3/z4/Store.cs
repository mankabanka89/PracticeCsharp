using System;

namespace StoreSystem
{
    class Store
    {
        private Order[] orders;

        public Store(Order[] orders)
        {
            this.orders = orders;
        }
                
        public Order[] GetHighValueOrders(decimal minAmount)
        {
            int count = 0;
                        
            for (int i = 0; i < orders.Length; i++)
            {
                if (orders[i].TotalAmount > minAmount)
                {
                    count++;
                }
            }
                        
            Order[] result = new Order[count];
            int index = 0;
                        
            for (int i = 0; i < orders.Length; i++)
            {
                if (orders[i].TotalAmount > minAmount)
                {
                    result[index] = orders[i];
                    index++;
                }
            }

            return result;
        }
                
        public Order[] GetOrdersByCustomer(string customerName)
        {
            int count = 0;
                        
            for (int i = 0; i < orders.Length; i++)
            {
                if (orders[i].CustomerName == customerName)
                {
                    count++;
                }
            }
                        
            Order[] result = new Order[count];
            int index = 0;
                        
            for (int i = 0; i < orders.Length; i++)
            {
                if (orders[i].CustomerName == customerName)
                {
                    result[index] = orders[i];
                    index++;
                }
            }

            return result;
        }
                
        public void ShowAllOrders()
        {
            Console.WriteLine("\n=== ВСЕ ЗАКАЗЫ ===");
            for (int i = 0; i < orders.Length; i++)
            {
                orders[i].Show();
            }
        }
                
        public void ShowOrders(Order[] ordersArray, string title)
        {
            Console.WriteLine($"\n{title}:");
            if (ordersArray.Length == 0)
            {
                Console.WriteLine("Заказы не найдены");
                return;
            }
            for (int i = 0; i < ordersArray.Length; i++)
            {
                ordersArray[i].Show();
            }
        }
    }
}