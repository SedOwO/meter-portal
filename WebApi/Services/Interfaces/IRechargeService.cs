using WebApi.Models.Response;

namespace WebApi.Services.Interfaces
{
    public interface IRechargeService
    {
        Task<RechargeRespone> RechargeSmartMeterAsync(int userId, decimal amount);
    }
}
