using Microsoft.AspNetCore.Identity.Data;
using WebUI.Models;
using WebUI.Models.Request;
using WebUI.Models.Response;

namespace WebUI.Services
{
    public interface IApiService
    {
        Task<LoginResponse?> LoginAsync(Models.Request.LoginRequest request);
        Task<ConsumerProfile?> GetProfileAsync(string token);
        Task<SmartMeter?> GetSmartMeterAsync(string token);
        Task<RechargeResponse?> RechargeAsync(string token, int meterId, decimal amount);
        Task<List<RechargeHistory>?> GetRechargeHistoryAsync(string token);
        Task<List<Complaint>?> GetComplaintsAsync(string token);
        Task<bool> CreateComplaintAsync(string token, CreateComplaintRequest request);

        // Admin
        Task<List<Complaint>?> GetAllComplaintsAdminAsync(string token);
        Task<List<SmartMeter>?> GetAllMetersAdminAsync(string token);

        // Notifications
        Task<List<Notification>?> GetUnreadNotificationsAsync(string token);
        Task<bool> MarkNotificationAsReadAsync(string token, int notificationId);

    }
}
