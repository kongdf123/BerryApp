using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerryApp.Infra.Devices
{
    public class PlcService
    {
        private readonly Random _random = new();

        public double ReadTemperature()
        {
            // Simulate reading from PLC
            return Math.Round(20 + _random.NextDouble() * 80, 2); // 20 + _random.NextDouble() * 10; // 20-30°C
        }

        public string ReadStatus()
        {
            // Simulate reading machine status from PLC
            //var statuses = new[] { "Running", "Stopped", "Error" };
            //return statuses[_random.Next(statuses.Length)];

            return _random.Next(3) switch
            {
                0 => "Running",
                1 => "Stopped",
                _ => "Error"
            };
        }
    }
}
