using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BinaryFileReader
{
    class Product
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string Category { get; set; }

        public Product(string name, double price, string category)
        {
            Name = name;
            Price = price;
            Category = category;
        }

        public void Display()
        {
            Console.WriteLine($"  {Name,-15} | {Price,10:F2} руб. | {Category,-15}");
        }
    }

    class ProductFileReader
    {
        private string filePath;

        public ProductFileReader(string filePath)
        {
            this.filePath = filePath;
        }

        public List<Product> ReadProducts()
        {
            List<Product> products = new List<Product>();

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Ошибка: файл {filePath} не существует");
                return products;
            }

            using (BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open)))
            {
                int count = reader.ReadInt32();

                for (int i = 0; i < count; i++)
                {
                    string name = reader.ReadString();
                    double price = reader.ReadDouble();
                    string category = reader.ReadString();

                    products.Add(new Product(name, price, category));
                }
            }

            return products;
        }
    }

    class ProductProcessor
    {
        private List<Product> products;

        public ProductProcessor(List<Product> products)
        {
            this.products = products;
        }

        public void SortByPrice(bool ascending)
        {
            for (int i = 0; i < products.Count - 1; i++)
            {
                for (int j = 0; j < products.Count - i - 1; j++)
                {
                    bool needSwap = ascending ?
                        products[j].Price > products[j + 1].Price :
                        products[j].Price < products[j + 1].Price;

                    if (needSwap)
                    {
                        Product temp = products[j];
                        products[j] = products[j + 1];
                        products[j + 1] = temp;
                    }
                }
            }
        }

        public void DisplayProducts(string title)
        {
            Console.WriteLine($"\n{title}");
            Console.WriteLine(new string('-', 55));
            Console.WriteLine($"  {"Название",-15} | {"Цена",10} | {"Категория",-15}");
            Console.WriteLine(new string('-', 55));

            foreach (Product p in products)
            {
                p.Display();
            }
            Console.WriteLine(new string('-', 55));
        }

        public List<Product> FilterByCategory(string category)
        {
            List<Product> result = new List<Product>();

            for (int i = 0; i < products.Count; i++)
            {
                if (products[i].Category.Equals(category, StringComparison.OrdinalIgnoreCase))
                {
                    result.Add(products[i]);
                }
            }

            return result;
        }

        public Product FindMostExpensive()
        {
            if (products.Count == 0)
                return null;

            Product mostExpensive = products[0];
            for (int i = 1; i < products.Count; i++)
            {
                if (products[i].Price > mostExpensive.Price)
                {
                    mostExpensive = products[i];
                }
            }
            return mostExpensive;
        }

        public Product FindCheapest()
        {
            if (products.Count == 0)
                return null;

            Product cheapest = products[0];
            for (int i = 1; i < products.Count; i++)
            {
                if (products[i].Price < cheapest.Price)
                {
                    cheapest = products[i];
                }
            }
            return cheapest;
        }

        public double GetAveragePrice()
        {
            if (products.Count == 0)
                return 0;

            double sum = 0;
            for (int i = 0; i < products.Count; i++)
            {
                sum += products[i].Price;
            }
            return sum / products.Count;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            string filePath = "file.data";

            Console.WriteLine("=== Задание 3: Чтение и обработка данных из file.data ===\n");

            ProductFileReader reader = new ProductFileReader(filePath);
            List<Product> products = reader.ReadProducts();

            if (products.Count == 0)
            {
                Console.WriteLine("Нет данных для обработки. Сначала запустите Задание 2.");
                Console.ReadKey();
                return;
            }

            ProductProcessor processor = new ProductProcessor(products);

            processor.DisplayProducts("Исходный список товаров:");

            processor.SortByPrice(true);
            processor.DisplayProducts("Сортировка по цене (по возрастанию):");

            processor.SortByPrice(false);
            processor.DisplayProducts("Сортировка по цене (по убыванию):");

            List<Product> electronics = processor.FilterByCategory("Электроника");
            Console.WriteLine("\nТовары категории 'Электроника':");
            foreach (Product p in electronics)
            {
                Console.WriteLine($"  {p.Name} - {p.Price} руб.");
            }

            Product mostExpensive = processor.FindMostExpensive();
            Product cheapest = processor.FindCheapest();

            Console.WriteLine($"\nСамый дорогой товар: {mostExpensive.Name} - {mostExpensive.Price} руб.");
            Console.WriteLine($"Самый дешевый товар: {cheapest.Name} - {cheapest.Price} руб.");

            double average = processor.GetAveragePrice();
            Console.WriteLine($"\nСредняя цена всех товаров: {average:F2} руб.");

            Console.WriteLine("\nНажмите любую клавишу для завершения...");
            Console.ReadKey();
        }
    }
}