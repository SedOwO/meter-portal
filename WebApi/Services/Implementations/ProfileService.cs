using WebApi.Models.DB;
using WebApi.Repositories.Interfaces;
using WebApi.Services.Interfaces;

namespace WebApi.Services.Implementations
{
    public class ProfileService : IProfileService
    {

        private readonly IConsumerRepository _consumerRepository;

        public ProfileService(IConsumerRepository consumerRepository)
        {
            _consumerRepository = consumerRepository;
        }

        public async Task<ConsumerDetail?> GetProfileByIdAsync(int userId)
        {
            try
            {
                if (userId <= 0) return null;

                var consumerProfile = await _consumerRepository.GetConsumerByUserIdAsync(userId);

                if (consumerProfile == null) return null;

                return consumerProfile;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
