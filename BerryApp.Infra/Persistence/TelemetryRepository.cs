using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;

namespace BerryApp.Infra.Persistence
{
    public class TelemetryRepository
    {
        private readonly string _connectionString;

        public TelemetryRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task InsertAsync(string machine, string metric, double value)
        {
            using var conn = new SqlConnection(_connectionString);

            var sql = @"
            INSERT INTO machine_telemetry (machine_name, metric, value, timestamp)
            VALUES (@Machine, @Metric, @Value, @Time)";

            await conn.ExecuteAsync(sql, new
            {
                Machine = machine,
                Metric = metric,
                Value = value,
                Time = DateTime.Now
            });
        }

        public async Task<IEnumerable<(DateTime Time, double Value)>>
            GetRecentAsync(string machine, string metric, int limit = 100)
        {
            using var conn = new SqlConnection(_connectionString);

            var sql = @"
            SELECT TOP (@Limit) timestamp, value
            FROM machine_telemetry
            WHERE machine_name = @Machine AND metric = @Metric
            ORDER BY timestamp DESC";

            var result = await conn.QueryAsync(sql, new
            {
                Machine = machine,
                Metric = metric,
                Limit = limit
            });

            return result.Select(r => ((DateTime)r.timestamp, (double)r.value));
        }
    }
}
