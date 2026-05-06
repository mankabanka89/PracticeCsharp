using System;

namespace BankWithdrawException
{
    public class InsufficientFundsException : Exception
    {
        public InsufficientFundsException() : base() { }

        public InsufficientFundsException(string message) : base(message) { }

        public InsufficientFundsException(string message, Exception innerException) : base(message, innerException) { }
    }

    class BankAccount
    {
        private string accountNumber;
        private decimal balance;

        public BankAccount(string accountNumber, decimal initialBalance)
        {
            this.accountNumber = accountNumber;
            balance = initialBalance;
        }

        public void Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                throw new InsufficientFundsException($"Сумма снятия должна быть положительной. Вы ввели: {amount}");
            }

            if (amount > balance)
            {
                throw new InsufficientFundsException(
                    $"Недостаточно средств на счете. Баланс: {balance} руб., запрошено: {amount} руб.");
            }

            balance -= amount;
            Console.WriteLine($"Снято {amount} руб. Остаток на счете: {balance} руб.");
        }

        public void ShowBalance()
        {
            Console.WriteLine($"Номер счета: {accountNumber}, Баланс: {balance} руб.");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            BankAccount account = new BankAccount("1234567890", 5000m);

            account.ShowBalance();
            Console.WriteLine();

            decimal[] withdrawAmounts = { 1000m, 2000m, 5000m, -500m, 3000m };

            foreach (decimal amount in withdrawAmounts)
            {
                Console.WriteLine($"\n--- Попытка снять {amount} руб. ---");

                try
                {
                    account.Withdraw(amount);
                }
                catch (InsufficientFundsException ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }

            Console.WriteLine("\nКонечный баланс:");
            account.ShowBalance();

            Console.WriteLine("\nНажмите любую клавишу для завершения...");
            Console.ReadKey();
        }
    }
}