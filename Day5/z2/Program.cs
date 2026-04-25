using System;

public class Author
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public Author(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public void DisplayInfo()
    {
        Console.Write($"{FirstName} {LastName}");
    }
}

public class TableOfContents
{
    private string[] chapters;

    public TableOfContents(string[] chapterTitles)
    {
        chapters = chapterTitles;
    }

    public void Display()
    {
        Console.WriteLine("Содержание:");
        for (int i = 0; i < chapters.Length; i++)
        {
            Console.WriteLine($"  {i + 1}. {chapters[i]}");
        }
    }
}

public class Book
{
    private static int nextId = 1;
    public int Id { get; private set; }
    public string Title { get; set; }

    public Author[] Authors { get; set; }

    private TableOfContents Contents { get; set; }

    public Library Library { get; set; }

    public Book(string title, Author[] authors, string[] chapters)
    {
        Id = nextId++;
        Title = title;
        Authors = authors;
        Contents = new TableOfContents(chapters);
        Library = null;
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"\nКнига #{Id}: {Title}");

        Console.Write("Авторы: ");
        for (int i = 0; i < Authors.Length; i++)
        {
            Authors[i].DisplayInfo();
            if (i < Authors.Length - 1)
            {
                Console.Write(", ");
            }
        }
        Console.WriteLine();

        Contents.Display();

        if (Library != null)
        {
            Console.WriteLine($"Находится в библиотеке: {Library.Name}");
        }
        else
        {
            Console.WriteLine("Не привязана к библиотеке");
        }
    }
}

public class Library
{
    public string Name { get; set; }
    public string Address { get; set; }

    public Library(string name, string address)
    {
        Name = name;
        Address = address;
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"Библиотека: {Name}, {Address}");
    }
}

public static class Program
{
    public static void Main()
    {
        Author[] authorsBook1 = new Author[]
        {
            new Author("Лев", "Толстой"),
            new Author("Алексей", "Пешков")
        };

        string[] chaptersBook1 = new string[]
        {
            "Введение",
            "Глава 1",
            "Глава 2",
            "Заключение"
        };

        Author[] authorsBook2 = new Author[]
        {
            new Author("Фёдор", "Достоевский")
        };

        string[] chaptersBook2 = new string[]
        {
            "Часть первая",
            "Часть вторая",
            "Эпилог"
        };

        Author[] authorsBook3 = new Author[]
        {
            new Author("Александр", "Пушкин"),
            new Author("Николай", "Гоголь"),
            new Author("Иван", "Тургенев")
        };

        string[] chaptersBook3 = new string[]
        {
            "Стихотворения",
            "Поэмы",
            "Проза"
        };

        Book[] books = new Book[]
        {
            new Book("Война и мир", authorsBook1, chaptersBook1),
            new Book("Преступление и наказание", authorsBook2, chaptersBook2),
            new Book("Сборник классики", authorsBook3, chaptersBook3)
        };

        Library centralLibrary = new Library("Центральная библиотека", "ул. Ленина, 10");

        books[0].Library = centralLibrary;
        books[2].Library = centralLibrary;

        Console.WriteLine("Информация о книгах:");
        Console.WriteLine("====================");

        foreach (Book book in books)
        {
            book.DisplayInfo();
        }

        Console.WriteLine("\n====================");
        centralLibrary.DisplayInfo();
    }
}