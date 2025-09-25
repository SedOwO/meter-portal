using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.DB
{
    public class Recharge
    {
        [Key]
        public int RechargeId { get; set; }

        [Required]
        public int ConsumerId { get; set; }

        [Required]
        [Range(0.01, 99999999.99, ErrorMessage = "Amount must be between 0.01 and 99,999,999.99.")]
        public decimal Amount { get; set; }

        public DateTime RechargeDate { get; set; } = DateTime.UtcNow;
    }
}
