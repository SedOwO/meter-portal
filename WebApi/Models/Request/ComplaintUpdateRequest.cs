using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Request
{
    public class ComplaintUpdateRequest
    {
        [Required]
        public int ComplaintId { get; set; }

        [StringLength(20, ErrorMessage = "Status cannot exceed 20 characters.")]
        public string Status { get; set; } = "open";    // open, in_progress, resolved, closed
    }
}
