using System;

public static class Program
{
    public static void Main()
    {
        Console.Write("Введите A: ");
        if (!int.TryParse(Console.ReadLine(), out int a) || a < 1 || a > 100)
        {
            Console.WriteLine("Ошибка: A должно быть целым числом от 1 до 100.");
            return;
        }

        Console.Write("Введите B: ");
        if (!int.TryParse(Console.ReadLine(), out int b) || b < 1 || b > 100)
        {
            Console.WriteLine("Ошибка: B должно быть целым числом от 1 до 100.");
            return;
        }

        if (a >= b)
        {
            Console.WriteLine("Ошибка: A должно быть меньше B.");
            return;
        }

        int count = 0;
        for (int i = a; i <= b; i++)
        {
            Console.Write(i);
            count++;
            if (i < b)
            {
                Console.Write(" ");
            }
        }
        Console.WriteLine();
        Console.WriteLine($"N = {count}");
    }
}