using System;

public static class Program
{
    public static void Main()
    {
        Console.Write("Введите натуральное число: ");
        if (!int.TryParse(Console.ReadLine(), out int number) || number <= 0)
        {
            Console.WriteLine("Ошибка: необходимо ввести натуральное число (целое положительное).");
            return;
        }

        int temp = number;
        int minDigit = 9;
        int maxDigit = 0;

        while (temp > 0)
        {
            int digit = temp % 10;
            if (digit < minDigit)
            {
                minDigit = digit;
            }
            if (digit > maxDigit)
            {
                maxDigit = digit;
            }
            temp /= 10;
        }

        Console.WriteLine($"Число: {number}");
        Console.WriteLine($"Наименьшая цифра: {minDigit}");
        Console.WriteLine($"Наибольшая цифра: {maxDigit}");
    }
}