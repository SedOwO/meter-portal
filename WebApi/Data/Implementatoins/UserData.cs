using Npgsql;
using System.Runtime.CompilerServices;
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
                    { "username", user.Username },
                    { "password_hash", user.Password },
                    { "role", user.Role }
                };

                var result = await _db.ExecuteScalarAsync<int>("query here;", parameters);
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    { "userid", userId }
                };
                var result = await _db.ExecuteScalarAsync<bool>("query here;", parameters);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            try
            {
                var users = new List<User>();

                using var reader = await _db.ExecuteReaderAsync("query here;");
                while (await reader.ReadAsync())
                {
                    users.Add(new User
                    {
                        UserId = reader.GetInt32(reader.GetOrdinal("userid")),
                        Username = reader.GetString(reader.GetOrdinal("username")),
                        Role = reader.GetString(reader.GetOrdinal("role"))
                    });
                }

                return users;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    { "userid", userId }
                };

                using var reader = await _db.ExecuteReaderAsync("query here", parameters);

                if (await reader.ReadAsync())
                {
                    return new User
                    {
                        UserId = reader.GetInt32(reader.GetOrdinal("userid")),
                        Username = reader.GetString(reader.GetOrdinal("username")),
                        Role = reader.GetString(reader.GetOrdinal("role"))
                    };
                }

                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> UpdateUserAsync(int userId, UserRequest user)
        {
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    { "userid", userId },
                    { "username", user.Username },
                    { "password_hash", user.Password },
                    { "role", user.Role }
                };

                var result = await _db.ExecuteScalarAsync<bool>("query here;", parameters);
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
