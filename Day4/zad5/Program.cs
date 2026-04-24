using System;

namespace AnimalHierarchy
{
    abstract class Animal
    {
        public abstract void MakeSound();

        public virtual void Sleep()
        {
            Console.WriteLine("Животное спит");
        }
    }

    class Dog : Animal
    {
        public override void MakeSound()
        {
            Console.WriteLine("Woof!");
        }

        public override void Sleep()
        {
            Console.WriteLine("Собака спит, свернувшись калачиком");
        }
    }

    class Cat : Animal
    {
        public override void MakeSound()
        {
            Console.WriteLine("Meow!");
        }

        public override void Sleep()
        {
            Console.WriteLine("Кошка спит, свернувшись клубочком и мурлыча");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Animal[] animals = new Animal[2];
            animals[0] = new Dog();
            animals[1] = new Cat();

            Console.WriteLine("--- Демонстрация работы животных ---\n");

            foreach (Animal animal in animals)
            {
                Console.Write($"{animal.GetType().Name}: ");
                animal.MakeSound();
                Console.Write($"{animal.GetType().Name}: ");
                animal.Sleep();
                Console.WriteLine();
            }

            Console.WriteLine("\n--- Демонстрация полиморфизма ---");
            Animal myPet = new Dog();
            Console.Write("Мой питомец (Dog): ");
            myPet.MakeSound();
            myPet.Sleep();

            myPet = new Cat();
            Console.Write("\nМой питомец (Cat): ");
            myPet.MakeSound();
            myPet.Sleep();

            Console.WriteLine("\nНажмите любую клавишу для завершения...");
            Console.ReadKey();
        }
    }
}