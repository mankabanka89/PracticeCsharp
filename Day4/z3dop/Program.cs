using System;

namespace TemperatureConversion
{
    class Program
    {
        static void ConvertCelsiusToFahrenheit(in double celsius, out double fahrenheit)
        {
            fahrenheit = (celsius * 9 / 5) + 32;
        }

        static void ConvertKelvinToFahrenheit(in double kelvin, out double fahrenheit)
        {
            fahrenheit = (kelvin - 273.15) * 9 / 5 + 32;
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("--- Перевод температур ---\n");

            double celsius = 25;
            ConvertCelsiusToFahrenheit(in celsius, out double fahrenheit1);
            Console.WriteLine($"{celsius}°C = {fahrenheit1}°F");

            double kelvin = 298.15;
            ConvertKelvinToFahrenheit(in kelvin, out double fahrenheit2);
            Console.WriteLine($"{kelvin}K = {fahrenheit2}°F");

            Console.WriteLine("\n--- Дополнительные примеры ---\n");

            double[] celsiusValues = { 0, 100, -40, 37 };
            foreach (double c in celsiusValues)
            {
                ConvertCelsiusToFahrenheit(in c, out double f);
                Console.WriteLine($"{c}°C = {f}°F");
            }

            Console.WriteLine();

            double[] kelvinValues = { 0, 273.15, 373.15, 310.15 };
            foreach (double k in kelvinValues)
            {
                ConvertKelvinToFahrenheit(in k, out double f);
                Console.WriteLine($"{k}K = {f:F2}°F");
            }

            Console.WriteLine("\nНажмите любую клавишу для завершения...");
            Console.ReadKey();
        }
    }
}