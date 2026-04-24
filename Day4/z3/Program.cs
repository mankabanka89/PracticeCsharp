using System;

namespace FibonacciRecursion
{
    class Program
    {
        static int Fibonacci(int n)
        {
            if (n < 0)
            {
                throw new ArgumentException("Номер числа Фибоначчи не может быть отрицательным");
            }

            if (n == 0)
            {
                return 0;
            }

            if (n == 1)
            {
                return 1;
            }

            return Fibonacci(n - 1) + Fibonacci(n - 2);
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            int[] numbers = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            Console.WriteLine("--- Числа Фибоначчи ---\n");
            Console.WriteLine("Последовательность Фибоначчи: 0, 1, 1, 2, 3, 5, 8, 13, 21, 34, 55...\n");

            foreach (int n in numbers)
            {
                try
                {
                    int result = Fibonacci(n);
                    Console.WriteLine($"Fibonacci({n}) = {result}");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }

            // Доп проверка для отрицательного числа
            Console.WriteLine("\n--- Проверка обработки ошибок ---");
            try
            {
                int result = Fibonacci(-5);
                Console.WriteLine($"Fibonacci(-5) = {result}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            Console.WriteLine("\nНажмите любую клавишу для завершения...");
            Console.ReadKey();
        }
    }
}