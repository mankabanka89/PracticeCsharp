using System;
using System.Linq;

public static class Program
{
    public static void Main()
    {
        double[] numbers = ReadArray();

        double sum = ComputeSum(numbers);
        int count = numbers.Length;

        Console.WriteLine($"Количество элементов: {count}");
        Console.WriteLine($"Сумма элементов: {sum:F4}");
        Console.WriteLine($"Среднее арифметическое: {sum / count:F4}");
    }

    private static double[] ReadArray()
    {
        Console.Write("Введите количество элементов массива: ");
        if (!int.TryParse(Console.ReadLine(), out int n) || n <= 0)
        {
            Console.WriteLine("Ошибка: введите положительное целое число.");
            return Array.Empty<double>();
        }

        double[] array = new double[n];

        for (int i = 0; i < n; i++)
        {
            Console.Write($"array[{i}] = ");
            if (!double.TryParse(Console.ReadLine(), out double value))
            {
                Console.WriteLine("Ошибка ввода. Элементу присвоено значение 0.");
                value = 0;
            }
            array[i] = value;
        }

        return array;
    }

    private static double ComputeSum(double[] array)
    {
        double sum = 0;
        foreach (double element in array)
        {
            sum += element;
        }
        return sum;
    }
}