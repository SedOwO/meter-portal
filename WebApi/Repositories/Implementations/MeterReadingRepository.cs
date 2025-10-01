using System.Runtime.InteropServices;
using WebApi.Data.Interfaces;
using WebApi.Models.DB;
using WebApi.Models.Request;
using WebApi.Repositories.Interfaces;

namespace WebApi.Repositories.Implementations
{
    public class MeterReadingRepository : IMeterReadingRepository
    {
        private readonly IMeterReadingData _meterReadingData;

        public MeterReadingRepository(IMeterReadingData meterReadingData)
        {
            _meterReadingData = meterReadingData;
        }

        public async Task<IEnumerable<MeterReading>> GetAllReadingsAsync()
        {
            return await GetAllReadingsAsync();
        }

        public async Task<IEnumerable<MeterReading>> GetAllReadingsByMeterIdAsync(int meterId)
        {
            return await GetAllReadingsByMeterIdAsync(meterId);
        }

        public async Task<MeterReading?> GetReadingByIdAsync(int readingId)
        {
            return await GetReadingByIdAsync(readingId);
        }

        public async Task<int> NewReadingAsync(MeterReadingRequest reading)
        {
            return await NewReadingAsync(reading);
        }
    }
}
