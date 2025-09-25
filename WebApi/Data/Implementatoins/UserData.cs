using Npgsql;
using WebApi.Data.Interfaces;
using WebApi.Models.DB;
using WebApi.Models.Request;
using WebApi.Utilities.Interfaces;

namespace WebApi.Data.Implementatoins
{
    public class UserData : IUserData
    {
        private readonly IDbConnectionUtil _db;

        public UserData(IDbConnectionUtil db)
        {
            _db = db;
        }

        public async Task<int> CreateUserAsync(UserRequest user)
        {
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    { }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<bool> DeleteUserAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateUserAsync(int userId, UserRequest user)
        {
            throw new NotImplementedException();
        }
    }
}
