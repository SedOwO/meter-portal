using WebApi.Data.Interfaces;
using WebApi.Models.DB;
using WebApi.Models.Request;
using WebApi.Utilities.Interfaces;

namespace WebApi.Data.Implementatoins
{
    public class ComplaintData : IComplaintData
    {
        private readonly IDbConnectionUtil _db;

        public ComplaintData(IDbConnectionUtil db)
        {
            _db = db;
        }

        public Task<int> CreateComplaintAsync(ComplaintRequest complaint)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteComplaintAsync(int ComplaintId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Complaint>> GetAllComplaintsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Complaint>> GetAllComplaintsByConsumerIdAsync(int consumerId)
        {
            throw new NotImplementedException();
        }

        public Task<Complaint?> GetComplaintByIdAsync(int complaintId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateComplaintAsync(int complaintId, ComplaintRequest complaint)
        {
            throw new NotImplementedException();
        }
    }
}
