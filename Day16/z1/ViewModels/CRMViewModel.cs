using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using CRMBusiness.Models;
using CRMBusiness.Services;

namespace CRMBusiness.ViewModels
{
    public class CRMViewModel : INotifyPropertyChanged
    {
        private readonly ClientService _clientService;
        private ObservableCollection<Client> _clients = new();
        private Client? _selectedClient;
        private bool _isLoading;

        public ICommand AddClientCommand { get; }
        public ICommand DeleteClientCommand { get; }
        public ICommand SaveClientCommand { get; }

        public ObservableCollection<Client> Clients
        {
            get => _clients;
            set { _clients = value; OnPropertyChanged(); }
        }

        public Client? SelectedClient
        {
            get => _selectedClient;
            set
            {
                _selectedClient = value;
                OnPropertyChanged();
            }
        }

        public bool IsLoading
        {
            get => _isLoading;
            set { _isLoading = value; OnPropertyChanged(); }
        }

        public CRMViewModel()
        {
            _clientService = new ClientService();

            AddClientCommand = new RelayCommand(_ => AddClient(), _ => !IsLoading);
            DeleteClientCommand = new RelayCommand(_ => DeleteClient(), _ => SelectedClient != null && !IsLoading);
            SaveClientCommand = new RelayCommand(_ => SaveClient(), _ => SelectedClient != null && !IsLoading);

            _ = LoadClientsAsync();
        }

        private async Task LoadClientsAsync()
        {
            IsLoading = true;
            try
            {
                Clients = await _clientService.LoadClientsAsync();
                if (Clients.Count > 0) SelectedClient = Clients[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
            finally
            {
                IsLoading = false;
            }
        }

        private void AddClient()
        {
            var newClient = new Client
            {
                FullName = "Новый клиент",
                Phone = "+7 (900) 000-00-00",
                Email = "new@example.com",
                ClientType = "Обычный",
                InteractionHistory = "Новый клиент."
            };
            Clients.Add(newClient);
            SelectedClient = newClient;
        }

        private void SaveClient()
        {
            if (SelectedClient == null) return;
            MessageBox.Show("Данные сохранены!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void DeleteClient()
        {
            if (SelectedClient == null) return;
            var result = MessageBox.Show($"Удалить '{SelectedClient.FullName}'?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Clients.Remove(SelectedClient);
                SelectedClient = Clients.Count > 0 ? Clients[0] : null;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}