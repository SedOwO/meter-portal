namespace WebUI.Models.Request
{
    public class RechargeRequest
    {
        public int MeterId { get; set; }
        public decimal Amount { get; set; }
        public DateTime RechargeDate { get; set; }
    }
}
