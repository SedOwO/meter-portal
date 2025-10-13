using WebApi.Data.Interfaces;
using WebApi.Models.DB;
using WebApi.Models.Misc;
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
            return await _complaintData.CreateComplaintAsync(complaint);
        }

        public async Task<bool> DeleteComplaintAsync(int complaintId)
        {
            return await _complaintData.DeleteComplaintAsync(complaintId);
        }

        public async Task<IEnumerable<Complaint>> GetAllComplaintsAsync()
        {
            return await _complaintData.GetAllComplaintsAsync();
        }

        public async Task<IEnumerable<Complaint>> GetAllComplaintsByConsumerIdAsync(int consumerId)
        {
            return await _complaintData.GetAllComplaintsByConsumerIdAsync(consumerId);
        }

        public async Task<PagedList<Complaint>> GetAllComplaintsPaginatedAsync(int page, int pageSize)
        {
            return await _complaintData.GetAllComplaintsPaginatedAsync(page, pageSize);
        }

        public async Task<Complaint?> GetComplaintByIdAsync(int complaintId)
        {
            return await _complaintData.GetComplaintByIdAsync(complaintId);
        }

        public async Task<bool> UpdateComplaintAsync(int complaintId, ComplaintRequest complaint)
        {
            return await _complaintData.UpdateComplaintAsync(complaintId, complaint);
        }
    }
}
