using System.Windows;

namespace CRMBusiness
{
    public partial class ClientDialogWindow : Window
    {
        public Client Client { get; private set; }
        public bool IsConfirmed { get; private set; }

        public ClientDialogWindow(Client? client = null)
        {
            InitializeComponent();

            if (client != null)
            {
                Client = client;
                FullNameTextBox.Text = client.FullName;
                PhoneTextBox.Text = client.Phone;
                EmailTextBox.Text = client.Email;
                HistoryTextBox.Text = client.InteractionHistory;
                Title = "Редактирование клиента";
            }
            else
            {
                Client = new Client();
                Title = "Добавление клиента";
            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FullNameTextBox.Text))
            {
                MessageBox.Show("Введите ФИО клиента!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Client.FullName = FullNameTextBox.Text;
            Client.Phone = PhoneTextBox.Text;
            Client.Email = EmailTextBox.Text;
            Client.InteractionHistory = HistoryTextBox.Text;

            IsConfirmed = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            IsConfirmed = false;
            Close();
        }
    }
}