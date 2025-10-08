namespace WebUI.Models.Response
{
    public class RechargeHistory
    {
        public int RechargeId { get; set; }
        public int MeterId { get; set; }
        public decimal Amount { get; set; }
        public DateTime RechargeDate { get; set; }
    }
}
