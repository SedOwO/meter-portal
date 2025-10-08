using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebUI.Models;
using WebUI.Services;

namespace WebUI.Pages.Consumer
{
    public class RechargeModel : PageModel
    {
        private readonly IApiService _apiService;

        [BindProperty]
        public decimal Amount { get; set; }

        public SmartMeter? Meter { get; set; }
        public string? SuccessMessage { get; set; }
        public string? ErrorMessage { get; set; }

        public RechargeModel(IApiService apiService)
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

            Meter = await _apiService.GetSmartMeterAsync(token);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToPage("/Auth/Login");
            }

            if (Amount <= 0)
            {
                ErrorMessage = "Amount must be greater than zero";
                Meter = await _apiService.GetSmartMeterAsync(token);
                return Page();
            }

            Meter = await _apiService.GetSmartMeterAsync(token);

            if (Meter == null)
            {
                ErrorMessage = "No smart meter found. Please contact support.";
                return Page();
            }

            var response = await _apiService.RechargeAsync(token, Meter.MeterId, Amount);

            if (response != null)
            {
                SuccessMessage = $"Recharge successful! New balance: ${response.NewBalance:F2}";
                Meter = await _apiService.GetSmartMeterAsync(token);
            }
            else
            {
                ErrorMessage = "Recharge failed. Please try again.";
                Meter = await _apiService.GetSmartMeterAsync(token);
            }

            return Page();
        }
    }
}
