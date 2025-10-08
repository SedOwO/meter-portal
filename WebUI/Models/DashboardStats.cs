namespace WebUI.Models
{
    public class DashboardStats
    {
        public int TotalComplaints { get; set; }
        public int PendingComplaints { get; set; }
        public int TotalConsumers { get; set; }
        public int TotalMeters { get; set; }
        public int ActiveMeters { get; set; }
        public int LowBalanceMeters { get; set; }
    }
}
