using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Runtime.InteropServices;
using WebUI.Models;
using WebUI.Models.Response;
using WebUI.Services;

namespace WebUI.Pages
{
    public class DashboardModel : PageModel
    {
        private readonly IApiService _apiService;

        public ConsumerProfile? Profile { get; set; }
        public SmartMeter? Meter { get; set; }
        public List<Notification> Notifications { get; set; }
        public string Username { get; set; } = string.Empty;

        public DashboardModel(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToPage("/Auth/Login");
            }

            Username = HttpContext.Session.GetString("Username") ?? "User";

            Profile = await _apiService.GetProfileAsync(token);
            Meter = await _apiService.GetSmartMeterAsync(token);
            Notifications = await _apiService.GetUnreadNotificationsAsync(token);

            return Page();
        }
        
        public async Task<IActionResult> OnPostMarkAsReadAsync(int notificationId)
        {
            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToPage("/Auth/Login");
            }

            await _apiService.MarkNotificationAsReadAsync(token, notificationId);

            return RedirectToPage();
        }
    }
}
