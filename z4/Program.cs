namespace СуммаЧисел;

using System;

public static class Программа
{
    public static void Main()
    {
        Console.Write("a = ");
        int a = int.Parse(Console.ReadLine());

        Console.Write("b = ");
        int b = int.Parse(Console.ReadLine());

        Console.Write("c = ");
        int c = int.Parse(Console.ReadLine());

        int суммаПрямой = a + b + c;
        int суммаОбратный = c + b + a;

        Console.WriteLine($"{a} + {b} + {c} = {c} + {b} + {a}");
        Console.WriteLine($"Сумма = {суммаПрямой}");

        Console.WriteLine("Для продолжения нажмите любую клавишу...");
        Console.ReadKey();
    }

}