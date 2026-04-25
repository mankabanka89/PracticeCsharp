using System;

namespace DelegateAsParameter
{
    public delegate int MathOperation(int a, int b);

    class Program
    {
        static int Calculate(int a, int b, MathOperation operation)
        {
            return operation(a, b);
        }

        static int Add(int a, int b)
        {
            return a + b;
        }

        static int Multiply(int a, int b)
        {
            return a * b;
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            int x = 15;
            int y = 3;

            Console.WriteLine("--- Передача делегата в качестве параметра ---\n");
            Console.WriteLine($"Числа: a = {x}, b = {y}\n");

            int sum = Calculate(x, y, Add);
            Console.WriteLine($"Сложение: {x} + {y} = {sum}");

            int product = Calculate(x, y, Multiply);
            Console.WriteLine($"Умножение: {x} * {y} = {product}");

            Console.WriteLine("\n--- Дополнительные примеры ---");

            int a = 8;
            int b = 4;

            Console.WriteLine($"\n{a} + {b} = {Calculate(a, b, Add)}");
            Console.WriteLine($"{a} * {b} = {Calculate(a, b, Multiply)}");

            Console.WriteLine("\nНажмите любую клавишу для завершения...");
            Console.ReadKey();
        }
    }
}