using System;
using System.Collections;
using System.Collections.Generic;

namespace TaskProcessingSystem
{
    class Task
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Priority { get; set; }

        public Task(int id, string title, int priority)
        {
            Id = id;
            Title = title;
            Priority = priority;
        }

        public override string ToString()
        {
            return $"ID: {Id}, Название: {Title}, Приоритет: {Priority}";
        }
    }

    class TaskManager
    {
        private Queue<Task> taskQueue;

        public TaskManager()
        {
            taskQueue = new Queue<Task>();
        }

        public void AddTask(Task task)
        {
            if (task == null)
            {
                Console.WriteLine("Ошибка: задача не может быть пустой");
                return;
            }
            taskQueue.Enqueue(task);
            Console.WriteLine($"Задача добавлена: {task}");
        }

        public void ProcessTask()
        {
            if (taskQueue.Count == 0)
            {
                Console.WriteLine("Нет задач для обработки");
                return;
            }

            Task processedTask = taskQueue.Dequeue();
            Console.WriteLine($"Задача обработана: {processedTask}");
        }

        public void GetPendingTasks()
        {
            if (taskQueue.Count == 0)
            {
                Console.WriteLine("Ожидающих задач нет");
                return;
            }

            Console.WriteLine($"\nОжидающих задач: {taskQueue.Count}");
            Console.WriteLine("Список ожидающих задач:");

            foreach (Task task in taskQueue)
            {
                Console.WriteLine($"  {task}");
            }
        }

        public void ShowQueueInfo()
        {
            Console.WriteLine($"\n=== Информация об очереди ===");
            Console.WriteLine($"Всего задач в очереди: {taskQueue.Count}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            TaskManager manager = new TaskManager();

            Task task1 = new Task(1, "Сделать отчет", 1);
            Task task2 = new Task(2, "Позвонить клиенту", 2);
            Task task3 = new Task(3, "Отправить письмо", 3);
            Task task4 = new Task(4, "Заплатить налоги", 1);

            Console.WriteLine("=== Система обработки задач (Queue) ===\n");

            Console.WriteLine("--- Добавление задач ---");
            manager.AddTask(task1);
            manager.AddTask(task2);
            manager.AddTask(task3);
            manager.AddTask(task4);

            manager.GetPendingTasks();

            Console.WriteLine("\n--- Обработка задач ---");
            manager.ProcessTask();
            manager.ProcessTask();

            manager.GetPendingTasks();

            Console.WriteLine("\n--- Добавление новой задачи ---");
            Task task5 = new Task(5, "Подготовить презентацию", 2);
            manager.AddTask(task5);

            manager.GetPendingTasks();

            Console.WriteLine("\n--- Обработка остальных задач ---");
            manager.ProcessTask();
            manager.ProcessTask();
            manager.ProcessTask();

            manager.GetPendingTasks();

            Console.WriteLine("\nНажмите любую клавишу для завершения...");
            Console.ReadKey();
        }
    }
}