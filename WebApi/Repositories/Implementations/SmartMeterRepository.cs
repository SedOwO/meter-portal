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
            return await AddRechargeAsync(meterId, amount);
        }

        public async Task<int> CreateMeterAsync(SmartMeterRequest meter)
        {
            return await CreateMeterAsync(meter);
        }

        public async Task<bool> DeductRechargeAsync(int meterId, decimal amount)
        {
            return await DeductRechargeAsync(meterId, amount);
        }

        public async Task<bool> DeleteMeterAsync(int meterId)
        {
            return await DeleteMeterAsync(meterId);
        }

        public async Task<IEnumerable<SmartMeter>> GetAllMetersAsync()
        {
            return await GetAllMetersAsync();
        }

        public async Task<SmartMeter?> GetMeterByIdAsync(int meterId)
        {
            return await GetMeterByIdAsync(meterId);
        }

        public async Task<bool> UpdateMeterAsync(int meterId, SmartMeterRequest meter)
        {
            return await UpdateMeterAsync(meterId, meter);
        }
    }
}
