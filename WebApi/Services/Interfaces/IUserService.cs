using WebApi.Models.Request;
using WebApi.Models.Response;

namespace WebApi.Services.Interfaces
{
    public interface IUserService
    {
        Task<int> SignUpConsumerAsync(AuthRequest user, ConsumerSignUpRequest consumer);
        Task<int> SignUpAdminAsync(AuthRequest user, AdminSignUpRequest admin);
        Task<LoginResponse?> LoginAsync(AuthRequest loginRequest);
        Task<bool> IsUsernameTakenAsync(string username);
    }
}
