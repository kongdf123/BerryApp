using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerryApp.Biz.Machines
{
    public interface IPlcService
    {
        Task StartMachineAsync();
        Task StopMachineAsync();
    }
}
