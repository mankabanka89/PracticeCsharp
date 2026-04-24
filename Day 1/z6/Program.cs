using System;

public static class Program
{
    public static void Main()
    {
        Console.Write("Введите год: ");
        if (!int.TryParse(Console.ReadLine(), out int year) || year < 1)
        {
            Console.WriteLine("Ошибка: необходимо ввести положительное число.");
            return;
        }

        // В восточном календаре цикл повторяется каждые 12 лет
        // За начальную точку отсчёта возьмём 1984 год (год Крысы)
        int offset = (year - 1984) % 12;
        if (offset < 0)
        {
            offset += 12;
        }

        string animal = GetAnimalByOffset(offset);
        Console.WriteLine($"Год {year} — это год {animal}.");
    }

    private static string GetAnimalByOffset(int offset)
    {
        switch (offset)
        {
            case 0:
                return "Крысы";
            case 1:
                return "Быка";
            case 2:
                return "Тигра";
            case 3:
                return "Кролика";
            case 4:
                return "Дракона";
            case 5:
                return "Змеи";
            case 6:
                return "Лошади";
            case 7:
                return "Козы";
            case 8:
                return "Обезьяны";
            case 9:
                return "Петуха";
            case 10:
                return "Собаки";
            case 11:
                return "Свиньи";
            default:
                return "неизвестного животного";
        }
    }
}