using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebUI.Models;
using WebUI.Models.Response;
using WebUI.Services;

namespace WebUI.Pages
{
    public class ProfileModel : PageModel
    {
        private readonly IApiService _apiService;

        public ConsumerProfile? Profile { get; set; }
        public SmartMeter? Meter { get; set; }

        public ProfileModel(IApiService apiService)
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

            Profile = await _apiService.GetProfileAsync(token);
            Meter = await _apiService.GetSmartMeterAsync(token);

            return Page();
        }
    }
}
