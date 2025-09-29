using WebApi.Models.DB;

namespace WebApi.Utilities.Interfaces
{
    public interface IJwtUtil
    {
        string GenerateToken(User user);
        int? ValidateToken(string token);
    }
}
