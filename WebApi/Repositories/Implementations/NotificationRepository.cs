using WebApi.Data.Interfaces;
using WebApi.Models.DB;
using WebApi.Repositories.Interfaces;

namespace WebApi.Repositories.Implementations
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly INotificationData _notificationData;

        public NotificationRepository(INotificationData notificationData)
        {
            _notificationData = notificationData;
        }

        public async Task<IEnumerable<Notification>> GetUnreadNotificationsAsync(int consumerId)
        {
            return await _notificationData.GetUnreadNotificationsAsync(consumerId);
        }

        public async Task MarkAsReadAsync(int notificationId)
        {
            await _notificationData.MarkAsReadAsync(notificationId);
        }
    }
}
