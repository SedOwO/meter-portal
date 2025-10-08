using WebApi.Models.DB;
using WebApi.Models.Response;

namespace WebApi.Services.Interfaces
{
    public interface INotificationService
    {
        Task<IEnumerable<Notification>> GetUnreadNotificationAsync(int userId);
        Task MarkNotificationAsRead(int userId, int notificationId);
    }
}
