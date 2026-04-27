using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerryApp.Infra.Devices
{
    public interface IPlcClient
    {
        Task<Dictionary<string, double>> ReadTelemetryAsync();
    }
}
