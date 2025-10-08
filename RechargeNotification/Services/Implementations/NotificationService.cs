using Npgsql;
using RechargeNotification.Models;
using RechargeNotification.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RechargeNotification.Services.Implementations
{
    public class NotificationService : INotificationService
    {
        private readonly string _connectionString;
        private readonly ILogger<BalanceMonitorService> _logger;

        public NotificationService(IConfiguration config, ILogger<BalanceMonitorService> logger)
        {
            _connectionString = config.GetConnectionString("DefaultConnection")
                   ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            _logger = logger;
        }

        public async Task AddNotificationAsync(Notification notification)
        {
            using NpgsqlConnection conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            using var cmd = new NpgsqlCommand(
                "INSERT INTO notifications (consumerid, message) VALUES (@consumer_id, @message);", conn);

            cmd.Parameters.AddWithValue("consumer_id", notification.ConsumerId);
            cmd.Parameters.AddWithValue("message", notification.Message);

            await cmd.ExecuteNonQueryAsync();
        }
    }
}
