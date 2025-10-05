using WebApi.Models.DB;
using WebApi.Models.Request;

namespace WebApi.Services.Interfaces
{
    public interface ISmartMeterService
    {
        Task<SmartMeter?> CreateSmartMeterAsync(int userId, SmartMeterRequest meter);
        Task<SmartMeter?> GetSmartMeterByIdAsync(int userId, int meterId);
        Task<IEnumerable<SmartMeter>> GetAllSmartMeterByConsumerIdAsync(int userId, int consumerId);
        Task<IEnumerable<SmartMeter>> GetAllSmartMetersAsync(int userId);
    }
}
