using System;

namespace IncTimeProcedure
{
    class Program
    {
        static void IncTime(ref int H, ref int M, ref int S, int T)
        {
            S += T;

            M += S / 60;
            S %= 60;

            H += M / 60;
            M %= 60;

            H %= 24;
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=== Увеличение времени на T секунд ===\n");

            int H = 10;
            int M = 30;
            int S = 45;
            int T = 5000;

            Console.WriteLine($"Исходное время: {H:D2}:{M:D2}:{S:D2}");
            Console.WriteLine($"Добавить секунд: {T}\n");

            IncTime(ref H, ref M, ref S, T);

            Console.WriteLine($"Новое время: {H:D2}:{M:D2}:{S:D2}");

            Console.WriteLine("\n=== Дополнительные примеры ===\n");

                        int H1 = 23, M1 = 59, S1 = 30, T1 = 40;
            Console.WriteLine($"{H1:D2}:{M1:D2}:{S1:D2} + {T1} сек = ", end: "");
            IncTime(ref H1, ref M1, ref S1, T1);
            Console.WriteLine($"{H1:D2}:{M1:D2}:{S1:D2}");

                        int H2 = 0, M2 = 0, M2S2 = 5, T2 = 55;
            Console.WriteLine($"{H2:D2}:{M2:D2}:{M2S2:D2} + {T2} сек = ", end: "");
            IncTime(ref H2, ref M2, ref M2S2, T2);
            Console.WriteLine($"{H2:D2}:{M2:D2}:{M2S2:D2}");

                        int H3 = 12, M3 = 0, S3 = 0, T3 = 7200;
            Console.WriteLine($"{H3:D2}:{M3:D2}:{S3:D2} + {T3} сек = ", end: "");
            IncTime(ref H3, ref M3, ref S3, T3);
            Console.WriteLine($"{H3:D2}:{M3:D2}:{S3:D2}");

            Console.WriteLine("\nНажмите любую клавишу для завершения...");
            Console.ReadKey();
        }
    }
}