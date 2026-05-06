using System.Windows;
using CRMBusiness.ViewModels;

namespace CRMBusiness.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new CRMViewModel();
        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AboutMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("CRM для бизнеса\nMVVM + Асинхронность", "О программе", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}