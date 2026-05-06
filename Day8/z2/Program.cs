using System;
using System.Collections.Generic;

namespace MyDictionaryImplementation
{
    class MyDictionary<TKey, TValue>
    {
        private struct KeyValuePair
        {
            public TKey Key;
            public TValue Value;

            public KeyValuePair(TKey key, TValue value)
            {
                Key = key;
                Value = value;
            }
        }

        private KeyValuePair[] items;
        private int count;
        private int capacity;

        public MyDictionary()
        {
            capacity = 4;
            items = new KeyValuePair[capacity];
            count = 0;
        }

        public int Count
        {
            get { return count; }
        }

        public TValue this[TKey key]
        {
            get
            {
                TValue value;
                if (Find(key, out value))
                {
                    return value;
                }
                throw new Exception($"Ключ {key} не найден");
            }
            set
            {
                for (int i = 0; i < count; i++)
                {
                    if (items[i].Key.Equals(key))
                    {
                        items[i] = new KeyValuePair(key, value);
                        return;
                    }
                }
                Add(key, value);
            }
        }

        private void Resize()
        {
            capacity *= 2;
            KeyValuePair[] newItems = new KeyValuePair[capacity];
            for (int i = 0; i < count; i++)
            {
                newItems[i] = items[i];
            }
            items = newItems;
        }

        public void Add(TKey key, TValue value)
        {
            if (key == null)
            {
                throw new Exception("Ключ не может быть null");
            }

            for (int i = 0; i < count; i++)
            {
                if (items[i].Key.Equals(key))
                {
                    throw new Exception($"Ключ {key} уже существует в словаре");
                }
            }

            if (count == capacity)
            {
                Resize();
            }

            items[count] = new KeyValuePair(key, value);
            count++;
            Console.WriteLine($"Добавлено: [{key}] = {value}");
        }

        public bool Remove(TKey key)
        {
            for (int i = 0; i < count; i++)
            {
                if (items[i].Key.Equals(key))
                {
                    for (int j = i; j < count - 1; j++)
                    {
                        items[j] = items[j + 1];
                    }
                    count--;
                    Console.WriteLine($"Удален ключ: {key}");
                    return true;
                }
            }
            Console.WriteLine($"Ключ {key} не найден для удаления");
            return false;
        }

        public bool Find(TKey key, out TValue value)
        {
            for (int i = 0; i < count; i++)
            {
                if (items[i].Key.Equals(key))
                {
                    value = items[i].Value;
                    Console.WriteLine($"Найден: [{key}] = {value}");
                    return true;
                }
            }
            value = default(TValue);
            Console.WriteLine($"Ключ {key} не найден");
            return false;
        }

        public bool ContainsKey(TKey key)
        {
            for (int i = 0; i < count; i++)
            {
                if (items[i].Key.Equals(key))
                {
                    return true;
                }
            }
            return false;
        }

        public void ShowAll()
        {
            if (count == 0)
            {
                Console.WriteLine("Словарь пуст");
                return;
            }

            Console.WriteLine($"\n--- Содержимое словаря (всего {count} элементов) ---");
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine($"  [{items[i].Key}] = {items[i].Value}");
            }
        }
    }

    class DictionaryManager<TKey, TValue>
    {
        private MyDictionary<TKey, TValue> dictionary;

        public DictionaryManager()
        {
            dictionary = new MyDictionary<TKey, TValue>();
        }

        public void AddItem(TKey key, TValue value)
        {
            dictionary.Add(key, value);
        }

        public void RemoveItem(TKey key)
        {
            dictionary.Remove(key);
        }

        public TValue FindItem(TKey key)
        {
            TValue value;
            dictionary.Find(key, out value);
            return value;
        }

        public bool CheckKey(TKey key)
        {
            return dictionary.ContainsKey(key);
        }

        public void ShowAllItems()
        {
            dictionary.ShowAll();
        }

        public int GetCount()
        {
            return dictionary.Count;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            DictionaryManager<string, int> manager = new DictionaryManager<string, int>();

            Console.WriteLine("--- Моя хеш-таблица ---\n");

            Console.WriteLine("--- Добавление элементов ---");
            manager.AddItem("apple", 10);
            manager.AddItem("banana", 20);
            manager.AddItem("orange", 30);
            manager.AddItem("grape", 40);

            manager.ShowAllItems();

            Console.WriteLine($"\n--- Поиск элементов ---");
            int value = manager.FindItem("banana");
            manager.FindItem("grape");
            manager.FindItem("watermelon");

            Console.WriteLine($"\n--- Проверка наличия ключей ---");
            Console.WriteLine($"Есть ключ 'apple'? {manager.CheckKey("apple")}");
            Console.WriteLine($"Есть ключ 'peach'? {manager.CheckKey("peach")}");

            Console.WriteLine($"\n--- Удаление элементов ---");
            manager.RemoveItem("orange");
            manager.RemoveItem("watermelon");

            manager.ShowAllItems();

            Console.WriteLine($"\n--- Обновление значения через индексатор (myDict[key] = value) ---");

            MyDictionary<string, int> myDict = new MyDictionary<string, int>();
            myDict.Add("cat", 5);
            myDict.Add("dog", 3);
            myDict.ShowAll();

            myDict["cat"] = 99;
            myDict["dog"] = 77;
            myDict["bird"] = 100;

            Console.WriteLine($"\nПосле обновления:");
            myDict.ShowAll();

            Console.WriteLine($"\nЗначение myDict[\"cat\"] = {myDict["cat"]}");
            Console.WriteLine($"Значение myDict[\"bird\"] = {myDict["bird"]}");

            Console.WriteLine("\nНажмите любую клавишу для завершения...");
            Console.ReadKey();
        }
    }
}