using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebUI.Models.Response;
using WebUI.Services;

namespace WebUI.Pages.Consumer
{
    public class HistoryModel : PageModel
    {
        private readonly IApiService _apiService;

        public List<RechargeHistory>? RechargeHistory { get; set; }

        public HistoryModel(IApiService apiService)
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

            RechargeHistory = await _apiService.GetRechargeHistoryAsync(token);
            return Page();
        }
    }
}
