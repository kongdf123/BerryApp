using BerryApp.Infra.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerryApp.Biz.Machines
{
    public class TelemetryService
    {
        private readonly TelemetryRepository _repo;

        public TelemetryService(TelemetryRepository repo)
        {
            _repo = repo;
        }

        public async Task RecordTemperatureAsync(string machine, double value)
        {
            await _repo.InsertAsync(machine, "temperature", value);
        }
    }
}
