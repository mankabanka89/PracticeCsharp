using System;

public static class Program
{
    public static void Main()
    {
        Console.WriteLine("Способ 1: цикл for");
        for (int i = 80; i >= 10; i -= 2)
        {
            Console.WriteLine(i);
        }

        Console.WriteLine("\nСпособ 2: цикл while");
        int j = 80;
        while (j >= 10)
        {
            Console.WriteLine(j);
            j -= 2;
        }

        Console.WriteLine("\nСпособ 3: цикл do while");
        int k = 80;
        do
        {
            Console.WriteLine(k);
            k -= 2;
        }
        while (k >= 10);
    }
}