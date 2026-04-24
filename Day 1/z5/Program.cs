using System;

public static class Program
{
    public static void Main()
    {
        Console.Write("Введите целое число: ");
        if (!int.TryParse(Console.ReadLine(), out int number))
        {
            Console.WriteLine("Ошибка: необходимо ввести целое число.");
            return;
        }

        bool isEven = number % 2 == 0;

        if (isEven)
        {
            Console.WriteLine($"Число {number} является четным.");
        }
        else
        {
            Console.WriteLine($"Число {number} является нечетным.");
        }
    }
}