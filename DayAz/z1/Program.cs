namespace CylinderVolumeCalculator;

using System;

public static class Program
{
    public static void Main()
    {
        Console.WriteLine("Вычисление объема цилиндра");
        Console.WriteLine("Введите исходные данные:");

        Console.Write("Радиус основания (см) —> ");
        if (!double.TryParse(Console.ReadLine(), out double radius) || radius < 0)
        {
            Console.WriteLine("Ошибка: радиус должен быть неотрицательным числом.");
            return;
        }

        Console.Write("Высота цилиндра (см) —> ");
        if (!double.TryParse(Console.ReadLine(), out double height) || height < 0)
        {
            Console.WriteLine("Ошибка: высота должна быть неотрицательным числом.");
            return;
        }

        double volume = CalculateCylinderVolume(radius, height);

        Console.WriteLine($"Объем цилиндра {volume:F2} куб. см.");
    }

    private static double CalculateCylinderVolume(double radius, double height)
    {
        double baseArea = Math.PI * radius * radius;
        double volume = baseArea * height;
        return volume;
    }
}