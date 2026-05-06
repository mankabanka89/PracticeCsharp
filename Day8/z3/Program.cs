using System;
using System.Collections.Generic;

namespace GenericQueue
{
    public interface IQueue<T>
    {
        void Enqueue(T item);
        T Dequeue();
        T Peek();
    }

    class SimpleQueue<T> : IQueue<T>
    {
        private Queue<T> queue;

        public SimpleQueue()
        {
            queue = new Queue<T>();
        }

        public void Enqueue(T item)
        {
            queue.Enqueue(item);
            Console.WriteLine($"Добавлено: {item}");
        }

        public T Dequeue()
        {
            if (queue.Count == 0)
            {
                throw new InvalidOperationException("Очередь пуста. Невозможно удалить элемент.");
            }
            T item = queue.Dequeue();
            Console.WriteLine($"Удалено: {item}");
            return item;
        }

        public T Peek()
        {
            if (queue.Count == 0)
            {
                throw new InvalidOperationException("Очередь пуста. Невозможно просмотреть элемент.");
            }
            T item = queue.Peek();
            Console.WriteLine($"Первый элемент: {item}");
            return item;
        }

        public bool IsEmpty()
        {
            return queue.Count == 0;
        }

        public int Count
        {
            get { return queue.Count; }
        }
    }

    class QueueManager<T>
    {
        private IQueue<T> queue;

        public QueueManager(IQueue<T> queue)
        {
            this.queue = queue;
        }

        public void AddItem(T item)
        {
            queue.Enqueue(item);
        }

        public T RemoveItem()
        {
            return queue.Dequeue();
        }

        public T ViewFirstItem()
        {
            return queue.Peek();
        }

        public void PrintQueue()
        {
            Console.WriteLine($"\n--- Содержимое очереди ---");
            Console.WriteLine($"Количество элементов: {(queue as SimpleQueue<T>)?.Count ?? 0}");

            if ((queue as SimpleQueue<T>)?.Count == 0)
            {
                Console.WriteLine("Очередь пуста");
                return;
            }

            Console.WriteLine("Элементы очереди (от первого к последнему):");

            SimpleQueue<T> tempQueue = new SimpleQueue<T>();
            SimpleQueue<T> originalQueue = queue as SimpleQueue<T>;

            if (originalQueue == null)
            {
                Console.WriteLine("Не удалось получить содержимое очереди");
                return;
            }

            while (!originalQueue.IsEmpty())
            {
                T item = originalQueue.Dequeue();
                Console.WriteLine($"  {item}");
                tempQueue.Enqueue(item);
            }

            while (!tempQueue.IsEmpty())
            {
                originalQueue.Enqueue(tempQueue.Dequeue());
            }
        }

        public bool IsEmpty()
        {
            return queue is SimpleQueue<T> sq && sq.IsEmpty();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            SimpleQueue<string> queue = new SimpleQueue<string>();
            QueueManager<string> manager = new QueueManager<string>(queue);

            Console.WriteLine("--- Работа с обобщённой очередью ---\n");

            Console.WriteLine("--- Добавление элементов ---");
            manager.AddItem("Первый");
            manager.AddItem("Второй");
            manager.AddItem("Третий");
            manager.AddItem("Четвертый");

            manager.PrintQueue();

            Console.WriteLine("\n--- Просмотр первого элемента ---");
            manager.ViewFirstItem();

            Console.WriteLine("\n--- Удаление элементов ---");
            manager.RemoveItem();
            manager.RemoveItem();

            manager.PrintQueue();

            Console.WriteLine("\n--- Проверка очереди ---");
            Console.WriteLine($"Очередь пуста? {manager.IsEmpty()}");

            Console.WriteLine("\n--- Удаление оставшихся элементов ---");
            manager.RemoveItem();
            manager.RemoveItem();

            Console.WriteLine($"\nОчередь пуста? {manager.IsEmpty()}");

            manager.PrintQueue();

            Console.WriteLine("\n--- Работа с очередью чисел ---");
            SimpleQueue<int> intQueue = new SimpleQueue<int>();
            QueueManager<int> intManager = new QueueManager<int>(intQueue);

            intManager.AddItem(100);
            intManager.AddItem(200);
            intManager.AddItem(300);

            intManager.PrintQueue();

            Console.WriteLine($"\nПервый элемент: {intManager.ViewFirstItem()}");
            Console.WriteLine($"Удалён: {intManager.RemoveItem()}");

            intManager.PrintQueue();

            Console.WriteLine("\nНажмите любую клавишу для завершения...");
            Console.ReadKey();
        }
    }
}