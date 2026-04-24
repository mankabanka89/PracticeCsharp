using System;

namespace StoreSystem
{
    partial class Order
    {
        public void Show()
        {
            Console.WriteLine($"Заказ #{OrderID} | Клиент: {CustomerName} | Сумма: {TotalAmount} руб.");
        }
    }
}