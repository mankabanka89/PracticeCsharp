using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BinaryFileWriter
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
            Console.WriteLine($"  Товар: {Name}, Цена: {Price} руб., Категория: {Category}");
        }
    }

    class ProductFileWriter
    {
        private string filePath;

        public ProductFileWriter(string filePath)
        {
            this.filePath = filePath;
        }

        public void WriteProducts(List<Product> products)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(filePath, FileMode.Create)))
            {
                writer.Write(products.Count);

                foreach (Product product in products)
                {
                    writer.Write(product.Name);
                    writer.Write(product.Price);
                    writer.Write(product.Category);
                }
            }

            Console.WriteLine($"Записано {products.Count} товаров в файл: {filePath}");
        }

        public List<Product> ReadProducts()
        {
            List<Product> products = new List<Product>();

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Файл {filePath} не существует");
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

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            string filePath = "file.data";

            List<Product> products = new List<Product>
            {
                new Product("Ноутбук", 45000.99, "Электроника"),
                new Product("Мышь", 1200.50, "Комплектующие"),
                new Product("Клавиатура", 2500.00, "Комплектующие"),
                new Product("Монитор", 15000.00, "Электроника"),
                new Product("Наушники", 3500.00, "Аудио")
            };

            Console.WriteLine("=== Запись списка товаров в двоичный файл ===\n");

            Console.WriteLine("Исходный список товаров:");
            foreach (Product p in products)
            {
                p.Display();
            }

            ProductFileWriter writer = new ProductFileWriter(filePath);
            writer.WriteProducts(products);

            Console.WriteLine("\nЧтение товаров из файла:");
            List<Product> loadedProducts = writer.ReadProducts();

            foreach (Product p in loadedProducts)
            {
                p.Display();
            }

            Console.WriteLine($"\nРазмер файла: {new FileInfo(filePath).Length} байт");

            Console.WriteLine("\nНажмите любую клавишу для завершения...");
            Console.ReadKey();
        }
    }
}