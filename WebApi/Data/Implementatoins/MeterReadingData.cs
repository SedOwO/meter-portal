using System.Diagnostics.Metrics;
using System.Threading;
using WebApi.Data.Interfaces;
using WebApi.Models.DB;
using WebApi.Models.Request;
using WebApi.Utilities.Interfaces;

namespace WebApi.Data.Implementatoins
{
    public class MeterReadingData : IMeterReadingData
    {
        private readonly IDbConnectionUtil _db;

        public MeterReadingData(IDbConnectionUtil db)
        {
            _db = db;
        }

        public async Task<IEnumerable<MeterReading>> GetAllReadingsAsync()
        {
            try
            {
                var meterReadings = new List<MeterReading>();

                using var reader = await _db.ExecuteReaderAsync("SELECT * FROM public.getallreadings();");
                while (await reader.ReadAsync())
                {
                    meterReadings.Add(new MeterReading
                    {
                        ReadingId = reader.GetInt32(reader.GetOrdinal("readingid")),
                        MeterId = reader.GetInt32(reader.GetOrdinal("meterid")),
                        ReadingValue = reader.GetDecimal(reader.GetOrdinal("reading_value")),
                        ReadingDate = reader.GetDateTime(reader.GetOrdinal("reading_date"))
                    });
                }

                return meterReadings;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<MeterReading>> GetAllReadingsByMeterIdAsync(int meterId)
        {
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    { "meterid", meterId }
                };

                var meterReadings = new List<MeterReading>();

                using var reader = await _db.ExecuteReaderAsync("SELECT * FROM public.getallreadingsbymeterid(@meterid);", parameters);
                while (await reader.ReadAsync())
                {
                    meterReadings.Add(new MeterReading
                    {
                        ReadingId = reader.GetInt32(reader.GetOrdinal("readingid")),
                        MeterId = reader.GetInt32(reader.GetOrdinal("meterid")),
                        ReadingValue = reader.GetDecimal(reader.GetOrdinal("reading_value")),
                        ReadingDate = reader.GetDateTime(reader.GetOrdinal("reading_date"))
                    });
                }

                return meterReadings;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<MeterReading?> GetReadingByIdAsync(int readingId)
        {
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    { "readingid", readingId }
                };

                using var reader = await _db.ExecuteReaderAsync("SELECT * FROM public.getreadingbyid(@readingid);", parameters);

                if (await reader.ReadAsync())
                {
                    return new MeterReading
                    {
                        ReadingId = reader.GetInt32(reader.GetOrdinal("readingid")),
                        MeterId = reader.GetInt32(reader.GetOrdinal("meterid")),
                        ReadingValue = reader.GetDecimal(reader.GetOrdinal("reading_value")),
                        ReadingDate = reader.GetDateTime(reader.GetOrdinal("reading_date"))
                    };
                }

                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> NewReadingAsync(MeterReadingRequest reading)
        {
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    { "meterid", reading.MeterId},
                    { "reading_value", reading.ReadingValue}
                };

                var result = await _db.ExecuteScalarAsync<int>
                    ("SELECT * FROM public.newmeterreading(@meterid, @reading_value);", parameters);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
