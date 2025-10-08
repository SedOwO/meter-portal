using Microsoft.Extensions.Configuration;
using Npgsql;
using RechargeNotification.Models;
using RechargeNotification.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace RechargeNotification.Services.Implementations
{
    public class BalanceMonitorService : IBalanceMonitorService
    {
        private readonly string _connectionString;
        private readonly decimal _threshold;
        private readonly ILogger<BalanceMonitorService> _logger;

        public BalanceMonitorService(IConfiguration config, ILogger<BalanceMonitorService> logger)
        {
            _connectionString = config.GetConnectionString("DefaultConnection")
                   ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            _threshold = config.GetValue("MonitorSettings:LowBalanceThreshold", 50.00m);
            _logger = logger;
        }

        public async Task<List<ConsumerBalance>> GetLowBalanceConsumersAsync()
        {
            var lowBalanceConsumers = new List<ConsumerBalance>();

            try
            {
                using NpgsqlConnection conn = new NpgsqlConnection(_connectionString);
                await conn.OpenAsync();

                using var cmd = new NpgsqlCommand(
                    "SELECT * FROM public.getlowbalancemeters(@threshold);", conn);

                cmd.Parameters.AddWithValue("threshold", _threshold);

                await using var reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    lowBalanceConsumers.Add(new ConsumerBalance
                    {
                        ConsumerId = reader.GetInt32(reader.GetOrdinal("consumerid")),
                        FirstName = reader.GetString(reader.GetOrdinal("firstname")),
                        LastName = reader.GetString(reader.GetOrdinal("lastname")),
                        Email = reader.GetString(reader.GetOrdinal("email")),
                        MeterId = reader.GetInt32(reader.GetOrdinal("meterid")),
                        MeterNumber = reader.GetString(reader.GetOrdinal("meter_number")),
                        BalanceAmount = reader.GetDecimal(reader.GetOrdinal("balance_amount")),
                        Status = reader.GetString(reader.GetOrdinal("status"))
                    });
                }
                _logger.LogInformation(
                    "Found {Count} consumers with balance below ${Threshold}",
                    lowBalanceConsumers.Count,
                    _threshold);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving low balance consumers");
                throw;
            }

            return lowBalanceConsumers;
        }
    }
}
