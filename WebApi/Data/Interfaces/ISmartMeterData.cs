using WebApi.Models.DB;
using WebApi.Models.Request;

namespace WebApi.Data.Interfaces
{
    public interface ISmartMeterData
    {
        Task<int> CreateMeterAsync(SmartMeterRequest meter);
        Task<SmartMeter?> GetMeterByIdAsync(int meterId);
        Task<IEnumerable<SmartMeter>> GetAllMetersAsync();
        Task<bool> UpdateMeterAsync(int meterId, SmartMeterRequest meter);
        Task<bool> DeleteMeterAsync(int meterId);

        // recharge realated functions
        Task<bool> AddRechargeAsync(int meterId, decimal amount);
        Task<bool> DeductRechargeAsync(int meterId, decimal amount);
    }
}
