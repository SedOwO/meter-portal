using System.Net.Http.Headers;
using WebUI.Models;
using WebUI.Models.Pagination;
using WebUI.Models.Request;
using WebUI.Models.Response;

namespace WebUI.Services
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:7137/api";

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<bool> CreateComplaintAsync(string token, CreateComplaintRequest request)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/Consumer/complaints", request);
            return response.IsSuccessStatusCode;
        }

        public async Task<List<Complaint>?> GetComplaintsAsync(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync($"{_baseUrl}/Consumer/complaints");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<Complaint>>();
            }
            return null;
        }

        public async Task<ConsumerProfile?> GetProfileAsync(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync($"{_baseUrl}/Common/profile");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ConsumerProfile>();
            }
            return null;
        }

        public async Task<List<RechargeHistory>?> GetRechargeHistoryAsync(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync($"{_baseUrl}/Consumer/recharge/history");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<RechargeHistory>>();
            }
            return null;
        }

        public async Task<SmartMeter?> GetSmartMeterAsync(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync($"{_baseUrl}/Consumer/smartmeter");
            if (response.IsSuccessStatusCode)
            {
                var meters = await response.Content.ReadFromJsonAsync<List<SmartMeter>>();
                return meters?.FirstOrDefault();
            }
            return null;
        }

        public async Task<LoginResponse?> LoginAsync(LoginRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/Auth/login", request);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<LoginResponse>();
            }
            return null;
        }

        public async Task<RechargeResponse?> RechargeAsync(string token, int meterId, decimal amount)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var request = new RechargeRequest {
                MeterId = meterId,
                Amount = amount 
            };
            var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/Consumer/recharge", request);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<RechargeResponse>();
            }
            return null;
        }

        public async Task<PagedResult<Complaint>?> GetAllComplaintsAdminAsync(string token, int page = 1, int pageSize = 10)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync($"{_baseUrl}/Admin/complaints?Page={page}&PageSize={pageSize}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<PagedResult<Complaint>>();
            }
            return null;
        }
        public async Task<List<SmartMeter>?> GetAllMetersAdminAsync(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync($"{_baseUrl}/Admin/smartmeter");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<SmartMeter>>();
            }
            return null;
        }

        public async Task<List<Notification>?> GetUnreadNotificationsAsync(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync($"{_baseUrl}/Consumer/notifications");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<Notification>>();
            }
            return null;
        }

        public async Task<bool> MarkNotificationAsReadAsync(string token, int notificationId)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.PutAsync($"{_baseUrl}/Consumer/notifications/{notificationId}", null);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateComplaintStatusAsync(string token, int complaintId, string status)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var request = new ComplaintUpdateRequest
            {
                ComplaintId = complaintId,
                Status = status
            };

            var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/Admin/updatecomplaint", request);

            return response.IsSuccessStatusCode;
        }
    }
}
