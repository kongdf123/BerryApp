using BerryApp.Biz.Machines;
using BerryApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerryApp.Infra.Persistence
{
    public class InMemoryMachineRepository : IMachineRepository
    {
        private readonly Dictionary<Guid, Machine> _store = new();

        public Machine Get(Guid id)
        {
            return _store[id];
        }

        public void Update(Machine machine)
        {
            _store[machine.Id] = machine;
        }

        // Seed for demo
        public void Seed(Machine machine)
        {
            _store[machine.Id] = machine;
        }
    }
}
