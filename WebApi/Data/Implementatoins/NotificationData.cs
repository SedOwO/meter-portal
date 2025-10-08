using System.Diagnostics.Metrics;
using WebApi.Data.Interfaces;
using WebApi.Models.DB;
using WebApi.Models.Response;
using WebApi.Utilities.Interfaces;

namespace WebApi.Data.Implementatoins
{
    public class NotificationData : INotificationData
    {
        private readonly IDbConnectionUtil _db;

        public NotificationData(IDbConnectionUtil db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Notification>> GetUnreadNotificationsAsync(int consumerId)
        {
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    { "consumerId", consumerId}
                };

                var notifications = new List<Notification>();

                using var reader = await _db.ExecuteReaderAsync("SELECT * FROM public.getunreadnotifications(@consumerid);", parameters);

                while (await reader.ReadAsync())
                {
                    notifications.Add(new Notification
                    {
                        NotificationId = reader.GetInt32(reader.GetOrdinal("notificationid")),
                        ConsumerId = reader.GetInt32(reader.GetOrdinal("consumerid")),
                        Message = reader.GetString(reader.GetOrdinal("message")),
                        CreatedAt = reader.GetDateTime(reader.GetOrdinal("created_at")),
                        IsRead = reader.GetBoolean(reader.GetOrdinal("is_read"))
                    });
                }

                return notifications;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task MarkAsReadAsync(int notificationId)
        {
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    { "notificaitonid", notificationId}
                };

                await _db.ExecuteScalarAsync<bool>("SELECT * FROM public.marknotificationasread(@notificaitonid);", parameters);              
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
