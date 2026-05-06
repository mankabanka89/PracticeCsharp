using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CRMBusiness.Models;

namespace CRMBusiness.Services
{
    public class ClientService
    {
        public async Task<ObservableCollection<Client>> LoadClientsAsync()
        {
            return await Task.Run(() =>
            {
                System.Threading.Thread.Sleep(2000);

                var clients = new ObservableCollection<Client>();

                clients.Add(new Client
                {
                    FullName = "Иванов Иван Иванович",
                    Phone = "+7 (900) 111-22-33",
                    Email = "ivan@example.com",
                    ClientType = "VIP",
                    InteractionHistory = "VIP клиент. Любит кофе."
                });

                clients.Add(new Client
                {
                    FullName = "Петров Петр Петрович",
                    Phone = "+7 (900) 444-55-66",
                    Email = "petr@example.com",
                    ClientType = "Обычный",
                    InteractionHistory = "Клиент с 2023 года."
                });

                clients.Add(new Client
                {
                    FullName = "Сидорова Анна Сергеевна",
                    Phone = "+7 (900) 777-88-99",
                    Email = "anna@example.com",
                    ClientType = "VIP",
                    InteractionHistory = "Постоянный клиент."
                });

                return clients;
            });
        }
    }
}