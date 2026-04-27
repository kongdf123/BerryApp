using BerryApp.Domain.Entities;
using BerryApp.Infra.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerryApp.Biz.Machines
{
    public class AlarmService
    {
        private readonly IAlarmRepository _repo;

        public AlarmService(IAlarmRepository repo)
        {
            _repo = repo;
        }

        public async Task InsertAsync(Alarm alarm)
        {
            // future: dedup, severity, rules
            await _repo.InsertAsync(alarm);
        }

        public Task<List<Alarm>> GetRecentAsync()
        {
            return _repo.GetRecentAsync();
        }
    }
}
