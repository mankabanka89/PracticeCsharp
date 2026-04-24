using System;

namespace ArrayOperations
{
    class Person
    {
        public string Name;
        public int Age;
        public double Salary;

        public Person(string name, int age, double salary)
        {
            Name = name;
            Age = age;
            Salary = salary;
        }

        public void Print()
        {
            Console.WriteLine($"Имя: {Name}, Возраст: {Age}, Зарплата: {Salary}");
        }
    }

    static class ArrayUtils
    {
        public static void SortByName(Person[] persons)
        {
            for (int i = 0; i < persons.Length - 1; i++)
            {
                for (int j = 0; j < persons.Length - i - 1; j++)
                {
                    if (persons[j].Name.CompareTo(persons[j + 1].Name) > 0)
                    {
                        Person temp = persons[j];
                        persons[j] = persons[j + 1];
                        persons[j + 1] = temp;
                    }
                }
            }
        }

        public static void PrintAll(Person[] persons)
        {
            foreach (Person p in persons)
            {
                p.Print();
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Person[] people = new Person[4];
            people[0] = new Person("Иван", 25, 50000);
            people[1] = new Person("Анна", 30, 60000);
            people[2] = new Person("Петр", 28, 55000);
            people[3] = new Person("Мария", 22, 45000);

            Console.WriteLine("До сортировки:");
            ArrayUtils.PrintAll(people);

            ArrayUtils.SortByName(people);

            Console.WriteLine("\nПосле сортировки по имени:");
            ArrayUtils.PrintAll(people);
        }
    }
}