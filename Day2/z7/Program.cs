using System;

public static class Program
{
    public static void Main()
    {
        Console.Write("Введите строку: ");
        string input = Console.ReadLine();

        if (string.IsNullOrEmpty(input))
        {
            Console.WriteLine("Строка пуста.");
            return;
        }

        string result = input.Replace(" ", "");

        Console.WriteLine($"Строка без пробелов: {result}");
    }
}