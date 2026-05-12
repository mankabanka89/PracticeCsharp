using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace z1
{
    public partial class MainWindow : Window
    {
        private List<Client> allClients = new List<Client>();

        private bool isShowingAll = true;

        public MainWindow()
        {
            InitializeComponent();
            InitializeClients();
        }

        private void MainGrid_Loaded(object sender, RoutedEventArgs e)
        {
            var fadeIn = (Storyboard)this.Resources["FadeInAnimation"];
            fadeIn?.Begin(MainGrid);
        }

        private void InitializeClients()
        {
            allClients = new List<Client>
            {
                new Client { Name = "Иванов Иван", Phone = "+7 (123) 456-78-90", Email = "ivan@mail.ru", IsActive = true },
                new Client { Name = "Петрова Мария", Phone = "+7 (234) 567-89-01", Email = "maria@mail.ru", IsActive = true },
                new Client { Name = "Сидоров Алексей", Phone = "+7 (345) 678-90-12", Email = "alex@mail.ru", IsActive = false },
                new Client { Name = "Козлова Елена", Phone = "+7 (456) 789-01-23", Email = "elena@mail.ru", IsActive = true },
                new Client { Name = "Морозов Дмитрий", Phone = "+7 (567) 890-12-34", Email = "dmitry@mail.ru", IsActive = false }
            };

            RefreshCards();
        }

        private void RefreshCards()
        {
            CardsContainer.Children.Clear();

            var clientsToShow = isShowingAll ? allClients : allClients.Where(c => c.IsActive).ToList();

            foreach (var client in clientsToShow)
            {
                var card = CreateClientCard(client);
                CardsContainer.Children.Add(card);

                var slideIn = (Storyboard)this.Resources["TabSlideInAnimation"];
                slideIn?.Begin(card);
            }
        }

        private Border CreateClientCard(Client client)
        {
            Border cardBorder = new Border
            {
                Style = (Style)this.Resources["ClientCardStyle"],
                Tag = client
            };

            StackPanel innerPanel = new StackPanel();

            TextBlock nameBlock = new TextBlock
            {
                Text = client.Name,
                FontSize = 16,
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(0, 0, 0, 5)
            };
            innerPanel.Children.Add(nameBlock);

            TextBlock phoneBlock = new TextBlock
            {
                Text = $"📞 {client.Phone}",
                FontSize = 12,
                Foreground = new SolidColorBrush(Colors.Gray),
                Margin = new Thickness(0, 0, 0, 3)
            };
            innerPanel.Children.Add(phoneBlock);

            TextBlock emailBlock = new TextBlock
            {
                Text = $"✉ {client.Email}",
                FontSize = 12,
                Foreground = new SolidColorBrush(Colors.Gray)
            };
            innerPanel.Children.Add(emailBlock);

            TextBlock statusBlock = new TextBlock
            {
                Text = client.IsActive ? "● Активный клиент" : "○ Неактивный клиент",
                FontSize = 11,
                Foreground = client.IsActive ? new SolidColorBrush(Color.FromRgb(46, 204, 113)) : new SolidColorBrush(Colors.Red),
                Margin = new Thickness(0, 10, 0, 0),
                Visibility = Visibility.Collapsed
            };
            innerPanel.Children.Add(statusBlock);

            Button editButton = new Button
            {
                Content = "Редактировать",
                Width = 100,
                Height = 25,
                Margin = new Thickness(0, 10, 0, 0),
                Background = new SolidColorBrush(Color.FromRgb(52, 152, 219)),
                Foreground = new SolidColorBrush(Colors.White),
                BorderThickness = new Thickness(0),
                Cursor = System.Windows.Input.Cursors.Hand,
                Visibility = Visibility.Collapsed
            };
            editButton.Click += (s, e) => EditClient(client);
            innerPanel.Children.Add(editButton);

            cardBorder.Child = innerPanel;

            cardBorder.MouseEnter += (s, e) =>
            {
                var hoverEnter = (Storyboard)this.Resources["CardHoverEnterAnimation"];
                hoverEnter?.Begin(cardBorder);
            };

            cardBorder.MouseLeave += (s, e) =>
            {
                var hoverLeave = (Storyboard)this.Resources["CardHoverLeaveAnimation"];
                hoverLeave?.Begin(cardBorder);
            };

            cardBorder.MouseLeftButtonDown += (s, e) =>
            {
                bool isExpanded = cardBorder.Height > 100;

                if (isExpanded)
                {
                    var collapseAnim = (Storyboard)this.Resources["CollapseCardAnimation"];
                    collapseAnim?.Begin(cardBorder);

                    statusBlock.Visibility = Visibility.Collapsed;
                    editButton.Visibility = Visibility.Collapsed;
                }
                else
                {
                    var expandAnim = (Storyboard)this.Resources["ExpandCardAnimation"];
                    expandAnim?.Begin(cardBorder);

                    statusBlock.Visibility = Visibility.Visible;
                    editButton.Visibility = Visibility.Visible;
                }
            };

            return cardBorder;
        }

        private void EditClient(Client client)
        {
            MessageBox.Show($"Редактирование клиента: {client.Name}\nФункция в разработке", "Информация");
        }

        private void TabAll_Click(object sender, RoutedEventArgs e)
        {
            if (!isShowingAll)
            {
                isShowingAll = true;
                UpdateTabButtons(true);
                RefreshCards();
            }
        }

        private void TabActive_Click(object sender, RoutedEventArgs e)
        {
            if (isShowingAll)
            {
                isShowingAll = false;
                UpdateTabButtons(false);
                RefreshCards();
            }
        }

        private void UpdateTabButtons(bool showAll)
        {
            if (showAll)
            {
                TabAll.Background = new SolidColorBrush(Color.FromRgb(52, 152, 219));
                TabActive.Background = new SolidColorBrush(Color.FromRgb(149, 165, 166));
            }
            else
            {
                TabAll.Background = new SolidColorBrush(Color.FromRgb(149, 165, 166));
                TabActive.Background = new SolidColorBrush(Color.FromRgb(52, 152, 219));
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string name = Microsoft.VisualBasic.Interaction.InputBox("Введите имя клиента:", "Новый клиент", "");
            if (!string.IsNullOrWhiteSpace(name))
            {
                string phone = Microsoft.VisualBasic.Interaction.InputBox("Введите телефон:", "Новый клиент", "");
                string email = Microsoft.VisualBasic.Interaction.InputBox("Введите email:", "Новый клиент", "");

                var newClient = new Client
                {
                    Name = name,
                    Phone = phone,
                    Email = email,
                    IsActive = true
                };

                allClients.Add(newClient);
                RefreshCards();
            }
        }
    }

     public class Client
    {
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public bool IsActive { get; set; }

        public Client()
        {
            Name = "";
            Phone = "";
            Email = "";
            IsActive = false;
        }
    }
}