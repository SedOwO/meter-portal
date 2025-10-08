namespace WebUI.Models.Response
{
    public class RechargeResponse
    {
        public int RechargeId { get; set; }
        public int MeterId { get; set; }
        public decimal Amount { get; set; }
        public decimal NewBalance { get; set; }
        public DateTime RechargeDate { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
