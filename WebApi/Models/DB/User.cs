using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.DB
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Username cannot exceed 50 characters.")]
        public string Username { get; set; } = "";

        [Required]
        public string Password { get; set; } = "";

        [Required]
        [StringLength(20, ErrorMessage = "Role cannot exceed 20 characters.")]
        public string Role { get; set; } = "Consumer";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
