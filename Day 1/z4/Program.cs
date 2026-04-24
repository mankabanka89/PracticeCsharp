using System;

public static class Program
{
    public static void Main()
    {
        Console.Write("Введите x: ");
        if (!double.TryParse(Console.ReadLine(), out double x))
        {
            Console.WriteLine("Ошибка ввода.");
            return;
        }

        double y = ComputeY(x);
        Console.WriteLine($"y = {y:F8}");
    }

    private static double ComputeY(double x)
    {
        if (x >= 10)
        {
            return 4 + x * x - Math.Exp(Math.Sqrt(x));
        }
        else
        {
            return 3.4 - x + 0.1 * Math.Pow(x, 3);
        }
    }
}