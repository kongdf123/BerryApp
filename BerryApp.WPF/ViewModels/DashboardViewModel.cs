using BerryApp.Domain.Entities;
using BerryApp.Shared.Base;
using BerryApp.Shared.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerryApp.WPF.ViewModels
{
    public class DashboardViewModel : ObservableObject
    {
        private readonly EventBus _eventBus;
        private readonly Random _random = new();

        public ObservableCollection<Machine> Machines { get; } = new();

        public DashboardViewModel(EventBus eventBus)
        {
            _eventBus = eventBus;

            // Initial load of machines
            Machines.Add(new Machine { Id = Guid.NewGuid(), Name = "Machine A", Status = "Running" });
            Machines.Add(new Machine { Id = Guid.NewGuid(), Name = "Machine B", Status = "Stopped" });

            StartMonitoring();
        }

        public async void StartMonitoring()
        {
            while (true)
            {
                await Task.Delay(3000); // Simulate periodic updates

                // Simulate machine status updates
                foreach (var machine in Machines)
                {
                    machine.Temperature = Math.Round(20 + _random.NextDouble() * 80, 2); // Random temperature between 20 and 100
                    machine.Status = _random.Next(3) switch { 0 => "Running", 1 => "Stopped", _ => "Error" };

                    if (machine.Temperature > 80)
                    {
                        // Publish an alert event if temperature exceeds threshold
                        _eventBus.Publish(new AlarmEvent { Message = $"{machine.Name} temperature is too high: {machine.Temperature:F1} °C" });
                    }
                }
            }
        }
    }
}
