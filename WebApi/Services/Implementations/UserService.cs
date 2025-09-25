using Microsoft.Extensions.Configuration.UserSecrets;
using WebApi.Models.DB;
using WebApi.Models.Request;
using WebApi.Repositories.Interfaces;
using WebApi.Services.Interfaces;
using WebApi.Utilities.Interfaces;

namespace WebApi.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConsumerRepository _consumerRepository;
        private readonly IPasswordUtil _password;

        public UserService(IPasswordUtil password, IUserRepository userRepository, IConsumerRepository consumerRepository)
        {
            _password = password;
            _userRepository = userRepository;
            _consumerRepository = consumerRepository;
        }

        public async Task<int> SignUpConsumerAsync(UserRequest user)
        {
            var existingUser = await _userRepository.GetUserByUsernameAsync(user.Username);
            if (existingUser != null)
                throw new Exception("Username already exists");

            // hash the password here
            string hashedPassword = _password.HashPassword(user.Password);
            var newUser = new UserRequest
            {
                Username = user.Username,
                Role = "consumer",
                Password = hashedPassword
            };




        }
    }
}
