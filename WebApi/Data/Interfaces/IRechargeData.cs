using WebApi.Models.DB;
using WebApi.Models.Request;

namespace WebApi.Data.Interfaces
{
    public interface IRechargeData
    {
        Task<int> CreateRechargeAsync(RechargeRequest recharge);
        Task<Recharge?> GetRechargeByIdAsync(int rechargeId);
        Task<Recharge?> GetRechargeByMeterIdAsync(int meterId);
        Task<bool> UpdateRechargeAsync(int rechargeId, RechargeRequest rechrage);
    }
}
