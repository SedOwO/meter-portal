using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Request
{
    public class RechargeRequest
    {
        [Required]
        public int MeterId { get; set; }

        [Required]
        [Range(0.01, 99999999.99, ErrorMessage = "Amount must be between 0.01 and 99,999,999.99.")]
        public decimal Amount { get; set; }

        public DateTime RechargeDate { get; set; } = DateTime.UtcNow;
    }
}
