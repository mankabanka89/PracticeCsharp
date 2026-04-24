using System;
using System.Collections.Generic;

public static class Program
{
    public static void Main()
    {
        Console.Write("Введите строку: ");
        string input = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine("Строка пуста.");
            return;
        }

        string[] words = input.Split(new char[] { ' ', '.', ',', '!', '?', ';', ':', '-', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries);

        if (words.Length == 0)
        {
            Console.WriteLine("В строке нет слов.");
            return;
        }

        Dictionary<string, int> wordCount = new Dictionary<string, int>();

        foreach (string word in words)
        {
            string lowerWord = word.ToLower();
            if (wordCount.ContainsKey(lowerWord))
            {
                wordCount[lowerWord]++;
            }
            else
            {
                wordCount[lowerWord] = 1;
            }
        }

        string mostFrequentWord = "";
        int maxCount = 0;

        foreach (KeyValuePair<string, int> pair in wordCount)
        {
            if (pair.Value > maxCount)
            {
                maxCount = pair.Value;
                mostFrequentWord = pair.Key;
            }
        }

        Console.WriteLine($"Самое часто встречающееся слово: \"{mostFrequentWord}\"");
        Console.WriteLine($"Встречается {maxCount} раз(а).");
    }
}