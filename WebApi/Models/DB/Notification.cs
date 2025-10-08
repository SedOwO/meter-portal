namespace WebApi.Models.DB
{
    public class Notification
    {
        public int NotificationId { get; set; }
        public int ConsumerId { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsRead { get; set; }
    }
}
