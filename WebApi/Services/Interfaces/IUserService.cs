using WebApi.Models.Request;

namespace WebApi.Services.Interfaces
{
    public interface IUserService
    {
        Task<int> SignUpConsumerAsync(UserRequest user); 
    }
}
