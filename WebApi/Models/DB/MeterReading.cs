using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.DB
{
    public class MeterReading
    {
        [Key]
        public int ReadingId { get; set; }

        [Required]
        public int MeterId { get; set; }

        [Required]
        [Range(0, 99999999.99, ErrorMessage = "Reading value must be between 0 and 99,999,999.99 units.")]
        public decimal ReadingValue { get; set; }
    }
}
