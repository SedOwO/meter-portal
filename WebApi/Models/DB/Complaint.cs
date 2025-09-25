using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.DB
{
    public class Complaint
    {
        [Key]
        public int ComplaintId { get; set; }

        [Required]
        public int ConsumerId { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters.")]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [StringLength(20, ErrorMessage = "Status cannot exceed 20 characters.")]
        public string Status { get; set; } = "open";

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
