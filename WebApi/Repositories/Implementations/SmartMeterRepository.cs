using System.Runtime.InteropServices;
using WebApi.Data.Interfaces;
using WebApi.Models.DB;
using WebApi.Models.Request;
using WebApi.Repositories.Interfaces;

namespace WebApi.Repositories.Implementations
{
    public class SmartMeterRepository : ISmartMeterRepository
    {
        private readonly ISmartMeterData _smartMeterData;

        public SmartMeterRepository(ISmartMeterData smartMeterData)
        {
            _smartMeterData = smartMeterData;
        }

        public async Task<bool> AddRechargeAsync(int meterId, decimal amount)
        {
            return await _smartMeterData.AddRechargeAsync(meterId, amount);
        }

        public async Task<int> CreateMeterAsync(SmartMeterRequest meter)
        {
            return await _smartMeterData.CreateMeterAsync(meter);
        }

        public async Task<bool> DeductRechargeAsync(int meterId, decimal amount)
        {
            return await _smartMeterData.DeductRechargeAsync(meterId, amount);
        }

        public async Task<bool> DeleteMeterAsync(int meterId)
        {
            return await _smartMeterData.DeleteMeterAsync(meterId);
        }

        public async Task<IEnumerable<SmartMeter>> GetAllMetersAsync()
        {
            return await _smartMeterData.GetAllMetersAsync();
        }

        public async Task<IEnumerable<SmartMeter>> GetAllMetersByConsumerId(int consumerId)
        {
            return await _smartMeterData.GetAllMetersByConsumerId(consumerId);
        }

        public async Task<SmartMeter?> GetMeterByIdAsync(int meterId)
        {
            return await _smartMeterData.GetMeterByIdAsync(meterId);
        }

        public async Task<bool> UpdateMeterAsync(int meterId, SmartMeterRequest meter)
        {
            return await _smartMeterData.UpdateMeterAsync(meterId, meter);
        }
    }
}
