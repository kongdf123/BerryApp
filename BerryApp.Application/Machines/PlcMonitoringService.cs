using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerryApp.Infra.Devices
{
    public class PlcMonitoringService
    {
        private readonly ModbusTcpClient _client;
        private bool _running;

        public event Action<bool> MachineStatusChanged;

        public PlcMonitoringService(ModbusTcpClient client)
        {
            _client = client;
        }

        public async Task StartAsync()
        {
            _running = true;
            await _client.ConnectAsync();

            _ = Task.Run(async () =>
            {
                while (_running)
                {
                    try
                    {
                        bool isRunning = await ReadMachineStatus();

                        MachineStatusChanged?.Invoke(isRunning);
                    }
                    catch
                    {
                        // TODO: logging + retry
                    }

                    await Task.Delay(500); // polling interval (500ms)
                }
            });
        }

        private async Task<bool> ReadMachineStatus()
        {
            var stream = _client.GetStream();

            // Modbus Read Coil (Function Code 01)
            byte[] request = new byte[]
            {
            0x00, 0x01,
            0x00, 0x00,
            0x00, 0x06,
            0x01,
            0x01,       // Read Coils
            0x00, 0x00,
            0x00, 0x01
            };

            await stream.WriteAsync(request, 0, request.Length);

            byte[] response = new byte[12];
            await stream.ReadAsync(response, 0, response.Length);

            return response[9] == 0x01;
        }

        public void Stop()
        {
            _running = false;
        }
    }
}
