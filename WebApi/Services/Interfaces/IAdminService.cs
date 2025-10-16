using WebApi.Models.DB;
using WebApi.Models.Misc;
using WebApi.Models.Request;

namespace WebApi.Services.Interfaces
{
    public interface IAdminService
    {
        Task<IEnumerable<Complaint>> GetAllComplaints();
        Task<PagedList<Complaint>> GetAllComplaintsPaginated(int page, int pageSize);
        Task<bool> UpdateComplaintAsync(int complaintId, ComplaintRequest complaint);
    }
}
