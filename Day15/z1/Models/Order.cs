using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CRMBusiness.Models
{
    public class Order : INotifyPropertyChanged
    {
        private string _productName = string.Empty;
        private decimal _amount;
        private DateTime _orderDate;
        private int _clientId;

        public int Id { get; set; }
        private static int _nextId = 1;

        public int ClientId
        {
            get => _clientId;
            set
            {
                _clientId = value;
                OnPropertyChanged();
            }
        }

        public string ProductName
        {
            get => _productName;
            set
            {
                _productName = value ?? string.Empty;
                OnPropertyChanged();
            }
        }

        public decimal Amount
        {
            get => _amount;
            set
            {
                _amount = value;
                OnPropertyChanged();
            }
        }

        public DateTime OrderDate
        {
            get => _orderDate;
            set
            {
                _orderDate = value;
                OnPropertyChanged();
            }
        }

        public Order()
        {
            Id = _nextId++;
            OrderDate = DateTime.Now;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}