using WebApi.Models.Request;
using WebApi.Models.Response;

namespace WebApi.Services.Interfaces
{
    public interface IUserService
    {
        Task<int> SignUpConsumerAsync(AuthRequest user, ConsumerSignUpRequest consumer);
<<<<<<< HEAD
        Task<int> SignUpAdminAsync(AuthRequest user, AdminSignUpRequest admin);
=======
>>>>>>> e6f4dfecf1743c74a449ce6b4e21c12bffde6171
        Task<LoginResponse?> LoginAsync(AuthRequest loginRequest);
        Task<bool> IsUsernameTakenAsync(string username);

    }
}
