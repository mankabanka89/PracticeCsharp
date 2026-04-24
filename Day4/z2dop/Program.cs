using System;

namespace AnimalSounds
{
    class Program
    {
        static string GetAnimalSound(string animal)
        {
            switch (animal.ToLower())
            {
                case "dog":
                    return "Woof";
                case "cat":
                    return "Meow";
                case "cow":
                    return "Moo";
                default:
                    return "Unknown animal";
            }
        }

        static string GetAnimalSound(string animal, string noise)
        {
            return noise;
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("--- Звуки животных ---\n");

            Console.WriteLine($"GetAnimalSound(\"Dog\") -> {GetAnimalSound("Dog")}");
            Console.WriteLine($"GetAnimalSound(\"Cat\") -> {GetAnimalSound("Cat")}");
            Console.WriteLine($"GetAnimalSound(\"Cow\") -> {GetAnimalSound("Cow")}");
            Console.WriteLine($"GetAnimalSound(\"Dog\", \"Bark\") -> {GetAnimalSound("Dog", "Bark")}");
            Console.WriteLine($"GetAnimalSound(\"Cat\", \"Purr\") -> {GetAnimalSound("Cat", "Purr")}");
            Console.WriteLine($"GetAnimalSound(\"Cow\", \"MoooOOO\") -> {GetAnimalSound("Cow", "MoooOOO")}");

            Console.WriteLine("\nНажмите любую клавишу для завершения...");
            Console.ReadKey();
        }
    }
}