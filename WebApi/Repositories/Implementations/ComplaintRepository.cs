using WebApi.Data.Interfaces;
using WebApi.Models.DB;
using WebApi.Models.Request;
using WebApi.Repositories.Interfaces;

namespace WebApi.Repositories.Implementations
{
    public class ComplaintRepository : IComplaintRepository
    {
        private readonly IComplaintData _complaintData;

        public ComplaintRepository(IComplaintData complaintData)
        {
            _complaintData = complaintData;
        }

        public async Task<int> CreateComplaintAsync(ComplaintRequest complaint)
        {
            return await CreateComplaintAsync(complaint);
        }

        public async Task<bool> DeleteComplaintAsync(int complaintId)
        {
            return await DeleteComplaintAsync(complaintId);
        }

        public async Task<IEnumerable<Complaint>> GetAllComplaintsAsync()
        {
            return await GetAllComplaintsAsync();
        }

        public async Task<IEnumerable<Complaint>> GetAllComplaintsByConsumerIdAsync(int consumerId)
        {
            return await GetAllComplaintsByConsumerIdAsync(consumerId);
        }

        public async Task<Complaint?> GetComplaintByIdAsync(int complaintId)
        {
            return await GetComplaintByIdAsync(complaintId);
        }

        public async Task<bool> UpdateComplaintAsync(int complaintId, ComplaintRequest complaint)
        {
            return await UpdateComplaintAsync(complaintId, complaint);
        }
    }
}
