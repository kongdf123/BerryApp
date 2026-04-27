using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BerryApp.Infra.Devices
{
    public class ModbusTcpClient
    {
        private readonly string _ip;
        private readonly int _port;
        private TcpClient _client;

        public ModbusTcpClient(string ip, int port = 502)
        {
            _ip = ip;
            _port = port;
        }

        public async Task ConnectAsync()
        {
            _client = new TcpClient();
            await _client.ConnectAsync(_ip, _port);
        }

        public NetworkStream GetStream()
        {
            if (_client == null)
                throw new InvalidOperationException("Not connected to PLC");
            return _client.GetStream();
        }

        public async Task SendStartSignalAsync()
        {
            if (_client == null)
                throw new InvalidOperationException("PLC not connected");

            var stream = _client.GetStream();

            // Simplified Modbus Write Coil (Function Code 05)
            byte[] request = new byte[]
            {
                0x00, 0x01, // Transaction ID
                0x00, 0x00, // Protocol ID
                0x00, 0x06, // Length
                0x01,       // Unit ID
                0x05,       // Function Code (Write Single Coil)
                0x00, 0x00, // Address
                0xFF, 0x00  // ON
            };

            await stream.WriteAsync(request, 0, request.Length);
        }

        public async Task SendStopSignalAsync()
        {
            var stream = _client.GetStream();

            byte[] request = new byte[]
            {
                0x00, 0x02,
                0x00, 0x00,
                0x00, 0x06,
                0x01,
                0x05,
                0x00, 0x00,
                0x00, 0x00 // OFF
            };

            await stream.WriteAsync(request, 0, request.Length);
        }
    }
}
