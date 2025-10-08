using MailKit.Net.Smtp;
using MimeKit;
using Npgsql;
using RechargeNotification.Models;
using RechargeNotification.Models.Settings;
using RechargeNotification.Services.Interfaces;
using System;


namespace RechargeNotification.Services.Implementations
{
    public class EmailService : IEmailService
    {
        private readonly string _connectionString;
        private readonly EmailSettings _emailSettings;
        private readonly ILogger<BalanceMonitorService> _logger;

        private EmailDto target = null;

        public EmailService(EmailSettings emailSettings, IConfiguration config, ILogger<BalanceMonitorService> logger)
        {
            _connectionString = config.GetConnectionString("DefaultConnection")
                   ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            _logger = logger;
            _emailSettings = config.GetSection("EmailSettings").Get<EmailSettings>()
                   ?? throw new InvalidOperationException("Email settings not configured");
        }

        public async Task SendLowBalanceAlertAsync(int consumerId)
        {
            try
            {
                using NpgsqlConnection conn = new NpgsqlConnection(_connectionString);
                await conn.OpenAsync();

                using var cmd = new NpgsqlCommand(
                    "SELECT * FROM public.getemaildetails(@consumerid)", conn);

                cmd.Parameters.AddWithValue("consumerid", consumerId);

                using var reader = await cmd.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    target = new EmailDto
                    {
                        ConsumerName = reader.GetString(reader.GetOrdinal("consumername")),
                        Email = reader.GetString(reader.GetOrdinal("email")),
                        MeterNumber = reader.GetString(reader.GetOrdinal("meter_number")),
                        BalanceAmount = reader.GetDecimal(reader.GetOrdinal("balance_amount"))
                    };
                }

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.SenderEmail));
                message.To.Add(new MailboxAddress(target.ConsumerName, target.Email));

                message.Subject = "Low Balance Alert - Smart Meter System";

                var bodyBuilder = new BodyBuilder
                {
                    HtmlBody = CreateEmailTemplate(target.ConsumerName, target.MeterNumber, target.BalanceAmount)
                };

                message.Body = bodyBuilder.ToMessageBody();

                using var client = new SmtpClient();

                await client.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.SmtpPort, _emailSettings.EnableSsl);
                await client.AuthenticateAsync(_emailSettings.SenderEmail, _emailSettings.SenderPassword);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);

                _logger.LogInformation("Low balance email sent to {Email} ({Name})", target.Email, target.ConsumerName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send email to {Email}", target.Email);
                throw;
            }
        }

        private string CreateEmailTemplate(string consumerName, string meterNumber, decimal balance)
        {
            return $@"
                <!DOCTYPE html>
                <html>
                <head>
                    <style>
                        body {{ font-family: Arial, sans-serif; background-color: #f4f4f4; margin: 0; padding: 0; }}
                        .container {{ max-width: 600px; margin: 20px auto; background-color: #ffffff; border-radius: 8px; overflow: hidden; box-shadow: 0 2px 4px rgba(0,0,0,0.1); }}
                        .header {{ background-color: #dc3545; color: white; padding: 20px; text-align: center; }}
                        .header h1 {{ margin: 0; font-size: 24px; }}
                        .content {{ padding: 30px; }}
                        .alert-box {{ background-color: #fff3cd; border-left: 4px solid #ffc107; padding: 15px; margin: 20px 0; }}
                        .balance {{ font-size: 32px; font-weight: bold; color: #dc3545; text-align: center; margin: 20px 0; }}
                        .button {{ display: inline-block; padding: 12px 30px; background-color: #007bff; color: white; text-decoration: none; border-radius: 5px; margin: 20px 0; }}
                        .footer {{ background-color: #f8f9fa; padding: 20px; text-align: center; font-size: 12px; color: #6c757d; }}
                        .info-table {{ width: 100%; margin: 20px 0; }}
                        .info-table td {{ padding: 10px; border-bottom: 1px solid #dee2e6; }}
                        .info-table td:first-child {{ font-weight: bold; width: 40%; }}
                    </style>
                </head>
                <body>
                    <div class='container'>
                        <div class='header'>
                            <h1>⚠️ Low Balance Alert</h1>
                        </div>
                        <div class='content'>
                            <p>Dear {consumerName},</p>
            
                            <div class='alert-box'>
                                <strong>⚠️ Important Notice:</strong> Your smart meter balance is running low!
                            </div>

                            <table class='info-table'>
                                <tr>
                                    <td>Meter Number:</td>
                                    <td>{meterNumber}</td>
                                </tr>
                                <tr>
                                    <td>Current Balance:</td>
                                    <td><strong>${balance:F2}</strong></td>
                                </tr>
                                <tr>
                                    <td>Status:</td>
                                    <td><span style='color: #dc3545;'>⚠️ Low Balance</span></td>
                                </tr>
                            </table>

                            <div class='balance'>
                                ${balance:F2}
                            </div>

                            <p><strong>What you need to do:</strong></p>
                            <ul>
                                <li>Recharge your meter as soon as possible</li>
                                <li>Avoid service interruption</li>
                                <li>Maintain a healthy balance for uninterrupted service</li>
                            </ul>

                            <p style='margin-top: 30px; font-size: 14px; color: #6c757d;'>
                                This is an automated message from the Smart Meter Monitoring System. 
                                Please do not reply to this email.
                            </p>
                        </div>
                        <div class='footer'>
                            <p>Smart Meter System | Automated Balance Monitoring</p>
                            <p>&copy; 2025 Smart Meter Portal. All rights reserved.</p>
                        </div>
                    </div>
                </body>
                </html>";
        }
    }
}
