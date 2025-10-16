using WebApi.Models.DB;

namespace WebApi.Services.Interfaces
{
    public interface IComplaintService
    {
        Task<Complaint?> GetComplaintsByIdAsync(int userId, int complaintId);
    }
}
