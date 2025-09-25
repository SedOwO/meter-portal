using WebApi.Models.DB;
using WebApi.Models.Request;

namespace WebApi.Data.Interfaces
{
    public interface IConsumerData
    {
        Task<int> CreateConsumerAsync(ConsumerDetailRequest consumer);
        Task<ConsumerDetail?> GetConsumerByUserIdAsync(int userId);

    }
}
