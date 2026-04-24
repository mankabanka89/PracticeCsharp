using System;

namespace ExtensionMethods
{
    static class IntExtensions
    {
        public static bool IsEven(this int number)
        {
            return number % 2 == 0;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            int num1 = 10;
            int num2 = 7;
            int num3 = 0;
            int num4 = -4;
            int num5 = -3;

            Console.WriteLine("--- Проверка чисел на чётность ---\n");

            Console.WriteLine($"{num1} чётное? {num1.IsEven()}");
            Console.WriteLine($"{num2} чётное? {num2.IsEven()}");
            Console.WriteLine($"{num3} чётное? {num3.IsEven()}");
            Console.WriteLine($"{num4} чётное? {num4.IsEven()}");
            Console.WriteLine($"{num5} чётное? {num5.IsEven()}");

            Console.WriteLine("\n--- Проверка в цикле ---");
            for (int i = 1; i <= 10; i++)
            {
                Console.WriteLine($"{i} -> {i.IsEven()}");
            }

            Console.WriteLine("\nНажмите любую клавишу для завершения...");
            Console.ReadKey();
        }
    }
}