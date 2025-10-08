using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebUI.Models.Response;
using WebUI.Services;

namespace WebUI.Pages.Admin
{
    public class ProfileModel : PageModel
    {
        private readonly IApiService _apiService;

        public ConsumerProfile? Profile { get; set; }

        public ProfileModel(IApiService apiService)
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

            Profile = await _apiService.GetProfileAsync(token);

            return Page();
        }
    }
}
