using WebApi.Data.Interfaces;
using WebApi.Models.DB;
using WebApi.Models.Request;
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

        public Task<int> CreateRechargeAsync(RechargeRequest recharge)
        {
            throw new NotImplementedException();
        }

        public Task<Recharge?> GetRechargeByIdAsync(int rechargeId)
        {
            throw new NotImplementedException();
        }

        public Task<Recharge?> GetRechargeByMeterIdAsync(int meterId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateRechargeAsync(int rechargeId, RechargeRequest rechrage)
        {
            throw new NotImplementedException();
        }
    }
}
