using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace CRMBusiness
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<Client> _clients = new ObservableCollection<Client>();
        private Client? _selectedClient;

        public MainWindow()
        {
            InitializeComponent();
            InitializeData();
        }

        private void InitializeData()
        {
            ClientsListView.ItemsSource = _clients;

            for (int i = 1; i <= 5; i++)
            {
                _clients.Add(new Client
                {
                    FullName = $"Клиент {i}",
                    Phone = $"+7 (900) 000-00-0{i}",
                    Email = $"client{i}@example.com",
                    InteractionHistory = $"История взаимодействий с клиентом {i}"
                });
            }
        }

        private void ClientsListView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            _selectedClient = ClientsListView.SelectedItem as Client;

            if (_selectedClient != null)
            {
                FullNameTextBox.Text = _selectedClient.FullName;
                PhoneTextBox.Text = _selectedClient.Phone;
                EmailTextBox.Text = _selectedClient.Email;
                HistoryTextBox.Text = _selectedClient.InteractionHistory;
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FullNameTextBox.Text))
            {
                MessageBox.Show("Введите ФИО клиента!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Client newClient = new Client
            {
                FullName = FullNameTextBox.Text,
                Phone = PhoneTextBox.Text,
                Email = EmailTextBox.Text,
                InteractionHistory = HistoryTextBox.Text
            };

            _clients.Add(newClient);
            ClearForm();
            MessageBox.Show("Клиент добавлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedClient == null)
            {
                MessageBox.Show("Выберите клиента для редактирования!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(FullNameTextBox.Text))
            {
                MessageBox.Show("Введите ФИО клиента!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            _selectedClient.FullName = FullNameTextBox.Text;
            _selectedClient.Phone = PhoneTextBox.Text;
            _selectedClient.Email = EmailTextBox.Text;
            _selectedClient.InteractionHistory = HistoryTextBox.Text;

            ClientsListView.Items.Refresh();
            MessageBox.Show("Данные клиента обновлены!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedClient == null)
            {
                MessageBox.Show("Выберите клиента для удаления!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            MessageBoxResult result = MessageBox.Show($"Удалить клиента '{_selectedClient.FullName}'?",
                "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                _clients.Remove(_selectedClient);
                ClearForm();
                _selectedClient = null;
                MessageBox.Show("Клиент удален!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            FullNameTextBox.Text = "";
            PhoneTextBox.Text = "";
            EmailTextBox.Text = "";
            HistoryTextBox.Text = "";
            ClientsListView.SelectedItem = null;
            _selectedClient = null;
        }
    }
}