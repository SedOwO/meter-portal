using WebApi.Data.Interfaces;
using WebApi.Models.DB;
using WebApi.Models.Request;
using WebApi.Repositories.Interfaces;

namespace WebApi.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly IUserData _userData;
        public UserRepository(IUserData userData)
        {
            _userData = userData;
        }
        public async Task<int> CreateUserAsync(UserRequest user)
        {
            return await _userData.CreateUserAsync(user);
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            return await _userData.DeleteUserAsync(userId);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userData.GetAllUsersAsync();
        }

        public async Task<User?> GetUserByIdAsync(int userId)
        {
            return await _userData.GetUserByIdAsync(userId);
        }

        public async Task<bool> UpdateUserAsync(int userId, UserRequest user)
        {
            return await _userData.UpdateUserAsync(userId, user);
        }
    }
}
