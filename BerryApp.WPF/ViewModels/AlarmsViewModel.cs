using BerryApp.Biz.Machines;
using BerryApp.Domain.Entities;
using BerryApp.Infra.Persistence;
using BerryApp.Shared.Base;
using BerryApp.Shared.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BerryApp.WPF.ViewModels
{
    public class AlarmsViewModel : ObservableObject
    {
        private readonly AlarmService _alarmService;
        public ObservableCollection<Alarm> Alarms { get; } = new ObservableCollection<Alarm>();
        public AlarmsViewModel(EventBus eventBus, AlarmService alarmService)
        {
            _alarmService = alarmService;

            // Load history
            _ = LoadHistoryAsync();

            eventBus.Substribe<AlarmEvent>(async e =>
            {
                var alarm = new Alarm
                {
                    Message = e.Message,
                    Time = e.Time
                };

                // UI Update
                Application.Current.Dispatcher.Invoke(() => Alarms.Insert(0, alarm));

                // DB save
                await _alarmService.InsertAsync(alarm); 
            });
        }

        private async Task LoadHistoryAsync()
        {
            var list = await _alarmService.GetRecentAsync();

            Application.Current.Dispatcher.Invoke(() =>
            {
                foreach (var a in list)
                    Alarms.Add(a);
            });
        }
    }
}
