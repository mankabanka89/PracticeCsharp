namespace FreeFallTime;

using System;

public static class Program
{
    public static void Main()
    {
        double h = Math.Sqrt(2.0 / 10.0);
        double g = 9.81523;

        double t = Math.Sqrt(2 * h / g);

        Console.WriteLine($"Высота h = {h:F8} м");
        Console.WriteLine($"Гравитация g = {g} м/с2");
        Console.WriteLine($"Время падения t = {t:F8} с");
    }
}