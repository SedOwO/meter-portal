using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Request
{
    public class CreateComplaintRequest
    {
        [Required]
        [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters.")]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;
    }
}
