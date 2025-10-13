using WebApi.Models.DB;
using WebApi.Models.Misc;
using WebApi.Models.Request;

namespace WebApi.Repositories.Interfaces
{
    public interface IComplaintRepository
    {
        Task<int> CreateComplaintAsync(ComplaintRequest complaint);
        Task<Complaint?> GetComplaintByIdAsync(int complaintId);
        Task<IEnumerable<Complaint>> GetAllComplaintsAsync();
        Task<IEnumerable<Complaint>> GetAllComplaintsByConsumerIdAsync(int consumerId);
        Task<bool> UpdateComplaintAsync(int complaintId, ComplaintRequest complaint);
        Task<bool> DeleteComplaintAsync(int complaintId);
        Task<PagedList<Complaint>> GetAllComplaintsPaginatedAsync(int page, int pageSize);
    }
}
