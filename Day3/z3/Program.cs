using System;

namespace BankSystem
{
    abstract class BankAccount
    {
        public string AccountNumber { get; set; }
        public double Balance { get; set; }
        public string OwnerName { get; set; }

        public BankAccount(string accountNumber, double balance, string ownerName)
        {
            AccountNumber = accountNumber;
            Balance = balance;
            OwnerName = ownerName;
        }

        public virtual void ShowInfo()
        {
            Console.WriteLine($"Владелец: {OwnerName}, Счет: {AccountNumber}, Баланс: {Balance:C}");
        }
    }

    sealed class SavingsAccount : BankAccount
    {
        public double InterestRate { get; set; }

        public SavingsAccount(string accountNumber, double balance, string ownerName, double interestRate)
            : base(accountNumber, balance, ownerName)
        {
            InterestRate = interestRate;
        }

        public override void ShowInfo()
        {
            Console.WriteLine($"[Сберегательный] Владелец: {OwnerName}, Счет: {AccountNumber}, Баланс: {Balance:C}, Ставка: {InterestRate}%");
        }
    }

    sealed class CheckingAccount : BankAccount
    {
        public double OverdraftLimit { get; set; }

        public CheckingAccount(string accountNumber, double balance, string ownerName, double overdraftLimit)
            : base(accountNumber, balance, ownerName)
        {
            OverdraftLimit = overdraftLimit;
        }

        public override void ShowInfo()
        {
            Console.WriteLine($"[Расчетный] Владелец: {OwnerName}, Счет: {AccountNumber}, Баланс: {Balance:C}, Овердрафт: {OverdraftLimit:C}");
        }
    }

    class Bank
    {
        private BankAccount[] accounts;

        public Bank(BankAccount[] accounts)
        {
            this.accounts = accounts;
        }

        public string GetRichestClient()
        {
            if (accounts == null || accounts.Length == 0)
                return "Нет клиентов";

            BankAccount richest = accounts[0];
            for (int i = 1; i < accounts.Length; i++)
            {
                if (accounts[i].Balance > richest.Balance)
                {
                    richest = accounts[i];
                }
            }
            return richest.OwnerName;
        }

        public double GetTotalBankBalance()
        {
            double total = 0;
            for (int i = 0; i < accounts.Length; i++)
            {
                total += accounts[i].Balance;
            }
            return total;
        }

        public void ShowAllAccounts()
        {
            Console.WriteLine("\n=== Все счета ===");
            for (int i = 0; i < accounts.Length; i++)
            {
                accounts[i].ShowInfo();
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            BankAccount[] accounts = new BankAccount[4];
            accounts[0] = new SavingsAccount("SAV001", 150000, "Иван Петров", 5.5);
            accounts[1] = new CheckingAccount("CHK001", 50000, "Мария Сидорова", 20000);
            accounts[2] = new SavingsAccount("SAV002", 250000, "Алексей Иванов", 6.0);
            accounts[3] = new CheckingAccount("CHK002", 75000, "Елена Смирнова", 15000);

            Bank bank = new Bank(accounts);

            bank.ShowAllAccounts();

            Console.WriteLine($"\nКлиент с самым большим балансом: {bank.GetRichestClient()}");
            Console.WriteLine($"Общий баланс всех клиентов: {bank.GetTotalBankBalance():C}");

            Console.WriteLine("\nНажмите любую клавишу для завершения...");
            Console.ReadKey();
        }
    }
}