using System;

namespace PowerProcedure
{
    class Program
    {
        static void PowerA234(double A, out double B, out double C, out double D)
        {
            B = Math.Pow(A, 2);  // вторая степень (A²)
            C = Math.Pow(A, 3);  // третья степень (A³)
            D = Math.Pow(A, 4);  // четвёртая степень (A⁴)
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            double[] numbers = { 2, 3, 4.5, 1.5, 10 };

            Console.WriteLine("--- Вычисление степеней чисел ---\n");

            for (int i = 0; i < numbers.Length; i++)
            {
                double A = numbers[i];
                double B, C, D;

                PowerA234(A, out B, out C, out D);

                Console.WriteLine($"Число A = {A}");
                Console.WriteLine($"  Вторая степень (A²) = {B}");
                Console.WriteLine($"  Третья степень (A³) = {C}");
                Console.WriteLine($"  Четвёртая степень (A⁴) = {D}");
                Console.WriteLine();
            }

            Console.WriteLine("Нажмите любую клавишу для завершения...");
            Console.ReadKey();
        }
    }
}