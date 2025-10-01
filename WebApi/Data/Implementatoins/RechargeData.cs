using System.Diagnostics.Metrics;
using WebApi.Data.Interfaces;
using WebApi.Models.DB;
using WebApi.Models.Request;
using WebApi.Models.Response;
using WebApi.Utilities.Interfaces;

namespace WebApi.Data.Implementatoins
{
    public class RechargeData : IRechargeData
    {
        private readonly IDbConnectionUtil _db;

        public RechargeData(IDbConnectionUtil db)
        {
            _db = db;
        }

        public async Task<IEnumerable<RechargeRespone>> GetAllRechargesByMeterIdAsync(int meterId)
        {
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    { "meterid", meterId}
                };

                var recharges = new List<RechargeRespone>();

                using var reader = await _db.ExecuteReaderAsync("SELECT * FROM public.getallrechargesbymeterid(@meterid);", parameters);
                while (await reader.ReadAsync())
                {
                    recharges.Add(new RechargeRespone
                    {
                        RechargeId = reader.GetInt32(reader.GetOrdinal("rechargeid")),
                        MeterId = reader.GetInt32(reader.GetOrdinal("meterid")),
                        Amount = reader.GetDecimal(reader.GetOrdinal("amount")),
                        RechargeDate = reader.GetDateTime(reader.GetOrdinal("recharge_date"))
                    });
                }

                return recharges;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<RechargeRespone?> GetRechargeByIdAsync(int rechargeId)
        {

            try
            {
                var parameters = new Dictionary<string, object>
                {
                    { "rechargeid", rechargeId}
                };

                using var reader = await _db.ExecuteReaderAsync("SELECT * FROM public.getallrechargesbymeterid(@rechargeid);", parameters);

                if (await reader.ReadAsync())
                {
                    return new RechargeRespone
                    {
                        RechargeId = reader.GetInt32(reader.GetOrdinal("rechargeid")),
                        MeterId = reader.GetInt32(reader.GetOrdinal("meterid")),
                        Amount = reader.GetDecimal(reader.GetOrdinal("amount")),
                        RechargeDate = reader.GetDateTime(reader.GetOrdinal("recharge_date"))
                    };
                }

                return null;
            }
            catch (Exception)
            {
                throw;
            }    
        }
    }
}
