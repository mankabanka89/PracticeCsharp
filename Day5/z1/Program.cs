using System;

public abstract class Animal
{
    public abstract void MakeSound();
}

public class Dog : Animal
{
    public override void MakeSound()
    {
        Console.WriteLine("Собака: Гав-гав!");
    }
}

public class Cat : Animal
{
    public override void MakeSound()
    {
        Console.WriteLine("Кошка: Мяу-мяу!");
    }
}

public class Cow : Animal
{
    public override void MakeSound()
    {
        Console.WriteLine("Корова: Му-у-у!");
    }
}

public static class Program
{
    public static void Main()
    {
        Animal[] animals = new Animal[]
        {
            new Dog(),
            new Cat(),
            new Cow(),
            new Dog(),
            new Cat()
        };

        Console.WriteLine("Звуки животных:");
        Console.WriteLine("----------------");

        foreach (Animal animal in animals)
        {
            animal.MakeSound();
        }
    }
}