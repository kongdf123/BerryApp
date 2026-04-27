using BerryApp.Shared.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerryApp.Domain.Entities
{
    public class Machine : ObservableObject
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        private string _status;
        public string Status { get { return _status; } set { SetProperty(ref _status, value); } }

        private double _temperature;
        public double Temperature { get { return _temperature; } set { SetProperty(ref _temperature, value); } }

        public bool IsRunning { get; private set; }
        public bool SafetyDoorClosed { get; private set; }

        public Machine() { }

        public Machine(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public void CloseSafetyDoor()
        {
            SafetyDoorClosed = true;
        }

        public void Start()
        {
            if (!SafetyDoorClosed)
                throw new InvalidOperationException("Cannot start: safety door is open");

            IsRunning = true;
        }

        public void Stop()
        {
            IsRunning = false;
        }
    }
}
