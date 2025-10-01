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

        public Task<ConsumerDetail?> GetProfileByIdAsync(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
