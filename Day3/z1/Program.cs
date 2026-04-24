namespace TaskClassA
{
    class A
    {
        private int a;
        private int b;

        public A(int a, int b)
        {
            this.a = a;
            this.b = b;
        }

        public int Sum()
        {
            return a + b;
        }

        public int Difference()
        {
            return a - b;
        }

        public int ValueA
        {
            get { return a; }
            set { a = value; }
        }

        public int ValueB
        {
            get { return b; }
            set { b = value; }
        }

        public override string ToString()
        {
            return $"a = {a}, b = {b}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            A obj = new A(15, 7);

            Console.WriteLine("=== Демонстрация работы класса A ===\n");

            Console.WriteLine($"Исходные значения: {obj}");

            Console.WriteLine($"\nМетод Sum (a + b): {obj.ValueA} + {obj.ValueB} = {obj.Sum()}");

            Console.WriteLine($"Метод Difference (a - b): {obj.ValueA} - {obj.ValueB} = {obj.Difference()}");

            Console.WriteLine("\n=== Изменение значений через свойства ===");
            obj.ValueA = 25;
            obj.ValueB = 10;
            Console.WriteLine($"Новые значения: {obj}");
            Console.WriteLine($"Sum (a + b): {obj.ValueA} + {obj.ValueB} = {obj.Sum()}");
            Console.WriteLine($"Difference (a - b): {obj.ValueA} - {obj.ValueB} = {obj.Difference()}");

            Console.WriteLine("\n=== Демонстрация с другими значениями ===");
            A anotherObj = new A(100, 45);
            Console.WriteLine($"Объект 2: {anotherObj}");
            Console.WriteLine($"Sum: {anotherObj.Sum()}");
            Console.WriteLine($"Difference: {anotherObj.Difference()}");

            Console.WriteLine("\nНажмите любую клавишу для завершения...");
            Console.ReadKey();
        }
    }
}