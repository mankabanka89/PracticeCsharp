namespace TrigonometricFormulas;

using System;

public static class Program
{
    public static void Main()
    {
        double[] testAnglesDegrees = { 0, 15, 30, 45, 60, 75, 90, 120, 135, 150, 180 };

        Console.WriteLine("Результаты вычислений по двум формулам:");
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("Угол(°) | z1 (формула 1) | z2 (формула 2) | Совпадают?");
        Console.WriteLine("----------------------------------------");

        foreach (double angleDeg in testAnglesDegrees)
        {
            double angleRad = angleDeg * Math.PI / 180.0;
            double z1 = ComputeZ1(angleRad);
            double z2 = ComputeZ2(angleRad);

            bool areEqual = Math.Abs(z1 - z2) < 1e-9;

            Console.WriteLine($"{angleDeg,6}° | {z1,12:F8} | {z2,12:F8} | {(areEqual ? "Да" : "Нет")}");
        }

        Console.WriteLine("\n--- Ручной ввод ---");
        Console.Write("Введите угол в градусах: ");
        if (double.TryParse(Console.ReadLine(), out double userAngleDeg))
        {
            double userAngleRad = userAngleDeg * Math.PI / 180.0;
            double z1user = ComputeZ1(userAngleRad);
            double z2user = ComputeZ2(userAngleRad);
            Console.WriteLine($"z1 = {z1user:F8}");
            Console.WriteLine($"z2 = {z2user:F8}");
        }
        else
        {
            Console.WriteLine("Ошибка ввода.");
        }
    }

    private static double ComputeZ1(double alphaRad)
    {
        double numerator = 1 - 2 * Math.Pow(Math.Sin(alphaRad), 2);
        double denominator = 1 + Math.Sin(2 * alphaRad);

        if (Math.Abs(denominator) < 1e-12)
        {
            return double.NaN;
        }

        return numerator / denominator;
    }

    private static double ComputeZ2(double alphaRad)
    {
        double tanAlpha = Math.Tan(alphaRad);
        double numerator = 1 - tanAlpha;
        double denominator = 1 + tanAlpha;

        if (Math.Abs(denominator) < 1e-12)
        {
            return double.NaN;
        }

        return numerator / denominator;
    }
}