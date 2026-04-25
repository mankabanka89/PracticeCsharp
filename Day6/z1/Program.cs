using System;

namespace DelegateMathOperations
{
    public delegate int MathOperation(int a, int b);

    class Addition
    {
        public int Add(int a, int b)
        {
            return a + b;
        }
    }

    class Multiplication
    {
        public int Multiply(int a, int b)
        {
            return a * b;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Addition adder = new Addition();
            Multiplication multiplier = new Multiplication();

            MathOperation operation = adder.Add;

            int x = 10;
            int y = 5;

            Console.WriteLine("--- Делегат для математических операций ---\n");

            Console.WriteLine($"Числа: a = {x}, b = {y}\n");

            int sum = operation(x, y);
            Console.WriteLine($"Сложение (Add): {x} + {y} = {sum}");

            operation = multiplier.Multiply;

            int product = operation(x, y);
            Console.WriteLine($"Умножение (Multiply): {x} * {y} = {product}");

            Console.WriteLine("\n--- Дополнительные примеры ---");

            int x2 = 7;
            int y2 = 8;

            operation = adder.Add;
            Console.WriteLine($"{x2} + {y2} = {operation(x2, y2)}");

            operation = multiplier.Multiply;
            Console.WriteLine($"{x2} * {y2} = {operation(x2, y2)}");

            Console.WriteLine("\nНажмите любую клавишу для завершения...");
            Console.ReadKey();
        }
    }
}