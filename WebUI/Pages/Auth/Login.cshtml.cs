using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebUI.Models.Request;
using WebUI.Services;

namespace WebUI.Pages.Auth
{
    public class LoginModel : PageModel
    {
        private readonly IApiService _apiService;

        [BindProperty]
        public Models.Request.LoginRequest LoginRequest { get; set; } = new();

        public string? ErrorMessage { get; set; }

        public LoginModel(IApiService apiService)
        {
            _apiService = apiService;
        }

        public IActionResult OnGet()
        {
            var token = HttpContext.Session.GetString("Token");
            if (!string.IsNullOrEmpty(token))
            {
                var role = HttpContext.Session.GetString("Role");
                if (role?.Equals("Admin", StringComparison.OrdinalIgnoreCase) == true)
                    return RedirectToPage("/Admin/Dashboard");
                else
                    return RedirectToPage("/Dashboard");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ErrorMessage = "Please enter username and password";
                return Page();
            }

            var response = await _apiService.LoginAsync(LoginRequest);
            if (response != null && !string.IsNullOrEmpty(response.Token))
            {
                HttpContext.Session.SetString("Token", response.Token);
                HttpContext.Session.SetString("Username", response.Username);
                HttpContext.Session.SetString("Role", response.Role);

                if (response.Role.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                    return RedirectToPage("/Admin/Dashboard");

                else
                    return RedirectToPage("/Dashboard");
            }
            ErrorMessage = "Invalid username or password";
            return Page();
        }
    }
}
