using System;

namespace BankAccountValidation
{
    public class InvalidAccountNumberException : Exception
    {
        public InvalidAccountNumberException() : base() { }

        public InvalidAccountNumberException(string message) : base(message) { }

        public InvalidAccountNumberException(string message, Exception innerException) : base(message, innerException) { }
    }

    class BankAccount
    {
        public void ValidateAccount(string accountNumber)
        {
            if (string.IsNullOrEmpty(accountNumber))
            {
                throw new InvalidAccountNumberException("Номер счета не может быть пустым");
            }

            if (accountNumber.Length != 10)
            {
                throw new InvalidAccountNumberException($"Номер счета '{accountNumber}' недействителен. Номер счета должен содержать ровно 10 цифр.");
            }

            foreach (char c in accountNumber)
            {
                if (!char.IsDigit(c))
                {
                    throw new InvalidAccountNumberException($"Номер счета '{accountNumber}' содержит недопустимые символы. Номер счета должен состоять только из цифр.");
                }
            }

            Console.WriteLine($"Номер счета '{accountNumber}' успешно проверен.");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            BankAccount account = new BankAccount();

            string[] testAccounts = { "1234567890", "12345", "abc1234567", "", null };

            foreach (string acc in testAccounts)
            {
                Console.WriteLine($"\n--- Проверка номера: {(acc == null ? "null" : acc)} ---");

                try
                {
                    account.ValidateAccount(acc);
                }
                catch (InvalidAccountNumberException ex)
                {
                    Console.WriteLine($"Ошибка валидации: {ex.Message}");
                }
            }

            Console.WriteLine("\nНажмите любую клавишу для завершения...");
            Console.ReadKey();
        }
    }
}