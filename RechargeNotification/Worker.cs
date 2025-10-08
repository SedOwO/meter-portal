using Newtonsoft.Json;
using RechargeNotification.Models;
using RechargeNotification.Services.Interfaces;
using System.Reflection.Metadata;

namespace RechargeNotification
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IBalanceMonitorService _monitorService;
        private readonly IConfiguration _configuration;
        private readonly int _intervalMinutes;
        private readonly INotificationService _notificationService;

        public Worker(INotificationService notificationService, ILogger<Worker> logger, IBalanceMonitorService monitorService, IConfiguration configuration)
        {
            _notificationService = notificationService;
            _logger = logger;
            _monitorService = monitorService;
            _configuration = configuration;
            _intervalMinutes = configuration.GetValue<int>("MonitorSettings:CheckIntervalMinutes", 5);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await CheckBalanceAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while checking balances");
                    Console.WriteLine($"ERROR: {ex.Message}");
                }
                await Task.Delay(TimeSpan.FromMinutes(_intervalMinutes), stoppingToken);
            }
        }

        private async Task CheckBalanceAsync()
        {
            var timestamp = DateTime.Now;

            var lowBalanceConsumers = await _monitorService.GetLowBalanceConsumersAsync();

            if (lowBalanceConsumers.Count == 0)
            {

                Console.WriteLine("All consumers have sufficient balance!");

            }
            else
            {
                Console.WriteLine($"WARNING: {lowBalanceConsumers.Count} consumer(s) with low balance detected!");

                var json = JsonConvert.SerializeObject(lowBalanceConsumers, Formatting.Indented);

                // Log critical consumers
                foreach (var consumer in lowBalanceConsumers)
                {
                    var message = $"Low balance alert: your remaining balance is {consumer.BalanceAmount}.";

                    await _notificationService.AddNotificationAsync(new Notification
                    {
                        ConsumerId = consumer.ConsumerId,
                        Message = message
                    });
                }
            }
            Console.WriteLine($"Next check in {_intervalMinutes} minutes...");
        }
    }
}
