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

        bool hasDigit = false;

        foreach (char c in input)
        {
            if (c >= '0' && c <= '9')
            {
                hasDigit = true;
                break;
            }
        }

        if (hasDigit)
        {
            Console.WriteLine("Строка содержит хотя бы одну цифру.");
        }
        else
        {
            Console.WriteLine("Строка не содержит цифр.");
        }
    }
}