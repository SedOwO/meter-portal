using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebUI.Models;
using WebUI.Services;

namespace WebUI.Pages.Admin
{
    public class DashboardModel : PageModel
    {
        private readonly IApiService _apiService;

        public string Username { get; set; } = string.Empty;
        public DashboardStats Stats { get; set; } = new();


        public DashboardModel(IApiService apiService)
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

            Username = HttpContext.Session.GetString("Username") ?? "Admin";

            // Load dashboard statistics
            await LoadStatsAsync(token);

            return Page();
        }

        private async Task LoadStatsAsync(string token)
        {
            try
            {
                var complaints = await _apiService.GetAllComplaintsAdminAsync(token);
                var meters = await _apiService.GetAllMetersAdminAsync(token);

                Stats.TotalComplaints = complaints?.Count ?? 0;
                Stats.PendingComplaints = complaints?.Count(c => c.Status == "open") ?? 0;
                Stats.TotalMeters = meters?.Count ?? 0;
                Stats.ActiveMeters = meters?.Count(m => m.Status == "active") ?? 0;
                Stats.LowBalanceMeters = meters?.Count(m => m.BalanceAmount < 50) ?? 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
