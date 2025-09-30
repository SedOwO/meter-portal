using WebApi.Models.DB;
using WebApi.Models.Request;

namespace WebApi.Data.Interfaces
{
    public interface IComplaintData
    {
        Task<int> CreateComplaintAsync(ComplaintRequest complaint);
        Task<Complaint?> GetComplaintByIdAsync(int complaintId);
        Task<IEnumerable<Complaint>> GetAllComplaintsAsync();
        Task<IEnumerable<Complaint>> GetAllComplaintsByConsumerIdAsync(int consumerId);
        Task<bool> UpdateComplaintAsync(int complaintId, ComplaintRequest complaint);
        Task<bool> DeleteComplaintAsync(int ComplaintId);
    }
}
