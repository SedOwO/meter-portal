using WebApi.Models.DB;
using WebApi.Models.Request;

namespace WebApi.Repositories.Interfaces
{
    public interface IMeterReadingRepository
    {
        Task<int> NewReadingAsync(MeterReadingRequest reading);
        Task<MeterReading?> GetReadingByIdAsync(int readingId);
        Task<IEnumerable<MeterReading>> GetAllReadingsAsync();
        Task<IEnumerable<MeterReading>> GetAllReadingsByMeterIdAsync(int meterId);
    }
}
