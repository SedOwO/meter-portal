using System.Security.Principal;
using WebApi.Models.DB;

namespace WebApi.Services.Interfaces
{
    public interface IProfileService
    {
        Task<ConsumerDetail?> GetProfileByIdAsync(int userId);
    }
}
