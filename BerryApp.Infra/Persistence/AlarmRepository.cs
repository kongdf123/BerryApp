using BerryApp.Domain.Entities;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerryApp.Infra.Persistence
{
    public class AlarmRepository : IAlarmRepository
    {
        private readonly string _connectionString;

        public AlarmRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task InsertAsync(Alarm alarm)
        {
            using var conn = new SqlConnection(_connectionString);

            var sql = "INSERT INTO Alarms (Message, Time) VALUES (@Message, @Time)";
            await conn.ExecuteAsync(sql, alarm);
        }

        public async Task<List<Alarm>> GetRecentAsync()
        {
            using var conn = new SqlConnection(_connectionString);

            var sql = "SELECT TOP 100 * FROM Alarms ORDER BY Time DESC";
            return (await conn.QueryAsync<Alarm>(sql)).ToList();
        }
    }
}
