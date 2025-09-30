using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Request
{
    public class SmartMeterRequest
    {
        [Required]
        public int ConsumerId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Meter number cannot exceed 50 characters.")]
        public string MeterNumber { get; set; } = string.Empty;

        [StringLength(100, ErrorMessage = "Location cannot exceed 100 characters.")]
        public string? Location { get; set; }

        [StringLength(20, ErrorMessage = "Status cannot exceed 20 characters.")]
        public string Status { get; set; } = "active";

    }
}
