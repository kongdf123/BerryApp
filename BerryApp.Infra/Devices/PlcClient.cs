using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerryApp.Infra.Devices
{
    public class PlcClient
    {
        public void SendStartSignal()
        {
            Console.WriteLine("PLC: Start signal sent");
        }

        public void SendStopSignal()
        {
            Console.WriteLine("PLC: Stop signal sent");
        }
    }
}
