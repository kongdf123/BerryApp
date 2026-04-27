using BerryApp.Domain.Entities;
using BerryApp.Infra.Devices;
using System;
using System.Collections.Generic;
using System.Linq; 
using System.Text;
using System.Threading.Tasks;

namespace BerryApp.Biz.Machines
{
    public class MachineService : IMachineService
    {
        private readonly PlcService plcService;

        public MachineService(PlcService plcService)
        {
            this.plcService = plcService;
        }

        public Machine GetMachineData()
        {
            // In a real implementation, this would read from the PLC
            return new Machine
            {
                Id = Guid.NewGuid(),
                Name = "Machine 1",
                Status = plcService.ReadStatus(),
                Temperature = plcService.ReadTemperature()
            };
        }
    }
}
