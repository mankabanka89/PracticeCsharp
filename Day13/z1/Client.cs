using System;
using System.ComponentModel;

namespace CRMBusiness
{
    public class Client : INotifyPropertyChanged
    {
        private string _fullName = string.Empty;
        private string _phone = string.Empty;
        private string _email = string.Empty;
        private string _interactionHistory = string.Empty;

        public int Id { get; set; }
        private static int _nextId = 1;

        public string FullName
        {
            get => _fullName;
            set
            {
                _fullName = value ?? string.Empty;
                OnPropertyChanged(nameof(FullName));
            }
        }

        public string Phone
        {
            get => _phone;
            set
            {
                _phone = value ?? string.Empty;
                OnPropertyChanged(nameof(Phone));
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value ?? string.Empty;
                OnPropertyChanged(nameof(Email));
            }
        }

        public string InteractionHistory
        {
            get => _interactionHistory;
            set
            {
                _interactionHistory = value ?? string.Empty;
                OnPropertyChanged(nameof(InteractionHistory));
            }
        }

        public Client()
        {
            Id = _nextId++;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}