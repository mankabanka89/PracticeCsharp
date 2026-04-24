using System;
using System.Text.RegularExpressions;

public static class Program
{
    public static void Main()
    {
        Console.Write("Введите email-адрес: ");
        string email = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(email))
        {
            Console.WriteLine("Строка пуста.");
            return;
        }

        string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        bool isValid = Regex.IsMatch(email, pattern);

        if (isValid)
        {
            Console.WriteLine("Email-адрес корректен.");
        }
        else
        {
            Console.WriteLine("Email-адрес некорректен.");
        }
    }
}