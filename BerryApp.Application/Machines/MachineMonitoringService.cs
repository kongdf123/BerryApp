using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerryApp.Infra.Devices
{
    public class MachineMonitoringService
    {
        private readonly PlcMonitoringService _plc;

        public event Action<bool> MachineStatusChanged;

        public MachineMonitoringService(PlcMonitoringService plc)
        {
            _plc = plc;

            _plc.MachineStatusChanged += status =>
            {
                MachineStatusChanged?.Invoke(status);
            };
        }

        public async Task StartAsync()
        {
            await _plc.StartAsync();
        }
    }
}
