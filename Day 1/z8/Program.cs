using System;

public static class Program
{
    public static void Main()
    {
        Console.Write("Введите N (1 <= N <= 20): ");
        if (!int.TryParse(Console.ReadLine(), out int n) || n < 1 || n > 20)
        {
            Console.WriteLine("Ошибка: N должно быть от 1 до 20.");
            return;
        }

        double sum = 0;
        double sign = 1.0;

        for (int i = 1; i <= n; i++)
        {
            double term = 1.0 + i / 10.0;
            sum += sign * term;
            sign = -sign;
        }

        Console.WriteLine($"Сумма = {sum:F4}");
    }
}