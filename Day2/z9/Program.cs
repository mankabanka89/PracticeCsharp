using System;
using System.Text;

public static class Program
{
    public static void Main()
    {
        StringBuilder sb = new StringBuilder("Привет, ");

        Console.WriteLine($"Исходная строка: {sb}");

        Console.Write("Введите строку для добавления: ");
        string input = Console.ReadLine();

        sb.Append(input);

        Console.WriteLine($"Результат: {sb}");
    }
}