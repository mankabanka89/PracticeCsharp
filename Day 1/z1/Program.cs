using System;

public static class Program
{
    public static void Main()
    {
        Console.Write("Введите массу в килограммах: ");

        if (!double.TryParse(Console.ReadLine(), out double kg) || kg < 0)
        {
            Console.WriteLine("Ошибка: введите неотрицательное число.");
            return;
        }

        int fullCentners = (int)(kg / 100);

        Console.WriteLine($"Число полных центнеров: {fullCentners}");
    }
}