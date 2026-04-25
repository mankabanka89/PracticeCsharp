namespace NotificationSystem;

using System;

public interface IEmailNotifier
{
    void SendNotification(string message);
}

public interface ISmsNotifier
{
    void SendNotification(string message);
}

public class NotificationService : IEmailNotifier, ISmsNotifier
{
    void IEmailNotifier.SendNotification(string message)
    {
        Console.WriteLine($"[EMAIL] Отправка email: {message}");
        Console.WriteLine($"  Адрес: user@example.com");
        Console.WriteLine($"  Тема: Уведомление");
    }

    void ISmsNotifier.SendNotification(string message)
    {
        Console.WriteLine($"[SMS] Отправка SMS: {message}");
        Console.WriteLine($"  Номер: +7 (999) 123-45-67");
    }
}

public static class Program
{
    public static void Main()
    {
        NotificationService service = new NotificationService();

        IEmailNotifier emailNotifier = service;
        ISmsNotifier smsNotifier = service;

        Console.WriteLine("Демонстрация работы системы уведомлений");
        Console.WriteLine("--------------------------------------\n");

        Console.WriteLine("Вызов через интерфейс IEmailNotifier:");
        emailNotifier.SendNotification("Ваш заказ подтверждён!");

        Console.WriteLine("\nВызов через интерфейс ISmsNotifier:");
        smsNotifier.SendNotification("Код подтверждения: 123456");

        Console.WriteLine("\n--------------------------------------");
        Console.WriteLine("Примечание: прямое обращение через объект класса невозможно:");
        // service.SendNotification("Текст"); // Ошибка компиляции!
        Console.WriteLine("Метод SendNotification() недоступен напрямую через объект NotificationService.");
    }
}