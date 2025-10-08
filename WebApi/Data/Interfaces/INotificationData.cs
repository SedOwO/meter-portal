using WebApi.Models.DB;

namespace WebApi.Data.Interfaces
{
    public interface INotificationData
    {
        Task<IEnumerable<Notification>> GetUnreadNotificationsAsync(int consumerId);
        Task MarkAsReadAsync(int notificationId);
    }
}
