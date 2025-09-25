using Npgsql;

namespace WebApi.Utilities.Interfaces
{
    public interface IDbConnectionUtil
    {
        Task<NpgsqlDataReader> ExecuteReaderAsync(string query, Dictionary<string, object>? parameters = null);
        Task<T?> ExecuteScalarAsync<T>(string query, Dictionary<string, object>? parameters = null);
    }
}
