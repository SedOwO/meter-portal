namespace WebApi.Utilities.Interfaces
{
    public interface IPasswordUtil
    {
        string HashPassword(string plainPassword);
        bool VerifyPassword(string plainPassword, string hashedPassword);
    }
}
