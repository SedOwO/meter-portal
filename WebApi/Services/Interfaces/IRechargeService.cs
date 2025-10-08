using WebApi.Models.Request;
using WebApi.Models.Response;

namespace WebApi.Services.Interfaces
{
    public interface IRechargeService
    {
        Task<RechargeRespone> RechargeSmartMeterAsync(int userId, RechargeRequest recharge);
        Task<IEnumerable<RechargeRespone>> GetUserRechargeHistoryAsync(int userId);
    }
}
