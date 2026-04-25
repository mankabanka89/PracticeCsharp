using System;

namespace PublisherSubscriber
{
    public delegate void StatusChangedHandler(string orderId, string oldStatus, string newStatus);

    class OrderStatusManager
    {
        public event StatusChangedHandler StatusChanged;

        private string orderId;
        private string status;

        public OrderStatusManager(string orderId, string initialStatus)
        {
            this.orderId = orderId;
            this.status = initialStatus;
        }

        public void ChangeStatus(string newStatus)
        {
            string oldStatus = status;
            status = newStatus;

            if (StatusChanged != null)
            {
                StatusChanged(orderId, oldStatus, newStatus);
            }
        }
    }

    class CustomerNotifier
    {
        public void OnStatusChanged(string orderId, string oldStatus, string newStatus)
        {
            Console.WriteLine($"[ОПОВЕЩЕНИЕ КЛИЕНТА] Заказ {orderId}: статус изменён с '{oldStatus}' на '{newStatus}'");
        }
    }

    class AdminLogger
    {
        public void OnStatusChanged(string orderId, string oldStatus, string newStatus)
        {
            Console.WriteLine($"[ЛОГ АДМИНИСТРАТОРА] Заказ {orderId}: {oldStatus} -> {newStatus} | Время: {DateTime.Now:HH:mm:ss}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            OrderStatusManager order = new OrderStatusManager("ORD-001", "Новый");

            CustomerNotifier notifier = new CustomerNotifier();
            AdminLogger logger = new AdminLogger();

            order.StatusChanged += notifier.OnStatusChanged;
            order.StatusChanged += logger.OnStatusChanged;

            Console.WriteLine("--- Изменение статуса заказа ---\n");

            order.ChangeStatus("Оплачен");
            Console.WriteLine();

            order.ChangeStatus("В доставке");
            Console.WriteLine();

            order.ChangeStatus("Доставлен");

            Console.WriteLine("\nНажмите любую клавишу для завершения...");
            Console.ReadKey();
        }
    }
}