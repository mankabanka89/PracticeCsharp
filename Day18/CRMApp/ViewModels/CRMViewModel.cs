using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Threading.Tasks;
using CRMApp.Models;
using CRMApp.Data;

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
            set { _selectedClient = value; OnPropertyChanged(); }
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
            var newClient = new Client { Name = "Новый клиент", Phone = "", Email = "", IsActive = true };
            await _repo.AddAsync(newClient);
            await _repo.SaveAsync();
            await LoadAsync();
        }

        private async Task UpdateAsync()
        {
            if (SelectedClient != null)
            {
                await _repo.UpdateAsync(SelectedClient);
                await _repo.SaveAsync();
                await LoadAsync();
            }
        }

        private async Task DeleteAsync()
        {
            if (SelectedClient != null)
            {
                await _repo.DeleteAsync(SelectedClient);
                await _repo.SaveAsync();
                await LoadAsync();
            }
        }

        private async Task SaveAsync()
        {
            await _repo.SaveAsync();
        }
    }
}