using WebApi.Data.Interfaces;
using WebApi.Models.Response;
using WebApi.Repositories.Interfaces;

namespace WebApi.Repositories.Implementations
{
    public class RechargeRepository : IRechargeRepository
    {
        private readonly IRechargeData _rechargeData;

        public RechargeRepository(IRechargeData rechargeData)
        {
            _rechargeData = rechargeData;
        }

        public async Task<IEnumerable<RechargeRespone>> GetAllRechargesByMeterIdAsync(int meterId)
        {
            return await GetAllRechargesByMeterIdAsync(meterId);
        }

        public async Task<RechargeRespone?> GetRechargeByIdAsync(int rechargeId)
        {
            return await GetRechargeByIdAsync(rechargeId);
        }
    }
}
