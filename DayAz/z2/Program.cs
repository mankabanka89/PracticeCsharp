namespace DigitsProduct;

using System;

public static class Program
{
    public static void Main()
    {
        Console.Write("Введите двузначное число: ");
        if (!int.TryParse(Console.ReadLine(), out int number) || number < 10 || number > 99)
        {
            Console.WriteLine("Ошибка: необходимо ввести двузначное число (от 10 до 99).");
            return;
        }

        int tens = number / 10;
        int units = number % 10;
        int product = tens * units;

        Console.WriteLine($"Произведение цифр числа {number} равно {product}.");
    }
}
