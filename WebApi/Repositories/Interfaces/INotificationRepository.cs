using WebApi.Models.DB;

namespace WebApi.Repositories.Interfaces
{
    public interface INotificationRepository
    {
        Task<IEnumerable<Notification>> GetUnreadNotificationsAsync(int consumerId);
        Task MarkAsReadAsync(int notificationId);
    }
}
