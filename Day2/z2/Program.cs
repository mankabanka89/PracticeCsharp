using System;

public static class Program
{
    public static void Main()
    {
        const int size = 50;
        double[] array = new double[size];
        Random random = new Random();

        for (int i = 0; i < size; i++)
        {
            array[i] = random.Next(1, 101);
        }

        Console.WriteLine("Исходный массив:");
        PrintArray(array);

        double lastElement = array[size - 1];
        int countDifferent = 0;

        for (int i = 0; i < size - 1; i++)
        {
            if (Math.Abs(array[i] - lastElement) > 1e-9)
            {
                countDifferent++;
            }
        }

        Console.WriteLine($"\nПоследний элемент: {lastElement}");
        Console.WriteLine($"Количество элементов, отличных от последнего: {countDifferent}");

        SortArray(array);

        Console.WriteLine("\nОтсортированный массив:");
        PrintArray(array);

        Console.Write("\nВведите число k для бинарного поиска: ");
        if (!double.TryParse(Console.ReadLine(), out double k))
        {
            Console.WriteLine("Ошибка ввода.");
            return;
        }

        int index = BinarySearch(array, k);

        if (index != -1)
        {
            Console.WriteLine($"Число {k} найдено на позиции {index} (индексация с 0).");
        }
        else
        {
            Console.WriteLine($"Число {k} не найдено в массиве.");
        }
    }

    private static void PrintArray(double[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            Console.Write($"{array[i],8:F2} ");
            if ((i + 1) % 10 == 0)
            {
                Console.WriteLine();
            }
        }
        Console.WriteLine();
    }

    private static void SortArray(double[] array)
    {
        for (int i = 0; i < array.Length - 1; i++)
        {
            for (int j = 0; j < array.Length - 1 - i; j++)
            {
                if (array[j] > array[j + 1])
                {
                    double temp = array[j];
                    array[j] = array[j + 1];
                    array[j + 1] = temp;
                }
            }
        }
    }

    private static int BinarySearch(double[] array, double key)
    {
        int left = 0;
        int right = array.Length - 1;

        while (left <= right)
        {
            int mid = (left + right) / 2;

            if (Math.Abs(array[mid] - key) < 1e-9)
            {
                return mid;
            }
            else if (array[mid] < key)
            {
                left = mid + 1;
            }
            else
            {
                right = mid - 1;
            }
        }

        return -1;
    }
}