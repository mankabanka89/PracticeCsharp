using System;
using System.IO;

namespace JsonParsingException
{
    public class ParsingException : Exception
    {
        public ParsingException() : base() { }

        public ParsingException(string message) : base(message) { }

        public ParsingException(string message, Exception innerException) : base(message, innerException) { }
    }

    class JsonParser
    {
        public void Parse(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                throw new Exception("JSON строка не может быть пустой");
            }

            if (!json.Contains("{") || !json.Contains("}"))
            {
                throw new Exception("Неверный формат JSON: отсутствуют фигурные скобки");
            }

            if (!json.Contains("\"name\"") && !json.Contains("\"age\""))
            {
                throw new Exception("JSON строка не содержит обязательных полей: name или age");
            }

            Console.WriteLine($"JSON успешно распарсен: {json}");
        }
    }

    class DataProcessor
    {
        private JsonParser parser;

        public DataProcessor()
        {
            parser = new JsonParser();
        }

        public void ProcessData(string json)
        {
            try
            {
                parser.Parse(json);
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw new ParsingException("Ошибка при обработке JSON данных", ex);
            }
        }

        private void LogException(Exception ex)
        {
            string logFile = "error_log.txt";
            string logMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}]\n";
            logMessage += $"Тип исключения: {ex.GetType().Name}\n";
            logMessage += $"Сообщение: {ex.Message}\n";
            logMessage += $"Стек вызовов: {ex.StackTrace}\n";

            if (ex.InnerException != null)
            {
                logMessage += $"Внутреннее исключение: {ex.InnerException.GetType().Name}\n";
                logMessage += $"Сообщение внутреннего исключения: {ex.InnerException.Message}\n";
                logMessage += $"Стек внутреннего исключения: {ex.InnerException.StackTrace}\n";
            }

            logMessage += "----------------------------------------\n";

            File.AppendAllText(logFile, logMessage);
            Console.WriteLine($"Исключение залогировано в файл: {logFile}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            DataProcessor processor = new DataProcessor();

            string[] testJson = {
                "{\"name\": \"Иван\", \"age\": 25}",
                "",
                "name: Иван",
                "{\"name\": \"Петр\"}"
            };

            foreach (string json in testJson)
            {
                Console.WriteLine($"\n=== Попытка парсинга: {json} ===");

                try
                {
                    processor.ProcessData(json);
                }
                catch (ParsingException ex)
                {
                    Console.WriteLine($"Перехвачено исключение: {ex.Message}");

                    if (ex.InnerException != null)
                    {
                        Console.WriteLine($"Внутреннее исключение: {ex.InnerException.Message}");
                    }
                }
            }

            Console.WriteLine("\nПроверьте файл error_log.txt для просмотра логов");

            Console.WriteLine("\nНажмите любую клавишу для завершения...");
            Console.ReadKey();
        }
    }
}