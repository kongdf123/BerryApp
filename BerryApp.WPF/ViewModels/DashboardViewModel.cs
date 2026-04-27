using BerryApp.Domain.Entities;
using BerryApp.Shared.Base;
using BerryApp.Shared.Events;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using Microsoft.Extensions.Logging;
using System.Reflection.PortableExecutable;
using Machine = BerryApp.Domain.Entities.Machine;

namespace BerryApp.WPF.ViewModels
{
    public class DashboardViewModel : ObservableObject
    {
        private readonly EventBus _eventBus;
        private readonly Random _random = new();
        private readonly ILogger<DashboardViewModel> _logger;

        public ObservableCollection<Machine> Machines { get; } = new();

        public DashboardViewModel(EventBus eventBus, ILogger<DashboardViewModel> logger)
        {
            _eventBus = eventBus;
            _logger = logger;

            _logger.LogInformation("Dashboard started");

            // Initial load of machines
            Machines.Add(new Machine { Id = Guid.NewGuid(), Name = "Machine A", Status = "Running" });
            Machines.Add(new Machine { Id = Guid.NewGuid(), Name = "Machine B", Status = "Stopped" });

            var seriesList = new List<ISeries>();

            foreach (var machine in Machines)
            {
                var values = new List<double>();
                _machineSeriesData[machine.Name] = values;

                seriesList.Add(new LineSeries<double>
                {
                    Name = machine.Name,
                    Values = values
                });
            }

            TemperatureSeries = seriesList.ToArray();

            XAxes = new Axis[]
            {
                new Axis
                {
                    Name = "Time"
                }
            };

            YAxes = new Axis[]
            {
                new Axis
                {
                    Name = "°C",
                    MinLimit = 0,
                    MaxLimit = 100
                }
            };

            StartMonitoring();

        }
        public ISeries[] TemperatureSeries { get; set; }
        public Axis[] XAxes { get; set; }
        public Axis[] YAxes { get; set; }

        //private readonly List<double> _tempValues = new();
        private readonly Dictionary<string, List<double>> _machineSeriesData = new Dictionary<string, List<double>>();
        private int _timeIndex = 0;

        private const double Threshold = 80;
        public async void StartMonitoring()
        {
            while (true)
            {
                await Task.Delay(3000); // Simulate periodic updates

                var seriesList = new List<ISeries>();

                // Simulate machine status updates
                foreach (var machine in Machines)
                {
                    machine.Temperature = Math.Round(20 + _random.NextDouble() * 80, 2); // Random temperature between 20 and 100
                    machine.Status = _random.Next(3) switch { 0 => "Running", 1 => "Stopped", _ => "Error" };

                    if (machine.Temperature > Threshold)
                    {
                        _logger.LogWarning("Temperature exceeded threshold: {Temp}", machine.Temperature);

                        // Publish an alert event if temperature exceeds threshold
                        _eventBus.Publish(new AlarmEvent { Message = $"{machine.Name} temperature is too high: {machine.Temperature:F1} °C" });
                    } 

                    // 🔥 update each machine series
                    var list = _machineSeriesData[machine.Name];
                    list.Add(machine.Temperature);

                    if (list.Count > 20)
                        list.RemoveAt(0);

                    seriesList.Add(new LineSeries<double>
                    {
                        Name = machine.Name,
                        Values = list,
                        Stroke = machine.Temperature > Threshold ? new SolidColorPaint(SKColors.Red, 3) : new SolidColorPaint(SKColors.Green, 2), // 🔥 Highlight logic // new SolidColorPaint(SKColors.Red),
                        GeometrySize = 1
                    });
                }

                TemperatureSeries = seriesList.ToArray();

                // 🔥 Add to chart
                //var avgTemp = Machines.Average(m => m.Temperature);

                //_tempValues.Add(avgTemp);
                //_timeIndex++;

                //// keep last 20 points
                //if (_tempValues.Count > 20)
                //    _tempValues.RemoveAt(0);

                //TemperatureSeries = new ISeries[]
                //{
                //    new LineSeries<double>
                //    {
                //        Values = _tempValues,
                //        Name = "Temperature"
                //    }
                //};

                // 🔥 IMPORTANT: refresh chart
                OnPropertyChanged(nameof(TemperatureSeries));
            }
        }
    }
}
