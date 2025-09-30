using System.Data;
using WebApi.Data.Interfaces;
using WebApi.Models.DB;
using WebApi.Models.Request;
using WebApi.Utilities.Interfaces;

namespace WebApi.Data.Implementatoins
{
    public class SmartMeterData : ISmartMeterData
    {
        private readonly IDbConnectionUtil _db;

        public SmartMeterData(IDbConnectionUtil db)
        {
            _db = db;
        }
        public async Task<int> CreateMeterAsync(SmartMeterRequest meter)
        {
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    { "consumerid", meter.ConsumerId},
                    { "meter_number", meter.MeterNumber},
                    { "location", meter.Location},
                    { "status", meter.Status }
                };

                var result = await _db.ExecuteScalarAsync<int>
                    ("SELECT * FROM public.createsmartmeter(@consumerid, @meter_number, @location, @status);", parameters);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteMeterAsync(int meterId)
        {
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    { "meterid", meterId }
                };
                var result = await _db.ExecuteScalarAsync<bool>("SELECT * FROM public.deletesmartmeter(@meterid);", parameters);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<SmartMeter>> GetAllMetersAsync()
        {
            try
            {
                var smartMeters = new List<SmartMeter>();

                using var reader = await _db.ExecuteReaderAsync("SELECT * FROM public.getallsmartmeters();");
                while (await reader.ReadAsync())
                {
                    smartMeters.Add(new SmartMeter
                    {
                        MeterId = reader.GetInt32(reader.GetOrdinal("meterid")),
                        ConsumerId = reader.GetInt32(reader.GetOrdinal("consumerid")),
                        MeterNumber = reader.GetString(reader.GetOrdinal("meter_number")),
                        Location = reader.GetString(reader.GetOrdinal("location")),
                        Status = reader.GetString(reader.GetOrdinal("status"))
                    });
                }

                return smartMeters;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<SmartMeter?> GetMeterByIdAsync(int meterId)
        {
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    { "meterid", meterId }
                };

                using var reader = await _db.ExecuteReaderAsync("SELECT * FROM public.getmeterbyid(meterid);", parameters);

                if (await reader.ReadAsync())
                {
                    return new SmartMeter
                    {
                        MeterId = reader.GetInt32(reader.GetOrdinal("meterid")),
                        ConsumerId = reader.GetInt32(reader.GetOrdinal("consumerid")),
                        MeterNumber = reader.GetString(reader.GetOrdinal("meter_number")),
                        Location = reader.GetString(reader.GetOrdinal("location")),
                        Status = reader.GetString(reader.GetOrdinal("status"))
                    };
                }

                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateMeterAsync(int meterId, SmartMeterRequest meter)
        {
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    { "meterid", meterId},
                    { "consumerid", meter.ConsumerId},
                    { "meter_number", meter.MeterNumber },
                    { "location", meter.Location},
                    { "status", meter.Status},
                };

                var result = await _db.ExecuteScalarAsync<bool>(
                    "SELECT * FROM public.updatesmartmeter(@meterid, @consumerid, @meter_number, @location, @status);", parameters);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
