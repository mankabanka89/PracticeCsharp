namespace FunctionValue;

using System;

public static class Program
{
    public static void Main()
    {
        double x = 5;
        double y = ComputeY(x);
        Console.WriteLine($"x = {x}");
        Console.WriteLine($"y = {y:F8}");
    }

    private static double ComputeY(double x)
    {
        double term1 = Math.Exp(2 * x);

        double sqrtArg = 1 - x;
        double term2 = sqrtArg >= 0 ? Math.Exp(Math.Sqrt(sqrtArg)) : double.NaN;

        double term3 = (2 * Math.Pow(Math.Sin(x), 2)) / (x * x);

        if (double.IsNaN(term2))
        {
            return double.NaN;
        }

        return term1 - term2 + term3;
    }
}