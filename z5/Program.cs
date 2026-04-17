namespace ProductOfDigits;

using System;

public static class Program
{
    public static void Main()
    {
        Console.Write("Введите четырёхзначное число: ");

        if (!int.TryParse(Console.ReadLine(), out int number) || number < 1000 || number > 9999)
        {
            Console.WriteLine("Ошибка: пожалуйста, введите четырёхзначное число (от 1000 до 9999).");
            return;
        }

        int тысячи = number / 1000;
        int сотни = (number / 100) % 10;
        int десятки = (number / 10) % 10;
        int единицы = number % 10;

        int произведение = тысячи * сотни * десятки * единицы;

        Console.WriteLine($"Произведение цифр числа {number} равно {произведение}.");
    }
}