using BerryApp.Biz.Machines;
using BerryApp.Domain.Entities;
using BerryApp.Shared.Base;
using BerryApp.Shared.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input; 

namespace BerryApp.WPF.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private string _name;
        private string _age;
        private string _greeeting;
        private readonly NavigationService _navSrv;

        public RelayCommand AddOrderCommand { get; }
        public RelayCommand DeleteOrderCommand { get; }
        public ICommand LoadCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand ShowDashboardCommand { get; }
        public ICommand ShowOrdersCommand { get; }
        public ICommand ShowAlarmsCommand { get; }

        public ObservableCollection<Order> Orders { get; } = new();
        public ObservableCollection<Machine> Machines { get; } = new();

        private readonly Random _random = new();

        private readonly IMachineService _machineService;

        public MainViewModel(IMachineService machineService, NavigationService navigationService,
            DashboardViewModel dashboard,
            OrdersViewModel orders,
            AlarmsViewModel alarms)
        {
            _machineService = machineService;
            //SubmitCommand = new RelayCommand(Submit,CanSubmit);
            //ClearCommand = new RelayCommand(Clear);

            //AddOrderCommand = new RelayCommand(AddOrder);
            //DeleteOrderCommand = new RelayCommand(DeleteOrder, CanDeleteOrder);

            //LoadCommand = new RelayCommand(async () => await LoadAsync(), CanLoad);
            //CancelCommand = new RelayCommand(async () => await CancelAsync());

            //StartMonitoring();

            _navSrv = navigationService;
            // Subscribe to navigation changes
            _navSrv.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(NavigationService.CurrentView))
                    OnPropertyChanged(nameof(CurrentView));
            };

            ShowDashboardCommand = new RelayCommand(async () => { 
                _navSrv.CurrentView = dashboard;
                
                await Task.CompletedTask; });
            ShowOrdersCommand = new RelayCommand(async () => {
                _navSrv.CurrentView = orders; 
                await Task.CompletedTask; 
            });
            ShowAlarmsCommand = new RelayCommand(async () => {
                _navSrv.CurrentView = alarms; 
                await Task.CompletedTask; 
            });

            _navSrv.CurrentView = dashboard; // default view
        }

        public object CurrentView
        {
            get => _navSrv.CurrentView;
            set => _navSrv.CurrentView = value;
        }

        //private string _statusMessage = "Idle";
        //public string StatusMessage
        //{
        //    get { return _statusMessage; }
        //    set
        //    {
        //        _statusMessage = value;
        //        OnPropertyChanged(nameof(StatusMessage));
        //    }
        //}

        //private double _temperature;
        //public double Temperature
        //{
        //    get { return _temperature; }
        //    set
        //    {
        //        _temperature = value;
        //        OnPropertyChanged(nameof(Temperature));
        //    }
        //}

        //private bool _isRunning;
        //private async void StartMonitoring()
        //{
        //    _isRunning = true;
        //    //while (_isRunning)
        //    while (true)
        //    {
        //        await Task.Delay(1000); // Update every 1 second

        //        // Simulate receiving telemetry data
        //        Temperature = 20 + _random.NextDouble() * 10; // Random temp between 20-30

        //        var r = _random.Next(3);
        //        StatusMessage = r switch
        //        {
        //            0 => "Running",
        //            1 => "Stopped",
        //            _ => "Error"
        //        }; //$"Temperature: {Temperature:F1} °C";

        //        var machine = _machineService.GetMachineData();

        //        StatusMessage = $"Machine: {machine.Name}, Status: {machine.Status}, Temp: {machine.Temperature:F1} °C";
        //        Temperature = machine.Temperature;
        //    }
        //}

        //private int _progress;
        //public int Progress
        //{
        //    get { return _progress; }
        //    set
        //    {
        //        _progress = value;
        //        OnPropertyChanged(nameof(Progress));
        //    }
        //}

        //private bool _isLoading;
        //public bool IsLoading
        //{
        //    get { return _isLoading; }
        //    set
        //    {
        //        _isLoading = value;
        //        OnPropertyChanged(nameof(IsLoading));
        //    }
        //}

        //private bool _isCanceled;
        //public bool IsCanceled
        //{
        //    get { return _isCanceled; }
        //    set
        //    {
        //        _isCanceled = value;
        //        OnPropertyChanged(nameof(IsCanceled));
        //    }
        //}

        //private Order _selectedOrder;
        //public Order SelectedOrder
        //{
        //    get { return _selectedOrder; }
        //    set
        //    {
        //        _selectedOrder = value;
        //        OnPropertyChanged(nameof(SelectedOrder));

        //        // Notify command system to re-query CanExecute
        //        DeleteOrderCommand.RaiseCanExecuteChanged();
        //    }
        //}

        private string _newOrderName;
        public string NewOrderName
        {
            get { return _newOrderName; }
            set { SetProperty(ref _newOrderName, value); }
        }

        private int? _newOrderQuantity;
        public int? NewOrderQuantity
        {
            get { return _newOrderQuantity; }
            set { SetProperty(ref _newOrderQuantity, value); }
        }

        //private bool CanLoad()
        //{
        //    return !IsLoading;
        //}

        //private async Task CancelAsync()
        //{
        //    // Implement cancellation logic if needed
        //    IsCanceled = true;
        //    RaiseCommand();
        //}

        //private async Task LoadAsync()
        //{
        //    if(IsCanceled)
        //    {
        //        IsLoading = false;
        //        IsCanceled = false; // reset for next load
        //        RaiseCommand();
        //        return;
        //    }

        //    IsLoading = true;
        //    Progress = 0;
        //    RaiseCommand();

        //    for (int i = 0; i <= 100; i++)
        //    {
        //        if (IsCanceled)
        //        {
        //            IsLoading = false;
        //            IsCanceled = false; // reset for next load
        //            Progress = 0;
        //            RaiseCommand();
        //            return;
        //        }

        //        await Task.Delay(50); // simulate work
        //        Progress = i;
        //    }

        //    IsLoading = false;
        //    RaiseCommand();
        //}

        private void RaiseCommand()
        {
            (LoadCommand as RelayCommand)?.RaiseCanExecuteChanged();
        }

        private void AddOrder()
        {
            if (NewOrderQuantity == null)
            {
                return;
            }

            Orders.Add(new Order { ProductName = NewOrderName, Quantity = NewOrderQuantity ?? 0 });
            NewOrderName = string.Empty;
            NewOrderQuantity = null;
        }

        //private void DeleteOrder()
        //{
        //    if (SelectedOrder != null)
        //    {
        //        Orders.Remove(SelectedOrder);
        //        SelectedOrder = null;
        //    }
        //}

        //private bool CanDeleteOrder()
        //{
        //    return SelectedOrder != null;
        //}

        public ICommand SubmitCommand { get; }
        public ICommand ClearCommand { get; }

        //public string Name { get { return _name; } set { _name = value; OnPropertyChanged(nameof(Name)); } }
        //public string Age { get => _age; set { _age = value; OnPropertyChanged(nameof(Age)); } }
        //public string Greeting { get { return _greeeting; } set { _greeeting = value; OnPropertyChanged(nameof(Greeting)); } }

        //private bool CanSubmit() => !string.IsNullOrEmpty(Name);

        //public void Submit()
        //{
        //    Greeting = $"Hello, {Name}, Age:{Age}!";
        //}

        //public void Clear()
        //{
        //    Name = string.Empty;
        //    Age = string.Empty;
        //}

        //public event PropertyChangedEventHandler PropertyChanged;
        //protected void OnPropertyChanged(string propertyName)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}
    }
}
