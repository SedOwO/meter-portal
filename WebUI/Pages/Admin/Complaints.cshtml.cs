using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebUI.Models;
using WebUI.Models.Pagination;
using WebUI.Services;

namespace WebUI.Pages.Admin
{
    public class ComplaintsModel : PageModel
    {
        private readonly IApiService _apiService;

        public PagedResult<Complaint>? PagedComplaints { get; set; } = new();
        public string? SuccessMessage { get; set; }
        public string? ErrorMessage { get; set; }

        [BindProperty]
        public int ComplaintId { get; set; }

        [BindProperty]
        public string Status { get; set; } = string.Empty;

        // Pagination properties
        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;

        [BindProperty(SupportsGet = true)]
        public int PageSize { get; set; } = 10;

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

            PagedComplaints = await _apiService.GetAllComplaintsAdminAsync(token, CurrentPage, PageSize) ?? new();

            Console.WriteLine($"Items Count: {PagedComplaints.Items?.Count ?? 0}");
            Console.WriteLine($"Total Count: {PagedComplaints.Pagination?.TotalCount ?? 0}");
            Console.WriteLine($"Page: {PagedComplaints.Pagination?.Page ?? 0}");

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

            PagedComplaints = await _apiService.GetAllComplaintsAdminAsync(token);
            return Page();
        }
    }
}
