using System.Security.Principal;
using WebApi.Models.DB;
using WebApi.Models.Request;

namespace WebApi.Data.Interfaces
{
    public interface IUserData
    {
        Task<int> CreateUserAsync(UserRequest user);
        Task<User?> GetUserByIdAsync(int userId);
        Task<User?> GetUserByUsernameAsync(string username);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<bool> UpdateUserAsync(int userId, UserRequest user);
        Task<bool> DeleteUserAsync(int userId);
    }
}
