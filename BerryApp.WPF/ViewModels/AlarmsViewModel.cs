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
    public class AlarmsViewModel : ObservableObject
    {
        public ObservableCollection<Alarm> Alarms { get; } = new ObservableCollection<Alarm>();
        public AlarmsViewModel(EventBus eventBus) {
            eventBus.Substribe<AlarmEvent>(e =>
            {
                Alarms.Add(new Alarm { Message = e.Message, Time = DateTime.Now });
            });
        }
    }
}
