using System;

public static class Program
{
    public static void Main()
    {
        Console.Write("Введите размер матрицы N (N < 10): ");
        if (!int.TryParse(Console.ReadLine(), out int n) || n <= 0 || n >= 10)
        {
            Console.WriteLine("Ошибка: N должно быть целым положительным числом меньше 10.");
            return;
        }

        Console.Write("Введите нижнюю границу диапазона a: ");
        if (!int.TryParse(Console.ReadLine(), out int a))
        {
            Console.WriteLine("Ошибка ввода a.");
            return;
        }

        Console.Write("Введите верхнюю границу диапазона b: ");
        if (!int.TryParse(Console.ReadLine(), out int b) || b < a)
        {
            Console.WriteLine("Ошибка: b должно быть больше или равно a.");
            return;
        }

        int[,] matrix = new int[n, n];
        Random random = new Random();

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                matrix[i, j] = random.Next(a, b + 1);
            }
        }

        Console.WriteLine("\nИсходная матрица:");
        PrintMatrix(matrix, n);

        int negativeSum = ComputeNegativeSum(matrix, n);
        Console.WriteLine($"\nСумма отрицательных элементов: {negativeSum}");

        Console.WriteLine("\nКоличество чётных элементов в каждой строке:");
        for (int i = 0; i < n; i++)
        {
            int evenCount = CountEvenInRow(matrix, i, n);
            Console.WriteLine($"Строка {i + 1}: {evenCount} чётных элементов");
        }
    }

    private static void PrintMatrix(int[,] matrix, int n)
    {
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                Console.Write($"{matrix[i, j],6}");
            }
            Console.WriteLine();
        }
    }

    private static int ComputeNegativeSum(int[,] matrix, int n)
    {
        int sum = 0;
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (matrix[i, j] < 0)
                {
                    sum += matrix[i, j];
                }
            }
        }
        return sum;
    }

    private static int CountEvenInRow(int[,] matrix, int row, int n)
    {
        int count = 0;
        for (int j = 0; j < n; j++)
        {
            if (matrix[row, j] % 2 == 0)
            {
                count++;
            }
        }
        return count;
    }
}