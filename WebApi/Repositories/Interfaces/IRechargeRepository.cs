using WebApi.Models.Response;

namespace WebApi.Repositories.Interfaces
{
    public interface IRechargeRepository
    {
        Task<RechargeRespone?> GetRechargeByIdAsync(int rechargeId);
        Task<IEnumerable<RechargeRespone>> GetAllRechargesByMeterIdAsync(int meterId);
    }
}
