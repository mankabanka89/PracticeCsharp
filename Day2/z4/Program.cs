using System;

public static class Program
{
    public static void Main()
    {
        Console.Write("Введите количество строк: ");
        if (!int.TryParse(Console.ReadLine(), out int rows) || rows <= 0)
        {
            Console.WriteLine("Ошибка ввода количества строк.");
            return;
        }

        Console.Write("Введите количество столбцов: ");
        if (!int.TryParse(Console.ReadLine(), out int cols) || cols <= 0)
        {
            Console.WriteLine("Ошибка ввода количества столбцов.");
            return;
        }

        int[,] matrix = new int[rows, cols];
        Random random = new Random();

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                matrix[i, j] = random.Next(-50, 51);
            }
        }

        Console.WriteLine("\nСгенерированная матрица:");
        PrintMatrix(matrix, rows, cols);

        Console.Write("\nВведите номер строки (начиная с 0): ");
        if (!int.TryParse(Console.ReadLine(), out int rowIndex) || rowIndex < 0 || rowIndex >= rows)
        {
            Console.WriteLine($"Ошибка: номер строки должен быть от 0 до {rows - 1}.");
            return;
        }

        Console.Write("Введите заданное число для сравнения: ");
        if (!int.TryParse(Console.ReadLine(), out int targetNumber))
        {
            Console.WriteLine("Ошибка ввода числа.");
            return;
        }

        int rowSum = ComputeRowSum(matrix, rowIndex, cols);

        Console.WriteLine($"\nСумма элементов строки {rowIndex}: {rowSum}");
        Console.WriteLine($"Заданное число: {targetNumber}");

        bool isSumGreater = rowSum > targetNumber;

        if (isSumGreater)
        {
            Console.WriteLine($"Верно: сумма элементов строки {rowIndex} превышает заданное число {targetNumber}.");
        }
        else
        {
            Console.WriteLine($"Неверно: сумма элементов строки {rowIndex} не превышает заданное число {targetNumber}.");
        }
    }

    private static void PrintMatrix(int[,] matrix, int rows, int cols)
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Console.Write($"{matrix[i, j],6}");
            }
            Console.WriteLine();
        }
    }

    private static int ComputeRowSum(int[,] matrix, int row, int cols)
    {
        int sum = 0;
        for (int j = 0; j < cols; j++)
        {
            sum += matrix[row, j];
        }
        return sum;
    }
}