using WebApi.Models.DB;
using WebApi.Models.Request;
using WebApi.Models.Response;

namespace WebApi.Data.Interfaces
{
    public interface IRechargeData
    {
        Task<RechargeRespone?> GetRechargeByIdAsync(int rechargeId);
        Task<IEnumerable<RechargeRespone>> GetAllRechargesByMeterIdAsync(int meterId);
    }
}
