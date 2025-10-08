using RechargeNotification;
using RechargeNotification.Services.Implementations;
using RechargeNotification.Services.Interfaces;

var builder = Host.CreateApplicationBuilder(args);

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

builder.Services.AddSingleton<IBalanceMonitorService, BalanceMonitorService>();
builder.Services.AddSingleton<INotificationService, NotificationService>();
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
