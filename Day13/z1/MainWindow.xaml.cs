using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace CRMBusiness
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<Client> _clients = new ObservableCollection<Client>();
        private Client? _selectedClient;

        public ICommand AddClientCommand { get; }
        public ICommand EditClientCommand { get; }
        public ICommand DeleteClientCommand { get; }

        public ObservableCollection<Client> Clients
        {
            get => _clients;
            set
            {
                _clients = value;
                ClientsListView.ItemsSource = _clients;
            }
        }

        public Client? SelectedClient
        {
            get => _selectedClient;
            set
            {
                _selectedClient = value;
                UpdateDisplay();
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            AddClientCommand = new RelayCommand(_ => AddClient(), _ => true);
            EditClientCommand = new RelayCommand(_ => EditClient(), _ => SelectedClient != null);
            DeleteClientCommand = new RelayCommand(_ => DeleteClient(), _ => SelectedClient != null);

            DataContext = this;

            InitializeData();
        }

        private void InitializeData()
        {
            Clients = new ObservableCollection<Client>();

            for (int i = 1; i <= 5; i++)
            {
                Clients.Add(new Client
                {
                    FullName = $"Клиент {i}",
                    Phone = $"+7 (900) 000-00-0{i}",
                    Email = $"client{i}@example.com",
                    InteractionHistory = $"История взаимодействий с клиентом {i}"
                });
            }
        }

        private void AddClient()
        {
            var dialog = new ClientDialogWindow();
            dialog.Owner = this;
            dialog.ShowDialog();

            if (dialog.IsConfirmed)
            {
                Clients.Add(dialog.Client);
                MessageBox.Show("Клиент добавлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void EditClient()
        {
            if (SelectedClient == null) return;

            var dialog = new ClientDialogWindow(SelectedClient);
            dialog.Owner = this;
            dialog.ShowDialog();

            if (dialog.IsConfirmed)
            {
                SelectedClient.FullName = dialog.Client.FullName;
                SelectedClient.Phone = dialog.Client.Phone;
                SelectedClient.Email = dialog.Client.Email;
                SelectedClient.InteractionHistory = dialog.Client.InteractionHistory;
                ClientsListView.Items.Refresh();
                UpdateDisplay();
                MessageBox.Show("Данные клиента обновлены!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void DeleteClient()
        {
            if (SelectedClient == null) return;

            MessageBoxResult result = MessageBox.Show($"Удалить клиента '{SelectedClient.FullName}'?",
                "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Clients.Remove(SelectedClient);
                SelectedClient = null;
                MessageBox.Show("Клиент удален!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void UpdateDisplay()
        {
            if (SelectedClient != null)
            {
                DisplayFullName.Text = SelectedClient.FullName;
                DisplayPhone.Text = SelectedClient.Phone;
                DisplayEmail.Text = SelectedClient.Email;
                DisplayHistory.Text = SelectedClient.InteractionHistory;
            }
            else
            {
                DisplayFullName.Text = string.Empty;
                DisplayPhone.Text = string.Empty;
                DisplayEmail.Text = string.Empty;
                DisplayHistory.Text = string.Empty;
            }
        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AboutMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("CRM для небольшого бизнеса\nВерсия 1.0", "О программе",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}