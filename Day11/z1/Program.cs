using System;

namespace BankCardFactoryPattern
{
    public interface IBankCard
    {
        void Use();
        string GetCardType();
    }

    public class CreditCard : IBankCard
    {
        private decimal _creditLimit;
        private decimal _balance;

        public CreditCard(decimal creditLimit = 100000)
        {
            _creditLimit = creditLimit;
            _balance = creditLimit;
        }

        public void Use()
        {
            Console.WriteLine($"Кредитная карта использована. Кредитный лимит: {_creditLimit:C}, Доступно: {_balance:C}");
        }

        public string GetCardType()
        {
            return "Кредитная карта";
        }
    }

    public class DebitCard : IBankCard
    {
        private decimal _balance;

        public DebitCard(decimal initialBalance = 0)
        {
            _balance = initialBalance;
        }

        public void Use()
        {
            Console.WriteLine($"Дебетовая карта использована. Баланс: {_balance:C}");
        }

        public string GetCardType()
        {
            return "Дебетовая карта";
        }
    }

    public class VirtualCard : IBankCard
    {
        private string _cardNumber;
        private string _expiryDate;

        public VirtualCard(string cardNumber = "XXXX-XXXX-XXXX-1234", string expiryDate = "12/25")
        {
            _cardNumber = cardNumber;
            _expiryDate = expiryDate;
        }

        public void Use()
        {
            Console.WriteLine($"Виртуальная карта использована. Номер: {_cardNumber}, Срок действия: {_expiryDate}");
        }

        public string GetCardType()
        {
            return "Виртуальная карта";
        }
    }

    public abstract class BankCardFactory
    {
        public abstract IBankCard CreateCard();

        public void UseCard()
        {
            IBankCard card = CreateCard();
            Console.WriteLine($"Создана: {card.GetCardType()}");
            card.Use();
        }
    }

    public class CreditCardFactory : BankCardFactory
    {
        private decimal _creditLimit;

        public CreditCardFactory(decimal creditLimit = 100000)
        {
            _creditLimit = creditLimit;
        }

        public override IBankCard CreateCard()
        {
            return new CreditCard(_creditLimit);
        }
    }

    public class DebitCardFactory : BankCardFactory
    {
        private decimal _initialBalance;

        public DebitCardFactory(decimal initialBalance = 0)
        {
            _initialBalance = initialBalance;
        }

        public override IBankCard CreateCard()
        {
            return new DebitCard(_initialBalance);
        }
    }

    public class VirtualCardFactory : BankCardFactory
    {
        private string _cardNumber;
        private string _expiryDate;

        public VirtualCardFactory(string cardNumber = "XXXX-XXXX-XXXX-1234", string expiryDate = "12/25")
        {
            _cardNumber = cardNumber;
            _expiryDate = expiryDate;
        }

        public override IBankCard CreateCard()
        {
            return new VirtualCard(_cardNumber, _expiryDate);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Паттерн 'Фабричный метод' - Банковские карты ===\n");

            BankCardFactory creditFactory = new CreditCardFactory(150000);
            BankCardFactory debitFactory = new DebitCardFactory(50000);
            BankCardFactory virtualFactory = new VirtualCardFactory("1234-5678-9012-3456", "05/27");

            Console.WriteLine("1. Создание и использование кредитной карты:");
            IBankCard creditCard = creditFactory.CreateCard();
            Console.WriteLine($"Тип: {creditCard.GetCardType()}");
            creditCard.Use();

            Console.WriteLine("\n2. Создание и использование дебетовой карты:");
            IBankCard debitCard = debitFactory.CreateCard();
            Console.WriteLine($"Тип: {debitCard.GetCardType()}");
            debitCard.Use();

            Console.WriteLine("\n3. Создание и использование виртуальной карты:");
            IBankCard virtualCard = virtualFactory.CreateCard();
            Console.WriteLine($"Тип: {virtualCard.GetCardType()}");
            virtualCard.Use();

            Console.WriteLine("\n4. Использование метода UseCard() для каждой фабрики:");
            creditFactory.UseCard();
            debitFactory.UseCard();
            virtualFactory.UseCard();

            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}