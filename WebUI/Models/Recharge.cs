using System.ComponentModel.DataAnnotations;

namespace WebUI.Models
{
    public class Recharge
    {
        [Key]
        public int RechargeId { get; set; }

        [Required]
        public int MeterId { get; set; }

        [Required]
        [Range(0.01, 99999999.99, ErrorMessage = "Amount must be between 0.01 and 99,999,999.99.")]
        public decimal Amount { get; set; }

        public DateTime RechargeDate { get; set; } = DateTime.UtcNow;
    }
}
