using BerryApp.Domain.Entities;
using BerryApp.Shared.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BerryApp.WPF.ViewModels
{
    public class OrdersViewModel : ObservableObject
    {
        public ObservableCollection<Order> Orders { get; } = new ObservableCollection<Order>();

        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }

        public OrdersViewModel()
        {
            AddCommand = new RelayCommand(async () =>
            {
                if (int.TryParse(NewQuantity, out int qty))
                {
                    Orders.Add(new Order { ProductName = NewProductName, Quantity = qty, Status = "New" });

                    NewProductName = string.Empty;
                    NewQuantity = string.Empty;
                }

                await Task.CompletedTask;
            });

            DeleteCommand = new RelayCommand(async () =>
            {
                if (SelectedOrder != null)
                {
                    Orders.Remove(SelectedOrder);
                    SelectedOrder = null;
                }
                await Task.CompletedTask;
            });
        }

        private Order _selectedOrder;
        public Order SelectedOrder
        {
            get { return _selectedOrder; }
            set { SetProperty(ref _selectedOrder, value); }
        }

        private string _newProductName;
        public string NewProductName
        {
            get { return _newProductName; }
            set { SetProperty(ref _newProductName, value); }
        }

        private string _newQuantity;
        public string NewQuantity
        {
            get { return _newQuantity; }
            set { SetProperty(ref _newQuantity, value); }
        }
    }
}
