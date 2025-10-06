using WebApi.Models.DB;
using WebApi.Models.Request;

namespace WebApi.Services.Interfaces
{
    public interface IConsumerService
    {
        Task<Complaint?> CreateComplaintAsync(int userId, CreateComplaintRequest complaint);
        Task<IEnumerable<Complaint>> GetUserComplaintAsync(int userId);
        Task<Complaint?> GetComplaintByIdAsync(int userId, int complaintId);
    }
}
