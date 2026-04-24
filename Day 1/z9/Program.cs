using System;

public static class Program
{
    public static void Main()
    {
        double a = Math.PI / 4;
        double b = Math.PI / 2;
        int m = 15;

        double h = (b - a) / m;

        Console.WriteLine($"Таблица значений функции F(x) = 2 - sin(x)");
        Console.WriteLine($"Отрезок [{a:F4}, {b:F4}], M = {m}, шаг H = {h:F4}");
        Console.WriteLine("--------------------------");

        double x = a;
        for (int i = 0; i <= m; i++)
        {
            double y = 2 - Math.Sin(x);
            Console.WriteLine($"x = {x,6:F4} | y = {y,8:F4}");
            x += h;
        }
    }
}