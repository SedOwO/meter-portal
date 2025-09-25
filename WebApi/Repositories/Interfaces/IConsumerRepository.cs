using WebApi.Models.DB;
using WebApi.Models.Request;

namespace WebApi.Repositories.Interfaces
{
    public interface IConsumerRepository
    {
        Task<int> CreateConsumerAsync(ConsumerDetailRequest consumer);
        Task<ConsumerDetail?> GetConsumerByUserIdAsync(int userId);
    }
}
