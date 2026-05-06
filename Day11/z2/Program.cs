using System;

namespace CoffeeDecoratorPattern
{
    public interface ICoffee
    {
        string GetDescription();
        double GetCost();
    }

    public class BasicCoffee : ICoffee
    {
        public string GetDescription()
        {
            return "Черный кофе";
        }

        public double GetCost()
        {
            return 50.0;
        }
    }

    public abstract class CoffeeDecorator : ICoffee
    {
        protected ICoffee _coffee;

        protected CoffeeDecorator(ICoffee coffee)
        {
            _coffee = coffee;
        }

        public virtual string GetDescription()
        {
            return _coffee.GetDescription();
        }

        public virtual double GetCost()
        {
            return _coffee.GetCost();
        }
    }

    public class MilkDecorator : CoffeeDecorator
    {
        public MilkDecorator(ICoffee coffee) : base(coffee) { }

        public override string GetDescription()
        {
            return _coffee.GetDescription() + ", с молоком";
        }

        public override double GetCost()
        {
            return _coffee.GetCost() + 20.0;
        }
    }

    public class SugarDecorator : CoffeeDecorator
    {
        public SugarDecorator(ICoffee coffee) : base(coffee) { }

        public override string GetDescription()
        {
            return _coffee.GetDescription() + ", с сахаром";
        }

        public override double GetCost()
        {
            return _coffee.GetCost() + 5.0;
        }
    }

    public class SyrupDecorator : CoffeeDecorator
    {
        private string _syrupFlavor;

        public SyrupDecorator(ICoffee coffee, string syrupFlavor = "ванильный") : base(coffee)
        {
            _syrupFlavor = syrupFlavor;
        }

        public override string GetDescription()
        {
            return _coffee.GetDescription() + $", с {_syrupFlavor} сиропом";
        }

        public override double GetCost()
        {
            return _coffee.GetCost() + 30.0;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Паттерн 'Декоратор' - Кофе с добавками ===\n");

            ICoffee coffee1 = new BasicCoffee();
            Console.WriteLine($"{coffee1.GetDescription()} : {coffee1.GetCost()} руб.");

            ICoffee coffee2 = new MilkDecorator(new BasicCoffee());
            Console.WriteLine($"{coffee2.GetDescription()} : {coffee2.GetCost()} руб.");

            ICoffee coffee3 = new SugarDecorator(new MilkDecorator(new BasicCoffee()));
            Console.WriteLine($"{coffee3.GetDescription()} : {coffee3.GetCost()} руб.");

            ICoffee coffee4 = new SyrupDecorator(new BasicCoffee(), "карамельный");
            Console.WriteLine($"{coffee4.GetDescription()} : {coffee4.GetCost()} руб.");

            ICoffee coffee5 = new SyrupDecorator(new SugarDecorator(new MilkDecorator(new BasicCoffee())), "шоколадный");
            Console.WriteLine($"{coffee5.GetDescription()} : {coffee5.GetCost()} руб.");

            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}