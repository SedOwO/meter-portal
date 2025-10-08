using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebUI.Models;
using WebUI.Services;

namespace WebUI.Pages.Admin
{
    public class ComplaintsModel : PageModel
    {
        private readonly IApiService _apiService;

        public List<Complaint>? Complaints { get; set; }
        public string? SuccessMessage { get; set; }
        public string? ErrorMessage { get; set; }

        [BindProperty]
        public int ComplaintId { get; set; }

        [BindProperty]
        public string Status { get; set; } = string.Empty;

        public ComplaintsModel(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var token = HttpContext.Session.GetString("Token");
            var role = HttpContext.Session.GetString("Role");

            if (string.IsNullOrEmpty(token) || role != "admin")
            {
                return RedirectToPage("/Auth/Login");
            }

            Complaints = await _apiService.GetAllComplaintsAdminAsync(token);

            return Page();
        }

        public async Task<IActionResult> OnPostUpdateStatusAsync()
        {
            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToPage("/Auth/Login");
            }

            try
            {
                //var success = await _apiService.UpdateComplaintStatusAsync(token, ComplaintId, Status);

                //if (success)
                //{
                //    SuccessMessage = "Complaint status updated successfully!";
                //}
                //else
                //{
                //    ErrorMessage = "Failed to update complaint status.";
                //}
            }
            catch (Exception)
            {
                ErrorMessage = "An error occurred while updating complaint.";
            }

            Complaints = await _apiService.GetAllComplaintsAdminAsync(token);
            return Page();
        }
    }
}
