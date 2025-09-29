using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Request
{
    public class AuthRequest
    {
        [Required]
        [StringLength(50, ErrorMessage = "Username cannot exceed 50 characters.")]
        public string Username { get; set; } = "";

        [Required]
        public string Password { get; set; } = "";
    }
}
