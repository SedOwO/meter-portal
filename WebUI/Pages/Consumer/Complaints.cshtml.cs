using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebUI.Models;
using WebUI.Models.Request;
using WebUI.Services;

namespace WebUI.Pages.Consumer
{
    public class ComplaintsModel : PageModel
    {
        private readonly IApiService _apiService;

        [BindProperty]
        public CreateComplaintRequest NewComplaint { get; set; } = new();

        public List<Complaint>? Complaints { get; set; }
        public string? SuccessMessage { get; set; }
        public string? ErrorMessage { get; set; }
        public bool ShowForm { get; set; } = false;

        public ComplaintsModel(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToPage("/Login");
            }

            Complaints = await _apiService.GetComplaintsAsync(token);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToPage("/Login");
            }

            if (string.IsNullOrWhiteSpace(NewComplaint.Title) || string.IsNullOrWhiteSpace(NewComplaint.Description))
            {
                ErrorMessage = "Title and description are required";
                Complaints = await _apiService.GetComplaintsAsync(token);
                ShowForm = true;
                return Page();
            }

            var success = await _apiService.CreateComplaintAsync(token, NewComplaint);

            if (success)
            {
                SuccessMessage = "Complaint submitted successfully!";
                NewComplaint = new(); // Clear form
            }
            else
            {
                ErrorMessage = "Failed to submit complaint. Please try again.";
                ShowForm = true;
            }

            Complaints = await _apiService.GetComplaintsAsync(token);
            return Page();
        }
    }
}
