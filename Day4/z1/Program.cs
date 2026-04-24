using System;

namespace StaticMethods
{
    static class ArrayUtils
    {
        public static int Sum(int[] array)
        {
            int sum = 0;
            for (int i = 0; i < array.Length; i++)
            {
                sum += array[i];
            }
            return sum;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            int[] numbers = { 10, 20, 30, 40, 50 };

            int result = ArrayUtils.Sum(numbers);

            Console.WriteLine($"Массив: [{string.Join(", ", numbers)}]");
            Console.WriteLine($"Сумма элементов: {result}");
        }
    }
}