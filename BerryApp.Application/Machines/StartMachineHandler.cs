using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerryApp.Biz.Machines
{
    public class StartMachineHandler
    {
        private readonly IMachineRepository _repo;

        public StartMachineHandler(IMachineRepository repo)
        {
            _repo = repo;
        }

        public void Handle(StartMachineCommand command)
        {
            var machine = _repo.Get(command.MachineId);
            machine.Start();
            _repo.Update(machine);
        }
    }
}
