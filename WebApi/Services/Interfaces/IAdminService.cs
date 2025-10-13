using WebApi.Models.DB;
using WebApi.Models.Misc;

namespace WebApi.Services.Interfaces
{
    public interface IAdminService
    {
        Task<IEnumerable<Complaint>> GetAllComplaints();
        Task<PagedList<Complaint>> GetAllComplaintsPaginated(int page, int pageSize);
    }
}
