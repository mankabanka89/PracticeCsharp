using System;

public static class Program
{
    public static void Main()
    {
        Console.Write("Введите количество строк в ступенчатом массиве: ");
        if (!int.TryParse(Console.ReadLine(), out int rows) || rows <= 0)
        {
            Console.WriteLine("Ошибка ввода количества строк.");
            return;
        }

        int[][] jaggedArray = new int[rows][];
        Random random = new Random();

        for (int i = 0; i < rows; i++)
        {
            Console.Write($"Введите количество элементов в строке {i}: ");
            if (!int.TryParse(Console.ReadLine(), out int cols) || cols <= 0)
            {
                Console.WriteLine("Ошибка ввода количества элементов. Строка будет иметь 3 элемента.");
                cols = 3;
            }

            jaggedArray[i] = new int[cols];
            for (int j = 0; j < cols; j++)
            {
                jaggedArray[i][j] = random.Next(-20, 41);
            }
        }

        Console.WriteLine("\nСтупенчатый массив:");
        PrintJaggedArray(jaggedArray);

        int maxSumRowIndex = FindRowWithMaxSum(jaggedArray);
        int maxSum = ComputeRowSum(jaggedArray[maxSumRowIndex]);

        Console.WriteLine($"\nСтрока с наибольшей суммой элементов: {maxSumRowIndex}");
        Console.WriteLine($"Сумма элементов этой строки: {maxSum}");
        Console.WriteLine("Элементы строки:");

        for (int j = 0; j < jaggedArray[maxSumRowIndex].Length; j++)
        {
            Console.Write($"{jaggedArray[maxSumRowIndex][j],6} ");
        }
        Console.WriteLine();
    }

    private static void PrintJaggedArray(int[][] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            Console.Write($"Строка {i}: ");
            for (int j = 0; j < array[i].Length; j++)
            {
                Console.Write($"{array[i][j],6} ");
            }
            Console.WriteLine();
        }
    }

    private static int FindRowWithMaxSum(int[][] array)
    {
        int maxSum = int.MinValue;
        int maxIndex = 0;

        for (int i = 0; i < array.Length; i++)
        {
            int currentSum = ComputeRowSum(array[i]);
            if (currentSum > maxSum)
            {
                maxSum = currentSum;
                maxIndex = i;
            }
        }

        return maxIndex;
    }

    private static int ComputeRowSum(int[] row)
    {
        int sum = 0;
        for (int i = 0; i < row.Length; i++)
        {
            sum += row[i];
        }
        return sum;
    }
}