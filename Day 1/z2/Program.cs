using System;

public static class Program
{
    public static void Main()
    {
        Console.Write("Введите x: ");
        if (!double.TryParse(Console.ReadLine(), out double x))
        {
            Console.WriteLine("Ошибка ввода x.");
            return;
        }

        Console.Write("Введите y: ");
        if (!double.TryParse(Console.ReadLine(), out double y))
        {
            Console.WriteLine("Ошибка ввода y.");
            return;
        }

        bool isInSecondQuadrant = (x < 0) && (y > 0);

        Console.WriteLine($"Точка ({x}; {y}) лежит во второй четверти: {isInSecondQuadrant}");
    }
}