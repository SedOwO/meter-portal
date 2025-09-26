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

        public async Task<int> SignUpConsumerAsync(UserRequest user, ConsumerSignUpRequest consumer)
        {
            bool isUsernameTaken = await IsUsernameTakenAsync(user.Username);

            // hash the password here
            string hashedPassword = _password.HashPassword(user.Password);

            var newUser = new UserRequest
            {
                Username = user.Username,
                Role = "consumer",
                Password = hashedPassword
            };

            var createdUserId = await _userRepository.CreateUserAsync(newUser);
            if (consumer != null)
            {
                var newConsumer = new ConsumerDetailRequest
                {
                    UserId = createdUserId,
                    FirstName = consumer.FirstName,
                    LastName = consumer.LastName,
                    Email = consumer.Email,
                    Phone = consumer.Phone,
                    Address = consumer.Address
                };
                await _consumerRepository.CreateConsumerAsync(newConsumer);
            }

            return createdUserId;
        }


        public async Task<bool> IsUsernameTakenAsync(string username)
        {
            var existingUser = await _userRepository.GetUserByUsernameAsync(username);
            var result = (existingUser != null);
            return result;
        }
    }
}
