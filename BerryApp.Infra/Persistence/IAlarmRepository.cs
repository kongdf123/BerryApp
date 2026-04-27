using BerryApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerryApp.Infra.Persistence
{
    public interface IAlarmRepository
    {
        Task InsertAsync(Alarm alarm);
        Task<List<Alarm>> GetRecentAsync();
    }
}
