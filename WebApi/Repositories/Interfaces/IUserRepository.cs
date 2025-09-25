using WebApi.Models.DB;
using WebApi.Models.Request;

namespace WebApi.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<int> CreateUserAsync(UserRequest user);
        Task<User?> GetUserByIdAsync(int userId);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<bool> UpdateUserAsync(int userId, UserRequest user);
        Task<bool> DeleteUserAsync(int userId);

    }
}
