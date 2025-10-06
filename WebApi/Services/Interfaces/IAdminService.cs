using WebApi.Models.DB;

namespace WebApi.Services.Interfaces
{
    public interface IAdminService
    {
        Task<IEnumerable<Complaint>> GetAllComplaints();
    }
}
