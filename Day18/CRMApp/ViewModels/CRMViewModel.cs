using CRMApp.Data;
using CRMApp.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CRMApp.ViewModels
{
    public class CRMViewModel : BaseViewModel
    {
        private readonly ClientRepository _repo;
        private ObservableCollection<Client> _clients = new ObservableCollection<Client>();
        private Client? _selectedClient;

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
                (UpdateCommand as AsyncRelayCommand)?.RaiseCanExecuteChanged();
                (DeleteCommand as AsyncRelayCommand)?.RaiseCanExecuteChanged();
            }
        }

        public ICommand LoadCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand SaveClientsCommand { get; }

        public CRMViewModel()
        {
            _repo = new ClientRepository();
            Clients = new ObservableCollection<Client>();

            LoadCommand = new AsyncRelayCommand(LoadAsync);
            AddCommand = new AsyncRelayCommand(AddAsync);
            UpdateCommand = new AsyncRelayCommand(UpdateAsync, () => SelectedClient != null);
            DeleteCommand = new AsyncRelayCommand(DeleteAsync, () => SelectedClient != null);
            SaveClientsCommand = new AsyncRelayCommand(SaveAsync);
        }

        private async Task LoadAsync()
        {
            var list = await _repo.GetAllAsync();
            Clients.Clear();
            foreach (var c in list) Clients.Add(c);
        }

        private async Task AddAsync()
        {
            await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                var dialog = new Window
                {
                    Title = "Добавление клиента",
                    Width = 420,
                    Height = 480,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                    Owner = Application.Current.MainWindow,
                    ResizeMode = ResizeMode.NoResize,
                    Background = new SolidColorBrush(Color.FromRgb(248, 249, 250))
                };

                var mainPanel = new Border
                {
                    Background = new SolidColorBrush(Color.FromRgb(248, 249, 250)),
                    Padding = new Thickness(25)
                };

                var stackPanel = new StackPanel();

                stackPanel.Children.Add(new TextBlock
                {
                    Text = "Новый клиент",
                    FontSize = 18,
                    FontWeight = FontWeights.Bold,
                    Margin = new Thickness(0, 0, 0, 20),
                    Foreground = new SolidColorBrush(Color.FromRgb(44, 62, 80))
                });

                stackPanel.Children.Add(new TextBlock
                {
                    Text = "Имя",
                    FontWeight = FontWeights.SemiBold,
                    Margin = new Thickness(0, 0, 0, 5),
                    Foreground = new SolidColorBrush(Color.FromRgb(52, 73, 94))
                });
                var txtName = new TextBox
                {
                    Height = 35,
                    Margin = new Thickness(0, 0, 0, 15),
                    Background = Brushes.White,
                    BorderBrush = new SolidColorBrush(Color.FromRgb(206, 212, 218)),
                    BorderThickness = new Thickness(1)
                };
                stackPanel.Children.Add(txtName);

                stackPanel.Children.Add(new TextBlock
                {
                    Text = "Телефон",
                    FontWeight = FontWeights.SemiBold,
                    Margin = new Thickness(0, 0, 0, 5),
                    Foreground = new SolidColorBrush(Color.FromRgb(52, 73, 94))
                });
                var txtPhone = new TextBox
                {
                    Height = 35,
                    Margin = new Thickness(0, 0, 0, 15),
                    Background = Brushes.White,
                    BorderBrush = new SolidColorBrush(Color.FromRgb(206, 212, 218)),
                    BorderThickness = new Thickness(1)
                };
                stackPanel.Children.Add(txtPhone);

                stackPanel.Children.Add(new TextBlock
                {
                    Text = "Email",
                    FontWeight = FontWeights.SemiBold,
                    Margin = new Thickness(0, 0, 0, 5),
                    Foreground = new SolidColorBrush(Color.FromRgb(52, 73, 94))
                });
                var txtEmail = new TextBox
                {
                    Height = 35,
                    Margin = new Thickness(0, 0, 0, 15),
                    Background = Brushes.White,
                    BorderBrush = new SolidColorBrush(Color.FromRgb(206, 212, 218)),
                    BorderThickness = new Thickness(1)
                };
                stackPanel.Children.Add(txtEmail);

                var chkActive = new CheckBox
                {
                    Content = "Активен",
                    IsChecked = true,
                    Margin = new Thickness(0, 5, 0, 30),
                    Foreground = new SolidColorBrush(Color.FromRgb(52, 73, 94))
                };
                stackPanel.Children.Add(chkActive);

                var buttonPanel = new StackPanel { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Center, Margin = new Thickness(0, 10, 0, 0) };

                var btnSave = new Button
                {
                    Content = "Сохранить",
                    Width = 110,
                    Height = 38,
                    Margin = new Thickness(5),
                    Background = new SolidColorBrush(Color.FromRgb(46, 204, 113)),
                    Foreground = Brushes.White,
                    FontWeight = FontWeights.SemiBold,
                    BorderThickness = new Thickness(0),
                    Cursor = Cursors.Hand
                };

                var btnCancel = new Button
                {
                    Content = "Отмена",
                    Width = 110,
                    Height = 38,
                    Margin = new Thickness(5),
                    Background = new SolidColorBrush(Color.FromRgb(149, 165, 166)),
                    Foreground = Brushes.White,
                    FontWeight = FontWeights.SemiBold,
                    BorderThickness = new Thickness(0),
                    Cursor = Cursors.Hand
                };

                buttonPanel.Children.Add(btnSave);
                buttonPanel.Children.Add(btnCancel);
                stackPanel.Children.Add(buttonPanel);

                mainPanel.Child = stackPanel;
                dialog.Content = mainPanel;

                btnSave.Click += async (s, e) =>
                {
                    if (string.IsNullOrWhiteSpace(txtName.Text))
                    {
                        MessageBox.Show("Введите имя клиента!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    var newClient = new Client
                    {
                        Name = txtName.Text,
                        Phone = txtPhone.Text,
                        Email = txtEmail.Text,
                        IsActive = chkActive.IsChecked ?? true
                    };

                    await _repo.AddAsync(newClient);
                    await _repo.SaveAsync();
                    await LoadAsync();
                    dialog.Close();
                };

                btnCancel.Click += (s, e) => dialog.Close();

                dialog.ShowDialog();
            });
        }

        private async Task UpdateAsync()
        {
            if (SelectedClient == null) return;

            await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                var dialog = new Window
                {
                    Title = "Редактирование клиента",
                    Width = 420,
                    Height = 480,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                    Owner = Application.Current.MainWindow,
                    ResizeMode = ResizeMode.NoResize,
                    Background = new SolidColorBrush(Color.FromRgb(248, 249, 250))
                };

                var mainPanel = new Border
                {
                    Background = new SolidColorBrush(Color.FromRgb(248, 249, 250)),
                    Padding = new Thickness(25)
                };

                var stackPanel = new StackPanel();

                stackPanel.Children.Add(new TextBlock
                {
                    Text = "Редактирование клиента",
                    FontSize = 18,
                    FontWeight = FontWeights.Bold,
                    Margin = new Thickness(0, 0, 0, 20),
                    Foreground = new SolidColorBrush(Color.FromRgb(44, 62, 80))
                });

                stackPanel.Children.Add(new TextBlock
                {
                    Text = "Имя",
                    FontWeight = FontWeights.SemiBold,
                    Margin = new Thickness(0, 0, 0, 5),
                    Foreground = new SolidColorBrush(Color.FromRgb(52, 73, 94))
                });
                var txtName = new TextBox
                {
                    Text = SelectedClient.Name ?? "",
                    Height = 35,
                    Margin = new Thickness(0, 0, 0, 15),
                    Background = Brushes.White,
                    BorderBrush = new SolidColorBrush(Color.FromRgb(206, 212, 218)),
                    BorderThickness = new Thickness(1)
                };
                stackPanel.Children.Add(txtName);

                stackPanel.Children.Add(new TextBlock
                {
                    Text = "Телефон",
                    FontWeight = FontWeights.SemiBold,
                    Margin = new Thickness(0, 0, 0, 5),
                    Foreground = new SolidColorBrush(Color.FromRgb(52, 73, 94))
                });
                var txtPhone = new TextBox
                {
                    Text = SelectedClient.Phone ?? "",
                    Height = 35,
                    Margin = new Thickness(0, 0, 0, 15),
                    Background = Brushes.White,
                    BorderBrush = new SolidColorBrush(Color.FromRgb(206, 212, 218)),
                    BorderThickness = new Thickness(1)
                };
                stackPanel.Children.Add(txtPhone);

                stackPanel.Children.Add(new TextBlock
                {
                    Text = "Email",
                    FontWeight = FontWeights.SemiBold,
                    Margin = new Thickness(0, 0, 0, 5),
                    Foreground = new SolidColorBrush(Color.FromRgb(52, 73, 94))
                });
                var txtEmail = new TextBox
                {
                    Text = SelectedClient.Email ?? "",
                    Height = 35,
                    Margin = new Thickness(0, 0, 0, 15),
                    Background = Brushes.White,
                    BorderBrush = new SolidColorBrush(Color.FromRgb(206, 212, 218)),
                    BorderThickness = new Thickness(1)
                };
                stackPanel.Children.Add(txtEmail);

                var chkActive = new CheckBox
                {
                    Content = "Активен",
                    IsChecked = SelectedClient.IsActive,
                    Margin = new Thickness(0, 5, 0, 30),
                    Foreground = new SolidColorBrush(Color.FromRgb(52, 73, 94))
                };
                stackPanel.Children.Add(chkActive);

                var buttonPanel = new StackPanel { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Center, Margin = new Thickness(0, 10, 0, 0) };

                var btnSave = new Button
                {
                    Content = "Сохранить",
                    Width = 110,
                    Height = 38,
                    Margin = new Thickness(5),
                    Background = new SolidColorBrush(Color.FromRgb(46, 204, 113)),
                    Foreground = Brushes.White,
                    FontWeight = FontWeights.SemiBold,
                    BorderThickness = new Thickness(0),
                    Cursor = Cursors.Hand
                };

                var btnCancel = new Button
                {
                    Content = "Отмена",
                    Width = 110,
                    Height = 38,
                    Margin = new Thickness(5),
                    Background = new SolidColorBrush(Color.FromRgb(149, 165, 166)),
                    Foreground = Brushes.White,
                    FontWeight = FontWeights.SemiBold,
                    BorderThickness = new Thickness(0),
                    Cursor = Cursors.Hand
                };

                buttonPanel.Children.Add(btnSave);
                buttonPanel.Children.Add(btnCancel);
                stackPanel.Children.Add(buttonPanel);

                mainPanel.Child = stackPanel;
                dialog.Content = mainPanel;

                btnSave.Click += async (s, e) =>
                {
                    if (string.IsNullOrWhiteSpace(txtName.Text))
                    {
                        MessageBox.Show("Введите имя клиента!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    SelectedClient.Name = txtName.Text;
                    SelectedClient.Phone = txtPhone.Text;
                    SelectedClient.Email = txtEmail.Text;
                    SelectedClient.IsActive = chkActive.IsChecked ?? true;

                    await _repo.UpdateAsync(SelectedClient);
                    await _repo.SaveAsync();
                    await LoadAsync();
                    dialog.Close();
                    MessageBox.Show("Клиент обновлён!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                };

                btnCancel.Click += (s, e) => dialog.Close();

                dialog.ShowDialog();
            });
        }

        private async Task DeleteAsync()
        {
            if (SelectedClient == null) return;

            var result = MessageBox.Show($"Удалить клиента '{SelectedClient.Name}'?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                await _repo.DeleteAsync(SelectedClient);
                await _repo.SaveAsync();
                await LoadAsync();
                SelectedClient = null;
            }
        }

        private async Task SaveAsync()
        {
            await _repo.SaveAsync();
            MessageBox.Show("Данные сохранены!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}