using System.Runtime.CompilerServices;
using WebApi.Models.DB;
using WebApi.Models.Response;
using WebApi.Repositories.Implementations;
using WebApi.Repositories.Interfaces;
using WebApi.Services.Interfaces;

namespace WebApi.Services.Implementations
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IConsumerRepository _consumerRepository;

        public NotificationService(INotificationRepository notificationRepository, IConsumerRepository consumerRepository)
        {
            _notificationRepository = notificationRepository;
            _consumerRepository = consumerRepository;
        }

        public async Task<IEnumerable<Notification>> GetUnreadNotificationAsync(int userId)
        {
            var consumer = await _consumerRepository.GetConsumerByUserIdAsync(userId);
            if (consumer == null)
                throw new InvalidOperationException("Consumer profile not found");

            var notifications = await _notificationRepository.GetUnreadNotificationsAsync(consumer.ConsumerId);

            return notifications;
        }

        public async Task MarkNotificationAsRead(int userId, int notificationId)
        {
            var consumer = await _consumerRepository.GetConsumerByUserIdAsync(userId);
            if (consumer == null)
                throw new InvalidOperationException("Consumer profile not found");

            await _notificationRepository.MarkAsReadAsync(notificationId);
            
        }
    }
}
