using BerryApp.Biz.Machines;
using BerryApp.Infra.Devices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BerryApp.WPF.ViewModels
{
    public class MachineViewModel : INotifyPropertyChanged
    {
        private readonly MachineMonitoringService _monitor;

        public MachineViewModel(MachineMonitoringService monitor)
        {
            _monitor = monitor;

            _monitor.MachineStatusChanged += OnMachineStatusChanged;

            Task.Run(async () => await _monitor.StartAsync());
        }

        private bool _isRunning;
        public bool IsRunning
        {
            get => _isRunning;
            set
            {
                _isRunning = value;
                OnPropertyChanged();
            }
        }

        private void OnMachineStatusChanged(bool status)
        {
            // Switch to UI thread
            Application.Current.Dispatcher.Invoke(() =>
            {
                IsRunning = status;
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
