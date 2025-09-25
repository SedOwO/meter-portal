using WebApi.Data.Interfaces;
using WebApi.Models.DB;
using WebApi.Models.Request;
using WebApi.Repositories.Interfaces;

namespace WebApi.Repositories.Implementations
{
    public class ConsumerRepository : IConsumerRepository
    {
        private readonly IConsumerData _consumerData;

        public ConsumerRepository(IConsumerData consumerData)
        {
            _consumerData = consumerData;
        }


        public async Task<int> CreateConsumerAsync(ConsumerDetailRequest consumer)
        {
            return await _consumerData.CreateConsumerAsync(consumer);
        }

        public async Task<ConsumerDetail?> GetConsumerByUserIdAsync(int userId)
        {
            return await _consumerData.GetConsumerByUserIdAsync(userId);
        }
    }
}
