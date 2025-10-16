using System.ComponentModel.DataAnnotations;

namespace WebUI.Models.Request
{
    public class ComplaintUpdateRequest
    {
        public int ComplaintId { get; set; }
        public string Status { get; set; }
    }
}
