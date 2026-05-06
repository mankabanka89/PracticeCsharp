using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace CRMBusiness
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private ObservableCollection<Client> _clients = new ObservableCollection<Client>();
        private Client? _selectedClient;

        public ICommand AddClientCommand { get; }
        public ICommand EditClientCommand { get; }
        public ICommand DeleteClientCommand { get; }
        public ICommand SaveCommand { get; }

        public ObservableCollection<Client> Clients
        {
            get => _clients;
            set
            {
                _clients = value;
                OnPropertyChanged();
                ClientsListBox.ItemsSource = _clients;
            }
        }

        public Client? SelectedClient
        {
            get => _selectedClient;
            set
            {
                _selectedClient = value;
                OnPropertyChanged();
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            AddClientCommand = new RelayCommand(_ => AddClient(), _ => true);
            EditClientCommand = new RelayCommand(_ => EditClient(), _ => SelectedClient != null);
            DeleteClientCommand = new RelayCommand(_ => DeleteClient(), _ => SelectedClient != null);
            SaveCommand = new RelayCommand(_ => SaveClient(), _ => SelectedClient != null);

            DataContext = this;

            InitializeData();
        }

        private void InitializeData()
        {
            Clients.Add(new Client
            {
                FullName = "Иванов Иван Иванович",
                Phone = "+7 (900) 111-22-33",
                Email = "ivan@example.com",
                ClientType = "VIP",
                InteractionHistory = "VIP клиент. Любит кофе."
            });

            Clients.Add(new Client
            {
                FullName = "Петров Петр Петрович",
                Phone = "+7 (900) 444-55-66",
                Email = "petr@example.com",
                ClientType = "Обычный",
                InteractionHistory = "Клиент с 2023 года."
            });

            Clients.Add(new Client
            {
                FullName = "Сидорова Анна Сергеевна",
                Phone = "+7 (900) 777-88-99",
                Email = "anna@example.com",
                ClientType = "VIP",
                InteractionHistory = "Постоянный клиент."
            });

            SelectedClient = Clients[0];
        }

        private void AddClient()
        {
            var newClient = new Client
            {
                FullName = "Новый клиент",
                Phone = "+7 (900) 000-00-00",
                Email = "new@example.com",
                ClientType = "Обычный",
                InteractionHistory = "Новый клиент. История пуста."
            };
            Clients.Add(newClient);
            SelectedClient = newClient;
        }

        private void EditClient()
        {
            if (SelectedClient == null) return;
            MessageBox.Show("Редактируйте данные в правой панели и нажмите 'Сохранить'",
                "Редактирование", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void SaveClient()
        {
            if (SelectedClient == null) return;
            ClientsListBox.Items.Refresh();
            MessageBox.Show("Данные сохранены!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void DeleteClient()
        {
            if (SelectedClient == null) return;

            var result = MessageBox.Show($"Удалить клиента '{SelectedClient.FullName}'?",
                "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Clients.Remove(SelectedClient);
                SelectedClient = Clients.Count > 0 ? Clients[0] : null;
            }
        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AboutMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("CRM для небольшого бизнеса\nВерсия 2.0",
                "О программе", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}